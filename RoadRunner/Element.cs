using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner
{
    abstract class Element
    {
        public Element parent;
        public List<Element> children;

        public Element(Element parent)
        {
            this.parent = parent;
            this.children = new List<Element>();
        }

        public string AppendDepth (int depth)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < depth; i++)
            {
                sb.Append("    ");
            }
            return sb.ToString();
        }

        public abstract string ToString(int depth);
        public abstract bool Equal(Element e);
        public abstract bool EqualRecursive(Element e);
    }
}
