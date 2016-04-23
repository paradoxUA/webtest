using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
namespace Prokard_Timing
{
    public partial class KartsControl : Form
    {

        MainForm parent;
        public KartsControl(MainForm P)
        {
            InitializeComponent();
            //  dataGridView1.DoubleBuffered(true);
            parent = P;
            parent.admin.ShowKarts(dataGridView1);
             toolStripButton3.Enabled = toolStripButton4.Enabled = toolStripButton5.Enabled = parent.admin.IS_ADMIN;
             toolStripButton8.Visible = parent.admin.IS_SUPER_ADMIN;
            //toolStrip1.Enabled = dataGridView1.Rows.Count > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddKart form = new AddKart(parent.admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            parent.admin.ShowKarts(dataGridView1);
            //toolStrip1.Enabled = dataGridView1.Rows.Count > 0;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                parent.admin.model.DelKart(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            AddKart form = new AddKart(parent.admin, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            parent.admin.ShowKarts(dataGridView1);
           
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int index = dataGridView1.SelectedCells[0].RowIndex;
                Hashtable message = parent.admin.model.GetMessageFromID(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                RepeirKart form = new RepeirKart(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), parent.admin, dataGridView1.SelectedRows[0].Cells[5].Value.Equals("True") ? 1 : 0, message.Count > 0 ? message["message"].ToString() : "");
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
                parent.admin.ShowKarts(dataGridView1);
                parent.admin.MaxKarts = parent.admin.model.GetMaxKarts();
                dataGridView1.Rows[index].Selected = true;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[7].Value))
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PaleGreen;

                } else

                if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].Value))
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;

                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                richTextBox1.Text = String.Empty;
                richTextBox1.Text += parent.admin.model.GetAllKartsMessages(Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

                richTextBox2.Text = String.Empty;
                richTextBox2.Text = parent.admin.model.GetKartFuelHistory(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                if (Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[7].Value))
                {
                    toolStripButton9.Text = "Убрать со стоянки";
                    toolStripButton9.Image = global::Prokard_Timing.Properties.Resources.house_go;
                }
                else
                {
                    toolStripButton9.Text = "Поставить на стоянку";
                    toolStripButton9.Image = global::Prokard_Timing.Properties.Resources.house;
                }
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            DayStatistic form = new DayStatistic(parent.admin, 4);
            form.Owner = this;
            form.ShowDialog();
            this.Close();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Kartinfo form = new Kartinfo(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), parent.admin);
                form.Owner = this;
                form.ShowDialog();
                parent.admin.ShowKarts(dataGridView1);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton7.PerformClick();
        }

        private void KartsControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                parent.admin.model.SetKartWait(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                    dataGridView1.SelectedRows[0].Cells[7].Value.Equals("True") ? "0" : "1");
                parent.admin.ShowKarts(dataGridView1);
                dataGridView1.SelectedRows[0].Selected = false;
                dataGridView1.Rows[index].Selected = true;
            }
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        { if (dataGridView1.Rows.Count > 0)
            {
                int r = dataGridView1.SelectedCells[0].RowIndex;
                AddFuel form = new AddFuel(parent.admin, dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    parent.admin.ShowKarts(dataGridView1);
                    dataGridView1.Rows[r].Selected = true;
                }

                form.Dispose();
        }}
    }
}


