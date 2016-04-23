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
    public partial class AddCash : Form
    {
        AdminControl admin;
        public AddCash(AdminControl ad)
        {
            InitializeComponent();
            admin = ad;

            admin.ShowPilotsCash(dataGridView1);
            Calculate();

            button2.Enabled = dataGridView1.Rows.Count > 0;

            if (!(admin.IS_ADMIN || admin.IS_SUPER_ADMIN)) this.Close();
        }

        private void Calculate()
        {
            double Dt = 0, Ct = 0, Sl = 0, Tmp = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Tmp = Double.Parse(dataGridView1[5, i].Value.ToString());
                if (Tmp < 0)
                    Ct += Math.Abs(Tmp);
                else
                    Dt += Tmp;
            }

            Sl = Dt - Ct;

            label5.Text = "Сальдо:  " + Sl.ToString() + " грн";
            label3.Text = "Дт:  " + Dt.ToString() + " грн";
            label4.Text = "Кт:  " + Ct.ToString() + " грн";
        }

        private void AddCash_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && Double.Parse(dataGridView1[5, e.RowIndex].Value.ToString()) < 0)
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 229, 235);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(dataGridView1, "Баланс пилотов");
        }
    }
}
