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
	public partial class AddPartner : Form
	{
		AdminControl admin;
		private int? id;

		public AddPartner(AdminControl ad, int? id = null, string name = null, string commission = null)
		{
			InitializeComponent();
			admin = ad;

			this.id = id;

			textBox1.Text = name ?? string.Empty;
			textBox2.Text = commission ?? "0.00";
		}

		private void AddGroup_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 27) this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (textBox1.Text.Length == 0)
			{
				MessageBox.Show(@"Ошибка! Название не может быть пустым");
				return;
			}
			if(!float.TryParse(textBox2.Text.Replace(".", ","), out var result))
			{
				MessageBox.Show(@"Ошибка! Комиссия не соответсвует формату");
				return;
			}
			admin.UpdatePartner(id, textBox1.Text, result);
			Close();
		}
	}
}
