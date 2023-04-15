using System.Windows.Forms;
using Svg;
using System.Diagnostics;

using Point = System.Drawing.Point;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SVGBanner
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();

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
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void showGridsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void showGridsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showGridsToolStripMenuItem.Checked)
                showGridsToolStripMenuItem.Checked = false;
            else showGridsToolStripMenuItem.Checked = true;
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