using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner
{
    class RoadRunner
    {
        public static string Parse(string filename1, string filename2)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(filename1);
            var root = doc.DocumentNode.FirstChild;
            Element rootElement = new Item(null, root);

            return rootElement.ToString(0);
        }
    }
}
