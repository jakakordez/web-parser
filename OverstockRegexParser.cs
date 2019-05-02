using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace WebParser
{
    class OverstockRegexParser
    {
        public static object Parse(string filename)
        {
            string txt = File.ReadAllText(filename);

            var firstRegex = new Regex("<b>(.+?)</b></a><br>[\\s\\S]*?List Price:</b></td.+?><s>(.+?)</s>", RegexOptions.Multiline);
            var priceRegex = new Regex("Price:.+?bigred\"><b>(.+?)</b>", RegexOptions.Multiline);
            var secondRegex = new Regex("<span class=\"littleorange\">(.+?) (\\(.+?%\\))</span></td></tr>[\\s\\S]*?</tbody></table>[\\s\\S]*?</td><td.+?><span.+?>([\\s\\S]*?)</span></a></span><br>",
                RegexOptions.Multiline);

            var firstMatch = firstRegex.Matches(txt);
            var priceMatch = priceRegex.Matches(txt);
            var secondMatch = secondRegex.Matches(txt);

            object[] result = new object[firstMatch.Count];
            for (int i = 0; i < firstMatch.Count; i++)
            {
                result[i] = new
                {
                    Title = firstMatch[i].Groups[1].Value,
                    ListPrice = firstMatch[i].Groups[2].Value,
                    Price = priceMatch[i].Groups[1].Value,
                    Saving = secondMatch[i].Groups[1].Value,
                    SavingPercent = secondMatch[i].Groups[2].Value,
                    Content = secondMatch[i].Groups[3].Value
                };
            }
            return result;
        }
    }
}
