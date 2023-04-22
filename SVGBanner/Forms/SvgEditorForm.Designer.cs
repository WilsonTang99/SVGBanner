namespace SVGBanner.Forms
{
    partial class SvgEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SvgXmlBox = new TextBox();
            SuspendLayout();
            // 
            // SvgXmlBox
            // 
            SvgXmlBox.Dock = DockStyle.Fill;
            SvgXmlBox.Location = new Point(0, 0);
            SvgXmlBox.Multiline = true;
            SvgXmlBox.Name = "SvgXmlBox";
            SvgXmlBox.Size = new Size(800, 450);
            SvgXmlBox.TabIndex = 0;
            SvgXmlBox.TextChanged += SvgXmlBox_TextChanged;
            // 
            // SvgEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SvgXmlBox);
            Name = "SvgEditor";
            Text = "SVG Editor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox SvgXmlBox;
    }
}