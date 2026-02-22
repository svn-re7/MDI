using System.Windows.Forms;
using System.IO;

namespace MDI
{
    public partial class MainForm : Form
    {
        public static Color CurrentColor = Color.Black; // цвет пера
        public static int CurrentWidth = 3; // толщина пера
        public static bool IsFilled = false; // по умолчанию рисуем без заливки
        public enum DrawingTool { Pen, Line, Ellipse, Eraser } // список констант - инструменты

        public static DrawingTool CurrentTool = DrawingTool.Pen; // текущий инструмент - перо


        public MainForm()
        {
            InitializeComponent();

            NumericUpDown num = new NumericUpDown(); // создаем объект счетчик
            num.Minimum = 1;
            num.Maximum = 50;
            num.Value = 3;
            num.Width = 50;

            num.ValueChanged += Num_ValueChanged; // подписываем Num_ValueChanged на изименение счетчика

            ToolStripControlHost host = new ToolStripControlHost(num); // обертка для счетчика чтобы положить его в ToolStrip

            ToolStripLabel hostLabel = new ToolStripLabel("Толщина:");
            toolStrip1.Items.Add(hostLabel);
            hostLabel.ToolTipText = "Толщина пера (от 1 до 50)";
            toolStrip1.Items.Add(host);
        }

        private void Num_ValueChanged(object? sender, EventArgs e) // изменение толщины
        {
            if (sender is NumericUpDown n) // событие это изменение счетчика
            {
                CurrentWidth = (int)n.Value;
            }
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

            Bitmap bmp = new Bitmap(newChild.pictureBox.Width, newChild.pictureBox.Height); // создаем пустой холст
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White); // делаем фон белым
            }
            newChild.pictureBox.Image = bmp; // определяем этот холст окну

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
                        activeChild.isModified = false;
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
                            activeChild.isModified = false;
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
                    break;
                case "Ластик":
                    CurrentTool = DrawingTool.Eraser;
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

    }
}
