using Microsoft.AspNetCore.SignalR;
using WebAPI.Models;

namespace WebAPI.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string message,string phone)
        {

            MessageModel messageModel = new MessageModel
            {
                CreateDate = DateTime.Now,
                MessageText = message,
                UserName = "",
                EventId =5,
                PhoneNumber="0"
            };
            await Clients.All.SendAsync("ReceiveMessage", messageModel);
        }

        //public async Task SendMessage(string userName, int userId,string phone)
        //{
        //    MessageModel MessageModel = new MessageModel
        //    {
        //        CreateDate = DateTime.Now,
        //        MessageText = userName + " joined chat",
        //        PhoneNumber:phone,
        //        UserName = "system"
        //    };
        //    await Clients.All.SendAsync("ReceiveMessage", MessageModel);
        //}
    }
}
