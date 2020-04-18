using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.News
{
    public class NewsViewModel : IMapFrom<NewsFeed>
    {

        public string Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);
        public string ImageUrl { get; set; }


    }
}
