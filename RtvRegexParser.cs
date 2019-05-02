using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace WebParser
{
    class RtvRegexParser
    {
        public static object Parse(string filename)
        {
            string txt = File.ReadAllText(filename);

            var title = new Regex("<h1>(.+?)</h1>").Match(txt);
            var subtitle = new Regex("<div class=\"subtitle\">(.+?)</div>").Match(txt);
            var lead = new Regex("<p class=\"lead\">(.+?)</p>").Match(txt);
            var content = new Regex("<article class=\"article\">([\\S\\s]*)</article>", RegexOptions.Multiline).Match(txt);
            var author = new Regex("<div class=\"author-name\">(.+?)</div>").Match(txt);
            var publishedTime = new Regex("<div class=\"author-timestamp\">[\\S\\s]*?</strong>\\|.(.+ob \\d\\d:\\d\\d)[\\S\\s]*?</div>").Match(txt);
            
            return new
            {
                Title = title.Groups[1].Value,
                Subtitle = subtitle.Groups[1].Value,
                Lead = lead.Groups[1].Value,
                Author = author.Groups[1].Value,
                PublishedTime = publishedTime.Groups[1].Value,
                Content = content.Groups[1].Value
            };
        }
    }
}
