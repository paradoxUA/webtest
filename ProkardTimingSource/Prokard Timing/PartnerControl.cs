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
	public partial class PartnerControl : Form
	{
		AdminControl admin;
		public PartnerControl(AdminControl ad)
		{
			InitializeComponent();
			admin = ad;
			admin.ShowPartners(dataGridView1);
			UpdateControls();
		}

		private void UpdateControls()
		{
			toolStripButton1.Enabled = admin.IS_ADMIN;
			toolStripButton2.Enabled = admin.IS_ADMIN && dataGridView1.SelectedRows.Count > 0;
			toolStripButton3.Enabled = admin.IS_ADMIN && dataGridView1.SelectedRows.Count > 0;
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
			var form = new AddPartner(admin);
			form.Owner = this;
			form.ShowDialog();
			admin.ShowPartners(dataGridView1);
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count == 0)
			{
				return;
			}
			if(MessageBox.Show(this, "Вы уверены, что хотите удалить партнера?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			var id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
			admin.model.RemovePartner(id);
			admin.ShowPartners(dataGridView1);
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			var id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
			var name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
			var commission = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[2].Value).ToString("F2");

			var form = new AddPartner(admin, id, name, commission);
			form.Owner = this;
			form.ShowDialog();
			admin.ShowPartners(dataGridView1);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			admin.PrintDataGridView(dataGridView1, "Партнеры");
		}

		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}
	}
}
