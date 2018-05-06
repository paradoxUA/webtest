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

namespace Rentix
{
    public partial class TestDataFiller : Form
    {

        AdminControl admin;
        comboBoxItem ci = new comboBoxItem("", -1);

        public TestDataFiller(AdminControl admin)
        {
            this.admin = admin;
            InitializeComponent();
            ShowGroups();
            fillTracks();

            startDateEvent_dateTimePicker1.Value = DateTime.Now.AddDays(-3);

        }

        private void fillTracks()
        {
            List<Hashtable> Tracks = admin.model.GetAllTracks();
            for (int i = 0; i < Tracks.Count; i++)
            {
                ci = new comboBoxItem(Convert.ToString(Tracks[i]["name"]),
                    Convert.ToInt32(Tracks[i]["id"]));              

                tracks_comboBox.Items.Add(ci);
            }
        }


        private void ShowGroups()
        {
            List<string> Groups = admin.model.GetAllGroupsName();
            for (int i = 0; i < Groups.Count; i++)
                comboBox1.Items.Add(Groups[i]);

            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }


        private void addPilots()
        {
            for (int i = 0; i < 2000; i++)
            {
                Hashtable data = new Hashtable();

                data["name"] = "testPilotName " + i.ToString(); // textBox2.Text + " " + i + " " + DateTime.Now.ToString();
                data["surname"] = "testPilotSurname " + i.ToString();
                data["nickname"] = "testPilotNick " + i.ToString();
                data["birthday"] = datetimeConverter.toDateTimeString(DateTime.Now.AddYears(-20));
                data["gender"] = 0;
                data["email"] = "testPilot" + i.ToString() + "@email";
                data["tel"] = i;
                data["group"] = admin.model.GetGroupID(comboBox1.Items[comboBox1.SelectedIndex].ToString());
                data["banned"] = false;
                data["barcode"] = i.ToString();

                status_label.Text = i.ToString();
                admin.model.AddNewPilot(data);
                Application.DoEvents();
            }    
        }


        private void addTestPilots_button_Click(object sender, EventArgs e)
        {
            addPilots();   
        }

        private void AddTestRaces_button_Click(object sender, EventArgs e)
        {
            //addPilots();   

            // 96 заездов по 15 минут добавим и запомним список id

          //  MessageBox.Show(startDateForTestRaces.Value.Year + " " + startDateForTestRaces.Value.Month + " " + startDateForTestRaces.Value.Day);

            DateTime startDate = startDateForTestRaces.Value; // DateTime.Now.AddHours(23 - DateTime.Now.Hour);
                        
            
            startDate = startDate.AddMinutes(59 - startDate.Minute);
            startDate = startDate.AddSeconds(60 - startDate.Second);

            // идентификаторы созданных заездов
            List<int> racesIds = new List<int>();

            for (int i = 0; i < 24; i++)
            {
                for (int cell = 0; cell < 4; cell++)
                {
                    string idRace = startDate.Hour.ToString() + "," + (cell+1).ToString();
                    
                    int newRaceId = admin.model.CreateRace(startDate, idRace, 
                        comboBoxItem.getSelectedValue(tracks_comboBox).ToString(), cell + 1);

                    racesIds.Add(newRaceId);
                    label3.Text = racesIds.Count.ToString();
                    Application.DoEvents();

                    startDate = startDate.AddMinutes(15);
                }
            }

            fillRaceDate(racesIds);
        }


        // добавим в каждый заезд по 12 пилотов
        private void fillRaceDate(List<int> idRaces)
        {
            for (int i = 0; i < idRaces.Count; i++)
            {
               // Random rnd = new Random();
                int amountOfPilots = 12; // rnd.Next(12);

                for (int r = 1; r <= amountOfPilots; r++)
                {
                    admin.model.AddPilotToRace(r, i, false, 0, 0, r);

                    label4.Text = (i * r).ToString();
                    Application.DoEvents();
                }
            }

            addLapTime();

        }

