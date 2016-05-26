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
    public partial class Sertificat : Form
    {
        AdminControl admin;
        int Findex = 0;

        public Sertificat(AdminControl ad)
        {
            InitializeComponent();
            admin = ad;

            admin.ShowCertificateType(dataGridView1,radioButton6.Checked?"1":"0");
                
            admin.ShowCertificate(dataGridView2, Findex);

            if (dataGridView1.Rows.Count > 0)
            {
                toolStripButton2.Enabled = toolStripButton3.Enabled = true;
            }
            else
            {
                toolStripButton2.Enabled = toolStripButton3.Enabled = false;
            }

        }

        private void Sertificat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int certificateType = 1;
            if (radioButton6.Checked)
            {
                certificateType = 1;
            }

            AddSertificat form = new AddSertificat(admin, certificateType);
            form.Owner = this;
            form.ShowDialog();
            admin.ShowCertificateType(dataGridView1, radioButton6.Checked ? "1" : "0");
            toolStripButton2.Enabled = toolStripButton3.Enabled = dataGridView1.Rows.Count > 0;
            form.Dispose();
        }

        // редактирование
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            int index = dataGridView1.SelectedCells[0].RowIndex;

            int certificateType = 0;
            if (radioButton6.Checked)
            {
                certificateType = 1;
            }

            AddSertificat form = new AddSertificat(admin, certificateType, true, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            form.Owner = this;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                admin.ShowCertificateType(dataGridView1, radioButton6.Checked ? "1" : "0");
                
                if (dataGridView1.Rows.Count > 0)
                {
                    toolStripButton2.Enabled = toolStripButton3.Enabled = true;

                    if (dataGridView1.Rows.Count > index)
                    {
                        dataGridView1.Rows[index].Selected = true;
                    }
                }
                else
                {
                    toolStripButton2.Enabled = toolStripButton3.Enabled = false;
                }

                
            }

            form.Dispose();
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            admin.model.DelCertificateType(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            toolStripButton2.Enabled = toolStripButton3.Enabled = dataGridView1.Rows.Count > 0;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Findex = 0;
            admin.ShowCertificate(dataGridView2, Findex);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Findex = 1;
            admin.ShowCertificate(dataGridView2, Findex);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Findex = 2;
            admin.ShowCertificate(dataGridView2, Findex);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Findex = 3;
            admin.ShowCertificate(dataGridView2, Findex);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            admin.ShowCertificateType(dataGridView1, radioButton6.Checked ? "1" : "0");
            setButtonsActivity();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            admin.ShowCertificateType(dataGridView1, radioButton6.Checked ? "1" : "0");
            setButtonsActivity();
        }

        private void setButtonsActivity()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                toolStripButton2.Enabled = toolStripButton3.Enabled = true;
            }
            else
            {
                toolStripButton2.Enabled = toolStripButton3.Enabled = false;
            }
        }

    }
}
