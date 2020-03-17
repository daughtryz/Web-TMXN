using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IEnumerable<T> GetAll<T>()
        {
            return this.newsFeedRepository.All().To<T>().ToList();
        }

        public NewsViewModel GetNewsById(string newsId)
        {
            var currentNews = this.newsFeedRepository.All().Where(x => x.Id == newsId).Select(x => new NewsViewModel
            {
                Title = x.Title,
                Content = x.Content,
                ImageUrl = x.ImageUrl,
            }).FirstOrDefault();

            return currentNews;
        }
    }
}
