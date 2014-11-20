using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Crawler.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetLinks()
        {
            var mockedRetriver = new Moq.Mock<IPageRetriever>();

           mockedRetriver.Setup(m=> m.LoadPage(Moq.It.IsAny<Uri>())).Returns(()=>{
               var doc = new HtmlAgilityPack.HtmlDocument();
               var file = FileHelper.GetFile("alphabetical_a.html");
               doc.LoadHtml(file);
               return doc;
           });

            var main = new Crawler.Core.CrawlerMain(mockedRetriver.Object);
            var links = main.GetDetailLinks();

            Assert.IsTrue((new List<string>(links)).Count == 91);
        }

        [TestMethod]
        public void GetDetail()
        {
            var mockedRetriver = new Moq.Mock<IPageRetriever>();

            mockedRetriver.Setup(m => m.LoadPage(Moq.It.IsAny<Uri>())).Returns(() =>
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                var file = FileHelper.GetFile("detail.html");
                doc.LoadHtml(file);
                return doc;
            });

            var main = new Crawler.Core.CrawlerMain(mockedRetriver.Object);
            var doctorInfo = main.GetDetail(new Uri("http://something.com")) ;

            
        }

    }
}
