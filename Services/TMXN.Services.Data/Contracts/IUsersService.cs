using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Models;
using TMXN.Web.ViewModels.Users;

namespace TMXN.Services.Data
{
    public interface IUsersService
    {
        public Task LeaveAsync(string teamId, ApplicationUser user);

        public Task JoinAsync(string teamId, ApplicationUser user);

        public Task AddFriendToFriendlistAsync(string id,ApplicationUser myUser);

        public Task RemoveFriendFromFriendlistAsync(string id, ApplicationUser myUser);

        public Task<IEnumerable<TViewModel>> GetAll<TViewModel>();

        public Task<IEnumerable<TViewModel>> AllFriendsAsync<TViewModel>(string id);
       

    }
}
