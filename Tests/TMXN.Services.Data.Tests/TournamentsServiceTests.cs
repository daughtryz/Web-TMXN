using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Models;
using TMXN.Data.Repositories;
using TMXN.Services.Mapping;
using Xunit;

namespace TMXN.Services.Data.Tests
{
    public class TournamentsServiceTests
    {
        //public Task GenerateAsync(string name, string organizer, TournamentGameType tournamentType); // gotov

        //public IEnumerable<TViewModel> All<TViewModel>(string gametype = null); // gotov

        //public Task ParticipateAsync(string userId, int tournamentId);

        //public Task RemoveAsync(int id); // gotov

        //public Task<int> RemoveTeamFromTournamentAsync(int tournamentId, string userId);


        //public TViewModel Info<TViewModel>(int id); //gotov

        //public Task EditAsync(string name, string organizer, TournamentGameType TournamentGameType, int tournamentId);


        [Fact]
        public async Task CheckIfGenerateWorks()
        {
            //IDeletableEntityRepository<ApplicationUser> userRepo, IRepository< TournamentTeam > tournamentsTeamsRepo,IDeletableEntityRepository<Tournament> tournamentRepository, IDeletableEntityRepository< Team > teamRepository,IDeletableEntityRepository<Bracket> bracketRepository)
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var teamRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var tournamentsTeamRepo = new EfRepository<TournamentTeam>(new ApplicationDbContext(options.Options));
            var tournamentRepository = new EfDeletableEntityRepository<Tournament>(new ApplicationDbContext(options.Options));
            var bracketsRepository = new EfDeletableEntityRepository<Bracket>(new ApplicationDbContext(options.Options));

            TournamentsService tournamentsService = new TournamentsService(userRepo, tournamentsTeamRepo, tournamentRepository, teamRepository, bracketsRepository);



            await tournamentsService.GenerateAsync("Milzzz", "Kolio", (TournamentGameType)1);

            var expectedTournamentName = "Milzzz";
            var expectedOrganizerName = "Kolio";
            var expectedGameType = (TournamentGameType)1;

            var currentTournament = await tournamentRepository.All().FirstOrDefaultAsync();

            Assert.Equal(expectedTournamentName, currentTournament.Name);
            Assert.Equal(expectedOrganizerName, currentTournament.Organizer);
            Assert.Equal(expectedGameType, currentTournament.TournamentGameType);
        }

        [Fact]
        public async Task CheckIfTournamentCountWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var teamRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var tournamentsTeamRepo = new EfRepository<TournamentTeam>(new ApplicationDbContext(options.Options));
            var tournamentRepository = new EfDeletableEntityRepository<Tournament>(new ApplicationDbContext(options.Options));
            var bracketsRepository = new EfDeletableEntityRepository<Bracket>(new ApplicationDbContext(options.Options));

            TournamentsService tournamentsService = new TournamentsService(userRepo, tournamentsTeamRepo, tournamentRepository, teamRepository, bracketsRepository);

            await tournamentsService.GenerateAsync("TestTour", "League", (TournamentGameType)2);

            var expectedResult = 1;

            Assert.Equal(expectedResult, tournamentRepository.All().Count());

        }


        [Fact]
        public async Task CheckIfRemoveWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var teamRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var tournamentsTeamRepo = new EfRepository<TournamentTeam>(new ApplicationDbContext(options.Options));
            var tournamentRepository = new EfDeletableEntityRepository<Tournament>(new ApplicationDbContext(options.Options));
            var bracketsRepository = new EfDeletableEntityRepository<Bracket>(new ApplicationDbContext(options.Options));

            TournamentsService tournamentsService = new TournamentsService(userRepo, tournamentsTeamRepo, tournamentRepository, teamRepository, bracketsRepository);

            await tournamentsService.GenerateAsync("TestTour", "League", (TournamentGameType)2);
            await tournamentsService.GenerateAsync("TestT", "Legeee", (TournamentGameType)1);
            await tournamentsService.RemoveAsync(1);
            var expectedResult = 1;

            Assert.Equal(expectedResult, tournamentRepository.All().Count());

        }

        [Fact]
        public async Task CheckIfInfoWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var teamRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var tournamentsTeamRepo = new EfRepository<TournamentTeam>(new ApplicationDbContext(options.Options));
            var tournamentRepository = new EfDeletableEntityRepository<Tournament>(new ApplicationDbContext(options.Options));
            var bracketsRepository = new EfDeletableEntityRepository<Bracket>(new ApplicationDbContext(options.Options));

            TournamentsService tournamentsService = new TournamentsService(userRepo, tournamentsTeamRepo, tournamentRepository, teamRepository, bracketsRepository);

            await tournamentsService.GenerateAsync("TestTour", "League", (TournamentGameType)2);
            AutoMapperConfig.RegisterMappings(typeof(MyTestTournamentViewModel).Assembly);
            var currentTournament = tournamentsService.Info<MyTestTournamentViewModel>(1);
           

            var expectedName = "TestTour";
            var expectedOrganizer = "League";
            var expectedTournamentGameType = (TournamentGameType)2;

            Assert.Equal(expectedName, currentTournament.Name);
            Assert.Equal(expectedOrganizer, currentTournament.Organizer);
            Assert.Equal(expectedTournamentGameType, currentTournament.TournamentGameType);
        }
    }

    public class MyTestTournamentViewModel : IMapFrom<Tournament>
    {
        public string Name { get; set; }

        public string Organizer { get; set; }

        public TournamentGameType TournamentGameType { get; set; }
    }

   
   
}

