using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner
{
    class Text : Element
    {
        string text;

        public Text(Element parent, string text) : base(parent)
        {
            this.text = text;
        }

        public override bool Equal(Element e)
        {
            throw new NotImplementedException();
        }

        public override string ToString(int depth)
        {
            return AppendDepth(depth+1) + text + "\n";
        }
    }
}
