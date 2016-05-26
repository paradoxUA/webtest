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
    public partial class AddSertificat : Form
    {
        AdminControl admin;
        string Sertif_ID = String.Empty;
        bool sUpdate = false;
        int certificateType; 

        public AddSertificat(AdminControl ad, int certificateType = 0, bool upd = false, string ID = "", string Nm = "", string Nml = "", string Cst = "")
        {
            InitializeComponent();
            admin = ad;

            if (upd)
            {
                sUpdate = upd;                

                textBox1.Text = Nm;
                numericUpDown1.Value = Convert.ToInt16(Nml);
                numericUpDown2.Value = Convert.ToDecimal(Cst);
                Sertif_ID = ID;
            }

            this.certificateType = certificateType;
            if (certificateType == 1)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

        }

        private void AddSertificat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        // сохранить
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) MessageBox.Show("Ошибка! Название не может быть пустым!");
            else
            {
                if (sUpdate)
                {
                    admin.model.ChangeSertificateType(Sertif_ID, textBox1.Text, numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(), radioButton1.Checked ? "1" : "0");
                }
                else
                {
                    admin.model.AddCertificateType(textBox1.Text, numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(), radioButton1.Checked ? "1" : "0");
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) labelSmooth2.Text = "% скидки";
            else labelSmooth2.Text = "Номинал";
        }
    }
}
