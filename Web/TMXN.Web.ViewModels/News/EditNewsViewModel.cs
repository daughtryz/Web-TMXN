using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.News
{
    public class EditNewsViewModel : IMapFrom<NewsFeed>
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Content { get; set; }

    }
}
