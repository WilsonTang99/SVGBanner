using SVGBanner;

namespace SVGBannerTest
{
    public class Tests
    {

        [Test]
        public void Test1()
        {
            var test = new GlyphChar("AbCd");
            Console.WriteLine(test.SvgXml);
        }
    }
}