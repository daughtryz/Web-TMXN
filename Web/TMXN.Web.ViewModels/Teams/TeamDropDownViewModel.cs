using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Teams
{
    public class TeamDropDownViewModel : IMapFrom<Team>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
