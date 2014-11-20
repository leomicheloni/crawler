using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public class PageRetriever : Crawler.Core.IPageRetriever
    {
        public HtmlDocument LoadPage(Uri url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            HtmlDocument doc = new HtmlDocument();
            
            var resultStream = resp.GetResponseStream();
            doc.Load(resultStream);
            return doc;
        }
    }
}
