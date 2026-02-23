namespace MDI
{
    partial class CanvasSizeForm
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
            Label1 = new Label();
            Label2 = new Label();
            numWidth = new NumericUpDown();
            numHeight = new NumericUpDown();
            btnOK = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(45, 49);
            Label1.Name = "Label1";
            Label1.Size = new Size(97, 30);
            Label1.TabIndex = 0;
            Label1.Text = "Ширина:";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(358, 49);
            Label2.Name = "Label2";
            Label2.Size = new Size(79, 30);
            Label2.TabIndex = 1;
            Label2.Text = "Длина:";
            // 
            // numWidth
            // 
            numWidth.Location = new Point(148, 49);
            numWidth.Maximum = new decimal(new int[] { 2500, 0, 0, 0 });
            numWidth.Minimum = new decimal(new int[] { 210, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(132, 35);
            numWidth.TabIndex = 2;
            numWidth.Value = new decimal(new int[] { 210, 0, 0, 0 });
            // 
            // numHeight
            // 
            numHeight.Location = new Point(443, 47);
            numHeight.Maximum = new decimal(new int[] { 2500, 0, 0, 0 });
            numHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(132, 35);
            numHeight.TabIndex = 3;
            numHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnOK
            // 
            btnOK.Location = new Point(358, 192);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(131, 40);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(555, 192);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(131, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // CanvasSizeForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(735, 255);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(numHeight);
            Controls.Add(numWidth);
            Controls.Add(Label2);
            Controls.Add(Label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CanvasSizeForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Размер холста";
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Label1;
        private Label Label2;
        private NumericUpDown numWidth;
        private NumericUpDown numHeight;
        private Button btnOK;
        private Button btnCancel;
    }
}