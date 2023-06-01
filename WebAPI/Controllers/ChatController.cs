using Business.Repositories.EventRepository;
using Business.Repositories.EventRequestRepository;
using Business.Repositories.UserRepository;
using DataAccess.Repositories.EventRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Hubs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hub;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly IEventRequestService _eventRequestService;
        public ChatController(IHubContext<ChatHub> hub,IUserService userService,IEventService eventService,IEventRequestService eventRequestService)
        {
            _hub = hub;
            _userService = userService;
            _eventService = eventService;
            _eventRequestService = eventRequestService;

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SendMessage(string MessageText, int UserId, string UserName, int EventId, int EventRequestId, string PhoneNumber)
        {
            
            MessageModel messageModel = new MessageModel
            {
                CreateDate = DateTime.Now,
                MessageText = MessageText,
                UserId = UserId,
                UserName = UserName,
                EventId = EventId,
                EventRequestId = EventRequestId,
                PhoneNumber = PhoneNumber
            };

            List<int> recipientUserIds = new List<int>();

            var activeEventUsers = await _eventService.EventUsers
                .Where(eu => eu.EventId == EventId && eu.EventStatus=="active")
                .ToListAsync();
            recipientUserIds.AddRange(activeEventUsers.Select(eu => eu.UserId));

            
            var activeEventRequestUsers = await _eventRequestService.EventRequestUsers
                .Where(eru => eru.EventId == EventRequestId && eru.Status == "active")
                .ToListAsync();
            recipientUserIds.AddRange(activeEventRequestUsers.Select(eru => eru.UserId));

            
            foreach (var recipientUserId in recipientUserIds)
            {
              
                await _hub.Clients.User(recipientUserId.ToString()).SendAsync("ReceiveMessage", messageModel);
            }

            return Ok();
        }

    }
}
