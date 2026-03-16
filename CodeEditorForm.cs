using System;
using System.Windows.Forms;
using PluginInterface;

namespace MDI
{
    public partial class CodeEditorForm : Form
    {
        // объект плагина после компиляции
        public IPlugin? CompiledPlugin { get; private set; }

        public CodeEditorForm()
        {
            InitializeComponent();
            // заполнение редактора шаблоном
            txtCode.Text = GetTemplate();
        }

        // шаблон кода плагина
        private string GetTemplate()
        {
            return @"using System;
using System.Drawing;
using System.Threading;
using PluginInterface;

namespace DynamicPlugin
{
    public class CustomFilter : IPlugin
    {
        // имя фильтра в меню
        public string Name => ""Пользовательский фильтр"";
        public string Author => ""Пользователь"";

        public void Transform(Bitmap bitmap, IProgress<int> progress, CancellationToken ct)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            for (int y = 0; y < height; y++)
            {
                // проверка отмены
                if (ct.IsCancellationRequested) return;

                for (int x = 0; x < width; x++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    
                    // инверсия цвета
                    Color newColor = Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
                    
                    bitmap.SetPixel(x, y, newColor);
                }

                // отчет о прогрессе
                progress.Report((int)((double)y / height * 100));
            }
        }
    }
}";
        }

        // запуск компиляции кода
        private void btnRun_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Компиляция...";
            lblStatus.ForeColor = Color.Blue;
            Application.DoEvents(); 

            string errors;
            // вызов компилятора
            var plugin = PluginCompiler.Compile(txtCode.Text, out errors);

            if (plugin != null)
            {
                // сохранение плагина и закрытие окна
                CompiledPlugin = plugin;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                // вывод ошибок пользователю
                lblStatus.Text = "Ошибка компиляции!";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show(errors, "Ошибки компиляции", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
