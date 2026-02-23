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
    public partial class TextForm : Form
    {
        public string UserText => txtInput.Text; // св-во чтобы забирать текст
        public TextForm()
        {
            InitializeComponent();
        }
    }
}
