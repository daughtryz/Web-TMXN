namespace TMXN.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using TMXN.Data.Common.Models;

    public class Team : BaseDeletableModel<string>
    {

        public Team()
        {
            this.Id = Guid.NewGuid().ToString();

            this.ApplicationUsers = new HashSet<ApplicationUser>();
            this.CreatedOn = DateTime.UtcNow;
            this.Awards = new HashSet<Award>();
            this.Tournaments = new HashSet<Tournament>();
            this.IsWinner = false;
            this.IsEliminate = false;
        }
       

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        
        public string Logo { get; set; }
        [Required]
        [MaxLength(6)]
        public string Tag { get; set; }

        public int Points { get; set; }


        public bool IsWinner { get; set; }


        public bool IsEliminate { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Award> Awards { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
