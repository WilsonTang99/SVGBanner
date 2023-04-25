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
        public void TestCheckAttribute()
        {
            var test = new GlyphChars("AbCd", "Montserrat-Regular", Color.FromName("Black"));

            XElement doc = GlyphCharsToXDoc(test);

            XElement xpath = doc.Element("path");


            IEnumerable<XElement> childElements =
                from el in doc.Elements()
                select el;
                        
            foreach (XElement el in childElements)
                Console.WriteLine("Name: " + el.Name);


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