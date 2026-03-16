using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using PluginInterface;

namespace MDI
{
    public partial class MainForm : Form
    {
        private Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        private List<PluginSettings> pluginSettings = new List<PluginSettings>();
        
        public class PluginSettings
        {
            public string Name { get; set; } = "";
            public bool IsEnabled { get; set; } = true;
        }

        public class AppConfig
        {
            public bool AutoMode { get; set; } = false;
            public List<PluginSettings> Plugins { get; set; } = new List<PluginSettings>();
        }

        private AppConfig currentConfig = new AppConfig();

        public class PluginItem
        {
            public IPlugin Plugin { get; set; }
            public string Name => Plugin.Name;
            public string Author => Plugin.Author;
            public string Version { get; set; } = "1.0";
            public bool IsEnabled { get; set; } = true;

            public PluginItem(IPlugin plugin)
            {
                Plugin = plugin;
            }
        }

        private List<PluginItem> allPlugins = new List<PluginItem>();

        public static Color CurrentColor = Color.Black; // цвет пера
        public static int CurrentWidth = 3; // толщина пера
        public static bool IsFilled = false; // по умолчанию рисуем без заливки
        public static Font? CurrentFont;
        public static int StarPoints = 5;  // количество лучей звезды
        public static double StarRatio = 0.4;      // отношение внутреннего радиуса к внешнему

        // счетчики для звезды
        private ToolStripControlHost? hostStarPoints;
        private ToolStripLabel? lblStarPoints;
        private ToolStripControlHost? hostStarRatio;
        private ToolStripLabel? lblStarRatio;
        public enum DrawingTool { Pen, Line, Ellipse, Eraser, ZoomIn, ZoomOut, Text, Bucket, Star } // список констант - инструменты

        public static DrawingTool CurrentTool = DrawingTool.Pen; // текущий инструмент - перо


        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
            FindPlugins();      // Нашли dll
            CreatePluginsMenu(); // Создали кнопки
            DoubleBuffered = true;

            // счетчик для выбора толщины кисти
            NumericUpDown numCurrentWidth = new NumericUpDown(); // создаем объект счетчик
            numCurrentWidth.Minimum = 1;
            numCurrentWidth.Maximum = 50;
            numCurrentWidth.Value = 3;
            numCurrentWidth.Width = 50;

            numCurrentWidth.ValueChanged += Num_ValueChanged; // подписываем Num_ValueChanged на изименение счетчика

            ToolStripControlHost hostCurrentWidth = new ToolStripControlHost(numCurrentWidth); // обертка для счетчика чтобы положить его в ToolStrip

            ToolStripLabel lblCurrentWidth = new ToolStripLabel("Толщина:");
            toolStrip1.Items.Add(lblCurrentWidth);
            lblCurrentWidth.ToolTipText = "Толщина пера (от 1 до 50)";
            toolStrip1.Items.Add(hostCurrentWidth);

            // счетчик для выбора кол-ва лучей в звезде
            NumericUpDown numStarPoints = new NumericUpDown();
            numStarPoints.Minimum = 3;
            numStarPoints.Maximum = 30;
            numStarPoints.Value = StarPoints;
            numStarPoints.Width = 50;
            numStarPoints.ValueChanged += NumStarPoints_ValueChanged;

            hostStarPoints = new ToolStripControlHost(numStarPoints);
            lblStarPoints = new ToolStripLabel("Лучи:");
            lblStarPoints.ToolTipText = "Количество лучей звезды";

            // счетчик для отношения радиусов
            NumericUpDown numStarRatio = new NumericUpDown();
            numStarRatio.Minimum = 2;    // будем использовать целые значения потом делить на 10
            numStarRatio.Maximum = 8;
            numStarRatio.Value = (int)(StarRatio * 10);
            numStarRatio.Increment = 1;
            numStarRatio.Width = 50;
            numStarRatio.ValueChanged += NumStarRatio_ValueChanged;

            hostStarRatio = new ToolStripControlHost(numStarRatio);
            lblStarRatio = new ToolStripLabel("Отношение радиусов:");
            lblStarRatio.ToolTipText = "Отношение внутреннего радиуса к внешнему (0.2–0.8)";

