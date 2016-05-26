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
    public partial class ProgramUsers : Form
    {
        AdminControl admin;
        public ProgramUsers(AdminControl ad)
        {
            InitializeComponent();
            admin = ad;
            admin.ShowProgramUsers(dataGridView1);

            toolStripButton1.Enabled = toolStripButton2.Enabled = toolStripButton3.Enabled = admin.IS_SUPER_ADMIN;
        }

        private void ProgramUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddProgramUsers form = new AddProgramUsers(admin);
            form.Owner = this;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                admin.ShowProgramUsers(dataGridView1);
            }

            form.Dispose();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                AddProgramUsers form = new AddProgramUsers(admin, true, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), dataGridView1.SelectedRows[0].Cells[6].Value.ToString(), dataGridView1.SelectedRows[0].Cells[5].Value.ToString(), dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
                form.Owner = this;
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    admin.ShowProgramUsers(dataGridView1);
                }

                form.Dispose();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (admin.USER_ID == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)) MessageBox.Show("Самого себя удалять нельзя!");
                else
                {
                    admin.model.DelProgramUsers(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    admin.ShowProgramUsers(dataGridView1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(dataGridView1, "Пользователи программы");
        }
    }
}
