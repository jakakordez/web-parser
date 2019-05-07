using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner
{
    class Text : Element
    {
        string text;
        bool string_mismatch;

        public Text(Element parent, string text) : base(parent)
        {
            this.text = text;
            string_mismatch = false;
        }

        private void StringMismatch()
        {
            string_mismatch = true;
            text = "#text";
        }

        public void Generalize(Element e)
        {
            if (!Equal(e))
                throw new Exception("[Text] error in Generalize: element is not the same as this");

            Text t = e as Text;
            if (this.text != t.text)
                StringMismatch();
        }

        public override bool Equal(Element e)
        {
            while (!(e is Text) && !(e is Item))
                e = e.children[0];
            return e is Text;
        }

        public override string ToString(int depth)
        {
            return AppendDepth(depth) + text + "\n";
        }

        public override bool EqualRecursive(Element e)
        {
            return Equal(e);
        }
    }
}
