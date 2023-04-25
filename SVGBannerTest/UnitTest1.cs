using NUnit.Framework.Internal;
using SVGBanner;
using System.Drawing;
using System.Net;
using System.Windows.Forms.Design.Behavior;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SVGBannerTest
{
    public class Tests
    {
        private XElement GlyphCharsToXDoc(GlyphChars glyphChars)
        {
            TextReader tr = new StringReader(glyphChars.SvgXml.ToString());
            XElement doc = XElement.Load(tr);

            return doc;

        }
        private bool CheckAttribute(XElement doc, XAttribute attribute)
        {
            throw new NotImplementedException();
        }


        [Test]
        public void TestGlyphChars()
        {
            var test = new GlyphChars("AbCd", "Montserrat-Regular", Color.FromName("Black"));
            Console.WriteLine(test.SvgXml);
        }

        [Test]
        public void TestGlyphCharsToXDoc()
        {
            var test = new GlyphChars("AbCd", "Montserrat-Regular", Color.FromName("Black"));

            Console.WriteLine( GlyphCharsToXDoc(test));
        }

        [Test]
        public void TestAddAttribute()
        {
            var test = new GlyphChars("AbCd", "Montserrat-Regular", Color.FromName("Black"));

            XElement doc = GlyphCharsToXDoc(test);

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

            doc.AddFirst(blur1 );

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
            var test = new GlyphChars("AbCd", "Montserrat-Regular", Color.FromName("Black"));

            XElement doc = GlyphCharsToXDoc(test);

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
        [Test]
        public void TestCheckAttribute()
        {
            var test = new GlyphChars("AbCd", "Montserrat-Regular", Color.FromName("Black"));

            XElement doc = GlyphCharsToXDoc(test);

            XElement xpath = doc.Element("path");


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
    }
}