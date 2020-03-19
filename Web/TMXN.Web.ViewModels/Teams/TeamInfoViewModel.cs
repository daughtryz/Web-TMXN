using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Teams
{
   public class TeamInfoViewModel : IMapFrom<Team>
    {

        public string Name { get; set; }
        public string Logo { get; set; }

        public int AwardsCount { get; set; }

        public ICollection<Tournament> Tournaments { get; set; }

    }
}
