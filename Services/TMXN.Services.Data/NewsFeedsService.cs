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
        private const string NewsOrderedByDateDescending = "news-ordered-by-date-descending";
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

        public async Task EditAsync(string id,string title, string content, IFormFile image)
        {
            var imageUrlCloudinary = await this.cloudinaryService
               .UploadAsync(image, image.Name);

            var currentNews = this.newsFeedRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (currentNews == null)
            {
                throw new NullReferenceException("No such news");
            }

            currentNews.Title = title;
            currentNews.Content = content;
            currentNews.ImageUrl = imageUrlCloudinary;

            this.newsFeedRepository.Update(currentNews);
            await this.newsFeedRepository.SaveChangesAsync();
        }

        private IEnumerable<TViewModel> GetAllNewsAscendingByTitle<TViewModel>()
        {
            return this.newsFeedRepository.All().OrderBy(x => x.Title).To<TViewModel>().ToList();
        }

        private IEnumerable<TViewModel> GetAllNewsDescendingByDate<TViewModel>()
        {
            return this.newsFeedRepository.All().OrderByDescending(x => x.CreatedOn).To<TViewModel>().ToList();
        }
        public IEnumerable<TViewModel> GetAll<TViewModel>(string criteria = null)
        {
            if(criteria == NewsOrderedByDateDescending)
            {
                return this.GetAllNewsDescendingByDate<TViewModel>();
            }
            return this.GetAllNewsAscendingByTitle<TViewModel>();
        }

        public async Task<TViewModel> GetNewsById<TViewModel>(string newsId)
        {
            var currentNews = await this.newsFeedRepository.All().Where(x => x.Id == newsId).To<TViewModel>().FirstOrDefaultAsync();

            return currentNews;
        }
    }
}
