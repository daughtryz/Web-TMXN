using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Users
{
   public class UserFriendViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public virtual UserFriendlist UserFriendlist { get; set; }

    }
}
