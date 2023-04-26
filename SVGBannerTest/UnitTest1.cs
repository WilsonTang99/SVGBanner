using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Svg.Transforms;
using SVGBanner;
using System;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms.Design.Behavior;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SVGBannerTest
{
    public class GlyphTest
    {
        public static XElement GlyphCharsToXDoc(GlyphChars glyphChars)
        {
            TextReader tr = new StringReader(glyphChars.svgXml.ToString());
            XElement doc = XElement.Load(tr);

            return doc;

        }

        /// <summary>
        /// Return all attributes of a path tag as a list of strings
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static IEnumerable<String> ListPathAttributes(XElement doc)
        {
            List<String> list = new List<String>();
            if (doc.Name == "path")
            {
                IEnumerable<XAttribute> attributes = 
                    from att in doc.Attributes()
                    select att;

                foreach (var att in attributes)
                    list.Add(att.Name.ToString());
            }
            return list;
        }

        /// <summary>
        /// return if path contains certain attribute
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="att"></param>
        /// <returns></returns>
        public static bool IsPathContainsAttribute(XElement doc, string att)
        {
            var list = ListPathAttributes(doc);

            return (list.Contains(att));        
        }

        /// <summary>
        /// remove specific attribute from path
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="att"></param>
        /// <returns></returns>
        public static XElement RemovePathAttribute(XElement doc, string att)
        {
            if (IsPathContainsAttribute(doc, att))
            {
                XAttribute temp = doc.Attribute(att);
                temp.Remove();
                return doc;
            }
            else return doc;
        }


        public static XElement SelectAllPaths(XElement doc)
        {
            throw new NotImplementedException();
        }

        public static bool CheckElement(XElement doc, string tag) 
        {
            throw new NotImplementedException ();
        }
    }

    public class Tests : GlyphTest
    {
        GlyphChars testXml = new GlyphChars("AbCd", "Montserrat-Regular", Color.FromName("Black"));

        [Test]
        public void TestGlyphChars()
        {
            Console.WriteLine(testXml.svgXml);
        }

        [Test]
        public void TestGlyphCharsToXDoc()
        {
             Console.WriteLine(GlyphTest.GlyphCharsToXDoc(testXml));
        }

        [Test]
        public void TestCheckAttribute()
        {
            XElement doc = GlyphTest.GlyphCharsToXDoc(testXml);

            IEnumerable<XElement> childElements =
                from el in doc.Elements()
                select el;

            foreach (var el in childElements)
            {
                el.SetAttributeValue("fill", "FFFFFF");
            }

            Console.WriteLine(doc.Attribute("width").Value);

            IEnumerable<XElement> path =
                from el in doc.Elements()
                where (string)el.Attribute("id") == "obj1"
                select el;
            foreach (XElement el in path)
                Console.WriteLine(el);
        }

        [Test]
        public void TestCheckAttribute2()
        {
            XElement doc = GlyphTest.GlyphCharsToXDoc(testXml);

            IEnumerable<XElement> childElements =
                from el in doc.Elements()
            select el;

            foreach (var el in childElements)
                Console.WriteLine ($"{el.Name} has Attribute is {el.HasAttributes}");
        }

        [Test]
        public void TestListPathAttribute()
        {
            XElement doc = GlyphTest.GlyphCharsToXDoc(testXml);
            var node1 = doc.Element("path");

            IEnumerable<XAttribute> attributes =
                from att in node1.Attributes()
                select att;

            foreach (var att in attributes)
                Console.WriteLine(att.Name.ToString());

            var attributes2 = ListPathAttributes(node1);
            foreach (var att in attributes2)
                Console.WriteLine(att.ToString());
        }

        [Test]
        public void TestIsPathContainsAttribute()
        {
            XElement doc = GlyphTest.GlyphCharsToXDoc(testXml);
            var node1 = doc.Element("path");

            Console.WriteLine(IsPathContainsAttribute(node1, "transform"));
            Console.WriteLine(IsPathContainsAttribute(node1, "stroke-width"));
        }

        [Test]
        public void TestRemovePathAttribute()
        {
            XElement doc = GlyphTest.GlyphCharsToXDoc(testXml);
            var node1 = doc.Element("path");

            node1 = RemovePathAttribute(node1, "stroke-width");

            Console.WriteLine(doc.ToString());
        }

        [Test]
        public void TestAddBlurFilter()
        {
            XElement doc = GlyphTest.GlyphCharsToXDoc(testXml);

            XElement xpath = doc.Element("path");

            XElement blur1 = new XElement("defs",
                            new XElement("filter")
                            );

            XElement el1 = blur1.Descendants().First();
            el1.Add(new XElement("feGaussianBlur"));

            el1.SetAttributeValue("id", "f1");
            el1.SetAttributeValue("x", "0");
            el1.SetAttributeValue("y", "0");

            XElement el2 = el1.Descendants().First();

            el2.SetAttributeValue("in", "SourceGraphic");
            el2.SetAttributeValue("stdDeviation", "15");

            doc.AddFirst(blur1);

            IEnumerable<XElement> childElements =
                from el in doc.Elements("path")
                select el;

            foreach (var el in childElements)
            {
                el.SetAttributeValue("filter", "url(#f1)");
            }

            Console.WriteLine(doc.ToString());
        }

        [Test]
        public void TestRemoveAttribute()
        {
            XElement doc = GlyphTest.GlyphCharsToXDoc(testXml);

            XElement xpath = doc.Element("path");

            XElement blur1 = new XElement("defs",
                            new XElement("filter")
                );

            XElement el1 = blur1.Descendants().First();
            el1.Add(new XElement("feGaussianBlur"));

            el1.SetAttributeValue("id", "f1");
            el1.SetAttributeValue("x", "0");
            el1.SetAttributeValue("y", "0");

            XElement el2 = el1.Descendants().First();

            el2.SetAttributeValue("in", "SourceGraphic");
            el2.SetAttributeValue("stdDeviation", "15");

            doc.AddFirst(blur1);

            IEnumerable<XElement> childElements =
                from el in doc.Elements("path")
                select el;

            foreach (var el in childElements)
            {
                el.SetAttributeValue("filter", "url(#f1)");
            }

            Console.WriteLine($"Added:\n {doc.ToString()}");

            blur1.Remove();

            foreach (var el in childElements)
            {
                XAttribute att = el.Attribute("filter");
                att.Remove();
            }

            Console.WriteLine($"Removed:\n {doc.ToString()}");


        }
    }
}