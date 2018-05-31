using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Rentix
{
    public partial class Kartinfo : Form
    {
        string KartNum = "";
        string KartID = "";
        AdminControl admin;
        public Kartinfo(string Num, AdminControl ad)
        {
            InitializeComponent();
            KartNum = Num;
            admin = ad;
            KartID = admin.model.GetKartID(Num).ToString();
            // Текстовые сообщения
            richTextBox1.Text = String.Empty;
            richTextBox1.Text += admin.model.GetAllKartsMessages(Convert.ToInt32(KartID), fromDateTimePicker.Value, toDateTimePicker.Value); //sgavrilenko похоже, что тут муть. в таблице перемешаны сообщения про карты и про пилотов

			fromDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			toDateTimePicker.Value = DateTime.Now;

			ShowStatistic();
        }

        private void ShowStatistic()
        {
            Hashtable Kart = admin.model.GetKart(Convert.ToInt32(KartID));
            labelSmooth13.Text = Kart["name"].ToString();
            labelSmooth15.Text = Kart["number"].ToString();
            labelSmooth17.Text = Kart["transponder"].ToString();

            // Статистика
            labelSmooth10.Text = admin.model.GetKartRepairs(KartID, fromDateTimePicker.Value, toDateTimePicker.Value).ToString();

            Hashtable Stat = admin.model.GetKartStatistic(KartID,Convert.ToDouble(admin.Settings["track_length"]), fromDateTimePicker.Value, toDateTimePicker.Value);

            labelSmooth4.Text = Stat["races"].ToString();
            labelSmooth5.Text = (Convert.ToDouble(Stat["length"])/1000).ToString() + " км";
            labelSmooth6.Text = admin.model.GetKartFuel(KartID, fromDateTimePicker.Value, toDateTimePicker.Value) + " л";

            richTextBox2.Text = String.Empty;
            richTextBox2.Text += admin.model.GetKartFuelHistory(KartID, fromDateTimePicker.Value, toDateTimePicker.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void Kartinfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RepeirKart form = new RepeirKart(KartNum, admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            admin.MaxKarts = admin.model.GetMaxKarts();

            // Текстовые сообщения
            richTextBox1.Text = String.Empty;
            richTextBox1.Text += admin.model.GetAllKartsMessages(Convert.ToInt32(KartID), fromDateTimePicker.Value, toDateTimePicker.Value);
            ShowStatistic();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AddFuel form = new AddFuel(admin, labelSmooth15.Text, KartID);

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ShowStatistic();
                }

                form.Dispose();
         }

		private void fromDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			ShowStatistic();
		}

		private void toDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			ShowStatistic();
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show(this, "Вы уверены, что хотите выполнить сброс статистики пробега и топлива. Данные не подлежат восстановлению", "Сброс статистики", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if(result != DialogResult.Yes)
			{
				return;
			}
			admin.model.DropFuel(Convert.ToInt32(KartID));
			admin.model.DropKartKmStats(Convert.ToInt32(KartID));
			ShowStatistic();
		}
	}
}
