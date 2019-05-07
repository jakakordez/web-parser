using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.RoadRunner
{
    class RoadRunner
    {
        public static string Parse(string filename1, string filename2)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(filename1);
            var root = doc.DocumentNode.FirstChild;
            Element rootElement = new Item(null, root);

            HtmlDocument doc1 = new HtmlDocument();
            doc1.Load(filename2);
            var root1 = doc1.DocumentNode.FirstChild;
            Element secondElement = new Item(null, root1);

            Generalize(rootElement, secondElement);

            return rootElement.ToString(0);
        }

        private static void Generalize(Element baseRoot, Element root2)
        {
            if (baseRoot is Iterator)
                baseRoot = baseRoot.children[0];

            if (baseRoot.Equal(root2))
            {
                int current2 = 0;
                for (int current1 = 0; current1 < baseRoot.children.Count; current1++)
                {
                    while (current2 < root2.children.Count && !baseRoot.children[current1].Equal(root2.children[current2]))
                    {
                        if (current1 > 0 && !(baseRoot.children[current1 - 1] is Optional) && baseRoot.children[current1-1].Equal(root2.children[current2]))
                        {
                            // prejšnji baseRoot je enak kot sedajšnji root2, to pomeni, da smo najdli iterator!! 
                            Generalize(baseRoot.children[current1 - 1], root2.children[current2]);
                            Iterator iterator = new Iterator(baseRoot, baseRoot.children[current1 - 1]);
                            baseRoot.children[current1-1] = iterator;
                            //current1 --;
                            while (current1 > 1 && baseRoot.children[current1-1].Equal(baseRoot.children[current1-2]))
                            {
                                Generalize(baseRoot.children[current1-1], baseRoot.children[current1-2]);
                                baseRoot.children.RemoveAt(current1-2);
                                current1 --;
                            }
                        }
                        else
                        {
                            Optional optional = new Optional(baseRoot, root2.children[current2]);
                            baseRoot.children.Insert(current1, optional);
                            // current1 se poveča samo zaradi tega, ker insertamo nov element v baseRoot
                            current1++;
                        }
                        current2++;
                    }
                    if (current2 >= root2.children.Count)
                    {
                        if (!(baseRoot.children[current1] is Optional))
                        {
                            // indekse iz base root zamenjamo z optional, saj jih ni v root2
                            Optional optional = new Optional(baseRoot, baseRoot.children[current1]);
                            baseRoot.children[current1] = optional;
                        }
                    }
                    else
                    {
                        if (baseRoot.children[current1].GetType() == typeof(Text))
                        {
                            Text t = baseRoot.children[current1] as Text;
                            t.Generalize(root2.children[current2]);
                        }
                        else if (baseRoot.children[current1].GetType() == typeof(Item))
                        {
                            ((Item)baseRoot.children[current1]).Generalize(root2.children[current2]);
                            Generalize(baseRoot.children[current1], root2.children[current2]);
                        }
                        //else if (baseRoot.children[current1].GetType() == typeof(Iterator))
                        //    current1 --;
                        current2 ++;
                    }
                }
                while (current2 < root2.children.Count)
                {
                    if (baseRoot.children.Count > 0 && !(baseRoot.children[baseRoot.children.Count-1] is Optional) && baseRoot.children[baseRoot.children.Count - 1].Equal(root2.children[current2]))
                    {
                        if (!(baseRoot.children[baseRoot.children.Count - 1] is Iterator))
                        {
                            // prejšnji baseRoot je enak kot sedajšnji root2, to pomeni, da smo najdli iterator!! 
                            Generalize(baseRoot.children[baseRoot.children.Count - 1], root2.children[current2]);
                            Iterator iterator = new Iterator(baseRoot, baseRoot.children[baseRoot.children.Count - 1]);
                            baseRoot.children[baseRoot.children.Count - 1] = iterator;
                            while (baseRoot.children.Count > 1 && baseRoot.children[baseRoot.children.Count - 1].Equal(baseRoot.children[baseRoot.children.Count - 2]))
                            {
                                Generalize(baseRoot.children[baseRoot.children.Count - 1], baseRoot.children[baseRoot.children.Count - 2]);
                                baseRoot.children.RemoveAt(baseRoot.children.Count - 2);
                            }
                        }
                    }
                    else
                    {
                        Element optional = root2.children[current2];
                        if (!(root2.children[current2] is Optional))
                        {
                            // v baseRoot dodamo nov optional, ki se je prikazal pri root2 in ne pri baseRoot
                            optional = new Optional(baseRoot, root2.children[current2]);
                        }
                        baseRoot.children.Add(optional);
                    }
                    current2++;
                }
            }
        }
    }
}
