using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DateTimeExtensions;

namespace Prokard_Timing
{
    public partial class PilotMonthRace : Form
    {
        AdminControl admin;
        public PilotMonthRace(AdminControl ad)
        {
            InitializeComponent();
            admin = ad;
            LastSundayFromYear(comboBox1,DateTime.Now.Year);
            admin.ShowMonthRace(dataGridView1,Convert.ToDateTime(comboBox1.Items[comboBox1.SelectedIndex].ToString()));
            labelSmooth2.Text = "Участников - " + dataGridView1.Rows.Count.ToString();

            button2.Enabled = dataGridView1.Rows.Count > 0;
        }

        private void LastSundayFromYear(ComboBox cb, int Year = 2011)
        {
            cb.Items.Clear();
            cb.BeginUpdate();
            for (int i = 1; i <= 12; i++)
            {
                cb.Items.Add (new DateTime(Year,i,1).Last(DayOfWeek.Sunday).ToString("dd MMMM yyyy"));
            }

            cb.SelectedIndex = cb.Items.IndexOf (DateTime.Now.Last(DayOfWeek.Sunday).ToString("dd MMMM yyyy"));

            cb.EndUpdate();
        }

        private void PilotMonthRace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            admin.ShowMonthRace(dataGridView1, Convert.ToDateTime(comboBox1.Items[comboBox1.SelectedIndex].ToString()));
            labelSmooth2.Text = "Участников - " + dataGridView1.Rows.Count.ToString();
            button2.Enabled = dataGridView1.Rows.Count > 0;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                PilotInfo form = new PilotInfo(Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells[0].Value.ToString()),admin);
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(dataGridView1, "Участники квалификации");
        }

       
    }
}
