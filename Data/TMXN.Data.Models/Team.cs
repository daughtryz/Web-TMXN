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
            this.Users = new HashSet<User>();
            this.CreatedOn = DateTime.UtcNow;
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }
        [Required]
        [MaxLength(6)]
        public string Tag { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        

        public ICollection<User> Users { get; set; }
    }
}
