using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Services.Data
{
    public class TeamsService : ITeamsService
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
   

        public TeamsService(IDeletableEntityRepository<Team> teamsRepository)
        {
            this.teamsRepository = teamsRepository;
            
            this.userRepository = userRepository;
           
        }

        public async Task AddAsync(string name,string logo,string tag,string userId)
        {


            var team = new Team
            {
                Name = name,
                Logo = logo,
                Tag = tag,  
               
            };
            var currentUser = this.userRepository.All().Where(x => x.Id == userId).FirstOrDefault();

            team.ApplicationUsers.Add(currentUser);
            await this.teamsRepository.AddAsync(team);
           
            await this.teamsRepository.SaveChangesAsync();

            
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.teamsRepository.All().To<T>().ToList();
        }

        public T GetInfo<T>(string teamId)
        {
            return this.teamsRepository.All().Where(x => x.Id == teamId).To<T>().FirstOrDefault();
        }
    }
}
