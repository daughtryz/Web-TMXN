using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class Player : BaseModel<string>, IDeletableEntity
    {
        public Player()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Awards = new HashSet<Award>();
        }
        [Required]
        [MaxLength(15)]
        public string PlayerUsername { get; set; }


        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }

        public Team Team { get; set; }

        public string TeamId { get; set; }
        public virtual ICollection<Award> Awards { get; set; }
    }
}
