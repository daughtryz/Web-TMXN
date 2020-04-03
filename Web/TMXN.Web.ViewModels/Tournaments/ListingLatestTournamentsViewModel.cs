using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Web.ViewModels.Tournaments
{
    public class ListingLatestTournamentsViewModel
    {
        public IEnumerable<LatestTournamentViewModel> LatestTournaments { get; set; }

    }
}
