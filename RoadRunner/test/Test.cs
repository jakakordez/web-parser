using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner.test
{
    class Test
    {
        static void aMain(string[] args)
        {
            string neki = RoadRunner.Parse("../../../RoadRunner/test/page1.html", "../../../RoadRunner/test/page2.html");
            Console.WriteLine(neki);
            Console.Read();
        }
    }
}
