using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Common;
using TMXN.Services.Data;
using TMXN.Web.CustomAttributes;
using TMXN.Web.ViewModels.Tournaments;

namespace TMXN.Web.ViewComponents
{
    [ViewComponent(Name = "Tournament")]
    [ListingViewComponents("Latest tournamentss", "Tournaments")]

    public class TournamentViewComponent : ViewComponent
    {
        private readonly ITournamentsService tournamentsService;

        public TournamentViewComponent(ITournamentsService tournamentsService)
        {
            this.tournamentsService = tournamentsService;
        }

        public IViewComponentResult Invoke()
        {
            ListingViewComponentsAttribute attr = (ListingViewComponentsAttribute)Attribute.GetCustomAttribute(typeof(TournamentViewComponent), typeof(ListingViewComponentsAttribute));

            var viewModel = new ListingLatestTournamentsViewModel
            {
                LatestTournaments = this.tournamentsService.All<LatestTournamentViewModel>().OrderByDescending(x => x.CreatedOn).Take(GlobalConstants.TakeLatestTournaments).ToList(),
            };
            viewModel.Header = attr.Header;
            viewModel.Footer = attr.Footer;
            return this.View(viewModel);

        }

    }
}
