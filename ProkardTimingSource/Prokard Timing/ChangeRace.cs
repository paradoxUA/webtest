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
    public partial class ChangeRace : Form
    {
        AdminControl admin;
        RaceClass Race;
        string PilotID;
        string MemberID;
        public ChangeRace(AdminControl ad, string PID, string MID, RaceClass R)
        {
            InitializeComponent();
            admin = ad;
            PilotID = PID;
            MemberID = MID;
            Race = R;
            LoadData();

            labelSmooth4.Text = "№" + (Race.RaceNum > admin.DopRace ? (Race.RaceNum - admin.DopRace).ToString() + "a" : Race.RaceNum.ToString());
            labelSmooth4.Text += "   " + Race.Hour + ":" + Race.Minute;
        }

        private void ChangeRace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadData()
        {
            Hashtable User = admin.model.GetPilot(Convert.ToInt32(PilotID));
            if (User.Count > 0)
                labelSmooth2.Text = User["surname"].ToString() + " " + User["name"].ToString();

            comboBox1.Items.Clear();
            comboBox1.BeginUpdate();
            for (int i = DateTime.Now.Hour; i <= Convert.ToInt32(admin.Settings["time_end"]); i++)
            {
                comboBox1.Items.Add(i);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
            comboBox1.EndUpdate();
            LoadSubRace(Convert.ToInt32(comboBox1.Items[0]));
        }

        private void LoadSubRace(int hour)
        {
            comboBox2.Items.Clear();
            comboBox2.BeginUpdate();

            string Hour = Race.Hour.ToString(); //comboBox1.Items[comboBox1.SelectedIndex].ToString();
            int RMin = Convert.ToInt32(Race.Minute.ToString());

			var minDateTime = DateTime.Now;
			var minutes = new int[] { 0, 15, 30, 45 };

			var dateTimes = minutes.Select(m => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, m, 0))
				.Where(dt => minDateTime < dt).ToArray();
			foreach (var item in dateTimes)
			{
				comboBox2.Items.Add(item.ToString("HH:mm"));
			}

			/*
            if (Hour == DateTime.Now.Hour.ToString() && RMin < 15 && RMin != 0) comboBox2.Items.Add(Hour + ":00"); else if (Hour != DateTime.Now.Hour.ToString()) comboBox2.Items.Add(comboBox1.Text + ":00");
            if (Hour == DateTime.Now.Hour.ToString() && RMin < 30 && RMin != 15) comboBox2.Items.Add(Hour + ":15"); else if (Hour != DateTime.Now.Hour.ToString()) comboBox2.Items.Add(comboBox1.Text + ":15");
            if (Hour == DateTime.Now.Hour.ToString() && RMin < 45 && RMin != 30) comboBox2.Items.Add(Hour + ":30"); else if (Hour != DateTime.Now.Hour.ToString()) comboBox2.Items.Add(comboBox1.Text + ":30");
            if (Hour == DateTime.Now.Hour.ToString() && RMin < 60 && RMin != 45) comboBox2.Items.Add(Hour + ":45"); else if (Hour != DateTime.Now.Hour.ToString()) comboBox2.Items.Add(comboBox1.Text + ":45");
            */
            comboBox2.EndUpdate();

            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;

            button2.Enabled = comboBox2.Items.Count > 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubRace(Convert.ToInt32(comboBox1.Items[comboBox1.SelectedIndex]));
        }

        private void button2_Click(object sender, EventArgs e)
        {


            int I = Convert.ToInt32(comboBox1.Items[comboBox1.SelectedIndex].ToString());
            int J = -1;
            string Time = comboBox2.Items[comboBox2.SelectedIndex].ToString();

            if (Time.IndexOf("00") > 0) J = 1;
            else
                if (Time.IndexOf("15") > 0) J = 2;
                else
                    if (Time.IndexOf("30") > 0) J = 3;
                    else
                        if (Time.IndexOf("45") > 0) J = 4;

            int RaceID = 0;
            if (J > 0)
            {
                if (admin.Races[I, J].Status == 0 && admin.CreateRace(I, J))
                {
                    admin.ReloadTable();
                    RaceID = admin.Races[I, J].RaceID;
                }
                else
                    RaceID = admin.Races[I, J].RaceID;

                admin.model.ChangePilotRace(MemberID, RaceID.ToString());
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            this.Close();
        }
    }
}
