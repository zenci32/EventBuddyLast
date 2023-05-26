using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator : AbstractValidator<RegisterAuthDto>
    {
        public AuthValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail adresi boş bırakılamaz");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Telefon boş bırakılamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli bir mail adresi yazın");
            //RuleFor(p => p.Image.FileName).NotEmpty().WithMessage("Kullanıcı resmi boş bırakılamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş bırakılamaz");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifreniz en az 1 adet büyük harf içermelidir");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifreniz en az 1 adet küçük harf içermelidir");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifreniz en az 1 adet sayı içermelidir");
            RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifreniz en az 1 adet özel karakter içermelidir");
        }
    }
}
