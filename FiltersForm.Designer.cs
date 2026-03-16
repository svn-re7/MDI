namespace MDI
{
    partial class FiltersForm
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
            dgvPlugins = new DataGridView();
            btnSelectAll = new Button();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPlugins).BeginInit();
            SuspendLayout();
            // 
            // dgvPlugins
            // 
            dgvPlugins.AllowUserToAddRows = false;
            dgvPlugins.AllowUserToDeleteRows = false;
            dgvPlugins.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPlugins.BackgroundColor = SystemColors.ControlDark;
            dgvPlugins.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlugins.Location = new Point(12, 12);
            dgvPlugins.Name = "dgvPlugins";
            dgvPlugins.RowHeadersWidth = 62;
            dgvPlugins.Size = new Size(776, 350);
            dgvPlugins.TabIndex = 0;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSelectAll.Location = new Point(12, 380);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(150, 45);
            btnSelectAll.TabIndex = 1;
            btnSelectAll.Text = "Выбрать все";
            btnSelectAll.UseVisualStyleBackColor = true;
            btnSelectAll.Click += BtnSelectAll_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(638, 380);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 45);
            btnSave.TabIndex = 2;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // FiltersForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(btnSelectAll);
            Controls.Add(dgvPlugins);
            Name = "FiltersForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FiltersForm";
            ((System.ComponentModel.ISupportInitialize)dgvPlugins).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dgvPlugins;
        private Button btnSelectAll;
        private Button btnSave;
    }
}
