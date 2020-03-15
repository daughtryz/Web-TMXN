using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IEnumerable<T> GetAll<T>()
        {
            return this.teamsRepository.All().To<T>().ToList();
        }
    }
}
