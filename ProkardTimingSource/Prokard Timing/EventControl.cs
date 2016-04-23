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
    public partial class EventControl : Form
    {
        MainForm parent;
        int lasttp = 1; // 0 = все, 1 = будущие, 2 = прошедшие, 3 - только на какую-то дату

        public EventControl(MainForm P)
        {
            InitializeComponent();
            parent = P;
            parent.admin.ShowEvents(dataGridView1, 2, DateTime.Now, 1);
        }

        private void EventControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddEvent form = new AddEvent(parent.admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            parent.admin.ShowEvents(dataGridView1, 2, DateTime.Now, lasttp);
        }

        /*
        // прошедшие
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            parent.admin.ShowEvents(dataGridView1, 2, DateTime.Now, 2);
            lasttp = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            parent.admin.ShowEvents(dataGridView1, 2, DateTime.Now, 1);
            lasttp = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            parent.admin.ShowEvents(dataGridView1, 2, DateTime.Now, 3);
            lasttp = 0;
        }
         */

        // редактировать событие
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return;
            }

            AddEvent form = new AddEvent(parent.admin, true, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            parent.admin.ShowEvents(dataGridView1, 2, DateTime.Now, lasttp);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                parent.admin.model.DelMessage(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                parent.admin.ShowEvents(dataGridView1, 2, DateTime.Now, lasttp);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowEventMessage form = new ShowEventMessage(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),parent.admin);
            form.ShowDialog();
            form.Dispose();
        }

       

        private void EventType_radioButton_Click(object sender, EventArgs e)
        {
            if (AllEvents_radioButton.Checked)
            {
                lasttp = 0;
            }
            else if (futureEvents_radioButton.Checked)
            {
                lasttp = 1;
            }
            else if (formerEvents_radioButton.Checked)
            {
                lasttp = 2;
            }

            parent.admin.ShowEvents(dataGridView1, 2, DateTime.Now, lasttp);
        }
    }
}
