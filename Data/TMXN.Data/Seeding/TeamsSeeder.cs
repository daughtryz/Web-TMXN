using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Models;

namespace TMXN.Data.Seeding
{
    public class TeamsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if(dbContext.Teams.Any())
            {
                return;
            }

            var teams = new List<Team>();

            teams.Add(new Team
            {
                Name = "TeamExtinction",
                Logo = "TMXN",
                Tag = "TMXN",

            });

            teams.Add(new Team
            {
                Name = "Fnatic",
                Logo = "FNC",
                Tag = "FNC",

            });
            teams.Add(new Team
            {
                Name = "Solo-Mid",
                Logo = "TSM",
                Tag = "TSM",

            });


            foreach (var team in teams)
            {
               await dbContext.Teams.AddAsync(new Team
                {
                    Name = team.Name,
                    Logo = team.Logo,
                    Tag = team.Tag,
                    
                });

            }


        }
    }
}
