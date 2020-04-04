using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Services.Mapping;

namespace TMXN.Data.Common.InputModels.Tournaments
{
    public class TournamentsInputModel 
    {
        [Required]

        public string Name { get; set; }

        [Required]
        public string Organizer { get; set; }

        [Required]
        public TournamentGameType TournamentGameType { get; set; }


    }
}
