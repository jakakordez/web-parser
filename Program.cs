using System;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebParser
{
    class Program
    {
        /*static void Main(string[] args)
        {
            var filename = args[0];
            var mode = args[1];

            var obj = Parse(filename, mode);
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            Console.WriteLine(json);
            Console.Read();
        }*/

        static object Parse(string filename, string mode)
        {
            switch (mode)
            {
                case "regex":
                    if (filename.Contains("rtvslo")) return RtvRegexParser.Parse(filename);
                    else if (filename.Contains("overstock")) return OverstockRegexParser.Parse(filename);
                    else if (filename.Contains("twitter")) return TwitterRegexParser.Parse(filename);
                    break;
                case "xpath":
                    if (filename.Contains("rtvslo")) return RtvXPathParser.Parse(filename);
                    else if (filename.Contains("overstock")) return OverstockXpathParser.Parse(filename);
                    else if (filename.Contains("twitter")) return TwitterXpathParser.Parse(filename);
                    break;
                case "roadrunner":
                    break;
            }
            return null;
        }
    }
}
