using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Web.ViewModels.Tournaments
{
    public class TournamentsListViewModel
    {
        public IEnumerable<TournamentsViewModel> Tournaments { get; set; }
    }
}
