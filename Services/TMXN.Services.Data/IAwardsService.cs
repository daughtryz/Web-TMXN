using System;
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

        public Task Create(string name, PlacingType placingType);

    }
}
