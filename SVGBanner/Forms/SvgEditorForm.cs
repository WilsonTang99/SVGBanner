using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svg;
using TextBox = System.Windows.Forms.TextBox;


namespace SVGBanner.Forms
{
    public partial class SvgEditorForm : Form
    {
        public SvgEditorForm()
        {
            InitializeComponent();
        }

        private void SvgXmlBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var svgDoc = SvgDocument.FromSvg<SvgDocument>(SvgXmlBox.Text);
                Mainform.svgDoc = svgDoc;
            }
            catch
            {
            }
        }
    }
}
