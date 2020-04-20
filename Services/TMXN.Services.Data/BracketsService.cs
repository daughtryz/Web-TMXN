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
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IRepository<TournamentTeam> tournamentsTeamsRepo;

        public BracketsService(IDeletableEntityRepository<Bracket> bracketRepository,IDeletableEntityRepository<Team> teamsRepository,IDeletableEntityRepository<Tournament> tournamentsRepository,IRepository<TournamentTeam> tournamentsTeamsRepo)
        {
            this.bracketRepository = bracketRepository;
            this.teamsRepository = teamsRepository;
            this.tournamentsRepository = tournamentsRepository;
            this.tournamentsTeamsRepo = tournamentsTeamsRepo;
        }

        public async Task CreateAsync(int tournamentId, string teamId)
        {
            Bracket bracket = new Bracket
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

        public async Task EliminateAsync(string teamId)
        {
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefault();

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
            var currentTeam = this.teamsRepository.All().Where(x => x.Id == teamId).FirstOrDefault();

            if(currentTeam == null)
            {
                return;
            }
            currentTeam.IsWinner = true;
            this.teamsRepository.Update(currentTeam);
            await this.teamsRepository.SaveChangesAsync();

        }

        IEnumerable<TViewModel> IBracketsService.GetAll<TViewModel>()
        {
            return this.bracketRepository.All().OrderBy(x => x.Tournament.Name).To<TViewModel>().ToList();
        }

    }
}
