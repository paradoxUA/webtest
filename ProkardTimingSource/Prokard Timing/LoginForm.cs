using System;
using System.Windows.Forms;
using System.Collections;

namespace Prokard_Timing
{
    public partial class LoginForm : Form
    {
        AdminControl admin;
        bool OnClose = true;
        int rep = 5;
        public LoginForm(AdminControl ad)
        {
            InitializeComponent();
            admin = ad;
        }

        private void LoginForm_Activated(object sender, EventArgs e)
        {
            textBox1.Select();
            textBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (admin.IS_ADMIN || admin.IS_SUPER_ADMIN || admin.IS_USER) this.Close(); else Application.Exit();
        }

        private bool LoginFromCard(string s)
        {
            bool ret = false;
            if (s.Length > 1 && textBox1.Text.Length > 0)
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
                    admin.User_Name = User["login"].ToString();
                    admin.model.Login(admin.USER_ID.ToString());
                    ret = true;
                }
            }
            return ret;
        }

        private bool LoginFromName(string l, string p)
        {
            bool ret = false;
            if (l.Length > 0 && p.Length > 0)
            {
                Hashtable User = admin.model.GetProgramUser("", true, l, admin.Encrypt(p));

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
                    admin.User_Name = User["login"].ToString();
                    admin.model.Login(admin.USER_ID.ToString());
                    ret = true;
                }
            }
            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (LoginFromCard(textBox1.Text) || LoginFromName(textBox1.Text, textBox2.Text))
            {
                OnClose = false;
                this.Close();
            }
            else
            {
                OnClose = true;
                --rep;
                if (rep > 0)

                    MessageBox.Show(@"Неверный логин или пароль.\n\rПопыток: " + rep.ToString() + @" из 5");
                else
                {
                    MessageBox.Show(@"Программа закрывается");
                    OnClose = false;
                    Application.Exit();
                    this.Close();

                }
                if (rep <= 0)
                {
                    OnClose = false;
                    Application.Exit();
                }
            }

            textBox2.Text = "";
            textBox2.Select();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) button2.PerformClick();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text.Length >= 13)
            {
                if (LoginFromCard(textBox1.Text)) { this.Close(); OnClose = false; } else OnClose = true;
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            e.Cancel = OnClose;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

    

        
    }
}
