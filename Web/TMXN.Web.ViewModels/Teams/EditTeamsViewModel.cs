using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Teams
{
    public class EditTeamsViewModel : IMapFrom<Team>
    {
        public string Id { get; set; }

        [Required]
        public IFormFile LogoImage { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Tag { get; set; }
    }
}
