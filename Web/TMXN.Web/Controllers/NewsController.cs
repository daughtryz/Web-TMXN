using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.News;
using TMXN.Services.Data;
using TMXN.Services.Data.Contracts;
using TMXN.Web.ViewModels.News;

namespace TMXN.Web.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsFeedsService newsService;
        

        public NewsController(INewsFeedsService newsService)
        {
            this.newsService = newsService;
           
        }
        public async Task<IActionResult> Info(string id)
        {
            var viewModel = await this.newsService.GetNewsById<NewsViewModel>(id);
            return this.View(viewModel);
        }

       

       
       


      


    }
}
