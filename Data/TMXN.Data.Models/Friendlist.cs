using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class Friendlist : BaseModel<int>, IDeletableEntity
    {

        public Friendlist()
        {
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }
    }
}
