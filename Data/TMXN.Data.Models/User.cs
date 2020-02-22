namespace TMXN.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser<string>
    {
        public Team Team { get; set; }

        public string TeamId { get; set; }
    }
}
