using Business.Repositories.CategoryRepository.Constants;
using Business.Repositories.CategoryRepository.Validation;
using Core.Aspects.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.CategoryRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.CategoryRepository
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public async Task<IResult> Add(Category category)
        {
            await _categoryDal.Add(category);
            return new SuccessResult(CategoryMessages.AddedCategory, 200);
        }

        public async Task<IResult> Delete(Category category)
        {
            await _categoryDal.Delete(category);
            return new SuccessResult(CategoryMessages.DeletedCategory,200);
        }

        public async Task<IDataResult<List<Category>>> GetList()
        {
            return new SuccessDataResult<List<Category>>(await _categoryDal.GetAll(),CategoryMessages.CategoryList, 200);
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public async Task<IResult> Update(Category category)
        {
            await _categoryDal.Update(category);
            return new SuccessResult(CategoryMessages.UpdatedCategory, 200);
            
        }
    }
}
