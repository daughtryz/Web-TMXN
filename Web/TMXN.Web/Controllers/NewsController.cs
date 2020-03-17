using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Services.Data;

namespace TMXN.Web.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsFeedsService newsService;

        public NewsController(INewsFeedsService newsService)
        {
            this.newsService = newsService;
        }
        public IActionResult Info(string newsId)
        {
            var viewModel = this.newsService.GetNewsById(newsId);
            return this.View(viewModel);
        }

    }
}
