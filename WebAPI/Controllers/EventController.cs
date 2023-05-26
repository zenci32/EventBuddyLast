using Business.Abstract;
using Business.Repositories.EventRepository;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddEvent(EventDto eventDto)
        {
            var result = await _eventService.Add(eventDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GettAllEvent(string phone)
        {
            var result = await _eventService.GetAllEvent(phone);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateEvent(Event eventt)
        {
            var result = await _eventService.Update(eventt);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var result = await _eventService.Delete(eventId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetPersonalEvent(string phone)
        {
            var result = await _eventService.GetPersonalEvent(phone);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetEventDetail(int eventId)
        {
            var result = await _eventService.GetEventDetail(eventId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
