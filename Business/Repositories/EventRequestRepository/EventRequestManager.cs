using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.EventRepository;
using DataAccess.Repositories.EventRequestRepository;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.EventRequestRepository
{
    internal class EventRequestManager : IEventRequestService
    {
        private readonly IEventDal _eventDal;
        private readonly IEventRequestDal _eventRequestDal;
        private readonly IUserDal _userDal;

        public EventRequestManager(IEventDal eventDal, IEventRequestDal eventRequestDal, IUserDal userDal)
        {
            _eventDal = eventDal;
            _eventRequestDal = eventRequestDal;
            _userDal = userDal;
        }

        /// <summary>
        /// İçinde bulunan eventten ayrılma
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="inviterPhone"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IResult> EventExit(int eventId, string inviterPhone)
        {
            var getEventRequest = _eventRequestDal.Get(x => x.EventId == eventId && x.InviterPhone == inviterPhone).Result;
            await _eventRequestDal.Delete(getEventRequest);
            var eventt = _eventDal.Get(x => x.EventId == eventId).Result;
            eventt.ActiveCount -= 1;
            await _eventDal.Update(eventt);
            return new SuccessResult("Eventten ayrıldınız edilmiştir", 200);
        }

        public async Task<IResult> EventRequest(int eventId, string inviterPhone)
        {
            var eventt = _eventDal.Get(x => x.EventId == eventId).Result;
            var existingRequest = await _eventRequestDal.Get(x => x.EventId == eventId && x.InviterPhone == inviterPhone);

            if (existingRequest != null)
            {
                // Kullanıcı daha önce istekte bulunmuş
                return new ErrorResult("Bu kullanıcı zaten bir istekte bulunmuştur.", 201);
            }

            if (eventt.ActiveCount < eventt.EventCount)
            {
                var eventRequest = new EventRequest { EventId = eventId, InviterPhone = inviterPhone, Status = "pending" };
                await _eventRequestDal.Add(eventRequest);
                return new SuccessResult("İstek atılmıştır", 200);
            }
            else
            {
                return new ErrorResult("Bu event için kişi sayısı dolmuştur", 201);
            }
        }


        /// <summary>
        /// Event katılma isteği iptal
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="inviterPhone"></param>
        /// <returns></returns>
        public async Task<IResult> EventRequestCancel(int eventId, string inviterPhone)
        {
            var getEventRequest = _eventRequestDal.Get(x => x.EventId == eventId && x.InviterPhone == inviterPhone).Result;
            await _eventRequestDal.Delete(getEventRequest);
            return new SuccessResult("Event katılma talebi iptal edilmiştir", 200);
        }

        

        /// <summary>
        /// evente katılmayı kabul etme veya reddetme
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="eventId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IResult GetAcceptOrReject(string invaterPhone, int eventId, bool status)
        {
            var eventt = _eventDal.Get(x => x.EventId == eventId).Result;
            var eventRequest = _eventRequestDal.Get(x => x.InviterPhone == invaterPhone && x.EventId == eventId).Result;
            if (status)
            {
                if (eventt.ActiveCount < eventt.EventCount)
                {
                    eventt.ActiveCount += 1;
                    _eventDal.Update(eventt);
                    var eventCountCheck = _eventDal.Get(x => x.EventId == eventId).Result;
                    if (eventCountCheck.EventCount == eventCountCheck.ActiveCount)
                    {
                        eventCountCheck.EventStatus = "full";
                        _eventDal.Update(eventCountCheck);
                    }
                    eventRequest.Status = "active";
                    _eventRequestDal.Update(eventRequest);
                    return new SuccessResult("Kişi evente eklenmiştir", 200);
                }
                else
                {
                    return new SuccessResult("Kişi sayısı dolmuştur", 200);
                }
            }
            else
            {
                eventRequest.Status = "reject";
                _eventRequestDal.Update(eventRequest);
                return new SuccessResult("Kişi talebi red edilmiştir", 200);
            }
        }

        /// <summary>
        /// request tablosundan aldığımız kişinin istek attığı eventIdleri event tablosundan getirip yolluyoruz
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public IDataResult<List<GetRequestedEventDto>> GetPersonalEventRequest(string phone)
        {
            var allRequestEvent = new List<GetRequestedEventDto>();
            var eventRequest = _eventRequestDal.GetAll(x => x.InviterPhone == phone).Result;
            foreach (var item in eventRequest)
            {
                var singleEvent = _eventDal.Get(x => x.EventId == item.EventId).Result;
                var getEventRequestDto = new GetRequestedEventDto
                {
                    ActiveCount = singleEvent.ActiveCount,
                    EventCount = singleEvent.EventCount,
                    EventStatus = singleEvent.EventStatus,
                    EventCategory = singleEvent.EventCategory,
                    EventDate = singleEvent.EventDate,
                    EventCreateDate = singleEvent.EventCreateDate,
                    EventDescription = singleEvent.EventDescription,
                    EventId = item.EventId,
                    EventLastUpdateDate = singleEvent.EventLastUpdateDate,
                    EventLatitude = singleEvent.EventLatitude,
                    EventLongitude = singleEvent.EventLongitude,
                    EventName = singleEvent.EventName,
                    IsDeleted = singleEvent.IsDeleted,
                    Phone = singleEvent.Phone,
                    RequestStatus = item.Status
                };
                allRequestEvent.Add(getEventRequestDto);
            }
            return new SuccessDataResult<List<GetRequestedEventDto>>(allRequestEvent, "İstek Atılan eventler başarılı bir şekilde listelenmiştir", 200);
        }

        /// <summary>
        /// Kişinin kabul veya red etmesi için gönderilen istekler
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public IDataResult<List<RequestEventNotifyDto>> GetRequestEventNotify(string phone)
        {
            var requestEventList = new List<RequestEventNotifyDto>();
            var getEvents = _eventDal.GetAll(x => x.Phone == phone && x.EventStatus == "active" && x.ActiveCount < x.EventCount).Result;
            foreach (var item in getEvents)
            {
                var requestDetail = _eventRequestDal.GetAll(x => x.EventId == item.EventId && x.Status == "pending").Result;

                foreach (var item2 in requestDetail)
                {
                    var inveterUser = _userDal.GetAll(x => x.Phone == item2.InviterPhone).Result.FirstOrDefault();
                    var requestEventNotify = new RequestEventNotifyDto
                    {
                        EventId = item2.EventId,
                        InviterPhone = item2.InviterPhone,
                        InviterName = inveterUser.Name,
                        EventName = item.EventName,
                        EventCount = item.EventCount,
                        ActiveCount = item.ActiveCount
                    };
                    requestEventList.Add(requestEventNotify);
                }
            }
            return new SuccessDataResult<List<RequestEventNotifyDto>>(requestEventList, "Event bildirimleri listelenmiştir", 200);
        }

        public Task<IResult> Update(EventRequest eventRequest)
        {
            throw new NotImplementedException();
        }
    }
}
