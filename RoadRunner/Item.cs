using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WebParser.RoadRunner
{
    class Item : Element
    {
        string tag;
        HtmlNode node;
        // TODO: change to set of elements
        //string attributes;

        public Item(Element parent, HtmlNode node) : base(parent)
        {
            this.node = node;
            tag = node.Name;

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                var child = node.ChildNodes[i];
                //Console.WriteLine(reg.Groups[1].Value.Trim());
                //Console.WriteLine(i);
                //Console.WriteLine(child.Name);
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

        public override bool Equal(Element e)
        {
            throw new NotImplementedException();
        }


        public override string ToString(int depth)
        {
            var reg = new Regex("(<[^<]*>)").Matches(node.OuterHtml);
            StringBuilder builder = new StringBuilder();
            builder.Append(AppendDepth(depth));
            builder.Append(reg[0].Groups[1].Value);
            builder.Append("\n");
            for (int i = 0; i < children.Count; i++)
            {
                builder.Append(children[i].ToString(depth + 1));
            }
            if (node.EndNode != null)
            {
                builder.Append(AppendDepth(depth));
                /*for (int i = 0; i < reg.Groups.Count; i++)
                {
                    Console.WriteLine(i + ", " + reg.Groups[i].Value);
                }*/
                builder.Append(reg[reg.Count-1].Groups[1].Value);
                builder.Append("\n");
            }

            return builder.ToString();
        }
    }
}
