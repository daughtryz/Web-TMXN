using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Data.Contracts;
using TMXN.Services.Mapping;
using TMXN.Web.ViewModels.News;

namespace TMXN.Services.Data
{
    public class NewsFeedsService : INewsFeedsService
    {
        private readonly IDeletableEntityRepository<NewsFeed> newsFeedRepository;
        private readonly ICloudinaryService cloudinaryService;

        public NewsFeedsService(IDeletableEntityRepository<NewsFeed> newsFeedRepository,ICloudinaryService cloudinaryService)
        {
            this.newsFeedRepository = newsFeedRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateNewsAsync(string title, string content, IFormFile image)
        {
            var imageUrlCloudinary = await this.cloudinaryService
                .UploadAsync(image,image.Name);
            var newsFeed = new NewsFeed
            {
                Title = title,
                Content = content,
                ImageUrl = imageUrlCloudinary,
            };
            await this.newsFeedRepository.AddAsync(newsFeed);
            await this.newsFeedRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string newsId)
        {
            var currentNews = this.newsFeedRepository.All().Where(x => x.Id == newsId).FirstOrDefault();
            if (currentNews == null)
            {
                throw new NullReferenceException("No such news");
            }
            this.newsFeedRepository.Delete(currentNews);
            await this.newsFeedRepository.SaveChangesAsync();
        }

        public async Task EditAsync(string id,string title, string content, string imageUrl)
        {
            var currentNews = this.newsFeedRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (currentNews == null)
            {
                throw new NullReferenceException("No such news");
            }

            currentNews.Title = title;
            currentNews.Content = content;
            currentNews.ImageUrl = imageUrl;

            this.newsFeedRepository.Update(currentNews);
            await this.newsFeedRepository.SaveChangesAsync();
        }

        public IEnumerable<TViewModel> GetAll<TViewModel>()
        {
            return this.newsFeedRepository.All().To<TViewModel>().ToList();
        }

        public async Task<TViewModel> GetNewsById<TViewModel>(string newsId)
        {
            var currentNews = await this.newsFeedRepository.All().Where(x => x.Id == newsId).To<TViewModel>().FirstOrDefaultAsync();

            return currentNews;
        }
    }
}
