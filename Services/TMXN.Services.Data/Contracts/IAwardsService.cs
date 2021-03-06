﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Common.Repositories;

namespace TMXN.Services.Data
{
    public interface IAwardsService
    {
        public Task<IEnumerable<TViewModel>> GetAll<TViewModel>();

        public Task CreateAsync(string name, PlacingType placingType);

        public Task RemoveAsync(string id);


        public Task EditAsync(string name, PlacingType placingType, string id);

        public TViewModel GetById<TViewModel>(string id);

    }
}
