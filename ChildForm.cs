using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDI
{
    public partial class ChildForm : Form
    {
        bool isDrawing = false;
        Point lastPoint;
        Point lastMovePoint;
        public string? CurrentFilePath = null; // путь до файла
        Bitmap? snapshot; // снимок экрана до начала рисования линии
        public bool isModified = false; // был ли изменен рисунок
        Pen? myPen;
        public ChildForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e) // кликнули мышкой
        {
            isDrawing = true;
            isModified = true;
            lastPoint = e.Location; // где начали рисовать
            lastMovePoint = e.Location;

            myPen = new Pen(MainForm.CurrentColor, MainForm.CurrentWidth);
            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round; // скруглять начало
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;   // скруглять конец
            myPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round; // скруглять стыки

            if (MainForm.CurrentTool == MainForm.DrawingTool.Line ||
                MainForm.CurrentTool == MainForm.DrawingTool.Ellipse)
            {
                snapshot?.Dispose(); // если почему то не удалился, то удалем предыдущий снимок
                snapshot = new Bitmap(pictureBox.Image); // делаем копию изображения, чтобы рисовать промежуточный размер
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e) // двигаем мышкой
        {
            if (isDrawing && myPen != null)
            {

                isModified = true;

                if (pictureBox.Image == null) pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height); // проверка есть ли холст


                if (MainForm.CurrentTool == MainForm.DrawingTool.Pen) // если инструмент - кисть
                {
                    using (Graphics g = Graphics.FromImage(pictureBox.Image)) // using для быстрой очистки мусора
                    {
                        g.DrawLine(myPen, lastPoint, e.Location); // рисуем линию от lastPoint до текущего положения e.Location
                    }
                    lastPoint = e.Location; // обновляем последную точку курсора

                }
                else if (MainForm.CurrentTool == MainForm.DrawingTool.Line) // если инструмент - линия
                {
                    if (snapshot != null)
                    {
                        using (Graphics g = Graphics.FromImage(pictureBox.Image))
                        {
                            g.DrawImage(snapshot, 0, 0); // рисуем на холсте чистый снимок без линии

                            g.DrawLine(myPen, lastPoint, e.Location); // рисуем линию поверх чистого снимка
                        }
                    }
                }
                else if (MainForm.CurrentTool == MainForm.DrawingTool.Ellipse) // если инструмент - эллипс
                {
                    if (snapshot != null)
                    {
                        using (Graphics g = Graphics.FromImage(pictureBox.Image))
                        {
                            g.DrawImage(snapshot, 0, 0); // рисуем на холсте чистый снимок без эллипса

                            // вычисляем размеры прямоугольника, в который вписан эллипс
                            int x = Math.Min(lastPoint.X, e.X); // самая левая точка
                            int y = Math.Min(lastPoint.Y, e.Y); // самая верхняя точка
                            int width = Math.Abs(e.X - lastPoint.X);
                            int height = Math.Abs(e.Y - lastPoint.Y);

                            if (MainForm.IsFilled) // если заливка включена
                            {
                                using (SolidBrush myBrush = new SolidBrush(MainForm.CurrentColor)) // используем SolidBrush для закрашивания
                                {
                                    g.FillEllipse(myBrush, x, y, width, height);
                                }
                            }
                            else
                            {
                                g.DrawEllipse(myPen, x, y, width, height);
                            }
                        }
                    }
                }
                else if (MainForm.CurrentTool == MainForm.DrawingTool.Eraser) // если инструмент - ластик
                {
                    using (Graphics g = Graphics.FromImage(pictureBox.Image)) // using для быстрой очистки мусора
                    {
                        myPen.Color = Color.White;
                        g.DrawLine(myPen, lastPoint, e.Location); // рисуем линию от lastPoint до текущего положения e.Location
                        myPen.Color = MainForm.CurrentColor;
                    }
                    lastPoint = e.Location; // обновляем последную точку курсора

                }

                pictureBox.Invalidate(); // перерисовываем
            }

            if (!isDrawing || Math.Abs(e.X - lastMovePoint.X) > 10 || Math.Abs(e.Y - lastMovePoint.Y) > 10)
            {
                if (MdiParent is MainForm main)
                {
                    main.lblMousePos.Text = $"Координаты: {e.X}, {e.Y}";
                    lastMovePoint = e.Location;
                }
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e) // опустили мышку
        {
            isDrawing = false;
            myPen?.Dispose();

            if (snapshot != null)
            {
                snapshot.Dispose(); // удаляем копию из памяти
                snapshot = null;
            }
        }

        private void ChildForm_Enter(object sender, EventArgs e) // когда окно активное - обновляем курсор
        {
            UpdateCursor();
        }

        public void UpdateCursor() // метод для обновления курсора
        {
            Cursor GetSafeCursor(string fullPath, Cursor defaultCursor) // вспомогателньый метод для безопсносого выбора курсора
            {
                if (File.Exists(fullPath))
                {
                    return new Cursor(fullPath);
                }
                return defaultCursor; // если нет - стандартный
            }

            switch (MainForm.CurrentTool)
            {
                case MainForm.DrawingTool.Pen:
                    pictureBox.Cursor = GetSafeCursor(@"Resources\Pen.cur", Cursors.Default);
                    break;
                case MainForm.DrawingTool.Line:
                    pictureBox.Cursor = Cursors.Cross;
                    break;
                case MainForm.DrawingTool.Ellipse:
                    pictureBox.Cursor = Cursors.SizeAll;
                    break;
                case MainForm.DrawingTool.Eraser:
                    pictureBox.Cursor = GetSafeCursor(@"Resources\Eraser.cur", Cursors.NoMove2D);
                    break;
            }
        }

        private void ChildForm_FormClosing(object sender, FormClosingEventArgs e) // при закрытии окнв
        {
            if (isModified) // если были изменения
            {
                // диалог с кнопками да нет отмена
                DialogResult result = MessageBox.Show(
                    $"Сохранить изменения в файле {this.Text}?",
                    "MDI-приложение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes) // если подтвердили
                {
                    if (MdiParent is MainForm main) // чтобы можно было обратиться к методу 
                    {
                        main.MenuSave_Click(sender, e);
                    }
                }
                else if (result == DialogResult.Cancel) // если отменили
                {
                    e.Cancel = true; // отменяем действие - не закрываем окно
                }
            }
        }
    }
}
