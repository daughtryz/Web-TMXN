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

namespace TMXN.Web.Areas.Administration.Controllers
{
    public class TournamentsController : AdministrationController
    {
        private readonly ITournamentsService tournamentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TournamentsController(ITournamentsService tournamentsService, UserManager<ApplicationUser> userManager)
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


            await this.tournamentsService.GenerateAsync(model.Name, model.Organizer, model.TournamentGameType);
            //return this.RedirectToAction(nameof(this.GetAll));
            return this.RedirectToAction("GetAll", "Tournaments", new { area = "Administration" });

        }

        public async Task<IActionResult> Remove(int id)
        {
            await this.tournamentsService.RemoveAsync(id);

            // return this.RedirectToAction(nameof(this.GetAll));
            return this.RedirectToAction("GetAll", "Tournaments", new { area = "Administration" });
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

            int tournamentId = await this.tournamentsService.RemoveTeamFromTournamentAsync(id, user.Id);

            if (tournamentId == 0)
            {
                return this.BadRequest();
            }
            else
            {
                return this.RedirectToAction(nameof(SuccessRemove));
            }


        }


        public IActionResult Info(int id)
        {
            var viewModel = this.tournamentsService.Info<TournamentInfoViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Success()
        {
            return this.View();
        }


        public IActionResult SuccessRemove()
        {


            return this.View();
        }

    }
}
