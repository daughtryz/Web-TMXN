using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Web.ViewModels.Teams;
using TMXN.Web.ViewModels.Tournaments;

namespace TMXN.Web.ViewModels.Brackets
{
    public class BracketDropdownViewModel
    {
        public int TournamentId { get; set; }
        public IEnumerable<TournamentDropdownViewModel> Tournaments { get; set; }
        public string TeamId { get; set; }
        public IEnumerable<TeamDropDownViewModel> Teams { get; set; }
    }
}
