using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Models;

namespace TMXN.Services.Data
{
    public interface ITeamsService
    {

        public IEnumerable<TViewModel> GetAll<TViewModel>();

        public Task AddAsync(string name, string logo, string tag,string userId);


        public TViewModel GetInfo<TViewModel>(string teamId);

        public Task RemoveAsync(string id);
        
        public IEnumerable<TViewModel> GetRanklist<TViewModel>();


        public Task WinAsync(string teamId);

        public Task LoseAsync(string teamId);

        public Task SendAwardAsync(string teamId,string awardId);

    }
}
