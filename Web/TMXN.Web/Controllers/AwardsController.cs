using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Awards;
using TMXN.Services.Data;

namespace TMXN.Web.Controllers
{
    public class AwardsController : BaseController
    {
        private readonly IAwardsService awardsService;

        public AwardsController(IAwardsService awardsService)
        {
            this.awardsService = awardsService;
        }
        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AwardInputModel input)
        {
            if(!this.ModelState.IsValid)
            {
                throw new Exception("Invalid award input model!");
            }
            await this.awardsService.CreateAsync(input.Name, input.PlacingType);
            return this.RedirectToAction(nameof(SuccessCreate));
        }

        public IActionResult SuccessCreate()
        {
            return this.View();
        }
    }
}
