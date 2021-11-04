using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Web.ViewModels.Tournaments
{
    public class ListingLatestTournamentsViewModel : Topic
    {
        public IEnumerable<LatestTournamentViewModel> LatestTournaments { get; set; }

    }
}
