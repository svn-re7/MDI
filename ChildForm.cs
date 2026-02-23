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
        Point lastPoint; // точка начала рисования фигуры
        Point lastMovePoint; // точка для рисования предварительной фигуры
        public string? CurrentFilePath = null; // путь до файла
        public bool isModified = false; // был ли изменен рисунок
        Pen? myPen;
        public ChildForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            BackColor = Color.Gray;
            UpdateCursor();

            // создаем холст
            Bitmap bmp = new Bitmap(800, 600);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            pictureBox.Image = bmp;

            // подгоняем размер pictureBox и формы
            pictureBox.Size = bmp.Size;
            ClientSize = new Size(pictureBox.Width, pictureBox.Height);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e) // кликнули мышкой
        {
            // Мастштабирование
            if (MainForm.CurrentTool == MainForm.DrawingTool.ZoomIn)
            {
                ChangeScale(1.2f); // зумим на 20%
                return;
            }
            if (MainForm.CurrentTool == MainForm.DrawingTool.ZoomOut)
            {
                ChangeScale(0.8f); // уменьшаем на 20
                return;
            }

            // инструмент - ведро
            if (MainForm.CurrentTool == MainForm.DrawingTool.Bucket)
            {
                Point point = MapCoordinates(e.Location);

                // проверка на границы
                if (point.X >= 0 && point.X < pictureBox.Image.Width &&
                    point.Y >= 0 && point.Y < pictureBox.Image.Height)
                {
                    FloodFill((Bitmap)pictureBox.Image, point, MainForm.CurrentColor);
                    isModified = true;
                    pictureBox.Invalidate();
                }
                return;
            }

            // инструмент - текст
            if (MainForm.CurrentTool == MainForm.DrawingTool.Text)
            {
                using (TextForm tf = new TextForm()) // создаем форму для ввода текста
                {
                    if (tf.ShowDialog() == DialogResult.OK)
                    {
                        string textToDraw = tf.UserText; // получили текст из диалога
                        if (string.IsNullOrEmpty(textToDraw)) return;

                        if (MdiParent is MainForm parent)
                        {
                            if (MainForm.currentFont == null) // если шрифт не установлен
                            {
                                parent.BtnFont_Click(sender, e);
                            }
                            Point point = MapCoordinates(e.Location); // пересчитываем координаты в реальные

                            using (Graphics g = Graphics.FromImage(pictureBox.Image))
                            {
                                // сглаживание 
                                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                                using (SolidBrush brush = new SolidBrush(MainForm.CurrentColor))
                                {
                                    if (MainForm.currentFont != null)
                                        g.DrawString(textToDraw, MainForm.currentFont, brush, point);
                                }
                            }
                        }
                        isModified = true;
                        pictureBox.Invalidate(); // перерисовываем
                    }
                }
                return; // чтобы не сработал код обычного рисования
            }

            // изменение флагов и т д
            isDrawing = true;
            isModified = true;
            lastPoint = MapCoordinates(e.Location); // где начали рисовать | предыдущая точка
            lastMovePoint = MapCoordinates(e.Location);

            // создание пера
            myPen = new Pen(MainForm.CurrentColor, MainForm.CurrentWidth);
            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round; // скруглять начало
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;   // скруглять конец
            myPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round; // скруглять стыки

        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e) // двигаем мышкой
        {

            lastMovePoint = MapCoordinates(e.Location); // обновляем текущую точку для предпросмотра

            if (isDrawing && myPen != null)
            {
                isModified = true; // пометка что рисунок изменен

                // рисуем сразу на Bitmap
                if (MainForm.CurrentTool == MainForm.DrawingTool.Pen ||
                    MainForm.CurrentTool == MainForm.DrawingTool.Eraser)
                {
                    if (pictureBox.Image == null) return; // проверка на всякий случай

                    using (Graphics g = Graphics.FromImage(pictureBox.Image)) // рисуем прямо на холсте
                    {
                        if (MainForm.CurrentTool == MainForm.DrawingTool.Eraser)
                            myPen.Color = Color.White; // если ластик - временно белым

                        g.DrawLine(myPen, lastPoint, lastMovePoint); // проводим линию от предыдущей точки до текущей

                        if (MainForm.CurrentTool == MainForm.DrawingTool.Eraser)
                            myPen.Color = MainForm.CurrentColor; // возвращаем основной цвет
                    }
                    lastPoint = lastMovePoint; // фиксируем новую точку как прошлую
                }

                // вызываем перерисовку (для кисти обновит холст, для линий и эллипсов - вызовет событие Paint)
                pictureBox.Invalidate();
            }

            // выводим координаты в главного окна
            if (MdiParent is MainForm main)
            {
                main.lblMousePos.Text = $"Координаты: {lastMovePoint.X}, {lastMovePoint.Y}";
            }
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing && myPen != null)
            {
                // вычисление масштаб (преобразуем реальные пиксели в экранные)
                float ratioX = (float)pictureBox.Width / pictureBox.Image.Width;
                float ratioY = (float)pictureBox.Height / pictureBox.Image.Height;

                e.Graphics.ScaleTransform(ratioX, ratioY); // применяем масштаб

                // рисуем поверх временную фигуру
                if (MainForm.CurrentTool == MainForm.DrawingTool.Line)
                {
                    e.Graphics.DrawLine(myPen, lastPoint, lastMovePoint);
                }
                else if (MainForm.CurrentTool == MainForm.DrawingTool.Ellipse)
                {
                    // вычисляем размеры прямоугольника, в который вписан эллипс
                    int x = Math.Min(lastPoint.X, lastMovePoint.X);
                    int y = Math.Min(lastPoint.Y, lastMovePoint.Y);
                    int width = Math.Abs(lastMovePoint.X - lastPoint.X);
                    int height = Math.Abs(lastMovePoint.Y - lastPoint.Y);

                    if (MainForm.IsFilled) // если включена заливка
                        using (SolidBrush br = new SolidBrush(MainForm.CurrentColor))
                            e.Graphics.FillEllipse(br, x, y, width, height);
                    else
                        e.Graphics.DrawEllipse(myPen, x, y, width, height); // без заливки
                }
            }
        }


        private void PictureBox_MouseUp(object sender, MouseEventArgs e) // опустили мышку
        {
            if (isDrawing)
            {
                lastMovePoint = MapCoordinates(e.Location);
                // финализируем рисунок на Bitmap
                using (Graphics g = Graphics.FromImage(pictureBox.Image))
                {
                    if (MainForm.CurrentTool == MainForm.DrawingTool.Line) // если инструмент - линия
                    {
                        g.DrawLine(myPen, lastPoint, lastMovePoint); // рисуем фниальную линию
                    }
                    else if (MainForm.CurrentTool == MainForm.DrawingTool.Ellipse) // если инструмент - эллипс
                    {
                        // вычисляем размеры прямоугольника, в который вписан эллипс
                        int x = Math.Min(lastPoint.X, lastMovePoint.X); // самая левая точка
                        int y = Math.Min(lastPoint.Y, lastMovePoint.Y); // самая верхняя точка
                        int width = Math.Abs(lastMovePoint.X - lastPoint.X);
                        int height = Math.Abs(lastMovePoint.Y - lastPoint.Y);

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
                isDrawing = false;
                pictureBox.Invalidate();
            }
            myPen?.Dispose();

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
                case MainForm.DrawingTool.Text:
                    pictureBox.Cursor = Cursors.IBeam;
                    break;
                default:
                    pictureBox.Cursor = Cursors.Default;
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

        public void ChangeScale(float factor) // метод чтобы менять размер pictureBox (для масштабирования)
        {
            // новый размер
            int newWidth = (int)(pictureBox.Width * factor);
            int newHeight = (int)(pictureBox.Height * factor);

            // ограничения
            if (newWidth < 210 || newWidth > 5000) return;

            pictureBox.Size = new Size(newWidth, newHeight);
        }

        private Point MapCoordinates(Point Point) // метод для пересчета координат с учетом масштаба
        {
            // вычисление пропрорации сжатия (преобразуем экранные пиксели в реальные)
            float ratioX = (float)pictureBox.Image.Width / pictureBox.Width; // ширина оригинала / ширина окна
            float ratioY = (float)pictureBox.Image.Height / pictureBox.Height;

            return new Point(
                (int)(Point.X * ratioX),
                (int)(Point.Y * ratioY)
            );
        }

        private void FloodFill(Bitmap bmp, Point pt, Color targetColor) // метод для заливки на основе очереди
        {
            Color originColor = bmp.GetPixel(pt.X, pt.Y); // цвет, куда нажали

            // если мы кликнули цветом по такому же цвету — ничего делать не надо
            if (originColor.ToArgb() == targetColor.ToArgb()) return;

            Queue<Point> pixels = new Queue<Point>();
            pixels.Enqueue(pt);

            while (pixels.Count > 0)
            {
                Point a = pixels.Dequeue(); // нынешняя точка

                if (a.X < 0 || a.X >= bmp.Width || a.Y < 0 || a.Y >= bmp.Height) // если вышли за пределы холста - пропускаем
                    continue;

                if (bmp.GetPixel(a.X, a.Y).ToArgb() == originColor.ToArgb()) // если пиксель начального цвета, который нужно красить
                {
                    bmp.SetPixel(a.X, a.Y, targetColor); // перекрашиваем пиксель в цвет заливки

                    // добавляем соседей в очередь
                    pixels.Enqueue(new Point(a.X - 1, a.Y));
                    pixels.Enqueue(new Point(a.X + 1, a.Y));
                    pixels.Enqueue(new Point(a.X, a.Y - 1));
                    pixels.Enqueue(new Point(a.X, a.Y + 1));
                }
            }
        }
    }
}
