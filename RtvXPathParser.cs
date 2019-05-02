using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HtmlAgilityPack;

namespace WebParser
{
    class RtvXPathParser
    {
        public static object Parse(string filename)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(filename);
            var root = doc.DocumentNode;

            return new
            {
                Title = root.SelectSingleNode("//h1").InnerText,
                Subtitle = root.SelectSingleNode("//div[@class='subtitle']").InnerText,
                Lead = root.SelectSingleNode("//p[@class='lead']").InnerText,
                Author = root.SelectSingleNode("//div[@class='author-timestamp']/strong").InnerText,
                PublishedTime = root.SelectSingleNode("//div[@class='author-timestamp']/strong/following-sibling::text()[1]").InnerText,
                Content = root.SelectSingleNode("//article[@class='article']").InnerHtml
            };
        }
    }
}
