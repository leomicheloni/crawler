
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
            var url = "http://ae.doctoruna.com/en/doctors/a";
            HttpWebRequest oReq = (HttpWebRequest)WebRequest.Create(url);
            oReq.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

            HttpWebResponse resp = (HttpWebResponse)oReq.GetResponse();

            var doc = new HtmlAgilityPack.HtmlDocument();
            

            try
            {
                var resultStream = resp.GetResponseStream();
                doc.Load(resultStream); // The HtmlAgilityPack
                //result = new Internal() { Url = url, HtmlDocument = doc };
            }
            catch (System.Net.WebException ex)
            {
               // result = new WebPage.Error() { Url = url, Exception = ex };
            }
            catch (Exception ex)
            {
                ex.Data.Add("Url", url);    // Annotate the exception with the Url
                throw;
            }

        }

    }


}
