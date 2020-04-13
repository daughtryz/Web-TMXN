using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.News;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.News;

namespace TMXN.Web.Areas.Administration.Controllers
{
    public class NewsController : AdministrationController
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

        [HttpPost]
        public async Task<IActionResult> Create(NewsInputModel newsCreateInputModel)
        {
            await this.newsService.CreateNewsAsync(newsCreateInputModel.Title, newsCreateInputModel.Content, newsCreateInputModel.Image);

            return this.RedirectToAction(nameof(this.Success));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.newsService.GetNewsById<EditNewsViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditNewsViewModel model)
        {
            await this.newsService.EditAsync(model.Id, model.Title, model.Content, model.Image);

            return this.Redirect("/");
        }

        public IActionResult All()
        {
            var viewModel = new NewsFeedsListViewModel();
            var all = this.newsService.GetAll<NewsViewModel>();
            viewModel.News = all;


            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult Success()
        {
            return this.View();
        }
        public IActionResult Edit()
        {
            return this.View();
        }




        public async Task<IActionResult> Remove(string id)
        {
            await this.newsService.DeleteByIdAsync(id);

            return this.Redirect("/");
        }
        public IActionResult SuccessEdit()
        {
            return this.View();
        }
    }
}
