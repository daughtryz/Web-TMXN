using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Teams
{
    public class RanklistTeamViewModel : IMapFrom<Team>
    {
        public string Id { get; set; }

        public int Points { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }
    }
}
