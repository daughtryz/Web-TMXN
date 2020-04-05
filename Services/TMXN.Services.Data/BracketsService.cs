using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Models;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Services.Data
{
    public class BracketsService : IBracketsService
    {
        private readonly IDeletableEntityRepository<Bracket> bracketRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;

        public BracketsService(IDeletableEntityRepository<Bracket> bracketRepository,IDeletableEntityRepository<Team> teamsRepository)
        {
            this.bracketRepository = bracketRepository;
            this.teamsRepository = teamsRepository;
        }

        public async Task CreateAsync(int tournamentId, string teamId)
        {
            var bracket = new Bracket
            {
                TournamentId = tournamentId,
            };
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefault();

            if(currentTeam == null)
            {
                return;
            }
            bracket.Teams.Add(currentTeam);
            await this.bracketRepository.AddAsync(bracket);
            await this.bracketRepository.SaveChangesAsync();
        }

        IEnumerable<TViewModel> IBracketsService.GetAll<TViewModel>()
        {
            return this.bracketRepository.All().OrderBy(x => x.Tournament.Name).To<TViewModel>().ToList();
        }

    }
}
