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
    public class AwardsServiceTests
    {


        

        private EfDeletableEntityRepository<Award> awardsRepository;
        public AwardsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.awardsRepository = new EfDeletableEntityRepository<Award>(new ApplicationDbContext(options.Options));
        }

        [Fact]
        public async Task CheckIfEditWorks()
        {



            AwardsService awardsService = new AwardsService(this.awardsRepository);


            await awardsService.CreateAsync("Mitko", (PlacingType)1);



            var currentAward = await this.awardsRepository.All().FirstOrDefaultAsync();

            await awardsService.EditAsync("Dimitar", (PlacingType)2, currentAward.Id);

            AutoMapperConfig.RegisterMappings(typeof(MyTestTournamentViewModel).Assembly);
            var currentAwardToEdit = awardsService.GetById<MyAwardTestViewModel>(currentAward.Id);

            var expectedAwardName = "Dimitar";
           
            var expectedPlacingType = (PlacingType)2;



            Assert.Equal(expectedAwardName, currentAwardToEdit.Name);
   
            Assert.Equal(expectedPlacingType, currentAwardToEdit.PlacingType);
        }


        [Fact]
        public async Task CheckIfCreateWorks()
        {


            AwardsService awardsService = new AwardsService(this.awardsRepository);


            await awardsService.CreateAsync("Mitko", (PlacingType)1);

            var expectedAwardName = "Mitko";
           
            var expectedPlacingType = (PlacingType)1;

            var currentAward = await this.awardsRepository.All().FirstOrDefaultAsync();

            Assert.Equal(expectedAwardName, currentAward.Name);
            Assert.Equal(expectedPlacingType, currentAward.PlacingType);
           

        }



        [Fact]
        public async Task CheckIfInfoWorks()
        {


            AwardsService awardsService = new AwardsService(this.awardsRepository);


            await awardsService.CreateAsync("Mitko", (PlacingType)1);
            AutoMapperConfig.RegisterMappings(typeof(MyTestTournamentViewModel).Assembly);

            var currentAward = await this.awardsRepository.All().FirstOrDefaultAsync();
            var currentTournament = awardsService.GetById<MyAwardTestViewModel>(currentAward.Id);


            var expectedName = "Mitko";
           
            var expectedPlacingType = (PlacingType)1;

            Assert.Equal(expectedName, currentAward.Name);
            Assert.Equal(expectedPlacingType, currentAward.PlacingType);
            
        }
        [Fact]
        public async Task CheckIfAddToDbWorks()
        {


            AwardsService awardsService = new AwardsService(this.awardsRepository);


            await awardsService.CreateAsync("Mitko", (PlacingType)1);

         

            var expectedResult = 1;

            Assert.Equal(expectedResult, this.awardsRepository.All().Count());

            


        }
        [Fact]
        public async Task CheckIfRemoveWorks()
        {
            AwardsService awardsService = new AwardsService(this.awardsRepository);


            await awardsService.CreateAsync("Mitko", (PlacingType)1);
            await awardsService.CreateAsync("Deont", (PlacingType)2);

            var firstAward = await this.awardsRepository.All().FirstOrDefaultAsync();

            await awardsService.RemoveAsync(firstAward.Id);
            var expectedResult = 1;

            Assert.Equal(expectedResult, this.awardsRepository.All().Count());
        }
    }

    public class MyAwardTestViewModel : IMapFrom<Award>
    {
        public string Name { get; set; }

        public PlacingType PlacingType { get; set; }
    }
}
