using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data;
using TMXN.Data.Models;
using TMXN.Data.Repositories;
using Xunit;

namespace TMXN.Services.Data.Tests
{
    public class UsersServiceTests
    {
        private readonly UserManager<ApplicationUser> userManager;

        //public Task LeaveAsync(string teamId, ApplicationUser user);

        //public Task JoinAsync(string teamId, ApplicationUser user);

        //public Task AddFriendToFriendlistAsync(string id, ApplicationUser myUser);

        //public Task RemoveFriendFromFriendlistAsync(string id, ApplicationUser myUser);

        //public Task<IEnumerable<TViewModel>> GetAll<TViewModel>();

        //public Task<IEnumerable<TViewModel>> AllFriendsAsync<TViewModel>(string id);

        private EfDeletableEntityRepository<ApplicationUser> userRepository;
        private EfDeletableEntityRepository<Team> teamsRepository;
     
        private EfRepository<UserFriend> userFriendRepo;
        private EfDeletableEntityRepository<UserFriendlist> friendlistRepository;
        public UsersServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.teamsRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            this.userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            this.userFriendRepo = new EfRepository<UserFriend>(new ApplicationDbContext(options.Options));
            this.friendlistRepository = new EfDeletableEntityRepository<UserFriendlist>(new ApplicationDbContext(options.Options));
            
        }

        [Fact]
        public async Task TestIfJoinWorks()
        {
            UsersService usersService = new UsersService(this.userRepository, null, this.userFriendRepo, this.teamsRepository, this.friendlistRepository);

            await this.teamsRepository.AddAsync(new Team { Name = "Test", Tag = "TST" });
            await this.teamsRepository.SaveChangesAsync();

            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();
            await this.userRepository.AddAsync(new ApplicationUser { UserName = "Mitko" });
            await this.userRepository.SaveChangesAsync();

            var currentUser = await this.userRepository.All().FirstOrDefaultAsync();

            await usersService.JoinAsync(currentTeam.Id, currentUser);

            var expectedCount = 1;

            Assert.Equal(expectedCount, currentTeam.ApplicationUsers.Count);

        }

    }
}
