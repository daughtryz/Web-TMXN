using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Brackets
{
    public class BracketViewModel : IMapFrom<Bracket>, IMapFrom<Tournament>
    {
        public string TournamentName { get; set; }

       
        public virtual ICollection<Team> Teams { get; set; }
    }
}
