using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMXN.Web.Controllers
{
    public class NewsController : BaseController
    {

        public IActionResult Info()
        {
            return this.View();
        }

    }
}
