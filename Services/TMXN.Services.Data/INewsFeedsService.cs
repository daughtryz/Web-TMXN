using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Web.ViewModels.News;

namespace TMXN.Services.Data
{
    public interface INewsFeedsService
    {

        IEnumerable<T> GetAll<T>();

        NewsViewModel GetNewsById(string newsId);
    }
}
