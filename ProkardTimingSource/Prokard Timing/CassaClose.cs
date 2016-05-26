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
    public partial class CassaClose : Form
    {
        AdminControl admin;
        double MaxSumm = 0;
        bool OnNumber = false;

        public CassaClose(AdminControl ad)
        {
            InitializeComponent();
            admin = ad;
            string Summ = admin.model.GetCashFromCassa(DateTime.Now);
            MaxSumm = Double.Parse(Summ == ""?"0":Summ);
            textBox1.Text = MaxSumm.ToString();
            label2.Text = MaxSumm.ToString() + " грн";
            Calculate();

            if (!(admin.IS_ADMIN)) this.Close();
        }

        private void CassaClose_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void Calculate()
        {
            if (textBox1.Text.Length > 0)
            {

                double Sum = Double.Parse(textBox1.Text);

                if (Sum <= 0 || Sum > MaxSumm)
                {
                    label5.Text = " 0 грн";
                    button2.Enabled = false;
                }
                else
                {
                    label5.Text = (MaxSumm - Sum).ToString() + " грн";
                    button2.Enabled = true && admin.IS_ADMIN;
                }
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) MessageBox.Show("Ошибка! Сумма не указана");
            else
            {
                admin.model.Jurnal_Cassa("15", -1, -1, textBox1.Text, "1", "Снятие наличных с кассы. Снял - " + admin.model.GetProgramUserName(admin.USER_ID.ToString()));
                this.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            OnNumber = false;

            if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9))
                if (/*e.KeyValue != 190 && */e.KeyValue != 188 && e.KeyCode != Keys.Back)
                {
                    OnNumber = true;
                }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (OnNumber) e.Handled = true;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Calculate();
        }
    }
}
