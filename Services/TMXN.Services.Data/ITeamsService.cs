using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TMXN.Services.Data
{
    public interface ITeamsService
    {

        public IEnumerable<T> GetAll<T>();

        public Task AddAsync(string name, string logo, string tag);
    }
}
