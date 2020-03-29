using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Tournaments
{
    public class TournamentConfirmationOutViewModel : IMapFrom<Tournament>
    {

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
