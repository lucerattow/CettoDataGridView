namespace CettoDataGridView
{
    partial class TestView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cgrd = new CettoDataGridView();
            SuspendLayout();
            // 
            // cgrd
            // 
            cgrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cgrd.AutoScrollMargin = new Size(0, 0);
            cgrd.AutoScrollMinSize = new Size(0, 0);
            cgrd.BorderColor = Color.Black;
            cgrd.ForeColor = Color.Black;
            cgrd.GroupGap = 0;
            cgrd.Location = new Point(12, 76);
            cgrd.Name = "cgrd";
            cgrd.Padding = new Padding(10);
            cgrd.Size = new Size(1069, 509);
            cgrd.TabIndex = 0;
            // 
            // TestView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1093, 597);
            Controls.Add(cgrd);
            ForeColor = Color.White;
            Name = "TestView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private CettoDataGridView cgrd;
    }
}