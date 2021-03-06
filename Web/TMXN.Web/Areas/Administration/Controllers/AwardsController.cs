﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Awards;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Awards;

namespace TMXN.Web.Areas.Administration.Controllers
{
    public class AwardsController : AdministrationController
    {

        private readonly IAwardsService awardsService;

        public AwardsController(IAwardsService awardsService)
        {
            this.awardsService = awardsService;
        }

        
       [HttpGet]
        public IActionResult Edit(string id)
        {
            var viewModel = this.awardsService.GetById<AwardEditViewModel>(id);
            return this.View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AwardEditViewModel model)
        {
            await this.awardsService.EditAsync(model.Name, model.PlacingType, model.Id);

            return this.RedirectToAction("GetAll", "Awards", new { area = "Administration" });
        }


        [HttpGet("/Administration/Awards/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var viewModel = new AwardListViewModel
            {
                Awards = await this.awardsService.GetAll<AwardViewModel>(),
            };
            return this.View(viewModel);
        }
        public IActionResult Transfer(string id)
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
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            await this.awardsService.CreateAsync(input.Name, input.PlacingType);
           
            return this.RedirectToAction("GetAll", "Awards", new { area = "Administration" });
        }


        public async Task<IActionResult> Remove(string id)
        {
            await this.awardsService.RemoveAsync(id);
           
            return this.RedirectToAction("GetAll", "Awards", new { area = "Administration" });
        }
    }
}
