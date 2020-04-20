namespace TMXN.Web.Controllers
{
    using System.Diagnostics;

    using TMXN.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using TMXN.Services.Data;
    using TMXN.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using TMXN.Services.Data.Contracts;
    using TMXN.Web.ViewModels.Chat;
    using System.Threading.Tasks;

    public class HomeController : BaseController
    {
        private readonly INewsFeedsService newsFeedsService;
        private readonly IChatRoomService chatRoomService;

        public HomeController(INewsFeedsService newsFeedsService,IChatRoomService chatRoomService)
        {
            this.newsFeedsService = newsFeedsService;
            this.chatRoomService = chatRoomService;
        }

        
        public IActionResult Index([FromQuery]string criteria = null)
        {
            var viewModel = new NewsFeedsListViewModel();
            var all = this.newsFeedsService.GetAll<NewsViewModel>(criteria);
            viewModel.News = all;

            

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Chat()
        {
            ChatViewModel viewModel = await this.chatRoomService.GetAllMessages();
            return this.View(viewModel);
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
