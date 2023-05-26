using Business.Repositories.EventRequestRepository;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRequestController : ControllerBase
    {
        private readonly IEventRequestService _eventRequestService;

        public EventRequestController(IEventRequestService eventRequestService)
        {
            _eventRequestService = eventRequestService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EventRequest(int eventId, string inviterPhone)
        {
            var result = await _eventRequestService.EventRequest(eventId, inviterPhone);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EventRequestCancel(int eventId, string inviterPhone)
        {
            var result = await _eventRequestService.EventRequestCancel(eventId, inviterPhone);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult GetPersonalEventRequest(string phone)
        {
            var result =  _eventRequestService.GetPersonalEventRequest(phone);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult GetRequestEventNotify(string phone)
        {
            var result = _eventRequestService.GetRequestEventNotify(phone);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult GetAcceptOrReject(string invaterPhone, int eventId, bool status)
        {
            var result = _eventRequestService.GetAcceptOrReject(invaterPhone, eventId, status);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
