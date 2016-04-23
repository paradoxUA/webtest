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
    public partial class AddKart : Form
    {
        AdminControl admin;
        private int CarID = -1;
        public AddKart(AdminControl ad, int ID = -1, string Name = "", string Number = "", string Trans = "")
        {
            InitializeComponent();
            CarID = ID;
            admin = ad;
            if (ID > 0)
            {
                textBox1.Text = Name;
                textBox2.Text = Number;
                textBox3.Text = Trans;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0) MessageBox.Show("Ошибка! Не указан номер карта!");
            else
                if (textBox3.Text.Length == 0) MessageBox.Show("Ошибка! Не указан номер транспондера!");
                else
                {
                    if (CarID == -1) admin.model.AddKart(textBox1.Text, textBox2.Text, textBox3.Text);
                    else admin.model.ChangeKart(CarID.ToString(), textBox1.Text, textBox2.Text, textBox3.Text);

                    this.Close();
                }
        }

        private void AddKart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void borderPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
