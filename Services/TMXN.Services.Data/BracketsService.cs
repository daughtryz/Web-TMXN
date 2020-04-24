using Microsoft.EntityFrameworkCore;
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
        private readonly IDeletableEntityRepository<Tournament> tournamentRepository;

        public BracketsService(IDeletableEntityRepository<Bracket> bracketRepository,IDeletableEntityRepository<Team> teamsRepository,IDeletableEntityRepository<Tournament> tournamentRepository)
        {
            this.bracketRepository = bracketRepository;
            this.teamsRepository = teamsRepository;
            this.tournamentRepository = tournamentRepository;
        }

       

        public async Task EliminateAsync(string teamId)
        {
            var currentTeam = await this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefaultAsync();

            if (currentTeam == null)
            {
                return;
            }

            currentTeam.IsEliminate = true;
            this.teamsRepository.Update(currentTeam);
            await this.teamsRepository.SaveChangesAsync();


           
        }

        public async Task WinAsync(string teamId)
        {
            var currentTeam = await this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefaultAsync();
            var currentTournament = await this.tournamentRepository.All().Where(x => x.TeamId == currentTeam.Id).FirstOrDefaultAsync();

            if(currentTeam == null)
            {
                return;
            }
            currentTeam.IsWinner = true;
            currentTournament.IsFinished = true;
            this.tournamentRepository.Update(currentTournament);
            await this.tournamentRepository.SaveChangesAsync();

            this.teamsRepository.Update(currentTeam);
            await this.teamsRepository.SaveChangesAsync();

        }

        IEnumerable<TViewModel> IBracketsService.GetAll<TViewModel>()
        {
            return this.bracketRepository.All().OrderBy(x => x.Tournament.Name).To<TViewModel>().ToList();
        }

    }
}
