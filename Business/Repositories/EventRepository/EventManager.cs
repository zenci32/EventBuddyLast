using Business.Abstract;
using Business.Repositories.EmailRepository;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.EventRepository;
using DataAccess.Repositories.EventRequestRepository;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.EventRepository
{
    internal class EventManager : IEventService
    {
        private readonly IEventDal _eventDal;
        private readonly IEventRequestDal _eventRequestDal;
        private readonly IUserDal _userDal;


        public EventManager(IEventDal eventDal, IEventRequestDal eventRequestDal, IUserDal userDal)
        {
            _eventDal = eventDal;
            _eventRequestDal = eventRequestDal;
            _userDal = userDal;
        }
        public async Task<IResult> Add(EventDto eventDto)
        {
            var eventt = new Event
            {
                ActiveCount = 0,
                EventCategory = eventDto.EventCategory,
                EventDate = eventDto.EventDate,
                EventCount = eventDto.EventCount,
                EventCreateDate = DateTime.Now,
                EventDescription = eventDto.EventDescription,
                EventLastUpdateDate = DateTime.Now,
                EventName = eventDto.EventName,
                EventLatitude = eventDto.EventLatitude,
                EventLongitude = eventDto.EventLongitude,
                EventFinishDate = eventDto.EventFinishDate,
                IsDeleted = false,
                Phone = eventDto.Phone,
                EventStatus = "active"
            };
            await _eventDal.Add(eventt);
            return new SuccessResult("Event başarıyla oluşturulmuştur", 200);
        }

        public async Task<IResult> Delete(int eventId)
        {
            var getEvent = _eventDal.Get(x => x.EventId == eventId).Result;
            getEvent.IsDeleted = true;
            getEvent.EventStatus = "deleted";
            await _eventDal.Update(getEvent);
            var eventRequest = _eventRequestDal.GetAll(x => x.EventId == eventId).Result;
            if (eventRequest.Any())
            {
                foreach (var item in eventRequest)
                {
                    item.Status = "deleted";
                    await _eventRequestDal.Update(item);
                }
            }
            return new SuccessResult("Event başarıyla silinmiştir", 200);
        }

        public async Task<IDataResult<List<Event>>> GetAllEvent(string phone)
        {
            var eventt = new List<Event>();
            var getAllEvent = await _eventDal.GetAll(x => x.IsDeleted == false);
            var getRequestedEvent =  _eventRequestDal.GetAll(x => x.InviterPhone == phone).Result;

            if (getRequestedEvent.Any())
            {
                foreach (var item in getRequestedEvent)
                {
                    getAllEvent.RemoveAll(x => x.EventId == item.EventId);
                }
            }
            return new SuccessDataResult<List<Event>>(getAllEvent, "Eventler listelenmiştir", 200);
        }

        public async Task<IDataResult<EventDetailDto>> GetEventDetail(int eventId)
        {
            var eventDetail = new EventDetailDto();
            var eventt =  _eventDal.Get(x => x.EventId == eventId).Result;
            var requestEventList = new List<RequestEventNotifyDto>();

            eventDetail.EventDetail = eventt;


            var requestDetail =  _eventRequestDal.GetAll(x => x.EventId == eventt.EventId && x.Status == "active").Result;

            foreach (var item in requestDetail)
            {
                var inveterUser = _userDal.GetAll(x => x.Phone == item.InviterPhone).Result.FirstOrDefault();
                var requestEventNotify = new RequestEventNotifyDto
                {
                    EventId = item.EventId,
                    InviterPhone = item.InviterPhone,
                    InviterName = inveterUser.Name,
                    EventName = eventt.EventName,
                    EventCount = eventt.EventCount,
                    ActiveCount = eventt.ActiveCount
                };
                requestEventList.Add(requestEventNotify);
            }
            eventDetail.EventUserList = requestEventList;
            return new SuccessDataResult<EventDetailDto>(eventDetail, "Eventler başarılı bir şekilde listelenmiştir", 200);


            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<Event>>> GetPersonalEvent(string phone)
        {
            var getPersonAllEvent = await _eventDal.GetAll(x => x.Phone == phone && x.IsDeleted == false);
            return new SuccessDataResult<List<Event>>(getPersonAllEvent, "Eventler başarılı bir şekilde listelenmiştir", 200);
        }

        public async Task<IResult> Update(Event eventt)
        {
            await _eventDal.Update(eventt);
            return new SuccessResult("Event başarıyla güncellenmiştir", 200);
        }
    }
}
