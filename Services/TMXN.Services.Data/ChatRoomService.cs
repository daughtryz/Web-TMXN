using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Data.Contracts;
using TMXN.Web.ViewModels.Chat;

namespace TMXN.Services.Data
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public ChatRoomService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }
        public async Task AddMessage(Message message)
        {
            if (message.Text.Length < 1)
            {
                throw new Exception("The message is too short!");
            }
            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task<ChatViewModel> GetAllMessages()
        {
            var allMessages = await this.messagesRepository.All().OrderByDescending(x => x.CreatedOn).ToListAsync();

            ChatViewModel chatViewModel = new ChatViewModel
            {
                Messages = allMessages,
            };

            return chatViewModel;

        }
    }

}
