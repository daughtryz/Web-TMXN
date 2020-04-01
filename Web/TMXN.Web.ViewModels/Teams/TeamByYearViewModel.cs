using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Teams
{
    public class TeamByYearViewModel : IMapFrom<Team>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Logo { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Points { get; set; }
    }
}
