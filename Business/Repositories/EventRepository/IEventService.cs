using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.EventRepository
{
    public interface IEventService
    {
        Task<IResult> Add(EventDto eventDto);
        Task<IResult> Update(Event eventt);
        Task<IResult> Delete(int eventId);
        Task<IDataResult<List<Event>>> GetAllEvent(string phone);
        Task<IDataResult<List<Event>>> GetPersonalEvent(string phone);

        Task<IDataResult<EventDetailDto>> GetEventDetail(int eventId);

    }
}
