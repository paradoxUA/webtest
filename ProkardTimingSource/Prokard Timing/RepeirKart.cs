using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prokard_Timing
{
    public partial class RepeirKart : Form
    {
        private string KartNum;
        AdminControl admin;
        public RepeirKart(string Number, AdminControl adm, int index = 0, string Message = "")
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.Date;
            KartNum = Number;
            comboBox1.SelectedIndex = index;
            label4.Text = "Номер карта - " + Number;
            textBox1.Text = Message;
            admin = adm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0: admin.model.KartRepair(KartNum, "1", dateTimePicker1.Value, textBox1.Text); break;
                case 1: admin.model.KartRepair(KartNum, "0", dateTimePicker1.Value, textBox1.Text); break;
                case 2: admin.model.AddMessage(Convert.ToInt32(admin.model.GetKartID(KartNum).ToString()), 0, 0, dateTimePicker1.Value, textBox1.Text); break;
            }


           this.Close();
        }

        private void RepeirKart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }
    }
}
