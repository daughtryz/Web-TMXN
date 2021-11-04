using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Teams;
using TMXN.Common;
using TMXN.Web.CustomAttributes;

namespace TMXN.Web.ViewComponents
{
    [ViewComponent(Name = "Team")]
    [ListingViewComponents("Top teamss", "Teams")]
    public class TeamViewComponent : ViewComponent
    {
        private readonly ITeamsService teamsService;

        public TeamViewComponent(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        public IViewComponentResult Invoke()
        {
            ListingViewComponentsAttribute attr = (ListingViewComponentsAttribute)Attribute.GetCustomAttribute(typeof(TeamViewComponent), typeof(ListingViewComponentsAttribute));
            var viewModel = new ListingTeamsByYearOfCreationViewModel
            {
                Teams =  this.teamsService.GetAll<TeamByYearViewModel>().OrderByDescending(x => x.Points).Take(GlobalConstants.TeamsCountInViewComponent).ToList(),
            };
            viewModel.Header = attr.Header;
            viewModel.Footer = attr.Footer;
            return this.View(viewModel);

        }
    }
}
