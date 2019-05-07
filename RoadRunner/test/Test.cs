using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner.test
{
    class Test
    {
        static void testMain(string[] args)
        {
            string neki = RoadRunner.Parse("../../../RoadRunner/test/page11.html", "../../../RoadRunner/test/page12.html");
            Console.WriteLine(neki);
            Console.Read();
        }
    }
}
