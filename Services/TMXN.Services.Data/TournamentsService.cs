using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Mapping;
using TMXN.Web.ViewModels.Tournaments;

namespace TMXN.Services.Data
{
    public class TournamentsService : ITournamentsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepo;
        private readonly IRepository<TournamentTeam> tournamentsTeamsRepo;
        private readonly IDeletableEntityRepository<Tournament> tournamentRepository;
        private readonly IDeletableEntityRepository<Team> teamRepository;

        public TournamentsService(IDeletableEntityRepository<ApplicationUser> userRepo,IRepository<TournamentTeam> tournamentsTeamsRepo,IDeletableEntityRepository<Tournament> tournamentRepository,IDeletableEntityRepository<Team> teamRepository)
        {
            this.userRepo = userRepo;
            this.tournamentsTeamsRepo = tournamentsTeamsRepo;
            this.tournamentRepository = tournamentRepository;
            this.teamRepository = teamRepository;
        }

        public IEnumerable<TViewModel> All<TViewModel>()
        {
            return this.tournamentRepository.All().To<TViewModel>().ToList();
        }

        public async Task GenerateAsync(string name,string organizer, TournamentGameType tournamentType)
        {


            var tournament = new Tournament
            {
                Name = name,
                Organizer = organizer,
                CreatedOn = DateTime.UtcNow,
                TournamentGameType = tournamentType,
            };

            await this.tournamentRepository.AddAsync(tournament);
            await this.tournamentRepository.SaveChangesAsync();

        }

       

        public TViewModel Info<TViewModel>(int id)
        {
            var currentTournament = this.tournamentRepository.All().Where(x => x.Id == id).To<TViewModel>().FirstOrDefault();

            return currentTournament;
        }

        public async Task ParticipateAsync(string userId,int tournamentId)
        {
            var currentTeam = this.teamRepository.All().Where(x => x.ApplicationUsers.Any(z => z.Id == userId)).FirstOrDefault();



            var currentTournament = this.tournamentRepository.All().Where(x => x.Id == tournamentId).FirstOrDefault();

            var tournamentTeam = new TournamentTeam
            {
                TournamentId = currentTournament.Id,
                TeamId = currentTeam.Id,
            };

            await this.tournamentsTeamsRepo.AddAsync(tournamentTeam);
            await this.tournamentsTeamsRepo.SaveChangesAsync();
            currentTournament.TeamId = currentTeam.Id;

            this.tournamentRepository.Update(currentTournament);
            await this.tournamentRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var currentTournament = this.tournamentRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.tournamentRepository.Delete(currentTournament);
            await this.tournamentRepository.SaveChangesAsync();

        }

        public async Task<int> RemoveTeamFromTournamentAsync(int tournamentId,string userId)
        {
            Tournament currentTournament = this.tournamentRepository.All().Where(x => x.Id == tournamentId).FirstOrDefault();

            Team currentTeam = this.userRepo.All().Where(x => x.Id == userId).Select(x => x.Team).FirstOrDefault();

            
            var currentTournamentTeam = this.tournamentsTeamsRepo.All().Where(x => x.TournamentId == currentTournament.Id && x.TeamId == currentTeam.Id).FirstOrDefault();
            if(currentTournamentTeam == null)
            {

                return 0;
                
            }
            currentTournament.TeamId = null;
            this.tournamentRepository.Update(currentTournament);
            await this.tournamentRepository.SaveChangesAsync();
            this.tournamentsTeamsRepo.Delete(currentTournamentTeam);
            await this.tournamentsTeamsRepo.SaveChangesAsync();
            return currentTournamentTeam.TournamentId;
        }

      
    }
}
