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
       

        public TeamsService(IDeletableEntityRepository<Team> teamsRepository)
        {
            this.teamsRepository = teamsRepository;
           
        }

        public async Task AddAsync(string name,string logo,string tag,string userId)
        {
            var team = new Team
            {
                Name = name,
                Logo = logo,
                Tag = tag,  
                UserId = userId,
            };
  
            await this.teamsRepository.AddAsync(team);
           
            await this.teamsRepository.SaveChangesAsync();

            
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.teamsRepository.All().To<T>().ToList();
        }
    }
}
