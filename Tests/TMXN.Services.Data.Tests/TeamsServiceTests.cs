using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Common;
using TMXN.Data;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Data.Repositories;
using TMXN.Services.Mapping;
using Xunit;

namespace TMXN.Services.Data.Tests
{
    public class TeamsServiceTests
    {

        

        private const int DefaultPoints = 50;
       

        private EfDeletableEntityRepository<Team> teamsRepository;
        private EfDeletableEntityRepository<Award> awardsRepository;
        private EfDeletableEntityRepository<ApplicationUser> userRepository;
        private CloudinaryService cloudinaryService;
        public TeamsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.teamsRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            this.awardsRepository = new EfDeletableEntityRepository<Award>(new ApplicationDbContext(options.Options));
            this.userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            this.cloudinaryService = new CloudinaryService(new Cloudinary(GlobalConstants.CloudinaryUrl));
        }

        //[Fact]
        //public async Task CheckIfRemovePlayerWorks()
        //{
        //    using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

        //    IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
        //    {
        //        Headers = new HeaderDictionary(),
        //        ContentType = "application/png",
        //    };

        //    TeamsService teamService = new TeamsService(this.userRepository,this.teamsRepository, this.awardsRepository, this.cloudinaryService);

        //    await this.userRepository.AddAsync(new ApplicationUser
        //    {
        //        UserName = "MitkoTest",
        //    });
        //    await this.userRepository.SaveChangesAsync();

        //    var currentUser = await this.userRepository.All().FirstOrDefaultAsync();

        //    await teamService.AddAsync("ZZTest", logo, "TST", currentUser);


        //    var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();

           

        //    await teamService.RemovePlayerAsync(currentUser);

        //    var expectedCount = 0;


        //    Assert.Equal(expectedCount, currentTeam.ApplicationUsers.Count);

        //}
        [Fact]
        public async Task CheckIfSendAwardWorks()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            TeamsService teamService = new TeamsService(this.userRepository, this.teamsRepository, this.awardsRepository, this.cloudinaryService);

           
            await teamService.AddAsync("TestTeam", logo, "TST", new ApplicationUser());
            

            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();

            await this.awardsRepository.AddAsync(new Award
            {
                Name = "Slitherin",
                PlacingType = (PlacingType)1,
               
            });
            await this.awardsRepository.SaveChangesAsync();
            var currentAward = await this.awardsRepository.All().FirstOrDefaultAsync();

            await teamService.SendAwardAsync(currentTeam.Id, currentAward.Id);

            var expectedCount = 1;


