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
        [MinLength(2)]
        [MaxLength(20)]

        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Organizer { get; set; }

        [Required]
        public TournamentGameType TournamentGameType { get; set; }


    }
}
