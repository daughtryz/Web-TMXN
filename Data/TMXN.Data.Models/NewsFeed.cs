using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class NewsFeed : BaseModel<string>,IDeletableEntity
    {

        public NewsFeed()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PlayerNewsFeeds = new HashSet<PlayerNewsFeed>();
        }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }

       
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }

        public virtual ICollection<PlayerNewsFeed> PlayerNewsFeeds { get; set; }
    }
}
