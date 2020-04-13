using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Awards;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Awards;

namespace TMXN.Web.Controllers
{
    public class AwardsController : BaseController
    {
        private readonly IAwardsService awardsService;

        public AwardsController(IAwardsService awardsService)
        {
            this.awardsService = awardsService;
        }
        public async Task<IActionResult> All()
        {
            var viewModel = new AwardListViewModel
            {
                Awards = await this.awardsService.GetAll<AwardViewModel>(),
            };
            return this.View(viewModel);
        }
     
    }
}
