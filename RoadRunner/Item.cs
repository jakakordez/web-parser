using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebParser.RoadRunner
{
    class Item : Element
    {
        string tag;
        string endTag;
        HtmlNode node;
        // TODO: change to set of elements
        Dictionary<string, Element> attributes;
        //string attributes;

        public Item(Element parent, HtmlNode node) : base(parent)
        {
            this.node = node;
            attributes = new Dictionary<string, Element>();

            for (int i = 0; i < node.Attributes.Count; i++)
            {
                Attribute tmp = new Attribute(this, node.Attributes[i].Name, node.Attributes[i].Value);
                if (!attributes.TryAdd(node.Attributes[i].Name, tmp))
                    tmp.StringMismatch();
            }

            var reg = new Regex("(<[^<]*>)").Matches(node.OuterHtml);
            tag = reg[0].Groups[1].Value.Trim();
            if (node.ChildNodes.Count > 0)
            {
                endTag = reg[reg.Count - 1].Groups[1].Value.Trim();
            }

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                var child = node.ChildNodes[i];
                if (child.Name.Contains("text"))
                {
                    string text = child.InnerText.Trim();
                    if (text == "")
                    {
                        //Console.WriteLine("NULL TEXT");
                    }
                    else
                    {
                        //Console.WriteLine(text);
                        children.Add(new Text(this, text));
                    }
                }
                else
                {
                    children.Add(new Item(this, child));
                }
                //Console.WriteLine(child.InnerHtml);
            }
        }

        public override bool EqualRecursive(Element e)
        {
            if (!Equal(e)) return false;
            int ie = 0;
            for (int i = 0; i < children.Count; i++)
            {
                if (ie >= e.children.Count)
                    return false;

                if (children[i] is Optional)
                {
                    if (children[i].EqualRecursive(e.children[ie]))
                        ie++;
                    continue;
                }
                if (!children[i].EqualRecursive(e.children[ie]))
                    return false;

                ie++;

                if (children[i] is Iterator)
                {
                    ie++;
                    while (ie < e.children.Count && children[i].EqualRecursive(e.children[ie]))
                        ie++;
                    ie--;
                }
            }
            return true;
        }

        public void Generalize(Element e)
        {
            if (!Equal(e))
                throw new Exception("[Attribute] error in Generalize: element is not the same as this");

            while (!(e is Text) && !(e is Item))
                e = e.children[0];

            Item item = e as Item;
            foreach (string key in attributes.Keys.ToList())
            {
                Element value = attributes[key];
                if (value is Optional)
                    continue;
                Attribute current = value as Attribute;
                Element tmp;
                if (item.attributes.TryGetValue(key, out tmp))
                {
                    current.Generalize(tmp);
                }
                else
                {
                    Optional optional = new Optional(this, current);
                    attributes[key] = optional;
                }
            }
            foreach (KeyValuePair<string, Element> entry in item.attributes)
            {
                Element current = entry.Value as Element;
                if (!attributes.ContainsKey(entry.Key))
                {
                    if (!(current is Optional))
                        current = new Optional(this, current);
                    attributes.Add(entry.Key, current);
                }
            }
        }

        public override bool Equal(Element e)
        {
            while (!(e is Text) && !(e is Item))
                e = e.children[0];
            Item el = e as Item;
            return el != null && el.node.Name == node.Name;
        }

        public override string ToString(int depth)
        {
            
            StringBuilder builder = new StringBuilder();
            builder.Append(AppendDepth(depth));
            //builder.Append(tag);
            builder.Append("<");
            builder.Append(node.Name);
            foreach (KeyValuePair<string, Element> entry in attributes)
            {
                builder.Append(entry.Value.ToString(0));
            }
            if (endTag == null)
                builder.Append("/");
            builder.Append(">\n");
            for (int i = 0; i < children.Count; i++)
            {
                builder.Append(children[i].ToString(depth + 1));
            }
            if (endTag != null)
            {
                builder.Append(AppendDepth(depth));
                /*for (int i = 0; i < reg.Groups.Count; i++)
                {
                    Console.WriteLine(i + ", " + reg.Groups[i].Value);
                }*/
                builder.Append(endTag);
                builder.Append("\n");
            }

            return builder.ToString();
        }
    }
}
