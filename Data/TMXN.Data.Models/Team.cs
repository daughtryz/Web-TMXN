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
        }
       

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }
        [Required]
        [MaxLength(6)]
        public string Tag { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public ICollection<Award> Awards { get; set; }
    }
}
