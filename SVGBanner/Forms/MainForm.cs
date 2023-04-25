using System.Windows.Forms;
using Svg;
using System.Diagnostics;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Point = System.Drawing.Point;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;
using SVGBanner.Forms;
using Svg.FilterEffects;
using static System.Windows.Forms.DataFormats;

namespace SVGBanner
{
    public partial class Mainform : Form
    {
        public FileHandlers? FileHandlers { get; }

        public SvgEditorForm svgEditor { get; set; } = new();

        public UserControls userControls { get; set; } = new();

        public static SvgDocument? svgDoc { get; set; }

        public static string? svgXml { get; set; }


        public Mainform()
        {
            InitializeComponent();
            comboBox1_initialize();

            userControls.Show();

            svgEditor.Show();
        }

        private void comboBox1_initialize()
        {
            this.comboBox1.DropDownWidth = 280;
            string directory = AppDomain.CurrentDomain.BaseDirectory + "\\fonts\\";
            string[] textFiles = System.IO.Directory.GetFiles(directory, "*.ttf");
            foreach (string file in textFiles)
            {
                // Remove the directory from the string
                string filename = file.Substring(file.LastIndexOf(@"\") + 1);
                // Remove the extension from the filename
                string name = filename.Substring(0, filename.LastIndexOf(@"."));
                // Add the name to the combo box
                this.comboBox1.Items.Add(name);
            }
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;

            //MessageBox.Show((string)comboBox1.SelectedItem);
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;

            //MessageBox.Show((string)comboBox2.SelectedItem);
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)

            {
                //MessageBox.Show((string)textBox1.Text);

                var temp = new GlyphChars(textBox1.Text, comboBox1.Text, textBox1.ForeColor);
                textBox2.Text = temp.SvgXml.ToString();


                var svgDoc = SvgDocument.FromSvg<SvgDocument>(temp.SvgXml);
                RenderSvg(svgDoc);
/*
                svgEditor.ChangeText(temp.SvgXml);
                svgXml = temp.SvgXml;
*/
            }

        }


        /// <summary>
        /// Code from SVG-net
        /// </summary>
        /// <param name="svgDoc"></param>
        public void RenderSvg(SvgDocument svgDoc)
        {
#if NET5_0_OR_GREATER
            if (OperatingSystem.IsWindows())
                svgImage.Image?.Dispose();
#else
            svgImage.Image?.Dispose();
#endif

            //using (var render = new DebugRenderer())
            //    svgDoc.Draw(render);
            if (svgDoc != null)
            {
                svgImage.Image = svgDoc.Draw();


                var baseUri = svgDoc.BaseUri;
                var outputDir = Path.GetDirectoryName(baseUri != null && baseUri.IsFile ? baseUri.LocalPath : Application.ExecutablePath);
#if NET5_0_OR_GREATER
                if (OperatingSystem.IsWindows())
                    svgImage.Image?.Save(Path.Combine(outputDir, "output.png"));
#else
            svgImage.Image?.Save(Path.Combine(outputDir, "output.png"));
#endif
                svgDoc.Write(Path.Combine(outputDir, "output.svg"));

            }
        }

        private void svgImage_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateMousePositionStatus();
        }


        /// <summary>
        /// Mouse Position
        /// </summary>
        /// <returns></returns>
        public Point MousePositionRelativeToPicture()
        {
            var pt = svgImage.PointToScreen(Point.Empty);
            return new Point(MousePosition.X - pt.X, MousePosition.Y - pt.Y);
        }

        public void UpdateMousePositionStatus()
        {
            var pt = MousePositionRelativeToPicture();
            statusLabelMouse.Text = $"X = {pt.X}, Y = {pt.Y}";
        }

        /// <summary>
        /// Grids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void showGridsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            svgImage.Invalidate();
        }
        private void showGridsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGridsToolStripMenuItem.Checked = !showGridsToolStripMenuItem.Checked;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void svgImage_Paint(object sender, PaintEventArgs e)
        {
            if (showGridsToolStripMenuItem.Checked)
            {
                int x, y;
                int w = svgImage.Size.Width;
                int h = svgImage.Size.Height;
                int inc = 50; // trackBar1.Value;

                Graphics gr = e.Graphics;
                if (showGridsToolStripMenuItem.Checked == true)
                {
                    for (x = 0; x < w; x += inc)
                        gr.DrawLine(Pens.Green, x, 0, x, h);

                    for (y = 0; y < h; y += inc)
                        gr.DrawLine(Pens.Green, 0, y, w, y);
                }
            }
        }

        /// <summary>
        /// dummy code for the hidden textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var svgDoc = SvgDocument.FromSvg<SvgDocument>(textBox2.Text);
                RenderSvg(svgDoc);
            }
            catch
            {
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.A)
                    (sender as TextBox).SelectAll();
            }
            catch
            {
            }
        }
        /// <summary>
        /// Implementaion of the clear button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // needs to throw everything to undo stack

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;

            // create empty instance
            var svgDoc = SvgDocument.FromSvg<SvgDocument>("<svg></svg>");
            RenderSvg(svgDoc);
        }

        private void ColourBtn_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = textBox1.ForeColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                textBox1.ForeColor = MyDialog.Color;
        }
    }
}