using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace CrazyKartActivation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "CrazyKarting.com.ua")
            {
                ProgramActivation pa = new ProgramActivation();
                pa.SaveKey();
            }
            else MessageBox.Show("Не-а, ключ не тот!", "Генерация ключа", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
