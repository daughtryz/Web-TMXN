using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Data.Models
{
    public class TournamentTeam
    {
        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }


        public string TeamId { get; set; }


        public virtual Team Team { get; set; }

    }
}
