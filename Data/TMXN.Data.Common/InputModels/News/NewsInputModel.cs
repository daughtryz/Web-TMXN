using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMXN.Data.Common.InputModels.News
{
    public class NewsInputModel
    {

      
        [Required]
        public string Title { get; set; }
       
        [Required]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Upload)]

        public IFormFile Image { get; set; }
    }
}
