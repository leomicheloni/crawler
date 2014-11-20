using System;
namespace Crawler.Core
{
    public interface IPageRetriever
    {
        HtmlAgilityPack.HtmlDocument LoadPage(Uri url);
    }
}
