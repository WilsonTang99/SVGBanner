using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Svg;

namespace SVGBanner.Forms
{
    public delegate void PassValueHandler2(string strValue);
    public partial class UserControls : Form
    {
        public event PassValueHandler2 PassValue;

        public UserControls()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            TextReader tr = new StringReader(Mainform.svgXml);
            XElement doc = XElement.Load(tr);

            IEnumerable<XElement> childElements =
               from el in doc.Elements()
               select el;

            foreach (XElement el in childElements)
                Console.WriteLine("Name: " + el.Name);

            int dx = 6;
            int dy = 8;
            XAttribute translate = new XAttribute("transform", "translate(30,40)");
            translate.SetValue($"translate({dx},{dy})");




        }
    }
}
