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
       

        private EfDeletableEntityRepository<Team> teamRepository;
        private EfDeletableEntityRepository<ApplicationUser> userRepo;
        private EfRepository<TournamentTeam> tournamentsTeamRepo;
        private EfDeletableEntityRepository<Tournament> tournamentRepository;
        private EfDeletableEntityRepository<Bracket> bracketsRepository;

        public TournamentsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.teamRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            this.userRepo = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            this.tournamentRepository = new EfDeletableEntityRepository<Tournament>(new ApplicationDbContext(options.Options));
            this.tournamentsTeamRepo = new EfRepository<TournamentTeam>(new ApplicationDbContext(options.Options));
            this.bracketsRepository = new EfDeletableEntityRepository<Bracket>(new ApplicationDbContext(options.Options));


        }
        [Fact]
        public async Task CheckIfGenerateWorks()
        {
           

            TournamentsService tournamentsService = new TournamentsService(this.userRepo, this.tournamentsTeamRepo, this.tournamentRepository, this.teamRepository, this.bracketsRepository);



            await tournamentsService.GenerateAsync("Milzzz", "Kolio", (TournamentGameType)1);

            var expectedTournamentName = "Milzzz";
            var expectedOrganizerName = "Kolio";
            var expectedGameType = (TournamentGameType)1;

            var currentTournament = await this.tournamentRepository.All().FirstOrDefaultAsync();

            Assert.Equal(expectedTournamentName, currentTournament.Name);
            Assert.Equal(expectedOrganizerName, currentTournament.Organizer);
            Assert.Equal(expectedGameType, currentTournament.TournamentGameType);
        }

        [Fact]
        public async Task CheckIfEditWorks()
        {
          

            TournamentsService tournamentsService = new TournamentsService(this.userRepo, this.tournamentsTeamRepo, this.tournamentRepository, this.teamRepository, this.bracketsRepository);



            await tournamentsService.GenerateAsync("Milzzz", "Kolio", (TournamentGameType)1);

           

            var currentTournament = await this.tournamentRepository.All().FirstOrDefaultAsync();

            await tournamentsService.EditAsync("Mitko", "Dilqn", (TournamentGameType)2,currentTournament.Id);

            AutoMapperConfig.RegisterMappings(typeof(MyTestTournamentViewModel).Assembly);
            var currentTournamentToEdit = tournamentsService.Info<MyTestTournamentViewModel>(1);

            var expectedTournamentName = "Mitko";
            var expectedOrganizerName = "Dilqn";
            var expectedGameType = (TournamentGameType)2;



            Assert.Equal(expectedTournamentName, currentTournamentToEdit.Name);
            Assert.Equal(expectedOrganizerName, currentTournamentToEdit.Organizer);
            Assert.Equal(expectedGameType, currentTournamentToEdit.TournamentGameType);
        }

        [Fact]
        public async Task CheckIfTournamentCountWorks()
        {
           

            TournamentsService tournamentsService = new TournamentsService(this.userRepo, this.tournamentsTeamRepo, this.tournamentRepository, this.teamRepository, this.bracketsRepository);
            await tournamentsService.GenerateAsync("TestTour", "League", (TournamentGameType)2);

            var expectedResult = 1;

            Assert.Equal(expectedResult, await this.tournamentRepository.All().CountAsync());

        }

        [Fact]
        public async Task CheckIfParticipateWorks()
        {
    

            TournamentsService tournamentsService = new TournamentsService(this.userRepo, this.tournamentsTeamRepo, this.tournamentRepository, this.teamRepository, this.bracketsRepository);
            await this.teamRepository.AddAsync(new Team
            {
                Name = "Testing",
                Tag = "TST",
            });

            await this.teamRepository.SaveChangesAsync();

            var currentTeam = await this.teamRepository.All().FirstOrDefaultAsync();

            await this.tournamentRepository.AddAsync(new Tournament
            {
                Name = "Kristo",
                Organizer = "Bibi",
            });
            await this.tournamentRepository.SaveChangesAsync();

            var currentTournament = await this.tournamentRepository.All().FirstOrDefaultAsync();

            await this.tournamentsTeamRepo.AddAsync(new TournamentTeam
            {
                TeamId = currentTeam.Id,
                TournamentId = currentTournament.Id,

            });
            await this.tournamentsTeamRepo.SaveChangesAsync();

            var currentTournamentTeam = await this.tournamentsTeamRepo.All().FirstOrDefaultAsync();
           
            var expectedTeamId = currentTeam.Id;
            var expectedTournamentId = currentTournament.Id;

            Assert.Equal(expectedTeamId, currentTournamentTeam.TeamId);

            Assert.Equal(expectedTournamentId, currentTournamentTeam.TournamentId);

           

        }
        [Fact]
        public async Task CheckIfRemoveTeamFromTournamentWorks()
        {
            

            TournamentsService tournamentsService = new TournamentsService(this.userRepo, this.tournamentsTeamRepo, this.tournamentRepository, this.teamRepository, this.bracketsRepository);
            await this.teamRepository.AddAsync(new Team
            {
                Name = "Testing",
                Tag = "TST",
            });

            await this.teamRepository.SaveChangesAsync();

            var currentTeam = await this.teamRepository.All().FirstOrDefaultAsync();

            await this.tournamentRepository.AddAsync(new Tournament
            {
                Name = "Kristo",
                Organizer = "Bibi",
            });
            await this.tournamentRepository.SaveChangesAsync();

            var currentTournament = await this.tournamentRepository.All().FirstOrDefaultAsync();

            await this.tournamentsTeamRepo.AddAsync(new TournamentTeam
            {
                TeamId = currentTeam.Id,
                TournamentId = currentTournament.Id,

            });
            await this.tournamentsTeamRepo.SaveChangesAsync();

            var currentTournamentTeam = await this.tournamentsTeamRepo.All().FirstOrDefaultAsync();

            this.tournamentsTeamRepo.Delete(currentTournamentTeam);
            await this.tournamentsTeamRepo.SaveChangesAsync();

            var expectedCount = 0;

            Assert.Equal(expectedCount,this.tournamentsTeamRepo.All().Count());


        }


        [Fact]
        public async Task CheckIfRemoveWorks()
        {
          

            TournamentsService tournamentsService = new TournamentsService(this.userRepo, this.tournamentsTeamRepo, this.tournamentRepository, this.teamRepository, this.bracketsRepository);

            await tournamentsService.GenerateAsync("TestTour", "League", (TournamentGameType)2);
            await tournamentsService.GenerateAsync("TestT", "Legeee", (TournamentGameType)1);
            await tournamentsService.RemoveAsync(1);
            var expectedResult = 1;

            Assert.Equal(expectedResult, await this.tournamentRepository.All().CountAsync());

        }

        [Fact]
        public async Task CheckIfInfoWorks()
        {
            

            TournamentsService tournamentsService = new TournamentsService(this.userRepo, this.tournamentsTeamRepo, this.tournamentRepository, this.teamRepository, this.bracketsRepository);
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

