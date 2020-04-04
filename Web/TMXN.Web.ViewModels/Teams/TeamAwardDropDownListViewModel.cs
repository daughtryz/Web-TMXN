using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Web.ViewModels.Awards;

namespace TMXN.Web.ViewModels.Teams
{
    public class TeamAwardDropDownListViewModel 
    {
        public string TeamId { get; set; }
        public IEnumerable<TeamDropDownViewModel> Teams { get; set; }
        public string AwardId { get; set; }
        public IEnumerable<AwardDropDownViewModel> Awards { get; set; }
    }
}