            // добавляем на панель
            toolStrip1.Items.Add(lblStarPoints);
            toolStrip1.Items.Add(hostStarPoints);
            toolStrip1.Items.Add(lblStarRatio);
            toolStrip1.Items.Add(hostStarRatio);

            // скрываем по умолчанию
            hostStarPoints.Visible = false;
            lblStarPoints.Visible = false;
            hostStarRatio.Visible = false;
            lblStarRatio.Visible = false;

        }

        private void Num_ValueChanged(object? sender, EventArgs e) // изменение толщины
        {
            if (sender is NumericUpDown n) // событие это изменение счетчика
            {
                CurrentWidth = (int)n.Value;
            }
        }

        private void NumStarPoints_ValueChanged(object sender, EventArgs e) // изменение кол-ва лучей звезды
        {
            if (sender is NumericUpDown n)
                StarPoints = (int)n.Value;
        }

        private void NumStarRatio_ValueChanged(object sender, EventArgs e) // изменение отношения радиусов звезды
        {
            if (sender is NumericUpDown n)
                StarRatio = (double)(n.Value / 10);
        }

        private void MenuExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuNew_Click(object sender, EventArgs e) // создание холста
        {
            ChildForm newChild = new ChildForm(); // создаем экземпляр дочернего окна
            newChild.MdiParent = this; // должен открываться внутри MainForm
            newChild.Text = "Рисунок " + this.MdiChildren.Length.ToString(); // меняем заголовок

            newChild.Show();
        }


        private void UpdateStatusTool() // вспомогательный метод для отображения текущего инструмента
        {
            lblCurrentTool.Text = $"Инструмент: {btnTool.Text}";
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e) // событие активации окна
        {
            bool hasActiveChild = (ActiveMdiChild != null);

            menuSave.Enabled = hasActiveChild;
            menuSaveAs.Enabled = hasActiveChild;
            btnSave.Enabled = hasActiveChild;

            if (hasActiveChild && ActiveMdiChild is ChildForm child)
            {
                if (child.pictureBox.Image != null)
                {
                    lblImageSize.Text = $"Размер: {child.pictureBox.Image.Width} x {child.pictureBox.Image.Height}";
                }

                UpdateStatusTool(); // отображаем инструмент

            }
            else
            {
                lblImageSize.Text = "";
                lblMousePos.Text = "";
            }
        }

