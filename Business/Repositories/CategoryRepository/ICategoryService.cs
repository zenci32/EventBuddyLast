using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.CategoryRepository
{
    public interface ICategoryService
    {
        Task<IResult> Add(Category category);
        Task<IResult> Update(Category category);
        Task<IResult> Delete(Category category);
        Task<IDataResult<List<Category>>> GetList();
    }
}
