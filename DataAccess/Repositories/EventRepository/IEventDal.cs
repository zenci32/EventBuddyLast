using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Repositories.EventRepository
{
    public interface IEventDal : IEntityRepository<Event>
    {
    }
}
