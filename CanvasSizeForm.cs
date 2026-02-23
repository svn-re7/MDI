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
    public partial class CanvasSizeForm : Form
    {

        public int NewWidth => (int)numWidth.Value;
        public int NewHeight => (int)numHeight.Value;

        public CanvasSizeForm(int currentWidth, int currentHeight)
        {
            InitializeComponent();

            // при открытии устанавливаем текущие размеры
            numWidth.Value = currentWidth;
            numHeight.Value = currentHeight;

            // настраиваем кнопки
            btnOK.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            // назначаем кнопки подтверждения и отмены для enter и esc
            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }

    }
}
