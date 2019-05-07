using System;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = args[0];
            var mode = args[1];

            string response;
            if (mode == "roadrunner")
            {
                var filename2 = args[2];
                response = ParseRoadRunner(filename2, filename);
            }
            else
            {
                var obj = Parse(filename, mode);
                response = JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            Console.WriteLine(response);
            Console.Read();
        }

        static string ParseRoadRunner(string filename1, string filename2)
        {
            return RoadRunner.RoadRunner.Parse(filename1, filename2);
        }

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
            }
            return null;
        }
    }
}
