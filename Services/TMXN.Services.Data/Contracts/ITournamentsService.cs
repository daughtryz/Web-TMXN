using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Enums;

namespace TMXN.Services.Data
{
    public interface ITournamentsService
    {
        public Task GenerateAsync(string name, string organizer,TournamentGameType tournamentType);

       public IEnumerable<TViewModel> All<TViewModel>();

        public Task ParticipateAsync(string userId, int tournamentId);

        public Task RemoveAsync(int id);

        public Task<int> RemoveTeamFromTournamentAsync(int tournamentId,string userId);


        public TViewModel Info<TViewModel>(int id);

        public Task EditAsync(string name, string organizer, TournamentGameType TournamentGameType, int tournamentId);


    }
}
