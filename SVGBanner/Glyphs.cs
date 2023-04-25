using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WaterTrans.GlyphLoader;

namespace SVGBanner
{
    public class GlyphChars : XDocument
    {
        public string SvgXml { get; }

        public GlyphChars(string str, string? font, Color color)
        {
            var fontpath = !(font.Length == 0)? "fonts/"+font+".ttf" : "fonts/Montserrat-Regular.ttf";
            string fontPath = System.IO.Path.Combine(Environment.CurrentDirectory, fontpath);
            using var fontStream = System.IO.File.OpenRead(fontPath);
            // Initialize stream only
            Typeface tf = new Typeface(fontStream);

            var svg = new System.Text.StringBuilder();
            double unit = 100;
            double x = 20;
            double y = 20;
            var id = 0;
            string text = str;
            string hexColor = (ColorTranslator.ToHtml(color) == "windowtext") ? "#000000" : ColorTranslator.ToHtml(color);

            svg.AppendLine($"<svg width='{str.Length*85}' height='200' viewBox='0 0 {str.Length * 85} 200'>");
                            // xmlns='http://www.w3.org/2000/svg' version='1.1'>");

            foreach (char c in text)
            {
                // Get glyph index
                ushort glyphIndex = tf.CharacterToGlyphMap[(int)c];

                // Get glyph outline
                var geometry = tf.GetGlyphOutline(glyphIndex, unit);

                // Get advanced width
                double advanceWidth = tf.AdvanceWidths[glyphIndex] * unit;

                // Get advanced height
                double advanceHeight = tf.AdvanceHeights[glyphIndex] * unit;

                // Get baseline
                double baseline = tf.Baseline * unit;

                // Convert to path mini-language
                string miniLanguage = geometry.Figures.ToString(x, y + baseline);

                svg.AppendLine($"<path id='obj{id}' d='{miniLanguage}' fill='{hexColor}' stroke='{hexColor}' stroke-width='0' />");
                svg.AppendLine();
                x += advanceWidth;
                id++;
            }

            svg.AppendLine("</svg>");

            SvgXml = svg.ToString();
            //Console.WriteLine(SvgXml);
        }

        public XElement GlyphCharsToXDoc (GlyphChars glyphChars)
        {
            TextReader tr = new StringReader(glyphChars.SvgXml.ToString());
            XElement doc = XElement.Load(tr);

            return doc;
        }

        public bool CheckAttribute(string attribute, GlyphChars glyphChars)
        {
            throw new NotImplementedException();
        }

        public GlyphChars AddAttribute(string attribute, GlyphChars glyphChars)
        {
            throw new NotImplementedException();
        }

        public GlyphChars RemoveAttribute(string attribute, GlyphChars glyphChars)
        {
            throw new NotImplementedException();
        }


    }
}
