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


namespace SVGBanner
{
    public partial class Mainform : Form
    {
        public FileHandlers FileHandlers { get; }

        public GlyphChar glyphChar { get; }

        public Mainform()
        {
            InitializeComponent();
            comboBox1_initialize();
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

            MessageBox.Show((string)comboBox1.SelectedItem);
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;

            MessageBox.Show((string)comboBox2.SelectedItem);
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)

            {
                MessageBox.Show((string)textBox1.Text);
                // return (textBox1.Text);
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateMousePositionStatus();

            //UpdateShape();
        }


        /// <summary>
        /// Mouse Position
        /// </summary>
        /// <returns></returns>
        public Point MousePositionRelativeToPicture()
        {
            var pt = pictureBox1.PointToScreen(Point.Empty);
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
            pictureBox1.Invalidate();
        }
        private void showGridsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGridsToolStripMenuItem.Checked = !showGridsToolStripMenuItem.Checked;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (showGridsToolStripMenuItem.Checked)
            {
                int x, y;
                int w = pictureBox1.Size.Width;
                int h = pictureBox1.Size.Height;
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


    }
}