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
       
        public IActionResult GetAll(string gametype = null)
        {
            var viewModel = new TournamentsListViewModel
            {
                Tournaments = this.tournamentsService.All<TournamentsViewModel>(gametype),
            };

            return this.View(viewModel);
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

            if(tournamentId == 0)
            {
                return this.BadRequest();
            } else
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
