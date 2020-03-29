using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class Award : BaseModel<string>, IDeletableEntity
    {

        public Award()
        {
            this.Id = Guid.NewGuid().ToString();

        }

        [Required]
       
        public string Name { get; set; }

        [Required]
        public PlacingType PlacingType { get; set; }
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }

        public string TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
