using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Common.InputModels.Enums;

namespace TMXN.Data.Common.InputModels.Awards
{
    public class AwardInputModel
    {

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public PlacingType PlacingType { get; set; }

    }
}
