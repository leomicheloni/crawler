
using System;
using System.Collections.Generic;
using System.Net;
namespace Crawler.Core
{
    public class CrawlerMain
    {
        IPageRetriever pageRetriver;

        public CrawlerMain()
        {
            this.pageRetriver = new PageRetriever();
        }

        public CrawlerMain(IPageRetriever pageRetriver)
        {
            this.pageRetriver = pageRetriver;
        }

        public IEnumerable<string> GetDetailLinks()
        {
            var page  = this.pageRetriver.LoadPage(new Uri("http://ae.doctoruna.com/en/doctors/a"));
            return WebPage.GetMainLinks(page);
        }
    }
}
