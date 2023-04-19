using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterTrans.GlyphLoader;

namespace SVGBanner
{
    public class GlyphChar : Svg.SvgPath
    {
        public string SvgXml { get; }
        public GlyphChar(string str)
        {
            string fontPath = System.IO.Path.Combine(Environment.CurrentDirectory, "fonts/Montserrat-Regular.ttf");
            using var fontStream = System.IO.File.OpenRead(fontPath);
            // Initialize stream only
            Typeface tf = new Typeface(fontStream);

            var svg = new System.Text.StringBuilder();
            double unit = 100;
            double x = 20;
            double y = 20;
            string text = str;
            svg.AppendLine("<svg width='440' height='140' viewBox='0 0 440 140' xmlns='http://www.w3.org/2000/svg' version='1.1'>");

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

                svg.AppendLine($"<path d='{miniLanguage}' fill='#46DBC4' stroke='#46DBC4' stroke-width='0' />");
                x += advanceWidth;
            }

            svg.AppendLine("</svg>");

            SvgXml = svg.ToString();
            //Console.WriteLine(SvgXml);
        }
    }
}
