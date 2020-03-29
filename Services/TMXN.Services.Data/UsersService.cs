using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Mapping;
using TMXN.Web.ViewModels.Users;

namespace TMXN.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<UserFriend> userFriendRepo;
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<UserFriendlist> friendlistRepository;

        public UsersService(IRepository<UserFriend> userFriendRepo,IDeletableEntityRepository<Team> teamRepository,IDeletableEntityRepository<ApplicationUser> userRepository,IDeletableEntityRepository<UserFriendlist> friendlistRepository)
        {
            this.userFriendRepo = userFriendRepo;
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
            this.friendlistRepository = friendlistRepository;
        }

        public async Task AddAnotherUserToFriendlistAsync(string id,string myId)
        {

            
            var currentUser = await this.userRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            var myUser = await this.userRepository.All().Where(x => x.Id == myId).FirstOrDefaultAsync();
            if (currentUser == null)
            {
                throw new Exception("No such user!");
            }
           

            

            var friendList = new UserFriendlist();

            friendList.ApplicationUsers.Add(currentUser);
            


            var mappingUserFriend = new UserFriend
            {
                ApplicationUserId = myUser.Id,
                UserFriendlistId = friendList.Id,
            };

            await this.friendlistRepository.AddAsync(friendList);

            await this.friendlistRepository.SaveChangesAsync();


            await this.userFriendRepo.AddAsync(mappingUserFriend);

            await this.userFriendRepo.SaveChangesAsync();

            
        }

        public async Task<IEnumerable<TViewModel>> AllFriendsAsync<TViewModel>(string id)
        {
            var currentUser = await this.userRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            var currentFriendlist = await this.userFriendRepo
                .All()
                .Where(z => z.ApplicationUserId == currentUser.Id)
                .SelectMany(x => x.UserFriendlist.ApplicationUsers.Where(l => l.UserFriendlistId == x.UserFriendlistId))
                .To<TViewModel>()
                .ToListAsync();
            
                
            return currentFriendlist;

        }

        public async Task<IEnumerable<TViewModel>> GetAll<TViewModel>()
        {
            
            return await this.userRepository.All().To<TViewModel>().ToListAsync();
        }

        public async Task JoinAsync(string teamId, string userId)
        {
            var currentTeam = this.teamRepository.All().Where(x => x.Id == teamId)
                .FirstOrDefault();

            var currentUser = this.userRepository.All().Where(x => x.Id == userId).FirstOrDefault();

            currentTeam.ApplicationUsers.Add(currentUser);

            await this.teamRepository.SaveChangesAsync();

            
        }

        
        public async Task LeaveAsync(string teamId,string userId)
        {
            var currentUser = this.userRepository.All().Where(x => x.Id == userId).FirstOrDefault();

            var currentTeam = this.teamRepository.All().Where(x => x.Id == teamId)
               .FirstOrDefault();
            currentTeam.ApplicationUsers.Remove(currentUser);


            await this.teamRepository.SaveChangesAsync();
        }
    }
}
