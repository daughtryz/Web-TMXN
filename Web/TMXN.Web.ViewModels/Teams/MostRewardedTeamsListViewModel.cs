using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Web.ViewModels.Teams
{
    public class MostRewardedTeamsListViewModel : Topic
    {
        public IEnumerable<MostRewardedTeamViewModel> Teams { get; set; }
    }
}
