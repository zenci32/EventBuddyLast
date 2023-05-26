using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Repositories.CategoryRepository
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NewsContextDb>, ICategoryDal
    {
    }
}
