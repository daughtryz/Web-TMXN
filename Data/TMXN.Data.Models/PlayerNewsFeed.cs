using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class PlayerNewsFeed : IDeletableEntity
    {

        public string PlayerId { get; set; }

        public Player Player { get; set; }

        

        public string NewsFeedId { get; set; }
        public NewsFeed NewsFeed { get; set; }


        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }
    }
}
