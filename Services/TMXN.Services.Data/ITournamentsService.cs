using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TMXN.Services.Data
{
    public interface ITournamentsService
    {
        public Task GenerateAsync(string name, string organizer);

       public IEnumerable<T> All<T>();

        public Task ParticipateAsync(string userId, int tournamentId);

        public Task<IEnumerable<TViewModel>> GetAllTournamentTeamsAsync<TViewModel> (int tournamentId);
       
        
    }
}
