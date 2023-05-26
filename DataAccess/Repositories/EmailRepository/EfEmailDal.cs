using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Repositories.EmailRepository
{
    public class EfEmailDal : EfEntityRepositoryBase<Email, NewsContextDb>, IEmailDal
    {
    }
}
