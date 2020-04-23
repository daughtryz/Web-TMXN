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
using TMXN.Data;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Data.Repositories;
using TMXN.Services.Mapping;
using Xunit;

namespace TMXN.Services.Data.Tests
{
    public class TeamsServiceTests
    {

       


        [Fact]
        public async Task CheckIfAddCountWorksCorrectly()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var teamRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));

            await teamRepository.AddAsync(new Team { Name = "Extinction", Logo = "blaaaa", Tag = "TMX" });
            await teamRepository.AddAsync(new Team { Name = "Gratz", Logo = "blaa", Tag = "GRF" });
            await teamRepository.SaveChangesAsync();
            var expectedResult = 2;



            Assert.Equal(expectedResult,await teamRepository.All().CountAsync());

        }

        [Fact]
        public async Task CheckIfAddWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var teamRepository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            var awardRepository = new EfDeletableEntityRepository<Award>(new ApplicationDbContext(options.Options));

            TeamsService teamService = new TeamsService(teamRepository, awardRepository, new CloudinaryService(new Cloudinary("cloudinary://139762851849643:BUAw31H4Q2VBJpC_DBa8zSRr10Q@degdnz5ro")));
            
           

            //await teamService.AddAsync("Tuzzxx", ,"TXZ" ,new ApplicationUser { UserName = "Mitko" });


        }



    }

    
}
