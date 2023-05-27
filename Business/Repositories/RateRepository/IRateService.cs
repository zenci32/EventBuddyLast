using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.RateRepository
{
    public interface IRateService
    {
        Task<IResult> AddByPhoneNumer(string phoneNumber);
        
        Task<IDataResult<List<Rate>>> GetList();
        Task<Rate> GetByPhoneNumber(string phoneNumber);
       

    }
}
