using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Data.Contracts;

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
    }

}
