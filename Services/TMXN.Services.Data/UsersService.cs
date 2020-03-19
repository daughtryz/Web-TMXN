using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;

namespace TMXN.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<Team> teamRepository,IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
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
