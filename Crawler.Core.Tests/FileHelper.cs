using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core.Tests
{
    public static class FileHelper
    {
        static string path = @"..\..\page samples\"; //improve this

        public static string GetFile(string name)
        {
            return File.ReadAllText(path + name);
        }
    }
}
