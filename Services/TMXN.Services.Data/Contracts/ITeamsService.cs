﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Models;

namespace TMXN.Services.Data
{
    public interface ITeamsService
    {

        public IEnumerable<TViewModel> GetAll<TViewModel>(string criteria = null);

        public Task AddAsync(string name, IFormFile logo, string tag,ApplicationUser user);


        public TViewModel GetInfo<TViewModel>(string teamId);

        public Task RemoveAsync(string id);
        
        public IEnumerable<TViewModel> GetRanklist<TViewModel>();


        public Task<int> WinAsync(string teamId);

        public Task<int> LoseAsync(string teamId);

        public Task SendAwardAsync(string teamId,string awardId);

        public Task EditAsync(string name, IFormFile logo, string tag,string teamId);

        public Task RemovePlayerAsync(ApplicationUser user);

    }
}
