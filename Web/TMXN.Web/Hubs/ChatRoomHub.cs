using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Data.Contracts;

namespace TMXN.Web.Hubs
{
   
    public class ChatRoomHub : Hub
    {
        
        private readonly IChatRoomService messageService;

        public ChatRoomHub(IChatRoomService messageService)
        {
            
            this.messageService = messageService;
        }
        [Authorize]
        public async Task Send(string message)
        {
            
            var messageToSend = new Message
            {
                UserName = this.Context.User.Identity.Name,
                Text = message,
                
            };
            await this.Clients.All.SendAsync(
                "NewMessage",
                new Message 
                {
                    UserName = this.Context.User.Identity.Name,
                    Text = message,
                   
                });
            await this.messageService.AddMessage(messageToSend);
        }

        //public async override Task OnConnectedAsync()
        //{
        //    Message message = new Message()
        //    {
        //        UserName = "System",
        //        Text = $"{this.Context.User.Identity.Name} has entered the chat.",
        //        When = DateTime.UtcNow,
        //    };

        //    await this.Clients.All.SendAsync(
        //        "NewSystemMessage",
        //        message);
        //}

        //public async override Task OnDisconnectedAsync(Exception exception)
        //{
        //    Message message = new Message()
        //    {
        //        UserName = "System",
        //        Text = $"{this.Context.User.Identity.Name} has left the chat.",
        //        When = DateTime.UtcNow,
        //    };

        //    await this.Clients.All.SendAsync(
        //        "NewSystemMessage",
        //        message);
        //}

    }
}
