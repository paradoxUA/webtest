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
    public partial class AddProgramUsers : Form
    {
        AdminControl admin;
        bool sUpdate = false;
        string UserID;
        public AddProgramUsers(AdminControl ad, bool upd = false, string UID="", string Login="", string Group="",string Password="", string Name="", string Surname ="", string Barcode="")
        {
            InitializeComponent();
            admin = ad;
            comboBox1.SelectedIndex = 0;
            if (upd)
            {
                sUpdate = upd;
                UserID = UID;
                comboBox1.SelectedIndex = Convert.ToInt32(Group);
                textBox1.Text = Login;
                textBox2.Text = textBox3.Text = Password;
                textBox4.Text = Name;
                textBox5.Text = Surname;
                textBox6.Text = Barcode;
            }
        }

        private void AddProgramUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) MessageBox.Show("Ошибка! Логин не может быть пустым"); else
            if (textBox2.Text != textBox3.Text)     MessageBox.Show("Ошибка! Пароли не совпадают");
            else
            {
                string password = admin.Encrypt(textBox2.Text);

                if (sUpdate)
                {
                    admin.model.ChangeProgramUsers(textBox1.Text, password, comboBox1.SelectedIndex.ToString(), UserID, textBox4.Text, textBox5.Text,textBox6.Text);
                } else
                    admin.model.AddProgramUser(textBox1.Text, password, comboBox1.SelectedIndex.ToString(), textBox4.Text, textBox5.Text, textBox6.Text);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
