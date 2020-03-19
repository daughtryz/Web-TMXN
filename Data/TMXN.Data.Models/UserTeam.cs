using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
   public class UserTeam : IDeletableEntity
    {

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string TeamId { get; set; }
        public Team Team { get; set; }
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }
    }
}
