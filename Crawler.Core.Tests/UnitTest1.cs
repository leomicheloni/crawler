using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crawler.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var main = new Crawler.Core.CrawlerMain();
            foreach (var link in main.GetRootPage())
            {
                Console.WriteLine(link);
            }


        }
    }
}
