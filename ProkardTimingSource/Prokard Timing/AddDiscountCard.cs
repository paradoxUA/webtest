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
    public partial class AddDiscountCard : Form
    {
        AdminControl admin;

        public AddDiscountCard(AdminControl admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void cancel_button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ok_button1_Click(object sender, EventArgs e)
        {
            admin.model.addDiscountCardType(Convert.ToInt16(percentOfDiscount_numericUpDown1.Value));
            this.Close();
        }
    }
}