            Assert.Equal(expectedCount, currentTeam.Awards.Count);

        }

        [Fact]
        public async Task CheckIfLoseWorks()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            TeamsService teamService = new TeamsService(this.userRepository, this.teamsRepository, this.awardsRepository, this.cloudinaryService);

            await teamService.AddAsync("Extinct", logo, "EXT", new ApplicationUser());

            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();





            currentTeam.Points = DefaultPoints;
            await this.teamsRepository.SaveChangesAsync();
            var losePoints = await teamService.LoseAsync(currentTeam.Id);
            currentTeam.Points = losePoints;
            

            var expectedResult = 49;
            Assert.Equal(expectedResult, currentTeam.Points);
        }

        [Fact]
        public async Task CheckIfWinWorks()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            TeamsService teamService = new TeamsService(this.userRepository, this.teamsRepository, this.awardsRepository, this.cloudinaryService);

            await teamService.AddAsync("Extinct", logo, "EXT", new ApplicationUser());

            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();




            currentTeam.Points = DefaultPoints;

            await this.teamsRepository.SaveChangesAsync();
            var winPoints = await teamService.WinAsync(currentTeam.Id);
            currentTeam.Points = winPoints;
            

            var expectedResult = 53;
            Assert.Equal(expectedResult, currentTeam.Points);
        }

        [Fact]
        public async Task CheckIfRanklistInfoWorks()
        {

            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            TeamsService teamService = new TeamsService(this.userRepository, this.teamsRepository, this.awardsRepository, this.cloudinaryService);

            await teamService.AddAsync("Extinct", logo, "EXT", new ApplicationUser());

            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();

            

            AutoMapperConfig.RegisterMappings(typeof(MyTeamViewModel).Assembly);

            var currentTeamToCheck = teamService.GetInfo<MyTeamViewModel>(currentTeam.Id);


            currentTeamToCheck.Points = DefaultPoints;

            var expectedName = "Extinct";
            var expectedTag = "EXT";
            var expectedLogo = "http://res.cloudinary.com/degdnz5ro/image/upload/v1587644649/Team_SoloMidlogo_profile.png.png";
            var expectedPoints = DefaultPoints;

            Assert.Equal(expectedName, currentTeamToCheck.Name);
            Assert.Equal(expectedTag, currentTeamToCheck.Tag);
            Assert.Equal(expectedLogo, currentTeamToCheck.Logo);
            Assert.Equal(expectedPoints, currentTeamToCheck.Points);


        }



        [Fact]
        public async Task CheckIfRemoveWorks()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            TeamsService teamService = new TeamsService(this.userRepository, this.teamsRepository, this.awardsRepository, this.cloudinaryService);

            await teamService.AddAsync("Extinct", logo, "EXT", new ApplicationUser());
            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();
            await teamService.RemoveAsync(currentTeam.Id);

            var expectedCount = 0;

            Assert.Equal(expectedCount, await this.teamsRepository.All().CountAsync());
        }


        [Fact]
        public async Task CheckIfInfoWorks()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            TeamsService teamService = new TeamsService(this.userRepository, this.teamsRepository, this.awardsRepository, this.cloudinaryService);

            await teamService.AddAsync("Extinct", logo, "EXT", new ApplicationUser());
            AutoMapperConfig.RegisterMappings(typeof(MyTeamViewModel).Assembly);
            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();
            var currentTeamToCheck = teamService.GetInfo<MyTeamViewModel>(currentTeam.Id);

            var expectedName = "Extinct";
            var expectedTag = "EXT";
            var expectedLogo = "http://res.cloudinary.com/degdnz5ro/image/upload/v1587644649/Team_SoloMidlogo_profile.png.png";

            Assert.Equal(expectedName, currentTeamToCheck.Name);
            Assert.Equal(expectedTag, currentTeamToCheck.Tag);
            Assert.Equal(expectedLogo, currentTeamToCheck.Logo);

        }


        [Fact]
        public async Task CheckIfAddWorksCorrectly()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            TeamsService teamService = new TeamsService(this.userRepository, this.teamsRepository, this.awardsRepository, this.cloudinaryService);

            await teamService.AddAsync("Extinct", logo, "EXT", new ApplicationUser());

            var expectedCount = 1;

            Assert.Equal(expectedCount, await this.teamsRepository.All().CountAsync());


        }
        [Fact]
        public async Task CheckIfEditWorks()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile logo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };
            TeamsService teamService = new TeamsService(this.userRepository, this.teamsRepository, this.awardsRepository, this.cloudinaryService);

            await teamService.AddAsync("Extinct", logo, "EXT", new ApplicationUser());


            AutoMapperConfig.RegisterMappings(typeof(MyTeamViewModel).Assembly);
            var currentTeam = await this.teamsRepository.All().FirstOrDefaultAsync();



            using var newStream = File.OpenRead(@"C:\Users\user\Desktop\sktt1.png");

            IFormFile newLogo = new FormFile(newStream, 0, newStream.Length, null, Path.GetFileName(newStream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            await teamService.EditAsync("Dim", newLogo,"DMS",currentTeam.Id);

            var currentTeamToEdit = teamService.GetInfo<MyTeamViewModel>(currentTeam.Id);

            var expectedName = "Dim";
            var expectedTag = "DMS";
            var expectedLogo = "http://res.cloudinary.com/degdnz5ro/image/upload/v1587648727/sktt1.png.png";

            Assert.Equal(expectedName, currentTeamToEdit.Name);
            Assert.Equal(expectedTag, currentTeamToEdit.Tag);
            Assert.Equal(expectedLogo, currentTeamToEdit.Logo);

        }



    }

    public class MyTeamViewModel : IMapFrom<Team>
    {

        public string Id { get; set; }
        public string Name { get; set; }

        public string Logo { get; set; }

        public string Tag { get; set; }

        public int Points { get; set; }
    }

   

    
}
