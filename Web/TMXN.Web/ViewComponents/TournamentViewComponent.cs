using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Common;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Tournaments;

namespace TMXN.Web.ViewComponents
{
    [ViewComponent(Name = "Tournament")]
    public class TournamentViewComponent : ViewComponent
    {
        private readonly ITournamentsService tournamentsService;

        public TournamentViewComponent(ITournamentsService tournamentsService)
        {
            this.tournamentsService = tournamentsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new ListingLatestTournamentsViewModel
            {
                LatestTournaments = this.tournamentsService.All<LatestTournamentViewModel>().OrderByDescending(x => x.CreatedOn).Take(GlobalConstants.TakeLatestTournaments).ToList(),
            };

            return this.View(viewModel);

        }

    }
}
