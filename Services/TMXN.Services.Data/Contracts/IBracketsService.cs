﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TMXN.Services.Data
{
    public interface IBracketsService
    {
        public IEnumerable<TViewModel> GetAll<TViewModel>();

        public Task CreateAsync(int tournamentId,string teamId);
    }
}