
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

        public dynamic GetDetail(Uri detailUrl)
        {
            var page = this.pageRetriver.LoadPage(detailUrl);

            var specialities = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/ul[@class='address'][1]/li[1]/ul/li[1]/a");
            var name = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/h1");
            var practiceName = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/ul[@class='doc-specialties']/li[1]/ul/li/a");
            var address = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/ul[@class='address']");
            var rate = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/ul[@class='doc-ratings']/li[@class='not-rated']");

            return new
            {
                Name = name,
                PracticeName = practiceName,
                Specialities = specialities,
                Address = address,
                Rate = rate
            };
        }

        private string TryGetToken(HtmlAgilityPack.HtmlDocument page, string xpath)
        {
            var element = page.DocumentNode.SelectSingleNode(xpath);
            return element == null ? "" : element.InnerText;
        }
    }
}
