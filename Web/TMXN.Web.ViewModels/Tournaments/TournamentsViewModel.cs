﻿using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Tournaments
{
    public class TournamentsViewModel : IMapFrom<Tournament>, IMapFrom<Team>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Organizer { get; set; }

        public string TeamId { get; set; }

        public bool IsFinished { get; set; }

        public bool IsEliminate { get; set; }
    }
}
