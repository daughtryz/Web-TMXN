using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class Tournament : BaseModel<int>, IDeletableEntity
    {

        public Tournament()
        {
            this.Teams = new HashSet<Team>();
        }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }


        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public bool IsDeleted { get  ; set ; }
        public DateTime? DeletedOn { get ; set ; }


    }
}
