using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Tournaments;
using TMXN.Data.Models;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Tournaments;
using TMXN.Web.ViewModels.Tournaments;

namespace TMXN.Web.Controllers
{
    [Authorize]
    public class TournamentsController : BaseController
    {
        private readonly ITournamentsService tournamentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TournamentsController(ITournamentsService tournamentsService,UserManager<ApplicationUser> userManager)
        {
            this.tournamentsService = tournamentsService;
            this.userManager = userManager;
        }
        public IActionResult Generate()
        {
            return this.View();
        }
        public IActionResult GetAll()
        {
            var viewModel = new TournamentsListViewModel
            {
                Tournaments = this.tournamentsService.All<TournamentsViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Generate(TournamentsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
           

            await this.tournamentsService.GenerateAsync(model.Name, model.Organizer,model.TournamentGameType);
            return this.RedirectToAction(nameof(this.GetAll));
           
        }

        public async Task<IActionResult> Participate(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);


            await this.tournamentsService.ParticipateAsync(user.Id, id);

            return this.Redirect("/Tournaments/Success");
        }

        public async Task<IActionResult> Out(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.tournamentsService.RemoveTeamFromTournamentAsync(id, user.Id);

            return this.Redirect("/");

        }
        public async Task<IActionResult> Info(int id)
        {
            var teams = await this.tournamentsService.GetAllTournamentTeamsAsync<TournamentTeamInfoViewModel>(id);
            var viewModel = new TournamentInfoViewModel
            {
                EnrolledTeams = teams,
            };

            return this.View(viewModel);

        }
       
        public IActionResult Success()
        {
            return this.View();
        }

        
    }
}
