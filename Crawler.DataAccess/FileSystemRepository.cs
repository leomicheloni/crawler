using Crawler.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DataAccess
{
    public class FileSystemRepository: IRepository<DoctorInfo>
    {
        public void SaveAll(IEnumerable<DoctorInfo> list)
        {
            var path = @"F:\temp\doctos_data.txt";
            var builder = new StringBuilder();
            foreach (var item in list)
            {
                builder.Append(item.ToString());
                builder.Append("=====================");
            }

            File.WriteAllText(path, builder.ToString());
        }
    }
}
