using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Common.Models;


namespace TMXN.Data.Models
{
    public class Tournament : BaseModel<int>, IDeletableEntity
    {

        public Tournament()
        {
           
        }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }


        [Required]
        public DateTime CreatedOn { get; set; }

        public string TeamId { get; set; }

        public virtual Team Team { get; set; }

        public bool IsDeleted { get  ; set ; }
        public DateTime? DeletedOn { get ; set ; }

        public string Organizer { get; set; }


        public TournamentGameType TournamentGameType { get; set; }

        public virtual ICollection<Bracket> Brackets { get; set; }

    }
}
