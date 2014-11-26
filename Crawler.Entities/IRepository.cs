using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Entities
{
    public interface IRepository<T>
    {
        void SaveAll(IEnumerable<T> list);
    }
}
