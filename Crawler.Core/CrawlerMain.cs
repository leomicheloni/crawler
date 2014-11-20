
using System;
using System.Collections.Generic;
using System.Net;
namespace Crawler.Core
{
    public class CrawlerMain
    {

        public CrawlerMain()
        {

        }

        public IEnumerable<string> GetRootPage()
        {
            var page  = WebPage.LoadPage(new Uri("http://ae.doctoruna.com/en/doctors/a"));
            return WebPage.GetMainLinks(page);
        }
    }
}
