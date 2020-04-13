using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Teams;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Awards;
using TMXN.Web.ViewModels.Teams;

namespace TMXN.Web.Controllers
{
    [Authorize]
    public class TeamsController : BaseController
    {
        private readonly ITeamsService teamsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IUsersService usersService;
        private readonly IAwardsService awardsService;

        public TeamsController(ITeamsService teamsService,UserManager<ApplicationUser> userManager,IDeletableEntityRepository<Team> teamsRepository,IUsersService usersService,IAwardsService awardsService)
        {
            this.teamsService = teamsService;
            this.userManager = userManager;
            this.teamsRepository = teamsRepository;
            this.usersService = usersService;
            this.awardsService = awardsService;
        }
      
        public  IActionResult ShowAll()
        {
            var viewModel = new TeamsListViewModel();
            var all = this.teamsService.GetAll<TeamViewModel>();
            viewModel.Teams = all;
            return this.View(viewModel);
        }
     
       

       
        public async Task<IActionResult> Leave(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.usersService.LeaveAsync(id, user);

            return this.RedirectToAction(nameof(this.ShowAll));

        }
        
        public async Task<IActionResult> Join(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.usersService.JoinAsync(id, user);


            return this.RedirectToAction(nameof(this.ShowAll));
        }

        
        public IActionResult TeamInfo(string id)
        {
            var viewModel = this.teamsService.GetInfo<TeamInfoViewModel>(id);
            
            return this.View(viewModel);
        }

       

        public IActionResult Ranklist()
        {
            var viewModel = new ListRanklistTeamViewModel
            {
                Teams = this.teamsService.GetRanklist<RanklistTeamViewModel>().ToList(),
            };
            return this.View(viewModel);
        }

       

       

    }
}
