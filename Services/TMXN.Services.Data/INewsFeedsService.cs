using System;
using System.Collections.Generic;
using System.Text;

namespace TMXN.Services.Data
{
    public interface INewsFeedsService
    {

        IEnumerable<T> GetAll<T>();
    }
}
