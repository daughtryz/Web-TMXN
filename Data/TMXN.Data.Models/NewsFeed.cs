using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class NewsFeed : BaseModel<string>, IDeletableEntity
    {
        public NewsFeed()
        {
            this.Id = Guid.NewGuid().ToString();

        }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public DateTime Date { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
