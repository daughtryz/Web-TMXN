using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize]
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<UserFriend> userFriendRepo;
        private readonly IDeletableEntityRepository<Team> teamRepository;
       
        private readonly IDeletableEntityRepository<UserFriendlist> friendlistRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository,UserManager<ApplicationUser> userManager,IRepository<UserFriend> userFriendRepo,IDeletableEntityRepository<Team> teamRepository,IDeletableEntityRepository<UserFriendlist> friendlistRepository)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.userFriendRepo = userFriendRepo;
            this.teamRepository = teamRepository;
           
            this.friendlistRepository = friendlistRepository;
        }

        public async Task AddFriendToFriendlistAsync(string id,ApplicationUser myUser)
        {


            var currentUser = await this.userManager.FindByIdAsync(id);

            

            if (currentUser == null || myUser == null)
            {
                throw new Exception("No such user!");
            }
           

            

            var friendList = new UserFriendlist();
            
            friendList.ApplicationUsers.Add(currentUser);
            currentUser.IsTaken = true;
            


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
            var currentUser = await this.userManager.FindByIdAsync(id);

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
            
            return await this.userRepository.All().OrderBy(x => x.UserName).To<TViewModel>().ToListAsync();
        }

        public async Task JoinAsync(string teamId, ApplicationUser user)
        {
            var currentTeam = this.teamRepository.All().Where(x => x.Id == teamId)
                .FirstOrDefault();
           
            

            if (currentTeam == null || user == null)
            {
                return;
            }

            currentTeam.ApplicationUsers.Add(user);

            await this.teamRepository.SaveChangesAsync();

            
        }

        
        public async Task LeaveAsync(string teamId, ApplicationUser user)
        {
          

            var currentTeam = this.teamRepository.All().Where(x => x.Id == teamId)
               .FirstOrDefault();
            currentTeam.ApplicationUsers.Remove(user);


            await this.teamRepository.SaveChangesAsync();
        }

        public async Task RemoveFriendFromFriendlistAsync(string id,ApplicationUser myUser)
        {

            var currentUser = await this.userManager.FindByIdAsync(id);
           
            var currentUserFriendlist = this.userFriendRepo.All().Where(x => x.ApplicationUserId == myUser.Id).FirstOrDefault();
            var currentFriendlist = this.friendlistRepository.All().Where(x => x.Id == currentUserFriendlist.UserFriendlistId).FirstOrDefault();
            

           
            if (currentUserFriendlist == null)
            {
                return;
            }
            currentUser.IsTaken = false;

            


            this.friendlistRepository.Delete(currentFriendlist);
            this.userFriendRepo.Delete(currentUserFriendlist);
            await this.userFriendRepo.SaveChangesAsync();
        }
    }
}
