using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.InputModels
{
    public class TeamAwardInputModel 
    {
        [Required]
        public ICollection<Award> Awards { get; set; }

        [Required]
        public ICollection<Team> Teams { get; set; }

    }
}
