using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Web.ViewModels.News;

namespace TMXN.Services.Data
{
    public interface INewsFeedsService
    {

        IEnumerable<TViewModel> GetAll<TViewModel>(string criteria = null);

        public Task<T> GetNewsById<T>(string newsId);

        public Task CreateNewsAsync(string title, string content, IFormFile image);


        public Task EditAsync(string id, string title, string content, IFormFile image);


        public Task DeleteByIdAsync(string newsId);

       
    }
}
