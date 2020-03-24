using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Web.ViewModels.Users
{
    public class UserListViewModel 
    {

        public IEnumerable<UserFriendViewModel> Users { get; set; }
    }
}