        // добавим записи о пройденных кругах 
        private void addLapTime()
        {
            int lapsAmount = 0;

            Random rnd = new Random();

            for (int i = 1; i < 1141; i++) // max 1141
            {
                for (int l = 1; l < 20; l++) // количество кругов в каждом заезде
                {

                    int randomNum = rnd.Next(500);
                    string seconds = Convert.ToString(1 + l) + "," + Convert.ToString(DateTime.Now.Millisecond * l + l + DateTime.Now.Millisecond + randomNum);


                    admin.model.AddTimeStamp(i, l, Convert.ToDecimal(seconds));
 

                    label5.Text = lapsAmount.ToString();
                    lapsAmount++;
                   
                }

                Application.DoEvents();
            } 
        }

        private void createEvents_button_Click(object sender, EventArgs e)
        {
            List<string[]> lines = new List<string[]>();

            string[] line = new string[] {"1", "Оплата участия в рейсе"}; lines.Add(line);
            line = new string[] {"4", "Отказ от участия в рейсе. Выдача денег наличными"}; lines.Add(line);
            line = new string[] {"7", "Добавление кассового остатка за прошлые дни"}; lines.Add(line);
           line = new string[] {"7", "Перенос кассы на следующий день"}; lines.Add(line);
           
            line = new string[] {"10", "Запуск рейса"}; lines.Add(line);
            line = new string[] {"11", "Приостановка рейса "}; lines.Add(line);
            line = new string[] {"12", "Завершение рейса"}; lines.Add(line);
            line = new string[] {"13", "Запуск рейса после паузы"}; lines.Add(line);
              
            
            /* 1 - Оплата участия в рейсе.
             * 4 - Отказ от участия в рейсе. Выдача денег наличными
             * 7 - Добавление кассового остатка за прошлые дни | Перенос кассы на следующий день
             * 10 - Запуск рейса 21 в 15:11:42
             * 11 - Приостановка рейса 27 в 16:33:41
             * 12 - Завершение рейса 21 в 15:22:31
             * 13 - Запуск рейса 27 после паузы  в 16:36:31
             */
             Random rnd = new Random();

            for (DateTime dt = startDateEvent_dateTimePicker1.Value; dt <=
                endDateEvents_dateTimePicker2.Value; dt = dt.AddMinutes(12)) 
            {
                events_label.Text = dt.ToString();
                Application.DoEvents();
                int someLine = rnd.Next(lines.Count);
                   line = lines.ElementAt(someLine);
                   admin.model.AddToJurnal(line[0], -1, -1, line[1]);
            }

        }

        // создать сертификаты
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random(1000000);

            for (int i = 1; i < 100; i++ )
            {
                admin.model.AddCertificate("1",
                           rnd.Next(1000000).ToString(), -1, DateTime.Now.AddDays(365));               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random randomNum = new Random(2000);
            for (int i = 1; i < 100; i++)
            {
                admin.model.Jurnal_Cassa("1", 1, randomNum.Next(80), randomNum.Next(100).ToString(), "0", "Оплата участия в рейсе. " + i.ToString());

                admin.model.Jurnal_Cassa("15", -1, -1, randomNum.Next(20).ToString(), "1", "Снятие наличных с кассы. Снял - " + admin.model.GetProgramUserName("1"));

                admin.model.Jurnal_Cassa("30", Convert.ToInt32(1), -1, randomNum.Next(20).ToString(), "0", "Покупка сертификата.");

                admin.model.Jurnal_Cassa("4", Convert.ToInt32(1), 1, randomNum.Next(20).ToString(), "1", "Отказ от участия в рейсе. Выдача денег наличными");

                admin.model.Jurnal_Cassa("7", -1, -1, "35", "0", "Добавление кассового остатка за прошлые дни");
                admin.model.Jurnal_Cassa("7", -1, -1, "35", "1", "Перенос кассы на следующий день", pastdate: true);
         

            }

            MessageBox.Show("Операции добавлены");
        }
    
    
    }
}
