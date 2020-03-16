using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Web.ViewModels.News
{
    public class NewsFeedsListViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }
    }
}
