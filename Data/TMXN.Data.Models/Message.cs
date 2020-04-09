using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class Message : BaseModel<string>, IDeletableEntity
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
       
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set; }

        public DateTime When { get; set; }
    }
}
