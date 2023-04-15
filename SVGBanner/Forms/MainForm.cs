using System.Windows.Forms;
using Svg;
using System.Diagnostics;

using Point = System.Drawing.Point;

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

        private void statusLabelMouse_Click(object sender, EventArgs e)
        {

        }
    }
}