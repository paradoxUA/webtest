using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Rentix;
using Rentix.Controls;
using System.Diagnostics;

namespace Rentix
{
    public partial class DayStatistic : Form
    {
        AdminControl admin;
        comboBoxItem ci = new comboBoxItem("", -1);

        PageLister Pages;

        
        public DayStatistic(AdminControl ad, int page)
        {
            InitializeComponent();
           

            admin = ad;

            #region pagelister для Журнала заездов

            Pages = new PageLister(pageNumber_toolStripComboBox, firstPage_toolStripButton11, previousPage_toolStripButton12, nextPage_toolStripButton9, lastPage_toolStripButton10);
            
            Pages.PageSize = 25;
            Pages.CurrentPageNumber = 1;            
            pageSize_toolStripComboBox2.SelectedIndex = pageSize_toolStripComboBox2.Items.IndexOf("25");

            #endregion


            tabControl1.SelectedIndex = page;

           // dateTimePicker1.Value = dateTimePicker2.Value = DateTime.Now.Date;
            ci = new comboBoxItem("", -1);
            tracks_comboBox1.Items.Add(ci);
            List<Hashtable> Tracks = admin.model.GetAllTracks();
           

            for (int i = 0; i < Tracks.Count; i++)
            {
                ci = new comboBoxItem(Convert.ToString(Tracks[i]["name"]), 
                    Convert.ToInt32(Tracks[i]["id"]));

                tracks_comboBox1.Items.Add(ci);
               // tracks_comboBox1.Items.Add(Tracks[i]["name"]);
            }

			comboBoxItem.selectComboBoxValueById(tracks_comboBox1, 
                Convert.ToInt32(ad.Settings["default_track"]));

          //  tracks_comboBox1.SelectedIndex = 0;

            /*
            ci.selectComboBoxValueById(eventsInRacesJournal_comboBox,
               Convert.ToInt32(ad.Settings["default_track"]));
             */

           //trackInRacesJournal_comboBox.SelectedIndex = 0;
            switch (page)
            {
                case 0: GetDaySatistic(); break;
                case 1: /*admin.ShowRaceJurnal(Pages, dataGridView1, dateTimePicker2.Value, dateTimePicker5.Value, eventsInRacesJournal_comboBox.SelectedIndex); RaceJurnalCalculate();*/ break;
                case 2: FillBestResults(); break;
                case 3: GetAllUsersStatistic(); break;
                case 4: GetAllKartStatistic(); break;
            }


            if (!admin.IS_SUPER_ADMIN)
            {
                tabPage5.Parent = tabControl2_del;
                tabPage7.Parent = tabControl2_del;
                tabPage8.Parent = tabControl2_del;
            }

			var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			dateTimePicker6.Value = firstDayOfMonth;
			dateTimePicker7.Value = firstDayOfMonth.AddMonths(1).AddDays(-1).Add(new TimeSpan(23,59,59));
		}

        private void RaceJurnalCalculate()
        {
            int st = 0, en = 0, val;
            for (int i = 0; i < racesJournal_dataGridView1.Rows.Count; i++)
            {
                val = Convert.ToInt16(racesJournal_dataGridView1[1, i].Value);
                if (val == 10) st++; else if (val == 12) en++;
            }

            labelSmooth16.Text = "Созданных:   " + st.ToString();
            labelSmooth17.Text = "Завершенных:   " + en.ToString();
            labelSmooth18.Text = "Записей:   " + racesJournal_dataGridView1.Rows.Count.ToString();
        }

