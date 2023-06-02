﻿using Microsoft.AspNetCore.SignalR;
using WebAPI.Models;

namespace WebAPI.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string UserName, int RandomUserId, string Message)
        {
            MessageModel MessageModel = new MessageModel
            {
                CreateDate = DateTime.Now,
                MessageText = Message,
                UserId = RandomUserId,
                UserName = UserName
            };
            await Clients.All.SendAsync("ReceiveMessage", MessageModel);
        }

        public async Task JoinUSer(string userName, int userId)
        {
            MessageModel MessageModel = new MessageModel
            {
                CreateDate = DateTime.Now,
                MessageText = userName + " joined chat",
                UserId = 0,
                UserName = "system"
            };
            await Clients.All.SendAsync("ReceiveMessage", MessageModel);
        }
    }
}
