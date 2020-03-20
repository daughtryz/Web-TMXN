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
    public class TournamentsService : ITournamentsService
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentRepository;
        private readonly IDeletableEntityRepository<Team> teamRepository;

        public TournamentsService(IDeletableEntityRepository<Tournament> tournamentRepository,IDeletableEntityRepository<Team> teamRepository)
        {
            this.tournamentRepository = tournamentRepository;
            this.teamRepository = teamRepository;
        }

        public IEnumerable<T> All<T>()
        {
            return this.tournamentRepository.All().To<T>().ToList();
        }

        public async Task GenerateAsync(string name,string organizer)
        {


            var tournament = new Tournament
            {
                Name = name,
                Organizer = organizer,
                CreatedOn = DateTime.UtcNow,
            };

            await this.tournamentRepository.AddAsync(tournament);
            await this.tournamentRepository.SaveChangesAsync();

        }

        public async Task ParticipateAsync(string userId,int tournamentId)
        {
            var currentTeam = this.teamRepository.All().Where(x => x.ApplicationUsers.Any(z => z.Id == userId)).FirstOrDefault();

            var currentTournament = this.tournamentRepository.All().Where(x => x.Id == tournamentId).FirstOrDefault();

            currentTournament.TeamId = currentTeam.Id;

            this.tournamentRepository.Update(currentTournament);
            await this.tournamentRepository.SaveChangesAsync();
        }
    }
}
