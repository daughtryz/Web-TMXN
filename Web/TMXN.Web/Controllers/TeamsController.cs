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

        public TeamsController(ITeamsService teamsService,UserManager<ApplicationUser> userManager,IDeletableEntityRepository<Team> teamsRepository,IUsersService usersService)
        {
            this.teamsService = teamsService;
            this.userManager = userManager;
            this.teamsRepository = teamsRepository;
            this.usersService = usersService;
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
            var isUserAlreadyInTeam = this.teamsRepository.All().Where(x => x.ApplicationUsers.Any(x => x.Id == user.Id)).FirstOrDefault();
            if(isUserAlreadyInTeam != null)
            {
              
                //TODO: Error view 
                throw new Exception("You are already in a team!");
            }
           
            
       await this.teamsService.AddAsync(model.Name, model.Logo, model.Tag,user.Id);

            return this.RedirectToAction(nameof(this.ShowAll));
        }

       
        public async Task<IActionResult> Leave(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.usersService.LeaveAsync(id, user.Id);

            return this.RedirectToAction(nameof(this.ShowAll));

        }
        
        public async Task<IActionResult> Join(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.usersService.JoinAsync(id, user.Id);


            return this.RedirectToAction(nameof(this.ShowAll));
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

    }
}
