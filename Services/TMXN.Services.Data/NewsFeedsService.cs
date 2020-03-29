using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Mapping;
using TMXN.Web.ViewModels.News;

namespace TMXN.Services.Data
{
    public class NewsFeedsService : INewsFeedsService
    {
        private readonly IDeletableEntityRepository<NewsFeed> newsFeedRepository;

        public NewsFeedsService(IDeletableEntityRepository<NewsFeed> newsFeedRepository)
        {
            this.newsFeedRepository = newsFeedRepository;
        }

        public async Task CreateNewsAsync(string title, string content, string imageUrl)
        {
            var newsFeed = new NewsFeed
            {
                Title = title,
                Content = content,
                ImageUrl = imageUrl,
            };
            await this.newsFeedRepository.AddAsync(newsFeed);
            await this.newsFeedRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string newsId)
        {
            var currentNews = this.newsFeedRepository.All().Where(x => x.Id == newsId).FirstOrDefault();

            this.newsFeedRepository.Delete(currentNews);
            await this.newsFeedRepository.SaveChangesAsync();
        }

        public async Task EditAsync(string id,string title, string content, string imageUrl)
        {
            var currentNews = this.newsFeedRepository.All().Where(x => x.Id == id).FirstOrDefault();

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
