using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Common.InputModels;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Teams;

namespace TMXN.Web.Controllers
{
    public class TeamsController : BaseController
    {
        private readonly ITeamsService teamsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TeamsController(ITeamsService teamsService,UserManager<ApplicationUser> userManager)
        {
            this.teamsService = teamsService;
            this.userManager = userManager;
        }
        public IActionResult ShowAll()
        {
            var viewModel = new TeamsListViewModel();
            var all = this.teamsService.GetAll<TeamViewModel>();
            viewModel.Teams = all;
            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(TeamInputModel model)
        {
            if(!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var user = await this.userManager.GetUserAsync(this.User);
            
       await this.teamsService.AddAsync(model.Name, model.Logo, model.Tag,user.Id);

            return this.RedirectToAction(nameof(this.ShowAll));
        }

       
    }
}
