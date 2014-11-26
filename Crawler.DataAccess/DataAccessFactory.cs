using Crawler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DataAccess
{
    public class DataAccessFactory
    {
        public IRepository<DoctorInfo> Build()
        {
            return new FileSystemRepository();
        }
    }
}
