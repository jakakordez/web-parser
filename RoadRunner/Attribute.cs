using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner
{
    class Attribute : Element
    {
        string name;
        string value;
        bool string_mismatch;

        public Attribute(Element parent, string name, string value) : base(parent)
        {
            this.name = name;
            this.value = value;
            string_mismatch = false;
        }

        public void StringMismatch()
        {
            string_mismatch = true;
            value = "#text";
        }

        public void Generalize(Element e)
        {
            if (!Equal(e))
                throw new Exception("[Attribute] error in Generalize: element is not the same as this");

            if (e is Optional)
                e = e.children[0];
            Attribute t = e as Attribute;
            if (this.value != t.value)
                StringMismatch();
        }

        public override bool Equal(Element e)
        {
            if (e is Optional)
                e = e.children[0];
            Attribute att =  e as Attribute;
            return att != null && name == att.name;
        }

        public override string ToString(int depth)
        {
            return  " " + name + "=\"" + value + "\"";
        }

        public override bool EqualRecursive(Element e)
        {
            return Equal(e);
        }
    }
}
