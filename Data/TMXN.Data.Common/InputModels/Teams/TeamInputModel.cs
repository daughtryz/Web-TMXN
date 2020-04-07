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
        public string Name { get; set; }
        [Required]
        public IFormFile Logo { get; set; }
        [Required]
        public string Tag { get; set; }
    }
}
