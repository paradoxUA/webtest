using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Prokard_Timing
{
    public partial class ActiveCertificate : Form
    {
        AdminControl admin;
        public ActiveCertificate(AdminControl ad, bool ReadOnly = true)
        {
            InitializeComponent();
            admin = ad;
        }

        private void ActiveCertificate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActiveCertificate_Activated(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            textBox1.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 3) MessageBox.Show("Ошибка! Неверный код, или строка пуста!");
            else
            {
                Hashtable Certificate = admin.model.GetCertificate(textBox1.Text.Trim());

                if (Certificate.Count == 0)
                {
                    MessageBox.Show("Сертификат не найден!");
                    textBox1.SelectAll();
                    labelSmooth7.Text = labelSmooth8.Text = labelSmooth9.Text = labelSmooth10.Text = labelSmooth12.Text = labelSmooth11.Text = "-";
                }
                else
                {
                    labelSmooth11.Text = admin.model.GetCertificateType(Certificate["c_id"].ToString())["name"].ToString();
                    
                    int uid = -1;

                    if (Certificate["user_id"]!= "")
                    {
                        uid = Convert.ToInt32(Certificate["user_id"]);
                        Hashtable User = admin.model.GetPilot(uid);
                        string Pname = User["name"] + " " + User["surname"];
                        if (Pname.Length < 3) Pname = User["nickname"].ToString();
                        labelSmooth10.Text = "Пилот - " + Pname;
                    }
                    else
                    {

                        labelSmooth10.Text = "CrazyKart";
                    }


                    labelSmooth9.Text = Certificate["count"].ToString();
                    labelSmooth8.Text = Convert.ToDateTime(Certificate["created"]).ToString("dd MMMM yyyy");
                    labelSmooth7.Text = Convert.ToDateTime(Certificate["date_end"]).ToString("dd MMMM yyyy");
                    labelSmooth12.Text = Convert.ToBoolean(Certificate["active"]) ? "Активен" : "Использован";

                    if (Convert.ToDateTime(Certificate["date_end"]).Ticks < DateTime.Now.Ticks && Convert.ToBoolean(Certificate["active"]))
                    {
                        labelSmooth12.Text = "Просрочен";
                        admin.model.ActivateCertificate(textBox1.Text.Trim(), "0");

                    }

                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

       
    }
}
