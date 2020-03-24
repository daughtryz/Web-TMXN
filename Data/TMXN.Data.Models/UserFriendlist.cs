using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class UserFriendlist : BaseModel<string>, IDeletableEntity
    {
        public UserFriendlist()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public bool IsDeleted { get; set ; }
        public DateTime? DeletedOn { get ; set ; }


    }
}
