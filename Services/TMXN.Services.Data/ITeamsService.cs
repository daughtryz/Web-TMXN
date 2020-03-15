using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Services.Data
{
    public interface ITeamsService
    {

        public IEnumerable<T> GetAll<T>();
    }
}
