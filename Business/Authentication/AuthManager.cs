using Business.Abstract;
using Business.Repositories.EventRepository;
using Business.Repositories.UserRepository;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IEventService _eventService;
        

        public AuthManager(IUserService userService, ITokenHandler tokenHandler, IEventService eventService)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
            _eventService = eventService;
        }

        /// <summary>
        /// Hesabın actifliği false çekiliyor ve kişiye ait aktif eventler kaldırılıyor
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAccount(string phone)
        {
            var user = _userService.GetByPhone(phone).Result.Data;
            user.IsActive = false;
            await _userService.Update(user);

            // kişinin aktifte bulunan bütün eventlerini silme kısmı
            var events = _eventService.GetPersonalEvent(phone).Result.Data;

            foreach (var item in events)
            {
                await _eventService.Delete(item.EventId);
            }

            return new SuccessResult("Hesap başarılı bir şekilde kaldırılmıştır", 200);
        }

        public async Task<IDataResult<Token>> Login(LoginAuthDto loginDto)
        {
            var user =  _userService.GetByPhone(loginDto.Phone).Result.Data;
            if (user == null || user.IsActive == false)
                return new ErrorDataResult<Token>(null, "Kullanıcı telefon sistemde bulunamadı!",404);

            //if (!user.IsConfirm)
            //   return new ErrorDataResult<Token>(null,"Kullanıcı telefon numarasını onaylanmamış!",404);

            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            List<OperationClaim> operationClaims = await _userService.GetUserOperationClaims(user.Id);

            if (result)
            {
                Token token = new();
                token = _tokenHandler.CreateToken(user, operationClaims);
                return new SuccessDataResult<Token>(token,"Giriş Başarılı",200);
            }
            return new ErrorDataResult<Token>(null,"Kullanıcı telefon ya da şifre yanlış",404);
        }

        [ValidationAspect(typeof(AuthValidator))]
        public async Task<IResult> Register(RegisterAuthDto registerDto)
        {
            IResult result = BusinessRules.Run(
                CheckIfPhoneExists(registerDto.Phone)
                //CheckIfImageExtesionsAllow(registerDto.Image.FileName),
                //CheckIfImageSizeIsLessThanOneMb(registerDto.Image.Length)
                );

            if (result != null)
            {
                return result;
            }

            await _userService.Add(registerDto);
            return new SuccessResult("Kullanıcı kaydı başarıyla tamamlandı",200);
        }

        private IResult CheckIfPhoneExists(string phone)
        {
            var list = _userService.GetByPhone(phone).Result.Data;
            if (list != null)
            {
                return new ErrorResult("Bu telefon numarası daha önce kullanılmış", 404);
            }
            return new SuccessResult();
        }





        //private IResult CheckIfImageSizeIsLessThanOneMb(long imgSize)
        //{
        //    decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000001);
        //    if (imgMbSize > 1)
        //    {
        //        return new ErrorResult("Yüklediğiniz resmi boyutu en fazla 1mb olmalıdır",404);
        //    }
        //    return new SuccessResult();
        //}

        //private IResult CheckIfImageExtesionsAllow(string fileName)
        //{
        //    var ext = fileName.Substring(fileName.LastIndexOf('.'));
        //    var extension = ext.ToLower();
        //    List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
        //    if (!AllowFileExtensions.Contains(extension))
        //    {
        //        return new ErrorResult("Eklediğiniz resim .jpg, .jpeg, .gif, .png türlerinden biri olmalıdır!",404);
        //    }
        //    return new SuccessResult();
        //}
    }
}
