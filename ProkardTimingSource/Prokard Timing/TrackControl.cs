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
    public partial class TrackControl : Form
    {
        MainForm parent;
        public TrackControl(MainForm P)
        {
            InitializeComponent();
            parent = P;
            parent.admin.ShowAllTracks(dataGridView1);

            if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows[0].Cells[3].Value.ToString().Length > 0)
            {
                if (System.IO.File.Exists(@dataGridView1.SelectedRows[0].Cells[3].Value.ToString()))
                {
                    pictureBox1.Image = Image.FromFile(@dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }

            // if (parent.admin.IS_ADMIN)
            {
                toolStrip1.Enabled = true;
            }
        }

        private void TrackControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddTrack form = new AddTrack(parent.admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            parent.admin.ShowAllTracks(dataGridView1);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                parent.admin.model.DelTrack(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                parent.admin.ShowAllTracks(dataGridView1);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (System.IO.File.Exists(@dataGridView1[3, e.RowIndex].Value.ToString()))
                
            if (e.RowIndex>=0 && dataGridView1[3, e.RowIndex].Value.ToString().Length > 0)
            {
                pictureBox1.Image = Image.FromFile(@dataGridView1[3, e.RowIndex].Value.ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                AddTrack form = new AddTrack(parent.admin,true,dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),dataGridView1.SelectedRows[0].Cells[1].Value.ToString(),dataGridView1.SelectedRows[0].Cells[3].Value.ToString(),dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
                parent.admin.ShowAllTracks(dataGridView1);
            }
        }
    }
}
