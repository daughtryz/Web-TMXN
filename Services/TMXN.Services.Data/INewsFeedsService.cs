using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Web.ViewModels.News;

namespace TMXN.Services.Data
{
    public interface INewsFeedsService
    {

        IEnumerable<T> GetAll<T>();

        NewsViewModel GetNewsById(string newsId);

        public Task CreateNewsAsync(string title, string content, string imageUrl);
    }
}
