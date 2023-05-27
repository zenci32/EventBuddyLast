using Business.Repositories.CategoryRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.RateRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.RateRepository
{
    public class RateManager : IRateService
    {
        private readonly IRateDal _rateDal;

        public RateManager(IRateDal rateDal)
        {
            _rateDal = rateDal;

        }

        public async Task<IResult> AddByPhoneNumber(string phoneNumber, decimal rateTotal)
        {
            try
            {

                var newRate = new Rate
                {
                    Phone = phoneNumber,
                    RateTotal = rateTotal,
                };

                await _rateDal.Add(newRate);

                return new SuccessResult("Rate başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Rate ekleme sırasında bir hata oluştu.");
            }
        }

        public async Task<Rate> GetByPhoneNumber(string phoneNumber)
        {
            return await _rateDal.Get(p => p.Phone == phoneNumber);
        }

        public async Task<IDataResult<List<Rate>>> GetList()
        {
            var rates = await _rateDal.GetAll();
            return new SuccessDataResult<List<Rate>>(rates);
        }


    }
}
