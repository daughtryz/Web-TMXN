using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Web.ViewModels.Teams
{
    public class ListingTeamsByYearOfCreationViewModel : Topic
    {

        public IEnumerable<TeamByYearViewModel> Teams { get; set; }
        

    }
}
