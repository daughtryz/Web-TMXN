using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMXN.Web.CustomAttributes
{
    public class ListingViewComponentsAttribute : Attribute
    {
        public ListingViewComponentsAttribute(string header, string footer)
        {
            this.Header = header;
            this.Footer = footer;
        }
        public string Header { get; set; }
        public string Footer { get; set; }
    }
}
