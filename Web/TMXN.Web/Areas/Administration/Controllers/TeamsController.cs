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

namespace TMXN.Web.Areas.Administration.Controllers
{
    public class TeamsController : AdministrationController
    {
        private readonly ITeamsService teamsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IUsersService usersService;
        private readonly IAwardsService awardsService;

        public TeamsController(ITeamsService teamsService, UserManager<ApplicationUser> userManager, IDeletableEntityRepository<Team> teamsRepository, IUsersService usersService, IAwardsService awardsService)
        {
            this.teamsService = teamsService;
            this.userManager = userManager;
            this.teamsRepository = teamsRepository;
            this.usersService = usersService;
            this.awardsService = awardsService;
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
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var viewModel = this.teamsService.GetInfo<EditTeamsViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamsViewModel model)
        {
            await this.teamsService.EditAsync(model.Name, model.LogoImage, model.Tag, model.Id);

           
            return this.RedirectToAction("ShowAll", "Teams", new { area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Add(TeamInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            var isUserAlreadyInTeam = this.teamsRepository.All().Where(x => x.ApplicationUsers.Any(x => x.Id == user.Id)).FirstOrDefault();
            if (isUserAlreadyInTeam != null)
            {

                //TODO: Error view 
                throw new Exception("You are already in a team!");
            }


            await this.teamsService.AddAsync(model.Name, model.LogoImage, model.Tag, user);

            
            return this.RedirectToAction("ShowAll", "Teams", new { area = "Administration" });
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

        public async Task<IActionResult> RemovePlayer(string id)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(id);
            await this.teamsService.RemovePlayerAsync(user);
            return this.RedirectToAction(nameof(ShowAll));
        }
        public IActionResult TeamInfo(string id)
        {
            var viewModel = this.teamsService.GetInfo<TeamInfoViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Remove(string id)
        {

            await this.teamsService.RemoveAsync(id);

            return this.RedirectToAction(nameof(RemoveSuccess));

        }

        public IActionResult RemoveSuccess()
        {
            return this.View();

        }

        public IActionResult Ranklist()
        {
            var viewModel = new ListRanklistTeamViewModel
            {
                Teams = this.teamsService.GetRanklist<RanklistTeamViewModel>().ToList(),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Win(string id)
        {
            await this.teamsService.WinAsync(id);

            return this.RedirectToAction(nameof(this.Ranklist));
        }
        public async Task<IActionResult> Lose(string id)
        {
            await this.teamsService.LoseAsync(id);

            return this.RedirectToAction(nameof(this.Ranklist));
        }
        public IActionResult SuccessTransfer()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SendAward(string teamId, string awardId)
        {
            await this.teamsService.SendAwardAsync(teamId, awardId);
            return this.RedirectToAction(nameof(SuccessTransfer));
        }

        public async Task<IActionResult> SendAward()
        {
            var viewModel = new TeamAwardDropDownListViewModel
            {
                Teams = this.teamsService.GetAll<TeamDropDownViewModel>(),
                Awards = await this.awardsService.GetAll<AwardDropDownViewModel>(),

            };
            return this.View(viewModel);
        }

    }
}
