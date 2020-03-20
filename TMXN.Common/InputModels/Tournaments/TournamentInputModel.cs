using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMXN.Common.InputModels.Tournaments
{
    public class TournamentInputModel 
    {

        [Required]
       
        public string Name { get; set; }

        [Required]
        public string  Organizer { get; set; }



    }
}