        private void DayStatistic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // выбрана другая траса на закладке Лучшее время
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (tabControl1.SelectedIndex == 2)
			{
				FillBestResults();
			}
        }




        

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: GetDaySatistic(); break;
                case 1: /*if (dataGridView1.Rows.Count == 0) { admin.ShowRaceJurnal(Pages, dataGridView1, 
                    dateTimePicker2.Value,
                    dateTimePicker5.Value, 
                    eventsInRacesJournal_comboBox.SelectedIndex);
                    RaceJurnalCalculate(); } */ break;
                case 2: FillBestResults(); break;
                case 3: GetAllUsersStatistic(); break;
                case 4: if (dataGridView3.Rows.Count == 0) GetAllKartStatistic(); break;
                case 5: if (unregisteredDetection_dataGridView4.Rows.Count == 0)
                    {
                        Detect();
                    }
                    break;
                case 6: if (dataGridView5.Rows.Count == 0) GetLogins(); break;
                case 7: fillPetroleumStat(); break;
                case 8: fillDailyStat(); break;

            }
        }

		private void FillBestResults()
		{
			admin.ShowBestResults(top40_dataGridView, comboBoxItem.getSelectedValue(tracks_comboBox1), top40Record_labelSmooth12, true, dateTimePicker6.Value, dateTimePicker7.Value, Convert.ToInt32(numericUpDown1.Value));
		}


        private void fillPetroleumStat()
        {
            petroleum_dataGridView1.Rows.Clear();

           IEnumerable<model.Petroleum> petroleumStat = admin.model.getPetroleum(datetimeConverter.toStartDateTime(petroleumStatStartDate_dateTimePicker7.Value),
                datetimeConverter.toEndDateTime(petroleumStatEndDate_dateTimePicker6.Value));
       
            double totalLitres = 0;
            double totalPrice = 0;

            for(int i =0; i < petroleumStat.Count(); i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(petroleum_dataGridView1);

                dr.Cells[0].Value = petroleumStat.ElementAt(i).Id;
                dr.Cells[1].Value = petroleumStat.ElementAt(i).Date.ToString();
                dr.Cells[2].Value = petroleumStat.ElementAt(i).litres;
                dr.Cells[3].Value = petroleumStat.ElementAt(i).Price;
                dr.Cells[4].Value = "[" + petroleumStat.ElementAt(i).program_users.login + "] " + petroleumStat.ElementAt(i).program_users.name + " " + petroleumStat.ElementAt(i).program_users.surname; 
               
                petroleum_dataGridView1.Rows.Add(dr);

                totalLitres += petroleumStat.ElementAt(i).litres;
                totalPrice += Convert.ToDouble(petroleumStat.ElementAt(i).Price);
            }
        
            totalPetroleum_labelSmooth23.Text = " Заправлено литров за период: " + totalLitres + ", на сумму: " + totalPrice + " грн";

        }

        private void fillDailyStat()
        {
            DateTime startDate = datetimeConverter.toStartDateTime(dailyStatistic_dateTimePicker6.Value);
            DateTime endDate = datetimeConverter.toEndDateTime(dailyStatistic_dateTimePicker6.Value);

            string report = "";



            dailyStatistic_webBrowser1.DocumentText = report;
          
           
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (Convert.ToInt16(racesJournal_dataGridView1[1, e.RowIndex].Value) == 11)
                {
                    racesJournal_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 252, 224);
                }
                else if (Convert.ToInt16(racesJournal_dataGridView1[1, e.RowIndex].Value) == 12)
                    {
                        racesJournal_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(226, 255, 224);
                    }
                else if (Convert.ToInt16(racesJournal_dataGridView1[1, e.RowIndex].Value) == 14)
                {
                    racesJournal_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(216, 225, 204);
                }

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GetDaySatistic();
        }

        private void GetDaySatistic()
        {
            Hashtable DayStat = admin.model.GetDayStatistic(dateTimePicker1.Value);

            labelSmooth8.Text = DayStat["tickets"].ToString();
            cashForToday_labelSmooth9.Text = (DayStat["cash"].ToString().Length == 0 ? "0" : DayStat["cash"].ToString()) + " грн";
            labelSmooth30.Text = (DayStat["virtual"].ToString().Length == 0 ? "0" : DayStat["virtual"].ToString()) + " грн";
            cashTotal_labelSmooth32.Text = admin.model.GetCashFromCassa(DateTime.Now,false,false,true,true) + " грн";
            labelSmooth10.Text = DayStat["users_new"].ToString();
            labelSmooth19.Text = DayStat["users_bann"].ToString();
            deletedPilotsCount_labelSmooth12.Text = DayStat["users_deleted"].ToString();
            totalRaces_label.Text = DayStat["races"].ToString();
            labelSmooth21.Text = DayStat["races_day"].ToString();
            finishedRacesForToday_label.Text = DayStat["races_end"].ToString();
            unfinishedRacesForToday_label.Text = DayStat["races_fail"].ToString();
        }

        private void GetAllUsersStatistic()
        {

            Hashtable DayStat = admin.model.GetUsersStatistic();

            totalPilots_label.Text = DayStat["users_all"].ToString();
            bannedPilots_labelSmooth24.Text = DayStat["users_bann"].ToString();
            totalPilotsBallance_labelSmooth28.Text = (DayStat["cash"].ToString().Length == 0 ? "0" : DayStat["cash"].ToString()) + " грн";

            totalAmountOfDeletedPilots_labelSmooth12.Text = DayStat["users_deleted"].ToString();

        }

        private void GetAllKartStatistic()
        {
            if (!admin.IS_SUPER_ADMIN)
            {
                tabPage5.Parent = tabControl2_del;
            }
            else
            {

                admin.ShowAllKartStatistic(dataGridView3, dateTimePicker3.Value, dateTimePicker4.Value);
                double all = 0;
                double isp = 0;

                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (dataGridView3[6, i].Value != null)
                        all += Convert.ToDouble(dataGridView3[6, i].Value.ToString());

                    if (dataGridView3[7, i].Value != null)
                        isp += Convert.ToDouble(dataGridView3[7, i].Value.ToString());
                }

                filledForPeriod_label.Text = "Залито:   " + all.ToString() + " л";
                usedForPeriod_label.Text = "Использовано:   " + isp.ToString() + " л";

            }
        }

        private void GetLogins()
        {
            if (admin.IS_SUPER_ADMIN)
            {
                List<Hashtable> log = admin.model.GetUserLogins(dateTimePicker8.Value);
                dataGridView5.Rows.Clear();

                for (int i = 0; i < log.Count; i++)
                {
                    dataGridView5.Rows.Add();

                    dataGridView5[0, i].Value = log[i]["stat"];
                    dataGridView5[1, i].Value = log[i]["user_id"].ToString() == "0" ? "system" : admin.model.GetProgramUserName(log[i]["user_id"].ToString());
                    dataGridView5[2, i].Value = log[i]["stat"].ToString() == "0" ? "Выход" : "Вход";
                    dataGridView5[3, i].Value = Convert.ToDateTime(log[i]["created"]).ToString("yyyy-MM-dd HH:mm");
                }
            }
        }

        private void Detect()
        {
            if (admin.IS_SUPER_ADMIN)
            {
                List<Hashtable> log = admin.model.GetTransponderDetect(startDate_dateTimePicker6.Value, endDate_dateTimePicker6.Value);
                unregisteredDetection_dataGridView4.Rows.Clear();

                int[,] Det = new int[30000, 11];

                int rt, tt;
                for (int i = 0; i < log.Count; i++)
                {
                    rt = Convert.ToInt32(log[i]["race_id"]);
                    tt = Convert.ToInt32(log[i]["transponder"]);
                    Det[rt, tt]++;
                }

                int rows = 0;
                DateTime dt;
                for (int i = 0; i < 30000; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        if (Det[i, j] > 1)
                        {
                            dt = admin.model.GetRaceDateTime(i);
                            unregisteredDetection_dataGridView4.Rows.Add();
                            unregisteredDetection_dataGridView4[0, rows].Value = i;
                            unregisteredDetection_dataGridView4[1, rows].Value = i;
                            unregisteredDetection_dataGridView4[2, rows].Value = dt.ToString("dd MMMM");
                            unregisteredDetection_dataGridView4[3, rows].Value = dt.ToString("HH:mm");
                            unregisteredDetection_dataGridView4[4, rows].Value = j;
                            unregisteredDetection_dataGridView4[5, rows].Value = Det[i, j];

                            rows++;
                        }
                    }
                }

                /*
                for (int i = 0; i < log.Count; i++)
                {
                    dataGridView4.Rows.Add();

                    dataGridView4[0, i].Value = log[i]["id"];
                    dataGridView4[1, i].Value = log[i]["transponder"];
                    dataGridView4[2, i].Value = log[i]["race_id"];
                    dataGridView4[3, i].Value = Convert.ToDateTime(log[i]["created"]).ToString("yyyy-MM-dd HH:mm");
                }*/
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            GetAllKartStatistic();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(dataGridView3, "Статистика по картам " + dateTimePicker3.Value.ToString("dd MMMM") + " - " + dateTimePicker4.Value.ToString("dd MMMM"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(top40_dataGridView, "Лучшие результаты по трассе '" + tracks_comboBox1.SelectedItem + "' (с)  Rentix Timing Software");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(racesJournal_dataGridView1, "Журнал заездов  " + dateTimePicker2.Value.ToString("dd MMMM"));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(unregisteredDetection_dataGridView4, "Детекция транспондеров " + startDate_dateTimePicker6.Value.ToString("dd MMMM"));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(dataGridView5, "Логины пользователей " + dateTimePicker8.Value.ToString("dd MMMM"));
        }

        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {
            GetLogins();
        }

        private void dataGridView5_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView5[0, e.RowIndex].Value != null && Convert.ToInt16(dataGridView5[0, e.RowIndex].Value) == 0)
                {
                    dataGridView5.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 253);
                }
                else
                {

                    dataGridView5.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(226, 255, 204);
                }

            }
        }

        #region диапазон дат для детекции
        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            Detect();
        }

          private void endDate_dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
             Detect();
        }

        #endregion

          private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyData == Keys.C) CopyToClip(top40_dataGridView);
        }

        private void CopyToClip(DataGridView dv) {

            //dv.SelectAll();

            DataObject LVLdata = dv.GetClipboardContent();

            Clipboard.SetDataObject(LVLdata, true);
        
        }


        #region Журнал заездов





        // построить отчёт
        private void createReport_button_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                Pages.CurrentPageNumber = 1;
                buildReport();
            }

        }


        private void buildReport()
        {
            borderPanel3.Enabled = false;

            admin.ShowRaceJurnal(Pages, racesJournal_dataGridView1, 
                new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, dateTimePicker5.Value.Day).AddDays(1).AddMilliseconds(-1),
                eventsInRacesJournal_comboBox.SelectedIndex);

            Application.DoEvents();        

            RaceJurnalCalculate();

            borderPanel3.Enabled = true;

        }

        #endregion


        private void firstPage_toolStripButton11_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.First();
                buildReport();
            }
        }

        private void previousPage_toolStripButton12_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.Prev();
                buildReport();
            }
        }

        private void pageNumber_toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.ToPage(Convert.ToInt32(pageNumber_toolStripComboBox.Items[pageNumber_toolStripComboBox.SelectedIndex].ToString()));
                buildReport();
            }
        }

        private void nextPage_toolStripButton9_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.Next();
                buildReport();
            }
        }

        private void lastPage_toolStripButton10_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.Last();
                buildReport();
            }
        }

        private void pageSize_toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                if (pageSize_toolStripComboBox2.Items[pageSize_toolStripComboBox2.SelectedIndex].ToString() == "Все")
                {
                    Pages.PageSize = Int32.MaxValue;
                }
                else
                {
                    Pages.PageSize = Convert.ToInt32(pageSize_toolStripComboBox2.Items[pageSize_toolStripComboBox2.SelectedIndex].ToString());
                }
                Pages.OnChange = true;
                Pages.CurrentPageNumber = 1;
                buildReport();
                Pages.FillPageNumbers();

                
            }
        }

        // удалить круг и записать событие в журнал
        private void deleteLap_button_Click(object sender, EventArgs e)
        {
            if (top40_dataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            BusyForm busyForm = new BusyForm("Удаление круга", 1);
            busyForm.Show();

            for (int i = 0; i < top40_dataGridView.SelectedRows.Count; i++)
            {
                int idLap = Convert.ToInt32(top40_dataGridView.SelectedRows[i].Cells[0].Value);
                string lapTime = top40_dataGridView.SelectedRows[i].Cells[5].Value.ToString();
                string pilotName = top40_dataGridView.SelectedRows[i].Cells[3].Value.ToString();

                admin.model.delLap(idLap, pilotName, admin.User_Name, lapTime);
                busyForm.SetProgressValue(1);                
            }           

            FillBestResults();;

            busyForm.CloseForm();

            if (top40_dataGridView.Rows.Count > 0)
            {
                top40_dataGridView.ClearSelection();
            }
        }

        private void petroleumStatStartDate_dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            fillPetroleumStat();
        }

        private void petroleumStatEndDate_dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            fillPetroleumStat();
        }

            


        private void deletePilotStat_button_Click(object sender, EventArgs e)
        {
            if (top40_dataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            for (int i = 0; i < top40_dataGridView.SelectedRows.Count; i++)
            {
                int idPilot = Convert.ToInt32(top40_dataGridView.SelectedRows[i].Cells[9].Value); 
                admin.model.delPilotStatistic(idPilot, admin.User_Name);
            }

            FillBestResults();;

            if (top40_dataGridView.Rows.Count > 0)
            {
                top40_dataGridView.ClearSelection();
            }
        }

        private void deletePilotStatForThisTrack_button_Click(object sender, EventArgs e)
        {
            if (top40_dataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            for (int i = 0; i < top40_dataGridView.SelectedRows.Count; i++)
            {
                int idPilot = Convert.ToInt32(top40_dataGridView.SelectedRows[i].Cells[9].Value);
                int trackId = comboBoxItem.getSelectedValue(tracks_comboBox1);
                admin.model.delPilotStatisticForSomeTrack(idPilot, admin.User_Name, trackId);
            }

            FillBestResults();;

            if (top40_dataGridView.Rows.Count > 0)
            {
                top40_dataGridView.ClearSelection();
            }
        }

		private void dateTimePicker6_ValueChanged_1(object sender, EventArgs e)
		{
			FillBestResults();
		}

		private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
		{
			FillBestResults();
		}

		private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			var selectedKartId = "";
			if(dataGridView3.SelectedRows.Count > 0)
			{
				var selectedRow = dataGridView3.SelectedRows[0].Index;
				selectedKartId = dataGridView3[1, selectedRow].Value.ToString();
			}
			if(selectedKartId == string.Empty || selectedKartId == null)
			{
				return;
			}
			Kartinfo form = new Kartinfo(selectedKartId, admin);
			form.Owner = this;
			form.ShowDialog();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			FillBestResults();
		}
	}
}
