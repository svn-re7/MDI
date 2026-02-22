using Microsoft.VisualBasic.Devices;

namespace MDI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            menuNew = new ToolStripMenuItem();
            menuOpen = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            menuSave = new ToolStripMenuItem();
            menuSaveAs = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            menuExit = new ToolStripMenuItem();
            рисунокToolStripMenuItem = new ToolStripMenuItem();
            размерХолстаToolStripMenuItem = new ToolStripMenuItem();
            окноToolStripMenuItem = new ToolStripMenuItem();
            menuCascade = new ToolStripMenuItem();
            menuHorizontal = new ToolStripMenuItem();
            menuVertical = new ToolStripMenuItem();
            menuArrangeIcons = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            menuAbout = new ToolStripMenuItem();
            btnNew = new ToolStripButton();
            btnOpen = new ToolStripButton();
            btnSave = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            вырезатьToolStripButton = new ToolStripButton();
            копироватьToolStripButton = new ToolStripButton();
            вставитьToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStrip1 = new ToolStrip();
            btnColor = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            btnTool = new ToolStripDropDownButton();
            btnPen = new ToolStripMenuItem();
            btnLine = new ToolStripMenuItem();
            btnEllipse = new ToolStripMenuItem();
            btnEraser = new ToolStripMenuItem();
            btnIsFilled = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            lblMousePos = new ToolStripStatusLabel();
            lblImageSize = new ToolStripStatusLabel();
            lblCurrentTool = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(28, 28);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, рисунокToolStripMenuItem, окноToolStripMenuItem, справкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.MdiWindowListItem = окноToolStripMenuItem;
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1875, 38);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuNew, menuOpen, toolStripMenuItem2, menuSave, menuSaveAs, toolStripMenuItem1, menuExit });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(80, 34);
            файлToolStripMenuItem.Text = "&Файл";
            // 
            // menuNew
            // 
            menuNew.Name = "menuNew";
            menuNew.ShortcutKeys = Keys.Control | Keys.N;
            menuNew.Size = new Size(396, 40);
            menuNew.Text = "&Новый";
            menuNew.Click += MenuNew_Click;
            // 
            // menuOpen
            // 
            menuOpen.Name = "menuOpen";
            menuOpen.ShortcutKeys = Keys.Control | Keys.O;
            menuOpen.Size = new Size(396, 40);
            menuOpen.Text = "&Открыть";
            menuOpen.Click += MenuOpen_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(393, 6);
            // 
            // menuSave
            // 
            menuSave.Enabled = false;
            menuSave.Name = "menuSave";
            menuSave.ShortcutKeys = Keys.Control | Keys.S;
            menuSave.Size = new Size(396, 40);
            menuSave.Text = "&Сохранить";
            menuSave.Click += MenuSave_Click;
            // 
            // menuSaveAs
            // 
            menuSaveAs.Enabled = false;
            menuSaveAs.Name = "menuSaveAs";
            menuSaveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            menuSaveAs.Size = new Size(396, 40);
            menuSaveAs.Text = "Сохранить &как";
            menuSaveAs.Click += MenuSaveAs_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(393, 6);
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.ShortcutKeys = Keys.Alt | Keys.F4;
            menuExit.Size = new Size(396, 40);
            menuExit.Text = "Вы&ход";
            menuExit.Click += MenuExitToolStripMenuItem_Click;
            // 
            // рисунокToolStripMenuItem
            // 
            рисунокToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { размерХолстаToolStripMenuItem });
            рисунокToolStripMenuItem.Name = "рисунокToolStripMenuItem";
            рисунокToolStripMenuItem.Size = new Size(109, 34);
            рисунокToolStripMenuItem.Text = "&Рисунок";
            // 
            // размерХолстаToolStripMenuItem
            // 
            размерХолстаToolStripMenuItem.Name = "размерХолстаToolStripMenuItem";
            размерХолстаToolStripMenuItem.Size = new Size(270, 40);
            размерХолстаToolStripMenuItem.Text = "Размер холста";
            // 
            // окноToolStripMenuItem
            // 
            окноToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuCascade, menuHorizontal, menuVertical, menuArrangeIcons });
            окноToolStripMenuItem.Name = "окноToolStripMenuItem";
            окноToolStripMenuItem.Size = new Size(81, 34);
            окноToolStripMenuItem.Text = "&Окно";
            // 
            // menuCascade
            // 
            menuCascade.Name = "menuCascade";
            menuCascade.Size = new Size(329, 40);
            menuCascade.Text = "Каскад";
            menuCascade.Click += MenuCascade_Click;
            // 
            // menuHorizontal
            // 
            menuHorizontal.Name = "menuHorizontal";
            menuHorizontal.Size = new Size(329, 40);
            menuHorizontal.Text = "Сверху вниз";
            menuHorizontal.Click += MenuHorizontal_Click;
            // 
            // menuVertical
            // 
            menuVertical.Name = "menuVertical";
            menuVertical.Size = new Size(329, 40);
            menuVertical.Text = "Слева направо";
            menuVertical.Click += MenuVertical_Click;
            // 
            // menuArrangeIcons
            // 
            menuArrangeIcons.Name = "menuArrangeIcons";
            menuArrangeIcons.Size = new Size(329, 40);
            menuArrangeIcons.Text = "Упорядочить значки";
            menuArrangeIcons.Click += menuArrangeIcons_Click;
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuAbout });
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(111, 34);
            справкаToolStripMenuItem.Text = "Справка";
            // 
            // menuAbout
            // 
            menuAbout.Name = "menuAbout";
            menuAbout.Size = new Size(261, 40);
            menuAbout.Text = "О программе";
            menuAbout.Click += MenuAbout_Click;
            // 
            // btnNew
            // 
            btnNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNew.Image = (Image)resources.GetObject("btnNew.Image");
            btnNew.ImageTransparentColor = Color.Magenta;
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(40, 34);
            btnNew.Text = "&Создать";
            btnNew.ToolTipText = "Создать новое изображение (Ctrl+N)";
            btnNew.Click += BtnNew_Click;
            // 
            // btnOpen
            // 
            btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpen.Image = (Image)resources.GetObject("btnOpen.Image");
            btnOpen.ImageTransparentColor = Color.Magenta;
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(40, 34);
            btnOpen.Text = "&Открыть";
            btnOpen.ToolTipText = "Открыть изображение (Ctrl + O)";
            btnOpen.Click += BtnOpen_Click;
            // 
            // btnSave
            // 
            btnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSave.Image = (Image)resources.GetObject("btnSave.Image");
            btnSave.ImageTransparentColor = Color.Magenta;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(40, 34);
            btnSave.Text = "&Сохранить";
            btnSave.ToolTipText = "Сохранить изображение (Ctrl+S)";
            btnSave.Click += BtnSave_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 40);
            // 
            // вырезатьToolStripButton
            // 
            вырезатьToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            вырезатьToolStripButton.Image = (Image)resources.GetObject("вырезатьToolStripButton.Image");
            вырезатьToolStripButton.ImageTransparentColor = Color.Magenta;
            вырезатьToolStripButton.Name = "вырезатьToolStripButton";
            вырезатьToolStripButton.Size = new Size(40, 34);
            вырезатьToolStripButton.Text = "Вы&резать";
            // 
            // копироватьToolStripButton
            // 
            копироватьToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            копироватьToolStripButton.Image = (Image)resources.GetObject("копироватьToolStripButton.Image");
            копироватьToolStripButton.ImageTransparentColor = Color.Magenta;
            копироватьToolStripButton.Name = "копироватьToolStripButton";
            копироватьToolStripButton.Size = new Size(40, 34);
            копироватьToolStripButton.Text = "&Копировать";
            // 
            // вставитьToolStripButton
            // 
            вставитьToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            вставитьToolStripButton.Image = (Image)resources.GetObject("вставитьToolStripButton.Image");
            вставитьToolStripButton.ImageTransparentColor = Color.Magenta;
            вставитьToolStripButton.Name = "вставитьToolStripButton";
            вставитьToolStripButton.Size = new Size(40, 34);
            вставитьToolStripButton.Text = "&Вставить";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 40);
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(28, 28);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnNew, btnOpen, btnSave, toolStripSeparator, вырезатьToolStripButton, копироватьToolStripButton, вставитьToolStripButton, toolStripSeparator1, btnColor, toolStripLabel1, btnTool, btnIsFilled });
            toolStrip1.Location = new Point(0, 38);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1875, 40);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnColor
            // 
            btnColor.Image = (Image)resources.GetObject("btnColor.Image");
            btnColor.ImageTransparentColor = Color.Magenta;
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(92, 34);
            btnColor.Text = "Цвет";
            btnColor.ToolTipText = "Выбрать цвет пера";
            btnColor.Click += BtnColor_Click;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(219, 34);
            toolStripLabel1.Text = "Текущий инструмент:";
            // 
            // btnTool
            // 
            btnTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnTool.DropDownItems.AddRange(new ToolStripItem[] { btnPen, btnLine, btnEllipse, btnEraser });
            btnTool.Image = (Image)resources.GetObject("btnTool.Image");
            btnTool.ImageTransparentColor = Color.Magenta;
            btnTool.Name = "btnTool";
            btnTool.Size = new Size(246, 34);
            btnTool.Text = "Выберите инструмент";
            // 
            // btnPen
            // 
            btnPen.Name = "btnPen";
            btnPen.Size = new Size(200, 40);
            btnPen.Text = "Кисть";
            btnPen.Click += ToolMenuItem_Click;
            // 
            // btnLine
            // 
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(200, 40);
            btnLine.Text = "Линия";
            btnLine.Click += ToolMenuItem_Click;
            // 
            // btnEllipse
            // 
            btnEllipse.Name = "btnEllipse";
            btnEllipse.Size = new Size(200, 40);
            btnEllipse.Text = "Эллипс";
            btnEllipse.Click += ToolMenuItem_Click;
            // 
            // btnEraser
            // 
            btnEraser.Name = "btnEraser";
            btnEraser.Size = new Size(200, 40);
            btnEraser.Text = "Ластик";
            btnEraser.Click += ToolMenuItem_Click;
            // 
            // btnIsFilled
            // 
            btnIsFilled.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnIsFilled.Image = (Image)resources.GetObject("btnIsFilled.Image");
            btnIsFilled.ImageTransparentColor = Color.Magenta;
            btnIsFilled.Name = "btnIsFilled";
            btnIsFilled.Size = new Size(94, 34);
            btnIsFilled.Text = "Заливка";
            btnIsFilled.Click += BtnIsFilled_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(28, 28);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblMousePos, lblImageSize, lblCurrentTool });
            statusStrip1.Location = new Point(0, 1017);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1875, 39);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblMousePos
            // 
            lblMousePos.DisplayStyle = ToolStripItemDisplayStyle.Text;
            lblMousePos.Name = "lblMousePos";
            lblMousePos.Size = new Size(214, 30);
            lblMousePos.Text = "Координаты курсора";
            // 
            // lblImageSize
            // 
            lblImageSize.Name = "lblImageSize";
            lblImageSize.Size = new Size(219, 30);
            lblImageSize.Text = "Размер изображения";
            // 
            // lblCurrentTool
            // 
            lblCurrentTool.Name = "lblCurrentTool";
            lblCurrentTool.Size = new Size(214, 30);
            lblCurrentTool.Text = "Текущий инструмент";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1875, 1056);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "MDI-приложение";
            Load += MainForm_Load;
            MdiChildActivate += MainForm_MdiChildActivate;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem menuNew;
        private ToolStripMenuItem menuOpen;
        private ToolStripMenuItem menuSave;
        private ToolStripMenuItem menuSaveAs;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem menuExit;
        private ToolStripMenuItem окноToolStripMenuItem;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnSave;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton вырезатьToolStripButton;
        private ToolStripButton копироватьToolStripButton;
        private ToolStripButton вставитьToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStrip toolStrip1;
        private ToolStripButton btnColor;
        private ToolStripMenuItem menuCascade;
        private ToolStripMenuItem menuHorizontal;
        private ToolStripMenuItem menuVertical;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem menuAbout;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem рисунокToolStripMenuItem;
        private ToolStripMenuItem размерХолстаToolStripMenuItem;
        private ToolStripMenuItem menuArrangeIcons;
        private ToolStripDropDownButton btnTool;
        private ToolStripMenuItem btnPen;
        private ToolStripMenuItem btnLine;
        private ToolStripLabel toolStripLabel1;
        private ToolStripMenuItem btnEllipse;
        private ToolStripMenuItem btnEraser;
        private ToolStripButton btnIsFilled;
        public StatusStrip statusStrip1;
        public ToolStripStatusLabel lblImageSize;
        public ToolStripStatusLabel lblCurrentTool;
        public ToolStripStatusLabel lblMousePos;
    }
}
