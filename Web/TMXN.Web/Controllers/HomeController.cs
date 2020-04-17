namespace TMXN.Web.Controllers
{
    using System.Diagnostics;

    using TMXN.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using TMXN.Services.Data;
    using TMXN.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;

    public class HomeController : BaseController
    {
        private readonly INewsFeedsService newsFeedsService;

        public HomeController(INewsFeedsService newsFeedsService)
        {
            this.newsFeedsService = newsFeedsService;
        }

        
        public IActionResult Index([FromQuery]string criteria = null)
        {
            var viewModel = new NewsFeedsListViewModel();
            var all = this.newsFeedsService.GetAll<NewsViewModel>(criteria);
            viewModel.News = all;

            

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return this.View();
        }
        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult HttpError(int statusCode)
        {
            if(statusCode == 404)
            {
                return this.View(statusCode);
            }
            return this.View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
