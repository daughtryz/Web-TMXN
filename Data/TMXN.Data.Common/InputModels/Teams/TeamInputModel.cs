using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMXN.Data.Common.InputModels.Teams
{
    public class TeamInputModel
    {

        [Required]

        [MinLength(3)]

        [MaxLength(20)]
        public string Name { get; set; }

       
        [Required]

        [MaxLength(6)]
        public string Tag { get; set; }
      
        [Required]
        public IFormFile LogoImage { get; set; }
       
    }
}
