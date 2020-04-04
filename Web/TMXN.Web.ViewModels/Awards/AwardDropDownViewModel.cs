using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Awards
{
    public class AwardDropDownViewModel : IMapFrom<Award>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
