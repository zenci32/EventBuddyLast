﻿using Business.Repositories.CategoryRepository.Constants;
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

        public async Task<IResult> Add(Rate rate)
        {
            await _rateDal.Add(rate);
            return new SuccessResult("Puan verildi", 200);
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


        public async Task<IResult> Update(Rate rate)
        {
            try
            {
                // Rate nesnesini güncelleyin
                await _rateDal.Update(rate);

                return new SuccessResult("Puan güncellendi.");
            }
            catch (Exception ex)
            {
                // Hata durumunda gerekli işlemler yapılabilir
                return new ErrorResult("Puan Güncelleme sırasında bir hata oldu");
            }
        }

    }
}