using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

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
    }
}