        private void BtnColor_Click(object sender, EventArgs e) // выбор цвета
        {
            ColorDialog cd = new ColorDialog(); // создаем меню выбора цвета

            if (cd.ShowDialog() == DialogResult.OK) // если подтвердили выбор
            {
                CurrentColor = cd.Color; // присваиваем выбранный цвет переменной

                btnColor.BackColor = cd.Color; // меняем цвет иконки для наглядности
            }
        }
        private void MenuCascade_Click(object sender, EventArgs e) // каскадное отображение
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void MenuHorizontal_Click(object sender, EventArgs e) // горизонтальное отображение
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void MenuVertical_Click(object sender, EventArgs e) // вертикальное отображение
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void menuArrangeIcons_Click(object sender, EventArgs e) // упорядочить иконки
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);

        }

        private void MenuAbout_Click(object sender, EventArgs e) // о программе
        {
            AboutForm frm = new AboutForm(); // создаем окно
            frm.ShowDialog(); // показываем окно (не закрывается пока не закроют)
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void MenuSave_Click(object sender, EventArgs e) // сохранить
        {
            if (ActiveMdiChild != null) // если есть активное окно
            {
                ChildForm activeChild = (ChildForm)ActiveMdiChild; // активное окно
                try
                {
                    if (activeChild.CurrentFilePath != null) // уже сохранялось раньше
                    {
                        activeChild.pictureBox.Image.Save(activeChild.CurrentFilePath); // сохраняем по тому же пути
                        activeChild.IsModified = false;
                    }
                    else // если файл до этого не сохраняли
                    {
                        MenuSaveAs_Click(sender, e);
                    }
                }
                catch (Exception ex) // если ошибка при сохранении
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void MenuSaveAs_Click(object sender, EventArgs e) // сохранить как
        {
            if (ActiveMdiChild != null) // если есть активное окно
            {
                SaveFileDialog sfd = new SaveFileDialog();// создаем окно для сохранения
                sfd.Filter = "PNG Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp"; // фильтры

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ChildForm activeChild = (ChildForm)ActiveMdiChild; // активное окно
                        if (activeChild.pictureBox.Image != null) // если есть изображение
                        {
                            activeChild.pictureBox.Image.Save(sfd.FileName); // сохраняем изображение
                            activeChild.CurrentFilePath = sfd.FileName; // сохраняем путь
                            activeChild.Text = Path.GetFileName(sfd.FileName); // меняем заголовок на имя файла
                            activeChild.IsModified = false;
                        }
                    }
                    catch (Exception ex) // если ошибка при сохранении
                    {
                        MessageBox.Show("Ошибка: " + ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void MenuOpen_Click(object sender, EventArgs e) // открыть
        {
            OpenFileDialog ofd = new OpenFileDialog(); // окно выбора файла
            ofd.Filter = "PNG Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp"; // фильтры

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ChildForm newChild = new ChildForm(); // создаем окно для холста
                    newChild.MdiParent = this;

                    using (Image tempImg = Image.FromFile(ofd.FileName)) // открываем файл во временную переменную
                    {
                        Bitmap bmp = new Bitmap(tempImg); // создаем Bitmap холста
                        newChild.pictureBox.Image = bmp; // присваиваем Bitmap окну
                        newChild.pictureBox.Size = bmp.Size; // подгоняем размер контрола
                        newChild.ClientSize = new Size(newChild.pictureBox.Width, newChild.pictureBox.Height); // подгоняем окно под новый размер

                    }

                    newChild.Text = Path.GetFileName(ofd.FileName); // заголовок окна - название файла
                    newChild.CurrentFilePath = ofd.FileName; // запоминаем путь к загруженному файлу
                    newChild.UpdateCursor(); // обновляем курсор

                    newChild.Show();


                }
                catch (Exception ex) // если ошибка при открытии
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // кнопки для панели управления
        private void BtnNew_Click(object sender, EventArgs e)
        {
            MenuNew_Click(sender, e);
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            MenuOpen_Click(sender, e);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MenuSave_Click(sender, e);
        }

        private void ToolMenuItem_Click(object sender, EventArgs e) // панель выбора инструментов
        {
            btnFont.Visible = false;
            btnIsFilled.Visible = false;

            // скрываем контролы звезды
            if (hostStarPoints != null && lblStarPoints != null && hostStarRatio != null && lblStarRatio != null)
            {
                hostStarPoints.Visible = false;
                lblStarPoints.Visible = false;
                hostStarRatio.Visible = false;
                lblStarRatio.Visible = false;
            }

            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender; // приводим sender к типу пункта меню


            btnTool.Text = clickedItem.Text; // меняем текст поля на название инструмента

            switch (clickedItem.Text) // меняет текущий инструмент на выбранный
            {
                case "Кисть":
                    CurrentTool = DrawingTool.Pen;
                    break;
                case "Линия":
                    CurrentTool = DrawingTool.Line;
                    break;
                case "Эллипс":
                    CurrentTool = DrawingTool.Ellipse;
                    btnIsFilled.Visible = true;
                    break;
                case "Ластик":
                    CurrentTool = DrawingTool.Eraser;
                    break;
                case "Масштаб+":
                    CurrentTool = DrawingTool.ZoomIn;
                    break;
                case "Масштаб-":
                    CurrentTool = DrawingTool.ZoomOut;
                    break;
                case "Текст":
                    CurrentTool = DrawingTool.Text;
                    btnFont.Visible = true;
                    break;
                case "Ведро с краской":
                    CurrentTool = DrawingTool.Bucket;
                    break;
                case "Звезда":
                    CurrentTool = DrawingTool.Star;
                    btnIsFilled.Visible = true;

                    if (hostStarPoints != null && lblStarPoints != null && hostStarRatio != null && lblStarRatio != null)
                    {
                        hostStarPoints.Visible = true;
                        lblStarPoints.Visible = true;
                        hostStarRatio.Visible = true;
                        lblStarRatio.Visible = true;
                    }
                    break;
            }

            if (ActiveMdiChild is ChildForm child) // если есть активное окно
            {
                child.UpdateCursor();
            }

            UpdateStatusTool();

        }

        private void BtnIsFilled_Click(object sender, EventArgs e)
        {
            IsFilled = !IsFilled;
            btnIsFilled.Checked = IsFilled; // визуально вдавливаем кнопку
        }

        private void MenuCanvasSize_Click(object sender, EventArgs e)
        {
            // есть ли активное дочернее окно
            if (ActiveMdiChild is ChildForm child)
            {
                // текущие размеры холста
                int currentW = child.pictureBox.Image.Width;
                int currentH = child.pictureBox.Image.Height;

                CanvasSizeForm sizeForm = new CanvasSizeForm(currentW, currentH); // создаем диалоговое окно для изменения размера холста

                if (sizeForm.ShowDialog() == DialogResult.OK) // если подтвердили изменение
                {
                    if (child.WindowState == FormWindowState.Maximized)
                    {
                        child.WindowState = FormWindowState.Normal;
                    }

                    // создаем битмап с новыми размерами
                    Bitmap newBmp = new Bitmap(sizeForm.NewWidth, sizeForm.NewHeight);

                    // переносим старое изображение на новое
                    using (Graphics g = Graphics.FromImage(newBmp))
                    {
                        g.Clear(Color.White); // заливаем фон
                        g.DrawImage(child.pictureBox.Image, 0, 0); // перенос старой части
                    }

                    // обновляем PictureBox
                    child.pictureBox.Image.Dispose(); // удаляем старый битмап
                    child.pictureBox.Image = newBmp;
                    child.pictureBox.Size = newBmp.Size; // подгоняем размер контрола
                    child.ClientSize = new Size(child.pictureBox.Width, child.pictureBox.Height); // подгоняем окно под новый размер
                    lblImageSize.Text = $"Размер: {child.pictureBox.Image.Width} x {child.pictureBox.Image.Height}";
                    child.Refresh();   // принудительно перерисовывает все дочернее окно

                    child.IsModified = true; // файл изменен
                }
            }
        }

        public void BtnFont_Click(object sender, EventArgs e) // изменить шрифт
        {
            using (FontDialog fd = new FontDialog()) // вызываем выбор шрифта
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    CurrentFont = fd.Font;
                }
            }
        }


        private bool configWasMissing = false;

        private void LoadSettings() // загрузка конфига
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins_config.json");
            if (!File.Exists(path))
            {
                configWasMissing = true;
                currentConfig = new AppConfig { AutoMode = false };
                return;
            }

            try
            {
                string json = File.ReadAllText(path);
                currentConfig = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
            }
            catch 
            {
                configWasMissing = true;
                currentConfig = new AppConfig();
            }
        }

        private void SaveSettings() // сохранение конфига
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins_config.json");
            try
            {
                currentConfig.Plugins = allPlugins.Select(p => new PluginSettings { Name = p.Name, IsEnabled = p.IsEnabled }).ToList();
                string json = JsonSerializer.Serialize(currentConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения настроек: " + ex.Message);
            }
        }

        void FindPlugins() // поиск плагинов
        {
            allPlugins.Clear();
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
            {
                if (file.EndsWith("PluginInterface.dll", StringComparison.OrdinalIgnoreCase))
                    continue;

                try
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    
                    Type[] types;
                    try 
                    {
                        types = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        types = ex.Types.Where(t => t != null).ToArray();
                    }

                    foreach (Type type in types)
                    {
                        if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            var item = new PluginItem(plugin);
                            
                            // Извлекаем версию
                            var versionAttr = type.GetCustomAttribute<VersionAttribute>();
                            if (versionAttr != null)
                                item.Version = $"{versionAttr.Major}.{versionAttr.Minor}";

                            // Применяем настройки
                            if (configWasMissing || currentConfig.AutoMode)
                            {
                                item.IsEnabled = true;
                            }
                            else
                            {
                                var saved = currentConfig.Plugins.FirstOrDefault(s => s.Name == plugin.Name);
                                item.IsEnabled = saved != null && saved.IsEnabled;
                            }

                            allPlugins.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки плагина {file}: {ex.Message}");
                }
            }
            
            if (configWasMissing)
            {
                SaveSettings();
            }

            if (allPlugins.Count == 0 && !configWasMissing)
            {
                 MessageBox.Show($"Плагины не найдены. Путь поиска: {folder}\nФайлов DLL в папке: {files.Length}");
            }
        }

        private void CreatePluginsMenu()
        {
            // Очищаем старые пункты, кроме настроек, редактора и разделителя
            while (filtersToolStripMenuItem.DropDownItems.Count > 3)
            {
                filtersToolStripMenuItem.DropDownItems.RemoveAt(3);
            }

            foreach (var item in allPlugins.Where(p => p.IsEnabled))
            {
                ToolStripMenuItem menuLine = new ToolStripMenuItem(item.Name);
                menuLine.Click += async (sender, e) => await OnPluginClick(item.Plugin);
                filtersToolStripMenuItem.DropDownItems.Add(menuLine);
            }
        }


        private void PluginSettingsMenuItem_Click(object sender, EventArgs e)
        {
            FiltersForm form = new FiltersForm(allPlugins, currentConfig);
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveSettings();
                CreatePluginsMenu();
            }
        }

        // вызов редактора кода и применение фильтра
        private async void menuCustomFilter_Click(object sender, EventArgs e)
        {
            CodeEditorForm editor = new CodeEditorForm();
            if (editor.ShowDialog() == DialogResult.OK && editor.CompiledPlugin != null)
            {
                // добавление фильтра в меню сессии
                if (!allPlugins.Any(p => p.Plugin.GetType() == editor.CompiledPlugin.GetType()))
                {
                    allPlugins.Add(new PluginItem(editor.CompiledPlugin));
                    CreatePluginsMenu(); // обновление кнопок в меню
                }
                
                // выполнение плагина
                await OnPluginClick(editor.CompiledPlugin);
            }
        }

        private async Task OnPluginClick(IPlugin plugin)
        {
            ChildForm? activeChild = ActiveMdiChild as ChildForm;

            if (activeChild != null && activeChild.pictureBox.Image != null)
            {
                if (activeChild.IsFilterRunning)
                {
                    MessageBox.Show("На этом холсте уже применяется фильтр.");
                    return;
                }

                try
                {
                    // Подготовка UI
                    activeChild.panelProgress.Visible = true;
                    activeChild.pbFilterProgress.Value = 0;
                    activeChild.IsFilterRunning = true;
                    activeChild.UpdateCursor();

                    activeChild.cts = new System.Threading.CancellationTokenSource();
                    var progress = new Progress<int>(v => 
                    {
                        activeChild.pbFilterProgress.Value = v;
                    });

                    // Копия изображения для работы в другом потоке
                    Bitmap bmp = new Bitmap(activeChild.pictureBox.Image);
                    
                    await Task.Run(() => 
                    {
                        plugin.Transform(bmp, progress, activeChild.cts.Token);
                    });

                    if (activeChild.cts.Token.IsCancellationRequested)
                    {
                        MessageBox.Show("Операция отменена.");
                    }
                    else
                    {
                        activeChild.pictureBox.Image?.Dispose();
                        activeChild.pictureBox.Image = bmp;
                        activeChild.IsModified = true;
                        activeChild.pictureBox.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выполнении фильтра '{plugin.Name}': {ex.Message}", 
                        "Ошибка плагина", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (activeChild != null)
                    {
                        activeChild.panelProgress.Visible = false;
                        activeChild.IsFilterRunning = false;
                        activeChild.UpdateCursor();
                        activeChild.cts?.Dispose();
                        activeChild.cts = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет активного изображения для применения фильтра.");
            }
        }
    }
}
