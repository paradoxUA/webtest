using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Rentix
{
    public partial class LoginUser : Form
    {
        AdminControl admin;
        int rep = 5;
        public LoginUser(AdminControl ad)
        {
            InitializeComponent();
            admin = ad;
        }

        private void LoginUser_Activated(object sender, EventArgs e)
        {
            textBox1.Select();
            textBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (admin.IS_ADMIN || admin.IS_SUPER_ADMIN || admin.IS_USER) this.Close(); else Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void check_login()
        {


            Hashtable User = admin.model.GetProgramUserBarCode(textBox1.Text);

            if (User.Count > 0)
            {
                if (admin.IS_ADMIN || admin.IS_SUPER_ADMIN || admin.IS_USER)
                    admin.model.LogOut(admin.USER_ID.ToString());

                admin.IS_USER = admin.IS_SUPER_ADMIN = admin.IS_ADMIN = false;
                admin.USER_ID = 0;

                admin.USER_ID = Convert.ToInt32(User["id"].ToString());

                int stat = Convert.ToInt32(User["stat"].ToString());

                switch (stat)
                {
                    case 0: admin.IS_USER = true; break;
                    case 1: admin.IS_ADMIN = true; break;
                    case 2: admin.IS_ADMIN = true; admin.IS_SUPER_ADMIN = true; break;
                }

                admin.model.Login(admin.USER_ID.ToString());
                this.Close();
            }
            
             else
            {
                --rep;
                if (rep > 0)
                MessageBox.Show("Не верный логин или пароль.\n\rПопыток: "+rep.ToString()+" из 5");
                else
                    MessageBox.Show("Программа закрывается");
                
                    textBox1.Text = String.Empty;
                if (rep <= 0) Application.Exit();
            }
            textBox1.Select();
            textBox1.SelectAll();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text.Length >= 13)
            {
                check_login();
                textBox1.Select();
                textBox1.SelectAll();
            }
        }
    }
}
