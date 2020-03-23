using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.News
{
    public class EditNewsViewModel : IMapFrom<NewsFeed>
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Content { get; set; }

    }
}
