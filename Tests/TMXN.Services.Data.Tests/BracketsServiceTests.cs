using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data;
using TMXN.Data.Models;
using TMXN.Data.Repositories;
using Xunit;

namespace TMXN.Services.Data.Tests
{
    public class BracketsServiceTests
    {
        
        
        private EfDeletableEntityRepository<Bracket> bracketsRepository;
        private EfDeletableEntityRepository<Team> teamsRepository;
        private EfDeletableEntityRepository<Tournament> tournamentsRepository;
        public BracketsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.bracketsRepository = new EfDeletableEntityRepository<Bracket>(new ApplicationDbContext(options.Options));
            this.teamsRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            this.tournamentsRepository = new EfDeletableEntityRepository<Tournament>(new ApplicationDbContext(options.Options));
        }

        [Fact]
        public async Task TestIfWinnerWorks()
        {
            BracketsService bracketsService = new BracketsService(this.bracketsRepository,this.teamsRepository,this.tournamentsRepository);

            await this.teamsRepository.AddAsync(new Team { Name = "Tested", Tag = "TSTS" });
            await this.teamsRepository.SaveChangesAsync();
            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();


            await bracketsService.WinAsync(currentTeam.Id);

            var expectedResult = true;

           
            Assert.Equal(expectedResult, currentTeam.IsWinner);
        }


        [Fact]
        public async Task TestIfEliminateWorks()
        {
            BracketsService bracketsService = new BracketsService(this.bracketsRepository, this.teamsRepository,this.tournamentsRepository);

            await this.teamsRepository.AddAsync(new Team { Name = "Tested", Tag = "TSTS" });
            await this.teamsRepository.SaveChangesAsync();
            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();


            await bracketsService.EliminateAsync(currentTeam.Id);

            var expectedResult = true;


            Assert.Equal(expectedResult, currentTeam.IsEliminate);
        }

       
    }
}
