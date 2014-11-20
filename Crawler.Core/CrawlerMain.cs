
using System;
using System.Net;
namespace Crawler.Core
{
    public class CrawlerMain
    {

        public CrawlerMain()
        {
            
        }

        public void GetRootPage()
        {
            foreach(var page in WebPage.GetAllPagesUnder(new Uri("http://ae.doctoruna.com/en/doctors/a"))){
                Console.WriteLine( page.Url);
            }
            
            
            
        }

    }


}
