using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Models;
using TMXN.Web.ViewModels.Chat;

namespace TMXN.Services.Data.Contracts
{
    public interface IChatRoomService
    {
        public Task AddMessage(Message message);

        public Task<ChatViewModel> GetAllMessages();

    }
}
