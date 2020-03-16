using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Models;

namespace TMXN.Data.Seeding
{
    public class NewsFeedsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.NewsFeeds.Any())
            {
                return;
            }

            var newsFeeds = new List<NewsFeed>();

            newsFeeds.Add(new NewsFeed
            {
                Title = "TMXN remained second in the rank list",
                ImageUrl = "bla",
                Content = "Yesterday TMXN didn't succeed in defeating the raining champions MAD Lions and remained second in the Clash tournament!",
            });

            newsFeeds.Add(new NewsFeed
            {
                Title = "WORLD CHAMPIONS - FNC",
                ImageUrl = "bla",
                Content = "Fnatic is your two times world champion after beating SKT in some even games!",
            });

            newsFeeds.Add(new NewsFeed
            {
                Title = "EAGLES - OUT OF THE TOURNAMENT",
                ImageUrl = "bla",
                Content = "Some players of the league team Eagles made some mistakes that cost them the participation in the clash tournament!",
            });


            newsFeeds.Add(new NewsFeed
            {
                Title = "WildCard teams",
                ImageUrl = "bla",
                Content = "We have very good news since league of legends wildcard teams can participate in our tournaments again!",
            });
            foreach (var newsFeed in newsFeeds)
            {
                await dbContext.NewsFeeds.AddAsync(new NewsFeed
                {
                    Title = newsFeed.Title,
                    ImageUrl = newsFeed.ImageUrl,
                    Content = newsFeed.Content,

                });

            }
        }
    }
}
