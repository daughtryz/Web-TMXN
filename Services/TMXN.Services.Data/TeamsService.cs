using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Mapping;
using TMXN.Common;
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

        public IEnumerable<TViewModel> GetAll<TViewModel>()
        {
            return this.teamsRepository.All().To<TViewModel>().ToList();
        }

        public TViewModel GetInfo<TViewModel>(string teamId)
        {
            return this.teamsRepository.All().Where(x => x.Id == teamId).To<TViewModel>().FirstOrDefault();
        }

        public IEnumerable<TViewModel> GetRanklist<TViewModel>()
        {
            return this.teamsRepository.All().OrderByDescending(x => x.Points).To<TViewModel>().ToList();
        }

        public async Task LoseAsync(string teamId)
        {
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefault();

            if(currentTeam.Points <= 0)
            {
                return;
            }

            currentTeam.Points -= GlobalConstants.PointsDecrease;

            this.teamsRepository.Update(currentTeam);
            await this.teamsRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            this.teamsRepository.Delete(currentTeam);

            await this.teamsRepository.SaveChangesAsync();
        }

        public async Task WinAsync(string teamId)
        {
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefault();

            if (currentTeam.Points >= 200)
            {
                return;
            }

            currentTeam.Points += GlobalConstants.PointsIncrease;

            this.teamsRepository.Update(currentTeam);
            await this.teamsRepository.SaveChangesAsync();
        }
    }
}
