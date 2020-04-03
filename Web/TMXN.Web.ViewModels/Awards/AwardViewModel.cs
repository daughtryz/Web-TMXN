using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Awards
{
   public class AwardViewModel : IMapFrom<Award>
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public PlacingType PlacingType { get; set; }
    }
}
