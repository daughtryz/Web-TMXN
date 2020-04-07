﻿using Microsoft.AspNetCore.Http;
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
        public IFormFile LogoImage { get; set; }
        [Required]
        public string Tag { get; set; }
    }
}
