﻿using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Tournaments
{
    public class TournamentsViewModel : IMapFrom<Tournament>
    {

        public string Name { get; set; }

        public string Organizer { get; set; }

        

    }
}