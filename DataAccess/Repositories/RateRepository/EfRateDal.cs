using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Repositories.RateRepository
{
    public class EfRateDal : EfEntityRepositoryBase<Rate, NewsContextDb>, IRateDal
    {
    }
}
