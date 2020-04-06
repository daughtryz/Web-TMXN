using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Web.ViewModels.Users;

namespace TMXN.Services.Data
{
    public interface IUsersService
    {
        public Task LeaveAsync(string teamId,string userId);

        public Task JoinAsync(string teamId, string userId);

        public Task AddAnotherUserToFriendlistAsync(string id,string myId);

        public Task<IEnumerable<TViewModel>> GetAll<TViewModel>();

        public Task<IEnumerable<TViewModel>> AllFriendsAsync<TViewModel>(string id);
       

    }
}
