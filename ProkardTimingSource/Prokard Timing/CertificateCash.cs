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
    public partial class CertificateCash : Form
    {
        AdminControl admin;
        int PilotID;
        public CertificateCash(AdminControl ad, string Cash, int PID)
        {
            InitializeComponent();
            textBox5.Text = Cash;
            admin = ad;
            PilotID = PID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CertificateCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Добавление денег в кассу, если оплата не идет через счет пользователя
            admin.model.Jurnal_Cassa("30", Convert.ToInt32(PilotID), -1, textBox5.Text, "0", "Покупка сертификата.");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
