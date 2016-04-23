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
    public partial class PetroleumSpend : Form
    {
        AdminControl admin;

        public PetroleumSpend(AdminControl adm)
        {
            InitializeComponent();
            this.admin = adm;
        }

        private void spendPetroleum_button1_Click(object sender, EventArgs e)
        {
            model.Petroleum somePetroleum = new model.Petroleum
            {
                litres = Convert.ToDouble(litres_numericUpDown1.Value),
                Price = Convert.ToDouble(price_numericUpDown2.Value),
                program_users_id = admin.USER_ID,
                Date = DateTime.Now
            };

            admin.model.petroleumSpent(somePetroleum);
            this.Close();

        }

        private void cancel_button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
