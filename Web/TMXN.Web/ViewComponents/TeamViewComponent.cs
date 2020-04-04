using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Teams;
using TMXN.Common;

namespace TMXN.Web.ViewComponents
{
    [ViewComponent(Name = "Team")]
    public class TeamViewComponent : ViewComponent
    {
        private readonly ITeamsService teamsService;

        public TeamViewComponent(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new ListingTeamsByYearOfCreationViewModel
            {
                Teams =  this.teamsService.GetAll<TeamByYearViewModel>().OrderByDescending(x => x.Points).Take(GlobalConstants.TeamsCountInViewComponent).ToList(),
            };

            return this.View(viewModel);

        }
    }
}
