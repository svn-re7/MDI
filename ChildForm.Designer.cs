namespace MDI
{
    partial class ChildForm
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
            pictureBox = new PictureBox();
            panelProgress = new Panel();
            pbFilterProgress = new ProgressBar();
            btnCancelFilter = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            panelProgress.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.White;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(800, 450);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            // 
            // panelProgress
            // 
            panelProgress.Controls.Add(pbFilterProgress);
            panelProgress.Controls.Add(btnCancelFilter);
            panelProgress.Dock = DockStyle.Bottom;
            panelProgress.Location = new Point(0, 400);
            panelProgress.Name = "panelProgress";
            panelProgress.Size = new Size(800, 50);
            panelProgress.TabIndex = 1;
            panelProgress.Visible = false;
            // 
            // pbFilterProgress
            // 
            pbFilterProgress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbFilterProgress.Location = new Point(12, 10);
            pbFilterProgress.Name = "pbFilterProgress";
            pbFilterProgress.Size = new Size(620, 30);
            pbFilterProgress.TabIndex = 0;
            // 
            // btnCancelFilter
            // 
            btnCancelFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelFilter.Location = new Point(638, 5);
            btnCancelFilter.Name = "btnCancelFilter";
            btnCancelFilter.Size = new Size(150, 40);
            btnCancelFilter.TabIndex = 1;
            btnCancelFilter.Text = "Отмена";
            btnCancelFilter.UseVisualStyleBackColor = true;
            // 
            // ChildForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 450);
            Controls.Add(panelProgress);
            Controls.Add(pictureBox);
            Name = "ChildForm";
            Text = "Документ";
            Activated += ChildForm_Enter;
            FormClosing += ChildForm_FormClosing;
            Enter += ChildForm_Enter;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            panelProgress.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public PictureBox pictureBox;
        public Panel panelProgress;
        public ProgressBar pbFilterProgress;
        public Button btnCancelFilter;
    }
}