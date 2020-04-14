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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TMXN.Services.Data.Contracts;

namespace TMXN.Services.Data
{
    public class TeamsService : ITeamsService
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Award> awardsRepository;
        private readonly ICloudinaryService cloudinaryService;
       
   

        public TeamsService(IDeletableEntityRepository<Team> teamsRepository,IDeletableEntityRepository<Award> awardsRepository,ICloudinaryService cloudinaryService)
        {
            this.teamsRepository = teamsRepository;
            this.awardsRepository = awardsRepository;
            this.cloudinaryService = cloudinaryService;
            
           
           
        }

        public async Task AddAsync(string name, IFormFile logo,string tag, ApplicationUser user)
        {

            var logoCloudinary = await this.cloudinaryService
               .UploadAsync(logo, logo.Name);
            var team = new Team
            {
                Name = name,
                Logo = logoCloudinary,
                Tag = tag,  
               
            };
           
            

            team.ApplicationUsers.Add(user);
            await this.teamsRepository.AddAsync(team);
           
            await this.teamsRepository.SaveChangesAsync();

            
        }

        public async Task EditAsync(string name, IFormFile logo, string tag,string teamId)
        {
            var logoCloudinary = await this.cloudinaryService
              .UploadAsync(logo, logo.Name);
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefault();
            if(currentTeam == null)
            {
                return;
            }

            currentTeam.Name = name;
            currentTeam.Logo = logoCloudinary;
            currentTeam.Tag = tag;

            this.teamsRepository.Update(currentTeam);
            await this.teamsRepository.SaveChangesAsync();

        }

        public IEnumerable<TViewModel> GetAll<TViewModel>()
        {
            return  this.teamsRepository.All().OrderByDescending(x => x.Points).To<TViewModel>().ToList();
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

        public async Task RemovePlayerAsync(ApplicationUser user)
        {
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == user.TeamId).FirstOrDefault();
            if(user == null || currentTeam == null)
            {
                return;
            }

            currentTeam.ApplicationUsers.Remove(user);
            this.teamsRepository.Update(currentTeam);
            user.TeamId = null;
            await this.teamsRepository.SaveChangesAsync();
        }

        public async Task SendAwardAsync(string teamId,string awardId)
        {
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefault();
            var currentAward = this.awardsRepository.All().Where(x => x.Id == awardId).FirstOrDefault();
            if(currentTeam == null || currentAward == null)
            {
                return;
            }
            if(currentTeam.Awards.Any(x => x.Id == currentAward.Id))
            {
                throw new Exception("This team already has the same award!");
               
            }
            currentTeam.Awards.Add(currentAward);
            this.teamsRepository.Update(currentTeam);
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
