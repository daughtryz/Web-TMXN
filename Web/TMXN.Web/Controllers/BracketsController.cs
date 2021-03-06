﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Brackets;
using TMXN.Web.ViewModels.Teams;
using TMXN.Web.ViewModels.Tournaments;

namespace TMXN.Web.Controllers
{
    public class BracketsController : BaseController
    {
        private readonly IBracketsService bracketsService;
        

        public BracketsController(IBracketsService bracketsService)
        {
            this.bracketsService = bracketsService;
            
        }

        public IActionResult All()
        {
            var viewModel = new BracketListViewModel
            {
                Brackets = this.bracketsService.GetAll<BracketViewModel>(),
            };
            return this.View(viewModel);
        }
       

    }
}
