using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Data.Models
{
    public class UserFriend
    {
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string UserFriendlistId { get; set; }

        public virtual UserFriendlist UserFriendlist { get; set; }
    }
}
