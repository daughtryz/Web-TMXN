using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class Ranklist : BaseModel<string>, IDeletableEntity
    {

        public virtual ICollection<Team> Teams { get; set; }
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }
    }
}
