﻿
using Crawler.Entities;
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

        public void CrawlAll()
        {
            var doctors = new List<DoctorInfo>();
            var repo = new DataAccess.DataAccessFactory().Build();
            //page a for now
            var links = this.GetDetailLinks();
            foreach (var link in links)
            {
                doctors.Add(this.GetDetail(new Uri(link)));
                
            }

            repo.SaveAll(doctors);

        }
        
        public IEnumerable<string> GetDetailLinks()
        {
            var page  = this.pageRetriver.LoadPage(new Uri("http://ae.doctoruna.com/en/doctors/Z"));
            return WebPage.GetMainLinks(page);
        }

        public DoctorInfo GetDetail(Uri detailUrl)
        {
            var page = this.pageRetriver.LoadPage(detailUrl);

            var specialities = TryGetTokenMultiple(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/ul[@class='doc-specialties']/li[2]/ul/li");
            var name = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/h1");
            var practiceName = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/ul[@class='doc-specialties']/li[1]/ul/li/a");
            var address = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/ul[@class='address'][last()]");
            //var rate = TryGetToken(page, "/html[@class='ltr']/body/div[@class='wrap']/div[@id='wrapsite']/div[@class='container content with-breadcrumbs clearfix']/div[@class='doc-profile clearfix']/div[@class='doc-info clearfix']/ul[@class='doc-ratings']/li/ul/span");
            var image = TryGetToken(page, "/html//a[@id='photo1']/img/@src", "src");

            return new DoctorInfo
            {
                Name = this.Sanitize(name),
                PracticeName = practiceName,
                Specialities = this.ProcessSpecialities(specialities),
                Address = address,
                //Rate = rate,
                Image = this.Sanitize(image)
            };
        }

        private string Sanitize(string origin)
        {
            return origin.Trim();
        }
        
        private IEnumerable<string> ProcessSpecialities(IEnumerable<string> source)
        {
            var list = new List<string>(source);
            for (var i = 0; i < list.Count - 2; i++)
            {
                yield return this.Sanitize(list[i]);
            }
        }

        private IEnumerable<string> TryGetTokenMultiple(HtmlAgilityPack.HtmlDocument page, string xpath)
        {
            var elements = page.DocumentNode.SelectNodes(xpath);
            if (elements == null) yield break;
            foreach (var element in elements)
            {
                yield return element == null ? "" : element.InnerText;
            }
        }

        private string TryGetToken(HtmlAgilityPack.HtmlDocument page, string xpath)
        {
            var element = page.DocumentNode.SelectSingleNode(xpath);
            return element == null ? "" : element.InnerText;
        }

        private string TryGetToken(HtmlAgilityPack.HtmlDocument page, string xpath, string attributeName)
        {
            var node = page.DocumentNode.SelectSingleNode(xpath);
            if(node == null) return "";
            return node.GetAttributeValue(attributeName,"");
        }

    }
}
