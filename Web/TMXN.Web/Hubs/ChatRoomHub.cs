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
    [Authorize]
    public class ChatRoomHub : Hub
    {
        
        private readonly IChatRoomService messageService;

        public ChatRoomHub(IChatRoomService messageService)
        {
            
            this.messageService = messageService;
        }
       
        public async Task Send(string messageInput)
        {
            
            var message = new Message
            {
                UserName = this.Context.User.Identity.Name,
                Text = messageInput,
                
            };
           
            await this.messageService.AddMessage(message);

            await this.Clients.All.SendAsync(
               "NewMessage",
               message);
        }

     

    }
}
