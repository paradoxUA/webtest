using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rentix
{
    public partial class AddEvent : Form
    {
        bool MUpdate = false;
        string MessageID = String.Empty;

        AdminControl admin;

        public AddEvent(AdminControl ad, bool updt = false, string MID = "", string Subject = "", string Message = "", string Date = "")
        {
            InitializeComponent();
            admin = ad;

            MUpdate = updt;
            if (MUpdate)
            {
                MessageID = MID;
                textBox1.Text = Subject;
                textBox2.Text = Message;
                dateTimePicker1.Value = Convert.ToDateTime(Date);
                dateTimePicker1.MinDate = dateTimePicker1.Value;
            }
            else dateTimePicker1.MinDate = DateTime.Now;
        }

        private void AddEvent_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length <= 2) MessageBox.Show("Текст темы короче двух символов");
            else
            {
                if (MUpdate)
                    admin.model.UpdateMessage(MessageID, dateTimePicker1.Value, textBox2.Text, textBox1.Text);
                else
                    admin.model.AddMessage(admin.USER_ID, 2, 0, dateTimePicker1.Value, textBox1.Text, textBox2.Text);

                this.Close();
            }
        }
    }
}
