using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Tournaments
{
    public class TournamentInfoViewModel : IMapFrom<Tournament>, IMapFrom<Team>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public string Organizer { get; set; }

        public DateTime CreatedOn { get; set; }

        public TournamentGameType TournamentGameType { get; set; }

        public string TeamId { get; set; }
        public Team Team { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
