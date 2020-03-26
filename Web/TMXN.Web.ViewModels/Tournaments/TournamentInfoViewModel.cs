﻿using System;
using System.Collections.Generic;
using System.Text;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Web.ViewModels.Tournaments
{
    public class TournamentInfoViewModel 
    {

        public IEnumerable<TournamentTeamInfoViewModel> EnrolledTeams { get; set; }


    }
}
