using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static MDI.MainForm;

namespace MDI
{
    public partial class FiltersForm : Form
    {
        private List<PluginItem> _plugins;
        private AppConfig _config;

        public FiltersForm(List<PluginItem> plugins, AppConfig config)
        {
            InitializeComponent();
            _plugins = plugins;
            _config = config;
            chkAutoMode.Checked = _config.AutoMode;
            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvPlugins.AutoGenerateColumns = false;
            
            // название
            dgvPlugins.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Название",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // автор
            dgvPlugins.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Author",
                HeaderText = "Автор",
                ReadOnly = true,
                Width = 150
            });

            // версия
            dgvPlugins.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Version",
                HeaderText = "Версия",
                ReadOnly = true,
                Width = 80
            });

            // выбрать (Checkbox)
            dgvPlugins.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsEnabled",
                HeaderText = "Выбрать",
                Width = 80
            });

            dgvPlugins.DataSource = _plugins;
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in _plugins)
            {
                item.IsEnabled = true;
            }
            dgvPlugins.Refresh();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _config.AutoMode = chkAutoMode.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
