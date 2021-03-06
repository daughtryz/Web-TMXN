﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMXN.Models.InputModels.AdminFolder.Teams
{
    public class TeamInputModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Logo { get; set; }
        [Required]
        public string Tag { get; set; }
    }
}
