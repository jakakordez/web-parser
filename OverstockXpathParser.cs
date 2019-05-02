using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace WebParser
{
    class OverstockXpathParser
    {
        public static object Parse(string filename)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(filename);
            var root = doc.DocumentNode;

            var nodes = root.SelectNodes("//td/b/text()[. = 'You Save:']/../../../../../../../../../..");

            List<object> result = new List<object>();

            foreach (var node in nodes)
            {
                var yousave = node.SelectSingleNode("table//td/b/text()[. = 'You Save:']/../../..//span").InnerHtml;
                result.Add(new
                {
                    Title = node.SelectSingleNode("a/b").InnerText,
                    ListPrice = node.SelectSingleNode("table//td/b/text()[. = 'List Price:']/../../..//s").InnerHtml,
                    Price = node.SelectSingleNode("table//td/b/text()[. = 'Price:']/../../..//span/b").InnerHtml,
                    Content = node.SelectSingleNode("table//td/span[@class='normal']").InnerHtml,
                    Saving = yousave.Split(' ')[0],
                    SavingPercent = yousave.Split(' ')[1]
                });
            }

            return result.ToArray();
        }
    }
}
