using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TMXN.Services.Data
{
    public interface ITournamentsService
    {
        public Task GenerateAsync(string name, string organizer, string userId);

       public IEnumerable<T> All<T>();
    }
}
