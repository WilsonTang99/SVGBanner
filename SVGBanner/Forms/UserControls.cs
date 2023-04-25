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
            XDocument temp = XDocument.Parse(Mainform.svgXml);

            int dx = 6;
            int dy = 8;
            XAttribute translate = new XAttribute("transform", "translate(30,40)");
            translate.SetValue($"translate({dx},{dy})");

            //XmlNodeList nodeList;

            var root = temp.Root;
            var nodeList = root.Nodes();

            foreach (XElement node in nodeList)
            {
                if (node.Attribute("transform") != null)
                {
                    MessageBox.Show(node.Attribute("transform").Value);
                }
                else node.Add(translate);
                
            }

            var svgDoc = SvgDocument.FromSvg<SvgDocument>(root.ToString());
            Mainform.svgDoc = svgDoc;
            //SvgXmlBoxText = nodeList.ToString();
            //MessageBox.Show(nodeList.ToString());
        }
    }
}
