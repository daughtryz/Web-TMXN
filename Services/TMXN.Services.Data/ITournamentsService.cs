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

        public Task<IEnumerable<TViewModel>> GetAllTournamentTeamsAsync<TViewModel> (int tournamentId);

        public Task RemoveTeamFromTournamentAsync(int tournamentId,string userId);

       
    }
}
