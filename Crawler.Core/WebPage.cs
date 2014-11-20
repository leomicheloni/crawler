using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Net;

namespace Crawler.Core
{
    public abstract class WebPage
    {
        public Uri Url { get; set; }


        public static IEnumerable<string> GetMainLinks(HtmlDocument doc)
        {
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='findlist byname']/ul[@class='clearfix']/li/a");
            var links = new List<string>();
            foreach (var node in nodes)
            {
                var href = node.GetAttributeValue("href", "");
                links.Add(href);
            }

            return links;
        }

        public static HtmlNode GetDetail(HtmlNode detailNode)
        {
            
            // some xpaths to retrive detail information
            //doc-portrait foto
            //doc-info clearfix => caja de info
            //ul .doc-specialties especialidades
            //ul .address => address
            //ul .doc-ratings

            return null;
        }
        
        public static IEnumerable<WebPage> GetAllPagesUnder(Uri urlRoot)
        {
            var queue = new Queue<Uri>();
            var allSiteUrls = new HashSet<Uri>();

            queue.Enqueue(urlRoot);
            allSiteUrls.Add(urlRoot);

            while (queue.Count > 0)
            {
                Uri url = queue.Dequeue();

                HttpWebRequest oReq = (HttpWebRequest)WebRequest.Create(url);
                oReq.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

                HttpWebResponse resp = (HttpWebResponse)oReq.GetResponse();

                WebPage result;

                if (resp.ContentType.StartsWith("text/html", StringComparison.InvariantCultureIgnoreCase))
                {
                    HtmlDocument doc = new HtmlDocument();
                    try
                    {
                        var resultStream = resp.GetResponseStream();
                        doc.Load(resultStream); // The HtmlAgilityPack
                        result = new Internal() { Url = url, HtmlDocument = doc };
                    }
                    catch (System.Net.WebException ex)
                    {
                        result = new WebPage.Error() { Url = url, Exception = ex };
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add("Url", url);    // Annotate the exception with the Url
                        throw;
                    }

                    // Success, hand off the page
                    yield return new WebPage.Internal() { Url = url, HtmlDocument = doc };

                    // And and now queue up all the links on this page
                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes(@"//a[@href]"))
                    {
                        HtmlAttribute att = link.Attributes["href"];
                        if (att == null) continue;
                        string href = att.Value;
                        if (href.StartsWith("javascript", StringComparison.InvariantCultureIgnoreCase)) continue;      // ignore javascript on buttons using a tags

                        Uri urlNext = new Uri(href, UriKind.RelativeOrAbsolute);

                        // Make it absolute if it's relative
                        if (!urlNext.IsAbsoluteUri)
                        {
                            urlNext = new Uri(urlRoot, urlNext);
                        }

                        if (!allSiteUrls.Contains(urlNext))
                        {
                            allSiteUrls.Add(urlNext);               // keep track of every page we've handed off

                            if (urlRoot.IsBaseOf(urlNext))
                            {
                                queue.Enqueue(urlNext);
                            }
                            else
                            {
                                yield return new WebPage.External() { Url = urlNext };
                            }
                        }
                    }
                }
            }
        }

        public class Error : WebPage
        {
            public int HttpResult { get; set; }
            public Exception Exception { get; set; }
        }

        public class External : WebPage
        {
        }

        public class Internal : WebPage
        {
            public virtual HtmlDocument HtmlDocument { get; internal set; }
        }
    }
}