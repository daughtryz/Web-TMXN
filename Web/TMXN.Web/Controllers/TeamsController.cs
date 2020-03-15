using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Common.InputModels;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Teams;

namespace TMXN.Web.Controllers
{
    public class TeamsController : BaseController
    {
        private readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }
        public IActionResult ShowAll()
        {
            var viewModel = new TeamsListViewModel();
            var all = this.teamsService.GetAll<TeamViewModel>();
            viewModel.Teams = all;
            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }


        [HttpPost]
        public IActionResult Add(TeamInputModel model)
        {
            if(!this.ModelState.IsValid)
            {
                throw new Exception("Invalid model state!");
            }

            this.teamsService.AddAsync(model.Name, model.Logo, model.Tag);
            return this.Redirect("/Home/Index");
        }
    }
}
