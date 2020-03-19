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
        private readonly IDeletableEntityRepository<Team> teamsRepository;

        public TeamsController(ITeamsService teamsService,UserManager<ApplicationUser> userManager,IDeletableEntityRepository<Team> teamsRepository)
        {
            this.teamsService = teamsService;
            this.userManager = userManager;
            this.teamsRepository = teamsRepository;
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
            var isUserAlreadyInTeam = this.teamsRepository.All().Where(x => x.UserId == user.Id).FirstOrDefault();
            if(isUserAlreadyInTeam != null)
            {
              
               // TODO: Error view 
                throw new Exception("You are already in a team!");
            }
           
            
       await this.teamsService.AddAsync(model.Name, model.Logo, model.Tag,user.Id);

            return this.RedirectToAction(nameof(this.ShowAll));
        }

       public async Task<IActionResult> Leave(string userId)
        {
            await this.teamsService.LeaveAsync(userId);

            return this.RedirectToAction(nameof(this.ShowAll));

        }
    }
}
