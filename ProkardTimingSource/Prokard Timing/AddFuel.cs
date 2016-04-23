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
    public partial class AddFuel : Form
    {
        AdminControl admin;
        string KartID;
        public AddFuel(AdminControl ad, string Num, string ID)
        {
            InitializeComponent();
            admin = ad;
            KartID = ID;
            labelSmooth3.Text = Num;
        }

        private void AddFuel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin.model.AddFuel(KartID, numericUpDown1.Value.ToString().Replace(",", "."));
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
