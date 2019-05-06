using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner
{
    class Iterator : Element
    {
        public Iterator(Element parent, Element e) : base(parent)
        {
            e.parent = this;
            children.Add(e);
        }

        public override bool Equal(Element e)
        {
            Iterator el = e as Iterator;
            if (el == null)
            {
                return children[0].Equal(e);
            }
            return el != null && children[0].Equal(el.children[0]);
        }

        public override bool EqualRecursive(Element e)
        {
            Iterator el = e as Iterator;
            if (el == null)
            {
                return children[0].EqualRecursive(e);
            }
            return Equal(e) && children[0].EqualRecursive(e.children[0]);
        }

        public override string ToString(int depth)
        {
            return AppendDepth(depth) + "(\n" + children[0].ToString(depth + 1) + AppendDepth(depth) + ") +\n";
        }
    }
}
