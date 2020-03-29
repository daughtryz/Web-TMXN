using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Awards;

namespace TMXN.Web.Controllers
{
    public class AwardsController : BaseController
    {

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(AwardInputModel input)
        {
            return this.View();
        }
    }
}
