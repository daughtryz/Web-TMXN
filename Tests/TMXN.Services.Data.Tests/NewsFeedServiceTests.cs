
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data;
using TMXN.Data.Models;
using TMXN.Data.Repositories;
using TMXN.Services.Mapping;
using Xunit;

namespace TMXN.Services.Data.Tests
{
    public class NewsFeedServiceTests
    {
       


        private EfDeletableEntityRepository<NewsFeed> newsRepository;
        private CloudinaryService cloudinaryService;
        public NewsFeedServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.newsRepository = new EfDeletableEntityRepository<NewsFeed>(new ApplicationDbContext(options.Options));
            this.cloudinaryService = new CloudinaryService(new Cloudinary("cloudinary://139762851849643:BUAw31H4Q2VBJpC_DBa8zSRr10Q@degdnz5ro"));
        }


        [Fact]
        public async Task TestIfDeleteWorks()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };


            NewsFeedsService newsService = new NewsFeedsService(this.newsRepository, this.cloudinaryService);

            await newsService.CreateNewsAsync("Title", "This is content!", image);
            var currentNews = await this.newsRepository.All().FirstOrDefaultAsync();
            await newsService.DeleteByIdAsync(currentNews.Id);
            var expectedCount = 0;

            Assert.Equal(expectedCount, await this.newsRepository.All().CountAsync());
        }


        [Fact]
        public async Task TestIfEditWorks()
        {
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };


            NewsFeedsService newsService = new NewsFeedsService(this.newsRepository, this.cloudinaryService);

            await newsService.CreateNewsAsync("Title", "This is content!", image);

            AutoMapperConfig.RegisterMappings(typeof(MyNewsViewModel).Assembly);
            var currentNews = await this.newsRepository.All().FirstOrDefaultAsync();

            

            using var newStream = File.OpenRead(@"C:\Users\user\Desktop\sktt1.png");

            IFormFile newImage = new FormFile(newStream, 0, newStream.Length, null, Path.GetFileName(newStream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };

            await newsService.EditAsync(currentNews.Id, "NewTitle", "NewContent", newImage);

            var currentNewsToEdit = await newsService.GetNewsById<MyNewsViewModel>(currentNews.Id);

            var expectedTitle = "NewTitle";
            var expectedContent = "NewContent";
            var expectedImageUrl = "http://res.cloudinary.com/degdnz5ro/image/upload/v1587648727/sktt1.png.png";

            Assert.Equal(expectedTitle, currentNewsToEdit.Title);
            Assert.Equal(expectedContent, currentNewsToEdit.Content);
            Assert.Equal(expectedImageUrl, currentNewsToEdit.ImageUrl);

        }


        [Fact]
        public async Task TestIfGetNewsByIdWorks()
        {

            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };


            NewsFeedsService newsService = new NewsFeedsService(this.newsRepository, this.cloudinaryService);

            await newsService.CreateNewsAsync("Title", "This is content!", image);

            AutoMapperConfig.RegisterMappings(typeof(MyNewsViewModel).Assembly);
            var currentNews = await this.newsRepository.All().FirstOrDefaultAsync();

            var currentNewsToGetById = await newsService.GetNewsById<MyNewsViewModel>(currentNews.Id);

            var expectedTitle = "Title";
            var expectedContent = "This is content!";
            var expectedImageUrl = "http://res.cloudinary.com/degdnz5ro/image/upload/v1587644649/Team_SoloMidlogo_profile.png.png";

            Assert.Equal(expectedTitle, currentNewsToGetById.Title);
            Assert.Equal(expectedContent, currentNewsToGetById.Content);
            Assert.Equal(expectedImageUrl, currentNewsToGetById.ImageUrl);

        }


        [Fact]
        public async Task TestIfCreateWorks()
        {
            
            using var stream = File.OpenRead(@"C:\Users\user\Desktop\Team_SoloMidlogo_profile.png");

            IFormFile image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/png",
            };
            

            NewsFeedsService newsService = new NewsFeedsService(this.newsRepository, this.cloudinaryService);

            

            await newsService.CreateNewsAsync("Title", "This is content!", image);

            var currentNews = await this.newsRepository.All().FirstOrDefaultAsync();
            var expectedTitle = "Title";
            var expectedContent = "This is content!";
            var expectedImageUrl = "http://res.cloudinary.com/degdnz5ro/image/upload/v1587644649/Team_SoloMidlogo_profile.png.png";

            Assert.Equal(expectedTitle, currentNews.Title);
            Assert.Equal(expectedContent, currentNews.Content);
            Assert.Equal(expectedImageUrl, currentNews.ImageUrl);

        }


    }

    public class MyNewsViewModel : IMapFrom<NewsFeed>
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }
    }
}
