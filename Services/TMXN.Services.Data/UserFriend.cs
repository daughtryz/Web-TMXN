using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;

namespace TMXN.Services.Data
{
    public class UserFriend
    {
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string UserFriendlistId { get; set; }

        public virtual UserFriendlist UserFriendlist { get; set; }
    }
}
