namespace MDI
{
    partial class CodeEditorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtCode = new RichTextBox();
            btnRun = new Button();
            btnCancel = new Button();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // txtCode
            // 
            txtCode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCode.Font = new Font("Consolas", 10F);
            txtCode.Location = new Point(12, 12);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(776, 380);
            txtCode.TabIndex = 0;
            txtCode.Text = "";
            // 
            // btnRun
            // 
            btnRun.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRun.Location = new Point(632, 398);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(156, 40);
            btnRun.TabIndex = 1;
            btnRun.Text = "Компилировать";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(470, 398);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(156, 40);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(12, 403);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 30);
            lblStatus.TabIndex = 3;
            // 
            // CodeEditorForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblStatus);
            Controls.Add(btnCancel);
            Controls.Add(btnRun);
            Controls.Add(txtCode);
            Name = "CodeEditorForm";
            Text = "Редактор плагина";
            ResumeLayout(false);
            PerformLayout();
        }

        private RichTextBox txtCode;
        private Button btnRun;
        private Button btnCancel;
        private Label lblStatus;
    }
}
