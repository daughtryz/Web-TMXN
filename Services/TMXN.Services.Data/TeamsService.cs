﻿using Microsoft.AspNetCore.Identity;
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
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Award> awardsRepository;
        private readonly ICloudinaryService cloudinaryService;

        private const string TeamsOrderedByDateAscending = "teams-ordered-by-date-ascending";
        private const string TeamsOrderedByDateDescending = "teams-ordered-by-date-descending";
        private const string TeamsOrderedByPointsDescending = "teams-ordered-by-points-descending";
        private const string TeamsOrderedByAwardsDescending = "teams-ordered-by-awards-descending";

        public TeamsService(IDeletableEntityRepository<ApplicationUser> userRepository,IDeletableEntityRepository<Team> teamsRepository,IDeletableEntityRepository<Award> awardsRepository,ICloudinaryService cloudinaryService)
        {
            this.userRepository = userRepository;
            this.teamsRepository = teamsRepository;
            this.awardsRepository = awardsRepository;
            this.cloudinaryService = cloudinaryService;
            
           
           
        }

      
        public async Task AddAsync(string name, IFormFile logo,string tag, ApplicationUser user)
        {

            var logoCloudinary = await this.cloudinaryService
               .UploadAsync(logo, logo.FileName);
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
              .UploadAsync(logo, logo.FileName);
            var currentTeam = await this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefaultAsync();
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

        public IEnumerable<TViewModel> GetTeamsOrderedByNameAscending<TViewModel>()
        {
            return this.teamsRepository.All().OrderBy(x => x.Name).To<TViewModel>().ToList();
        }

        public IEnumerable<TViewModel> GetTeamsOrderedByPointsDescending<TViewModel>()
        {
            return this.teamsRepository.All().OrderByDescending(x => x.Points).To<TViewModel>().ToList();
        }

        public IEnumerable<TViewModel> GetTeamsOrderedByDateDescending<TViewModel>()
        {
            return this.teamsRepository.All().OrderByDescending(x => x.CreatedOn).To<TViewModel>().ToList();
        }

        public IEnumerable<TViewModel> GetTeamsOrderedByDateAscending<TViewModel>()
        {
            return this.teamsRepository.All().OrderBy(x => x.CreatedOn).To<TViewModel>().ToList();
        }

        public IEnumerable<TViewModel> GetTeamsOrderedByAwardsDescending<TViewModel>()
        {
            return this.teamsRepository.All().OrderByDescending(x => x.Awards.Count).To<TViewModel>().ToList();
        }
        public IEnumerable<TViewModel> GetAll<TViewModel>(string criteria = null)
        {
            if(criteria == TeamsOrderedByDateAscending)
            {
                return this.GetTeamsOrderedByDateAscending<TViewModel>();
            } else if(criteria == TeamsOrderedByDateDescending)
            {
                return this.GetTeamsOrderedByDateDescending<TViewModel>();
            }
            else if (criteria == TeamsOrderedByPointsDescending)
            {
                return this.GetTeamsOrderedByPointsDescending<TViewModel>();
            }
            else if (criteria == TeamsOrderedByAwardsDescending)
            {
                return this.GetTeamsOrderedByAwardsDescending<TViewModel>();
            }

            return this.GetTeamsOrderedByNameAscending<TViewModel>();
        }

        public TViewModel GetInfo<TViewModel>(string teamId)
        {
            return this.teamsRepository.All().Where(x => x.Id == teamId).To<TViewModel>().FirstOrDefault();
        }

        public IEnumerable<TViewModel> GetRanklist<TViewModel>()
        {
            return this.teamsRepository.All().OrderByDescending(x => x.Points).To<TViewModel>().ToList();
        }

        public async Task<int> LoseAsync(string teamId)
        {
            var currentTeam = await this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefaultAsync();

            if(currentTeam.Points <= 0)
            {
                throw new Exception("Team points cannot be less than zero!");
            }

            currentTeam.Points -= GlobalConstants.PointsDecrease;

            this.teamsRepository.Update(currentTeam);
            await this.teamsRepository.SaveChangesAsync();

            return currentTeam.Points;
        }

        public async Task RemoveAsync(string id)
        {
            var currentTeam = await this.teamsRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            this.teamsRepository.Delete(currentTeam);

            await this.teamsRepository.SaveChangesAsync();
        }

        public async Task RemovePlayerAsync(ApplicationUser user)
        {
            var currentTeam = await this.teamsRepository.All().Where(x => x.Id == user.TeamId).FirstOrDefaultAsync();
            if(user == null || currentTeam == null)
            {
                return;
            }

            currentTeam.ApplicationUsers.Remove(user);
            //this.teamsRepository.Update(currentTeam);
            user.TeamId = null;
            await this.userRepository.SaveChangesAsync();
            //await this.teamsRepository.SaveChangesAsync();
        }

        public async Task SendAwardAsync(string teamId,string awardId)
        {
            var currentTeam = await this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefaultAsync();
            var currentAward = await this.awardsRepository.All().Where(x => x.Id == awardId).FirstOrDefaultAsync();


            if(currentTeam == null || currentAward == null)
            {
                return;
            }
            if(currentTeam.Awards.Any(x => x.Id == currentAward.Id))
            {
                throw new Exception("This team already has the same award!");
               
            }
            currentTeam.Awards.Add(currentAward);
           // this.teamsRepository.Update(currentTeam);
            await this.awardsRepository.SaveChangesAsync();
        }

        public async Task<int> WinAsync(string teamId)
        {
            var currentTeam = await this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefaultAsync();

            if (currentTeam.Points >= 200)
            {
                throw new Exception("You exceeded the maximum points");
            }

            currentTeam.Points += GlobalConstants.PointsIncrease;

            this.teamsRepository.Update(currentTeam);
            await this.teamsRepository.SaveChangesAsync();

            return currentTeam.Points;

        }
    }
}
