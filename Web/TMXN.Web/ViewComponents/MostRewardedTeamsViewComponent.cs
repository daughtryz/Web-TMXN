using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Common;
using TMXN.Services.Data;
using TMXN.Web.CustomAttributes;
using TMXN.Web.ViewModels.Teams;

namespace TMXN.Web.ViewComponents
{
    [ViewComponent(Name = "Award")]
    [ListingViewComponents("Most rewarded fafadfdadaf", "Rewards")]
    public class MostRewardedTeamsViewComponent : ViewComponent
    {
        private readonly ITeamsService teamsService;

        public MostRewardedTeamsViewComponent(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        public IViewComponentResult Invoke()
        {
            ListingViewComponentsAttribute attr = (ListingViewComponentsAttribute)Attribute.GetCustomAttribute(typeof(MostRewardedTeamsViewComponent), typeof(ListingViewComponentsAttribute));
            var viewModel = new MostRewardedTeamsListViewModel
            {
                Teams = this.teamsService.GetAll<MostRewardedTeamViewModel>().OrderByDescending(x => x.AwardsCount).Take(GlobalConstants.TeamsAwardCountInViewComponent).ToList(),
            };
            viewModel.Header = attr.Header;
            viewModel.Footer = attr.Footer;
            return this.View(viewModel);

        }
    }
}
