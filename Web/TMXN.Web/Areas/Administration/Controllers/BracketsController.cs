using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Brackets;
using TMXN.Web.ViewModels.Teams;
using TMXN.Web.ViewModels.Tournaments;

namespace TMXN.Web.Areas.Administration.Controllers
{
    public class BracketsController : AdministrationController
    {
        private readonly IBracketsService bracketsService;
        private readonly ITournamentsService tournamentsService;
        private readonly ITeamsService teamsService;

        public BracketsController(IBracketsService bracketsService, ITournamentsService tournamentsService, ITeamsService teamsService)
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

      

        
        public async Task<IActionResult> Eliminate(string id)
        {
            await this.bracketsService.EliminateAsync(id);
            return this.RedirectToAction("All", "Brackets", new { area = "Administration" });
        }

       

        public async Task<IActionResult> Win(string id)
        {
            await this.bracketsService.WinAsync(id);
            return this.RedirectToAction("All", "Brackets", new { area = "Administration" });

        }
    }
}
