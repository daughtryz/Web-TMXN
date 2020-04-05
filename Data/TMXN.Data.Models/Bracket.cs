using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Common.Models;

namespace TMXN.Data.Models
{
    public class Bracket : BaseModel<string>, IDeletableEntity
    {
        public Bracket()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

    }
}
