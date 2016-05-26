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
    public partial class GroupControl : Form
    {
        AdminControl admin;
        public GroupControl(AdminControl ad)
        {
            InitializeComponent();
            admin = ad;
            admin.ShowGroups(dataGridView1);
            toolStripButton1.Enabled = toolStripButton2.Enabled = toolStripButton3.Enabled = admin.IS_ADMIN;

            button2.Enabled = dataGridView1.Rows.Count > 0;
        }

        private void GroupControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddGroup form = new AddGroup(admin);
            form.Owner = this;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                admin.ShowGroups(dataGridView1);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                admin.model.DelGroup(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                admin.ShowGroups(dataGridView1);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                AddGroup form = new AddGroup(admin, true, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                form.Owner = this;

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    admin.ShowGroups(dataGridView1);
                }


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(dataGridView1, "Группы пользователей");
        }
    }
}
