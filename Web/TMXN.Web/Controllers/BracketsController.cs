using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Brackets;
using TMXN.Web.ViewModels.Teams;
using TMXN.Web.ViewModels.Tournaments;

namespace TMXN.Web.Controllers
{
    public class BracketsController : BaseController
    {
        private readonly IBracketsService bracketsService;
        private readonly ITournamentsService tournamentsService;
        private readonly ITeamsService teamsService;

        public BracketsController(IBracketsService bracketsService,ITournamentsService tournamentsService,ITeamsService teamsService)
        {
            this.bracketsService = bracketsService;
            this.tournamentsService = tournamentsService;
            this.teamsService = teamsService;
        }

        public IActionResult All()
        {
            var viewModel = new BracketListViewModel
            {
                Brackets = this.bracketsService.GetAll<BracketViewModel>(),
            };
            return this.View(viewModel);
        }
        //public IActionResult Create()
        //{
        //    var viewModel = new BracketDropdownViewModel
        //    {
        //        Tournaments = this.tournamentsService.All<TournamentDropdownViewModel>(),
        //        Teams =  this.teamsService.GetAll<TeamDropDownViewModel>(),

        //    };
        //    return this.View(viewModel);
           
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(int tournamentId,string teamId)
        //{
        //    await this.bracketsService.CreateAsync(tournamentId, teamId);
        //    return this.RedirectToAction(nameof(All));
        //}
    }
}
