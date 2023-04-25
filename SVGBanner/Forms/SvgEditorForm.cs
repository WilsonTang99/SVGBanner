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
using SVGBanner;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace SVGBanner.Forms
{
/*    public delegate void PassValueHandler(string strValue);*/

    public partial class SvgEditorForm : Form
    {
/*        public event PassValueHandler PassValue;*/

        public string SvgXmlBoxText { get; set; }

        public SvgEditorForm()
        {
            InitializeComponent();
            //userControls.PassValue += new PassValueHandler(userControls_PassValue);
        }

        public void ChangeText(string text)
        {
            if (SvgXmlBoxText != null)
            {
                SvgXmlBox.Clear();
                SvgXmlBox.Text = text;
            }
        }

        public void SvgXmlBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
/*                if (PassValue != null)
                {
                    PassValue(SvgXmlBox.Text);
                }*/
            }
            catch
            {
            }
        }
    }
}
