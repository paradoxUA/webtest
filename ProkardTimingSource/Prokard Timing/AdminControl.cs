using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using Prokard_Timing.model;
using System.Data;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Linq;

namespace Prokard_Timing
{
    public class AdminControl
    {
        public int TimeToCreate = 20; // на сколько минут позже можно создать заезд от его даты старта (то есть, если заезд должн стартовать в 11, а сейчас 11.20, то создать его можно. а если 11.21, то уже нельзя
        public int CurrentMonth = 0;
        public bool UniqueBestResult = true; // Показывать только уникальные имена
        public int CurrentYear = 0;
        public int CurrentDay = 0;
        private DateTime CurrDate = new DateTime();
        public int MaxKarts = 0;
        public int DopRace = 900;
        public RaceClass[,] Races = new RaceClass[24, 6]; // Массив рейсов 24 часа и 5 этапов
       
        public List<races> SportRaces = new List<races>(); // набор заездов на сегодня для режима Спорт, расширяется по мере добавления новых заездов. 

        public ProkardModel model;
        public RaceThread RaceTH;
        public Hashtable Settings = new Hashtable();
        public bool isSportMode;

        private List<PricesForRaceModes> Prices = new List<PricesForRaceModes>();
      
        // массив цен на все дни, для одного режима заезда
        public class PricesForRaceModes
        {
            public int idRaceMode;

            // массив цен в виде d1/d2/...d7
            public string[] Prices = new string[8];
        }

        public bool InError = false;
        public int TypeError = 0;
        public string ErrorMessage = String.Empty;

        public bool IS_USER = false;
        public bool IS_ADMIN = false;
        public bool IS_SUPER_ADMIN = false;
        public string User_Name = "No User";
        public int USER_ID = 0;      

        DataGridViewPrinter MyDataGridViewPrinter;

        // Функция запуска, перегон данных в рейс
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RaceID"></param>
        /// <param name="isContinue">тип гонки, 0 =  новая, 1 = продолжение</param>
        /// <param name="TimeIndex"></param>
        public void RaceStart(int RaceID, int isContinue = 0, int TimeIndex=0)
        {
            Hashtable res = model.GetDetalRaceInfo(RaceID), row = new Hashtable();

            if (isContinue == 0)
            {
                RaceTH.ClearPilot();
                for (int i = 0; i < res.Count; i++)
                {
                    row = (Hashtable)res[i];
                    RaceMember Member = new RaceMember();

                    if (row["car_id"].ToString().Length > 0)
                    {
                        Member.CarID = Convert.ToInt32(row["car_id"]);
                    }
                    else
                    {
                        Member.CarID = -1;
                    }


                    if (Member.CarID == -1 || Convert.ToInt32(row["light"]) == 1)
                    {
                        Member.CarNum = String.Empty;
                        Member.CarTransponder = String.Empty;
                        Member.LightMode = true;
                        Member.IsNoKart = true;
                    }
                    else
                    {
                        Member.LightMode = false;
                        Member.IsNoKart = false;
                        Member.CarNum = row["number"].ToString();
                        Member.CarTransponder = row["transponder"].ToString() == "" ? "0" : row["transponder"].ToString();
                    }

                    Member.MemberID = Convert.ToInt32(row["id"]);
                    Member.PilotID = Convert.ToInt32(row["pilot_id"]);
                    Member.PilotName = row["name"].ToString();
                    Member.PilotLastName = row["surname"].ToString();

                    Member.PilotNickName = row["nickname"].ToString();
                    Member.RaceID = Convert.ToInt32(row["race_id"]);
                    Member.FirstTick = 0;

                    if (Member != null)
                    {
                        RaceTH.AddPilot(Member);
                    }
                }
            }

            if (isContinue == 1)
            {
                int MemID = 0;
                for (int i = 0; i < res.Count; i++)
                {
                    row = (Hashtable)res[i];

                    MemID = -1;
                    for (int j = 0; j < RaceTH.Members.Count; j++)
                    {
                        if (RaceTH.Members[j].MemberID == Convert.ToInt32(row["id"]))
                        {
                            MemID = j;
                            break;
                        }
                    }

                    if (MemID != -1)
                    {
                        if (!RaceTH.Members[MemID].LightMode)
                        {
                            RaceTH.Members[MemID].CarID = Convert.ToInt32(row["car_id"]);
                            RaceTH.Members[MemID].CarNum = row["number"].ToString();
                            RaceTH.Members[MemID].CarTransponder = row["transponder"].ToString() == "" ? "0" : row["transponder"].ToString();
                        }
                    }


                }

            }
        }

        //Распечатывает Datagrid
        public void PrintDataGridView(DataGridView dv, string Title)
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            PrintDocument printDocument1 = new PrintDocument();

            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            printDocument1.PrinterSettings =
                        MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings =
            MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Margins =
                             new Margins(40, 40, 40, 40);

            printDocument1.DocumentName = Title;
            printDocument1.PrinterSettings.PrinterName = Settings["printer_result"].ToString();

            if (printDocument1.PrinterSettings.IsValid)
            {
                MyDataGridViewPrinter = new DataGridViewPrinter(dv,
        printDocument1, false, true, Title, new Font("Tahoma", 12,
        FontStyle.Bold, GraphicsUnit.Point), Color.Black, true, new Point(0, 0));

                PrintPreviewDialog pvd = new PrintPreviewDialog();
                pvd.Document = printDocument1;
                pvd.ShowDialog();
                //printDocument1.Print();
                pvd.Dispose();
            }
            MyDataGridViewPrinter = null;
            MyPrintDialog.Dispose();

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        // Функция для разрешения старта
        public bool EnabledRaceStart(RaceClass Race)
        {
            bool ret = true;
            // Добавить проверку завершенности предыдущего заезда
            if (DateTime.Now.Date.DayOfYear != Race.Date.DayOfYear)
            {
                ret = false;
            }
            else
            {
                long t20 = DateTime.Now.AddMinutes(-TimeToCreate).Ticks;
                long tcurr = Race.Date.Ticks;

                if (tcurr < t20) //  && Race.Status == 0 &&  model.isRaceCanBeStarted(Convert.ToInt32(Race.ID)) == false) // если не был запущен за 20 минут, то и нельзя запускать
                {
                    // добавить проверку - если есть пилоты в заезде, и он не запускался, то разрешить его запуск
                  
                    ret = false;
                }
                else
                {
                    ret = true;
                }
                /*      if (DateTime.Now.Hour != Convert.ToInt16(Race.Hour)) ret = false;
                      else
                      {

                      } */
            }

            return ret;
        }

        public void ShowRaceLoad(DataGridView dv)
        {

            int H, M, I, J;
            GetNextRaceTime(out H, out M, out I, out J);

            dv.Rows.Clear();

            int cr = 0;
            for (int i = J; i <= 4; i++)
            {
                dv.Rows.Add();
                dv.Rows[cr].Height = 40;
                dv.Rows[cr].Cells[0].Value = (Races[I, i].Hour.ToString().Length == 1 ? "0" + Races[I, i].Hour.ToString() : Races[I, i].Hour.ToString()) + ":" + (Races[I, i].Minute.ToString().Length == 1 ? "0" + Races[I, i].Minute.ToString() : Races[I, i].Minute.ToString());
                dv.Rows[cr].Cells[1].Value = model.GetRacePilotsCount(Races[I, i].RaceID.ToString());
                cr++;
            }

            I++;
            for (int i = I; i < I + 3; i++)
            {

                if (H > Int16.Parse(Settings["time_end"].ToString()) || i >= 24) continue;

                for (int j = 1; j <= 4; j++)
                {

                    dv.Rows.Add();
                    dv.Rows[cr].Height = 40;
                    dv.Rows[cr].Cells[0].Value = (Races[i, j].Hour.ToString().Length == 1 ? "0" + Races[i, j].Hour.ToString() : Races[i, j].Hour.ToString()) + ":" + (Races[i, j].Minute.ToString().Length == 1 ? "0" + Races[i, j].Minute.ToString() : Races[i, j].Minute.ToString());
                    dv.Rows[cr].Cells[1].Value = model.GetRacePilotsCount(Races[i, j].RaceID.ToString());
                    cr++;
                }
            }

        }


        public void ShowNextRacePilots(DataGridView dv, LabelSmooth l1, LabelSmooth l2)
        {
            int H, M, I, J;

            if (RaceTH.Status == 0)
            {
                GetNextRaceTime(out H, out M, out I, out J);
                if (Races[I, J].Status == 2)
                {
                    GetNextRaceTime(out H, out M, out I, out J, true);
                }
            }
            else
            {
                //GetNextRaceTime(out H, out M, out I, out J);
                //if (Races[I, J].Status == 2)
                GetNextRaceTime(out H, out M, out I, out J, true);
            }
            

            l1.Text = (H.ToString().Length == 1 ? "0" + H.ToString() : H.ToString()) + ":" + (M.ToString().Length == 1 ? "0" + M.ToString() : M.ToString());
            dv.Rows.Clear();

           IEnumerable<race_data> races_data = model.GetRacePilots(Races[I, J].RaceID); //, row = new Hashtable();

            if (RaceTH.Status == 0)
            {
                l2.Text = "Заезд №" + (Races[I, J].RaceNum > DopRace ? (Races[I, J].RaceNum - DopRace).ToString() + "a" : Races[I, J].RaceNum.ToString()) + "   " + races_data.Count().ToString() + "/" + MaxKarts.ToString();
            }
            else
            {
                l2.Text = "Следующий заезд №" + (Races[I, J].RaceNum > DopRace ? (Races[I, J].RaceNum - DopRace).ToString() + "a" : Races[I, J].RaceNum.ToString()) + "   " + races_data.Count().ToString() + "/" + MaxKarts.ToString();
            }

            bool c2 = false;
            int cr = -1;
            foreach (race_data item in races_data) 
            {              
                if (c2)
                {
                    dv.Rows[cr].Cells[1].Value = item.user.nickname + "*" + item.user.name;
                }
                else
                {
                    cr++;
                    dv.Rows.Add();

                    dv.Rows[cr].Height = 46;

                    dv.Rows[cr].Cells[0].Value = item.user.nickname + "*" + item.user.name;
                }
                c2 = !c2;
            }


        }

        /*
        public void ShowNextRacePilotsOld(DataGridView dv, LabelSmooth l1, LabelSmooth l2)
        {
            int H, M, I, J;

            if (RaceTH.Status == 0)
            {
                GetNextRaceTime(out H, out M, out I, out J);
                if (Races[I, J].Status == 2)
                {
                    GetNextRaceTime(out H, out M, out I, out J, true);
                }
            }
            else
            {
                //GetNextRaceTime(out H, out M, out I, out J);
                //if (Races[I, J].Status == 2)
                GetNextRaceTime(out H, out M, out I, out J, true);
            }


            l1.Text = (H.ToString().Length == 1 ? "0" + H.ToString() : H.ToString()) + ":" + (M.ToString().Length == 1 ? "0" + M.ToString() : M.ToString());
            dv.Rows.Clear();

            Hashtable res = model.GetRacePilots(Races[I, J].RaceID), row = new Hashtable();

            if (RaceTH.Status == 0)
            {
                l2.Text = "Заезд №" + (Races[I, J].RaceNum > DopRace ? (Races[I, J].RaceNum - DopRace).ToString() + "a" : Races[I, J].RaceNum.ToString()) + "   " + res.Count.ToString() + "/" + MaxKarts.ToString();
            }
            else
            {
                l2.Text = "Следующий заезд №" + (Races[I, J].RaceNum > DopRace ? (Races[I, J].RaceNum - DopRace).ToString() + "a" : Races[I, J].RaceNum.ToString()) + "   " + res.Count.ToString() + "/" + MaxKarts.ToString();
            }

            bool c2 = false;
            int cr = -1;
            for (int i = 0; i < res.Count; i++)
            {
                row = (Hashtable)res[i];
                if (c2)
                {
                    dv.Rows[cr].Cells[1].Value = row["nickname"].ToString() + "*" + row["name"].ToString();


                }
                else
                {
                    cr++;
                    dv.Rows.Add();

                    dv.Rows[cr].Height = 46;

                    dv.Rows[cr].Cells[0].Value = row["nickname"].ToString() + "*" + row["name"].ToString();
                }
                c2 = !c2;

            }


        }
        */
        // Показывает все сертификаты
        public void ShowCertificateType(DataGridView dv, string TP = "0", string filter = "")
        {
            List<Hashtable> data = model.GetAllCertificateType(TP, filter);
            dv.Rows.Clear();


            for (int i = 0; i < data.Count; i++)
            {
                dv.Rows.Add();

                dv[0, i].Value = data[i]["id"];
                dv[1, i].Value = data[i]["name"];
                dv[2, i].Value = data[i]["nominal"];
                dv[3, i].Value = data[i]["cost"];
                dv[4, i].Value = Convert.ToDateTime(data[i]["created"]).ToString("dd MMMM yyyy");
                dv[5, i].Value = model.GetCertificateTypeCount(data[i]["id"].ToString());
            }
        }


        // Преобразует TimeTicks во время
        private string ConvertTicks(long Ticks, int type = 0, bool mtk = true, long tm = 0)
        {
            if (Ticks < 0)
            {
                Ticks = Math.Abs(Ticks);// Ticks = 1;
            }

            if (mtk)
            {
                Ticks = Ticks - tm;
            }
            if (Ticks <= 0)
            {
                Ticks = Math.Abs(Ticks);// Ticks = 1;
            }

            DateTime dt = new DateTime(Ticks <= 0 ? Math.Abs(Ticks) : Ticks);
            string ret = String.Empty;
            switch (type)
            {

                case 0: ret = dt.Minute.ToString("00") + ":" + dt.Second.ToString("00") + "." + dt.Millisecond.ToString("000"); break;
                case 1: ret = dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00") + ":" + dt.Second.ToString("00") + "." + dt.Millisecond.ToString("000"); break;
                case 2: ret = new TimeSpan(Ticks <= 0 ? Math.Abs(Ticks) : Ticks).TotalSeconds.ToString() + " сек"; break;
               
                    /*
                case 0: ret = dt.Minute.ToString() + ":" + dt.Second.ToString() + "." + dt.Millisecond.ToString(); break;
                case 1: ret = dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString() + "." + dt.Millisecond.ToString(); break;
                case 2: ret = new TimeSpan(Ticks <= 0 ? Math.Abs(Ticks) : Ticks).TotalSeconds.ToString() + " сек"; break;
                     */
            }

            while (ret.Substring(0, 3) == "00:")
            {
                ret = ret.Substring(3);
            }

            return ret;
        }

        // Функция отображения результата
        public void ShowRaceResults(DataGridView dv, bool big = false)
        {
            dv.Rows.Clear();

            for (int i = 0; i < RaceTH.Members.Count; i++)
            {
                dv.Rows.Add();
                if (big)
                {
                    dv.Rows[i].Height = 40;
                }
                dv.Rows[i].Cells[0].Value = 0;
                if (RaceTH.Members[i].LightMode == true)
                {
                   dv.Rows[i].Cells[1].Value = "";
                }
                else
                {
                    if (RaceTH.Members[i].PilotNickName.Length == 0)
                    {
                        dv.Rows[i].Cells[1].Value = RaceTH.Members[i].PilotName + " " + RaceTH.Members[i].PilotLastName;
                    }
                    else
                    {
                        dv.Rows[i].Cells[1].Value = "[" + RaceTH.Members[i].PilotNickName + "] " + RaceTH.Members[i].PilotName + " " + RaceTH.Members[i].PilotLastName; ;
                    }
                }
               // dv.Rows[i].Cells[1].Value = RaceTH.Members[i].LightMode ? "" : RaceTH.Members[i].PilotNickName;
                dv.Rows[i].Cells[2].Value = RaceTH.Members[i].CarNum;
                dv.Rows[i].Cells[3].Value = RaceTH.Members[i].CarTransponder == "" ? "" : Convert.ToInt32(RaceTH.Members[i].CarTransponder).ToString();
                dv.Rows[i].Cells[4].Value = RaceTH.Members[i].Laps;
                dv.Rows[i].Cells[5].Value =  ConvertTicks(RaceTH.Members[i].LapTime, 0, false, RaceTH.Members[i].TimeST);
                dv.Rows[i].Cells[6].Value = RaceTH.Members[i].DeltaTime;
                dv.Rows[i].Cells[7].Value = ConvertTicks(RaceTH.Members[i].BestLapTime, 0, false, RaceTH.Members[i].TimeST);

                if (RaceTH.Members[i].TotalPilotTimeOfThisRace > 0)
                {
                    dv.Rows[i].Cells[9].Value = ConvertTicks(RaceTH.Members[i].TotalPilotTimeOfThisRace, 0, false);
                }
                else
                {
                    dv.Rows[i].Cells[9].Value = 1;
                }

               
                 dv.Rows[i].Cells[8].Value = RaceTH.Members[i].TotalPilotTimeOfThisRace <= 0 ? 1 : RaceTH.Members[i].TotalPilotTimeOfThisRace;
                //dv.Rows[i].Cells[8].Value = new TimeSpan(RaceTH.Members[i].AllTime <= 0 ? 1 : RaceTH.Members[i].AllTime).TotalSeconds;
            }

            dv.Sort(new RowComparer(SortOrder.Descending));
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                dv[0, i].Value = i + 1;
            }

            if (big)
            {

                dv[6, 0].Value = "";
                //DateTime dl = DateTime.Parse(dv[8, 0].Value.ToString().Length < 2 ? "1980-01-01 00:00:00" : dv[8, 0].Value.ToString());
                for (int i = 1; i < dv.Rows.Count; i++)
                {
                //    dv[6, i].Value = "+" + ConvertTicks(dl.Ticks - DateTime.Parse(dv[8, i].Value.ToString().Length < 2 ? "1980-01-01 00:00:00" : dv[8, i].Value.ToString()).Ticks);

                    dv[6, i].Value = "+" + ConvertTicks(long.Parse(dv[8, 0].Value.ToString()) - long.Parse(dv[8, i].Value.ToString()));

                }

            }
        }

        

        // Показывает лучшее время за день, неделю, месяц, год
        public void ShowAnonserBestResult(DataGridView dv, int TP, int idTrack)
        {
            // Tp == 1 - Day
            // Tp == 2 - Week
            // TP == 3 - for whole time

            /*
            // Tp == 3 - Month
            // Tp == 4 - Year
             */

            dv.Rows.Clear();

            DateTime startDate = DateTime.Now, endDate;

            switch (TP)
            {

                case 1:
                    startDate = datetimeConverter.toStartDateTime(DateTime.Now); //.AddDays(-1);
                    break;

                case 2:
                    startDate = datetimeConverter.toStartDateTime(DateTime.Now.AddDays(-7)); //.AddDays(-1);
                    // DateTime.Now.AddDays(-7);
                    break;

                case 3:
                    startDate = datetimeConverter.toStartDateTime(DateTime.Now.AddYears(-100));
                                    
                    break;

                    /*
                case 3:
                    startDate = datetimeConverter.toStartDateTime(DateTime.Now.AddMonths(-1)); //.AddDays(-1);
                  
                   // startDate = DateTime.Now.AddMonths(-1);
                    break;

                case 4:
                    startDate = datetimeConverter.toStartDateTime(DateTime.Now.AddYears(-1)); //.AddDays(-1);
                  
                   // startDate = DateTime.Now.AddYears(-1);
                    break;
 */
            }

            endDate = datetimeConverter.toEndDateTime(DateTime.Now);
                    
            //endDate = DateTime.Now;

            List<Hashtable> Data;

            Data = model.GetAnonserBestResultsFromDateRange(startDate, endDate, UniqueBestResult, idTrack);
            for (int i = 0; i < Data.Count; i++)
            {
                dv.Rows.Add();
                dv.Rows[i].Height = 40;
                dv[0, i].Value = i + 1;
                
                if (Data[i]["nickname"].ToString().Length > 0)
                {
                    dv[1, i].Value = "[" + Data[i]["nickname"] + "]";
                }
                else
                {
                    dv[1, i].Value = Data[i]["name"] + " " + Data[i]["surname"];
                }

                dv[2, i].Value = Math.Round(Convert.ToDouble(Data[i]["seconds"]), 2).ToString().Replace(',', '.');

                if (Convert.ToDateTime(Data[i]["racedate"]) >= datetimeConverter.toStartDateTime(DateTime.Now) &&
                    Convert.ToDateTime(Data[i]["racedate"]) <= datetimeConverter.toEndDateTime(DateTime.Now))
                {
                    dv[3, i].Value = "сегодня";
                }
                else if (Convert.ToDateTime(Data[i]["racedate"]) >= datetimeConverter.toStartDateTime(DateTime.Now.AddDays(-1)) &&
                    Convert.ToDateTime(Data[i]["racedate"]) <= datetimeConverter.toEndDateTime(DateTime.Now.AddDays(-1)))
                {
                    dv[3, i].Value = "вчера";
                }
                else
                {
                    dv[3, i].Value = Convert.ToDateTime(Data[i]["racedate"]).ToString("dd.M.yy");
                }                 
                
                /*
                dv[3, i].Value = Convert.ToDateTime(Data[i]["racedate"]).ToString("dd MMMM");
                 */
            }

            dv.Sort(new RowComparer3(SortOrder.Descending, 2));
            for (int i = 0; i < dv.Rows.Count; i++)
                {
                dv[0, i].Value = i + 1;
                }


        }


        // Показывает лучший результат дня
        public void ShowBestDMResult(DataGridView dv, string Date, int TrackID, bool OnDay, string Num)
        {
            List<Hashtable> Data;

            if (OnDay == false)
            {
                Data = model.GetBestResults(TrackID, UniqueBestResult, datetimeConverter.toStartDateTime(DateTime.Now.AddMonths(-1)),
                    datetimeConverter.toEndDateTime(DateTime.Now), 10);

            }
            else
            {
                Data = model.GetBestResults(TrackID, UniqueBestResult, 
                    datetimeConverter.toStartDateTime(DateTime.Now),
                    datetimeConverter.toEndDateTime(DateTime.Now), 10);

            }

            dv.Rows.Clear();

            int Count;

            if (OnDay)
            {
                Count = 30;
            }
            else
            {
                Count = 5;
            }

            for (int i = 0; i < Count; i++)
            {
                dv.Rows.Add();


                dv[0, i].Value = i+1;

                if (i < Data.Count)
                {
                    if (!OnDay)
                    {
                        dv[1, i].Value = Convert.ToDateTime(Data[i]["racedate"]).ToString("dd MMMM yyyy");
                    }
                   

                    string pilotName = "";

                    if (Data[i]["nickname"].ToString().Length > 0)
                    {
                       // pilotName = "[" + Data[i]["nickname"] + "] ";
                    }

                    pilotName = pilotName + Data[i]["name"] + " " + Data[i]["surname"];
                    dv[2, i].Value = pilotName;

                    dv[3, i].Value = Math.Round(Convert.ToDouble(Data[i]["seconds"]), 2);
                }
            }

           /* dv.Sort(new RowComparer3(SortOrder.Descending, 3));
            for (int i = 0; i < dv.Rows.Count; i++)
                dv[0, i].Value = i + 1;

            */

        }

        // Функция отображает финальный вид заезда
        public void ShowFinnalyRaceResult(DataGridView dv, int RaceID)
        {
            DateTime startTime = DateTime.Now;

            List<LapResult> Data = model.GetFinallyRaceInfo(RaceID);
            string TempLap;

            dv.Rows.Clear();
            dv.Columns.Clear();

            dv.Columns.Add("Position", "Поз");
            dv.Columns.Add("PilotID", "№");
            dv.Columns.Add("PilotName", "Пилот");
            dv.Columns.Add("Kart", "Карт");
            dv.Columns.Add("BestLap", "Лучший круг");

            dv.Columns["PilotID"].Visible = false;

            int MaxLaps = 0;
            for (int i = 0; i < Data.Count; i++)
            {
                if (MaxLaps < Data[i].Times.Count)
                {
                    MaxLaps = Data[i].Times.Count;
                }
            }

            for (int i = 1; i <= MaxLaps; i++)
            {
                TempLap = i.ToString();
                dv.Columns.Add("Lap" + TempLap, TempLap);
            }

            dv.Columns.Add("AverageTime", "сред");

            for (int i = 0; i < Data.Count; i++)
            {
                dv.Rows.Add();

                if (i < Data.Count)
                {
                    dv[0, i].Value = 0;
                    dv[1, i].Value = Data[i].Light ? "" : Data[i].PilotID.ToString();

                    if (Data[i].Light == true)
                    {
                        dv[2, i].Value = "";
                    }
                    else
                    {
                        string pilotName = "";

                        if (Data[i].PilotNickName.ToString().Length > 0)
                        {
                            pilotName = "[" + Data[i].PilotNickName + "] ";
                        }

                        pilotName = pilotName + Data[i].PilotName;
                        dv[2, i].Value = pilotName;
                    }

                  //  dv[2, i].Value = Data[i].Light ? "" : Data[i].PilotNickName;
                    dv[4, i].Value = formatTimeString(Data[i].BestTime) + "/" + Data[i].BestTimeLap; // Math.Round(Data[i].BestTime, 2) + "/" + Data[i].BestTimeLap;
                    dv[3, i].Value = Data[i].CarNum;

                    for (int j = 0; j < Data[i].Times.Count; j++)
                    {
                        TempLap = (j + 1).ToString();
                                                
                        dv["Lap" + TempLap, i].Value = formatTimeString(Data[i].Times[j]); // (Math.Round(Data[i].Times[j], 3)).ToString("0.000");
                    }

                    dv["AverageTime", i].Value = formatTimeString(Data[i].AverageTime);
                }
            }

            dv.Sort(new RowComparer2(SortOrder.Descending, dv.ColumnCount));
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                dv[0, i].Value = i + 1;
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ShowFinnalyRaceResult", Logger.LogType.info, executionTime);
        }

        private string formatTimeString(double someTime)
        {
            string result = (Math.Round(someTime, 3)).ToString("#0.000");
            return result.Replace(",", ".");
        }

        // Показывает все сертификаты
        public void ShowCertificate(DataGridView dv, int Filt)
        {
            dv.Rows.Clear();

            string Filter = String.Empty;
            switch (Filt)
            {
                case 0: Filter = " and active=1 and date_end >= GETDATE() "; break;
                case 1: Filter = " and active=0 and  date_end >= GETDATE() "; break;
                case 2: Filter = " and  date_end < GETDATE() "; break;
                case 3: Filter = ""; break;
                
                    /*
                case 0: Filter = " and active=1 and date(date_end)>=date(now()) "; break;
                case 1: Filter = " and active=0 and date(date_end)>=date(now()) "; break;
                case 2: Filter = " and date(date_end)<date(now())"; break;
                case 3: Filter = ""; break;
                     */
            }

            List<Hashtable> data = model.GetAllCertificates(Filter);

            for (int i = 0; i < data.Count; i++)
            {
                dv.Rows.Add();
                dv[0, i].Value = data[i]["id"];
                dv[1, i].Value = data[i]["bar_number"];
                dv[2, i].Value = model.GetCertificateTypeName(data[i]["c_id"].ToString());
                dv[3, i].Value = model.GetPilotName(data[i]["user_id"].ToString());
                dv[4, i].Value = Convert.ToDateTime(data[i]["date_end"]).Ticks < DateTime.Now.Ticks && Convert.ToBoolean(data[i]["active"]) ? "Просрочен" : (Convert.ToBoolean(data[i]["active"]) ? "Активен" : "Использован");
                dv[5, i].Value = data[i]["count"];
                dv[6, i].Value = Convert.ToDateTime(data[i]["created"]).ToString("dd MMMM yyyy");
                dv[7, i].Value = Convert.ToDateTime(data[i]["date_end"]).ToString("dd MMMM yyyy");
            }
        }

        private int GetNowRaceMin()
        {

            int M, j;
            int Mt = DateTime.Now.Minute;

            if (Mt >= 0 && Mt <= 5) { M = 0; j = 1; }
            else
                if (Mt > 5 && Mt <= 20) { M = 15; j = 2; }
                else
                    if (Mt > 20 && Mt <= 35) { M = 30; j = 3; }
                    else
                        if (Mt > 35 && Mt < 50) { M = 45; j = 4; }
                        else
                        { M = 0; j = 1; }

            return M;
        }

        // Получает время следующего рейса
        public void GetNextRaceTime(out int H, out int M, out int i, out int j, bool nnext = false)
        {
            int Ht = DateTime.Now.Hour;
            int Mt;
            if (nnext)
            {
                Mt = GetNowRaceMin() + 15;

            }
            else Mt = DateTime.Now.Minute;

            if (Mt < 50) H = Ht; else H = DateTime.Now.AddHours(1).Hour;

            if (Mt >= 0 && Mt <= 5) { M = 0; j = 1; }
            else
                if (Mt > 5 && Mt <= 20) { M = 15; j = 2; }
                else
                    if (Mt > 20 && Mt <= 35) { M = 30; j = 3; }
                    else
                        if (Mt > 35 && Mt < 50) { M = 45; j = 4; }
                        else
                        { M = 0; j = 1; }

            i = H;
        }

        // Создает рейс в данном часу 
        public bool CreateRace(int Hour, int Stage)
        {
            if (Settings["default_track"] == null || Settings["default_track"].ToString().Length == 0 || Convert.ToString(Settings["default_track"]) == "0")
            {
                MessageBox.Show("Необходимо указать трассу по умолчанию (в разделе Управление - Настройки - Гонка - Трек по умолчанию)");
                return false;
            }

            bool result = true;
            if ((Hour <= 0 && Stage <= 0) || Races[Hour, Stage].Status != 0 || CurrDate.Date < DateTime.Now.Date) result = false;

            DateTime dt = new DateTime(CurrentYear, CurrentMonth, CurrentDay, Hour, ProkardModel.GetMinutesFromIndex(Stage), 0);
            long tk = dt.Ticks;
            long tk20 = DateTime.Now.AddMinutes(-TimeToCreate).Ticks;
            if (result && CurrDate.Date.DayOfYear == DateTime.Now.Date.DayOfYear && tk < tk20)
            {
                result = false;
            }
            //if (Hour < DateTime.Now.Hour) result = false;
            //else if (Stage == DateTime.Now.Hour && GetMinutesFromIndex(Stage) < DateTime.Now.Minute) result = false;

            if (result)
            {

               // int idRace = model.CreateRace(new DateTime(CurrentYear, CurrentMonth, CurrentDay).ToString("yyyy-MM-dd ") + Hour.ToString() + ":" + (GetMinutesFromIndex(Stage) == 60 ? 59 : GetMinutesFromIndex(Stage)).ToString(), Races[Hour, Stage].ID, Settings["default_track"].ToString());
                                           
                 int minutes = ProkardModel.GetMinutesFromIndex(Stage);

                int idRace = model.CreateRace(new DateTime(CurrentYear, CurrentMonth,
                CurrentDay, Hour, minutes, 0), Races[Hour, Stage].ID, Settings["default_track"].ToString(), Stage);



                    //      CurrentDay).ToString("yyyy-MM-dd ") + "0:0:0", Races[Hour, Stage].ID, Settings["default_track"].ToString());
                
                Races[Hour, Stage].Status = 1; // по умолчанию сделать неактивными кнопки для запуска заезда
                Races[Hour, Stage].RaceID = idRace;

            }
            return result;
        }

        // Получает список пилотов и выводит их на панель
        public void ShowRacePilots(int Raceid, DataGridView dv, int tp = 0)
        {
            DateTime startTime = DateTime.Now;
            dv.Rows.Clear();

            IEnumerable<race_data> races_data = model.GetRacePilots(Raceid);

            int i = 0;

            foreach (race_data item in races_data) //  (int i = 0; i < res.Count; i++)
            {
                dv.Rows.Add();
            //    row = (Hashtable)res[i];

                // Режим предпросмотра в главной форме
                dv.Rows[i].Cells[0].Value = (i + 1).ToString();
                if (item.id > 0)
                {
                    dv.Rows[i].Cells[1].Value = item.id; // row["id"] ?? 0;
                }
                else
                {
                    dv.Rows[i].Cells[1].Value = 0;
                }

                string pilotName = "";

                if (item.user.nickname.Length > 0)
                {
                    pilotName = "[" + item.user.nickname + "]";
                }

                pilotName = pilotName + " " + item.user.name + " " + item.user.surname;

                dv.Rows[i].Cells[2].Value = pilotName;

                // dv.Rows[i].Cells[2].Value = row["nickname"] ?? "Удален";

                if (item.car_id.HasValue)
                {
                    if (item.light_mode == false)
                    {
                        dv.Rows[i].Cells[3].Value = item.kart.number;
                    }
                    
                }


                dv.Rows[i].Cells[4].Value = item.id; // row["BaseID"] ?? 0;
                
                dv.Rows[i].Cells[8].Value = item.pilot_id; 


                if (item.reserv.HasValue && item.reserv.Value == false)
                {
                    dv.Rows[i].Cells[5].Value = item.reserv.Value.ToString();
                }
                else
                {
                    dv.Rows[i].Cells[5].Value = "False";
                }
                //dv.Rows[i].Cells[5].Value = row["reserv"] ?? "False";


                dv.Rows[i].Cells["idRaceMode_column"].Value = item.id_race_mode; // row["id_race_mode"];
                dv.Rows[i].Cells["RaceMode_column"].Value = item.race_modes.name; // row["race_mode"];


                if (tp == 1)
                {
                    dv.Rows[i].Height = 40;
                }

                i++;

            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ShowRacePilots", Logger.LogType.info, executionTime);

        }

        /*
        // Получает список пилотов и выводит их на панель
        public void ShowRacePilots(int Raceid, DataGridView dv, int tp = 0)
        {
            DateTime startTime = DateTime.Now;
            dv.Rows.Clear();

            Hashtable res = model.GetRacePilots(Raceid), row = new Hashtable();




            for (int i = 0; i < res.Count; i++)
            {
                dv.Rows.Add();
                row = (Hashtable)res[i];

                // Режим предпросмотра в главной форме
                dv.Rows[i].Cells[0].Value = (i + 1).ToString();
                dv.Rows[i].Cells[1].Value = row["id"] ?? 0;

                string pilotName = "";

                if (row["nickname"].ToString().Length > 0)
                {
                    pilotName = "[" + row["nickname"].ToString() + "]";
                }

                pilotName = pilotName + " " + row["name"] + " " + row["surname"];

                dv.Rows[i].Cells[2].Value = pilotName;

               // dv.Rows[i].Cells[2].Value = row["nickname"] ?? "Удален";

                
                if (row["car_id"] != null && row["car_id"].ToString().Length > 0)
                {
                    dv.Rows[i].Cells[3].Value = row["light_mode"].ToString() == "1" ? "" : model.GetKart(Int32.Parse(row["car_id"].ToString()))["number"];
                }
                    
                dv.Rows[i].Cells[4].Value = row["BaseID"] ?? 0;
                dv.Rows[i].Cells[5].Value = row["reserv"] ?? "False";

               dv.Rows[i].Cells["idRaceMode_column"].Value = row["id_race_mode"];
                dv.Rows[i].Cells["RaceMode_column"].Value = row["race_mode"];


                if (tp == 1)
                    dv.Rows[i].Height = 40;
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ShowRacePilots", Logger.LogType.info, executionTime);

        }
        */
        // Получает список пользователей
        public void ShowProgramUsers(DataGridView dv)
        {
            dv.Rows.Clear();

            List<Hashtable> res = model.GetAllPgrogramUsers(0);
            for (int i = 0; i < res.Count; i++)
            {
                dv.Rows.Add();
                dv.Rows[i].Cells[0].Value = res[i]["id"];
                dv.Rows[i].Cells[1].Value = res[i]["login"];
                dv.Rows[i].Cells[2].Value = GetProgramUsersStat(Convert.ToInt32(res[i]["stat"]));
                dv.Rows[i].Cells[3].Value = res[i]["stat"];
                dv.Rows[i].Cells[4].Value = Decrypt(res[i]["password"].ToString());
                dv.Rows[i].Cells[5].Value = res[i]["surname"];
                dv.Rows[i].Cells[6].Value = res[i]["name"];
                dv.Rows[i].Cells[7].Value = res[i]["barcode"];
            }

        }

        public string GetProgramUsersStat(int Stat)
        {
            string ret = String.Empty;

            switch (Stat)
            {
                case 0: ret = "Пользователь"; break;
                case 1: ret = "Администратор"; break;
                case 2: ret = "Супер Администратор"; break;
            }
            return ret;
        }



        // Показывает список пилотов месячного заезда
        public void ShowMonthRace(DataGridView dv, DateTime date)
        {
            List<Hashtable> Pilots = model.GetMonthRaceMembers(date);
            dv.Rows.Clear();

            for (int i = 0; i < Pilots.Count; i++)
            {
                dv.Rows.Add();
                dv[0, i].Value = Pilots[i]["pilot_id"];

                dv[2, i].Value = Pilots[i]["surname"] + " " + Pilots[i]["name"];
                dv[3, i].Value = Pilots[i]["email"];
                dv[4, i].Value = Pilots[i]["tel"];
                dv[5, i].Value = Math.Round(Convert.ToDouble(Pilots[i]["seconds"]), 3);
                dv[6, i].Value = Pilots[i]["track_name"];
                dv[7, i].Value = Convert.ToDateTime(Pilots[i]["created"]).ToString("dd MMMM yyyy");
                dv[8, i].Value = Pilots[i]["nickname"];
            }

            dv.Sort(dv.Columns[5], System.ComponentModel.ListSortDirection.Ascending);

            for (int i = 0; i < dv.Rows.Count; i++)
                dv[1, i].Value = (i + 1).ToString();
        }

        // Показывает журнал заездов
        public void ShowRaceJurnal(PageLister pages, DataGridView dv, DateTime startDate, DateTime endDate, int eventId)
        {
           // List<Hashtable> RaceJurnal = model.GetRaceJurnal(Date, Date2, Flt);

            List<Hashtable> RaceJurnal = model.GetRaceJurnal(pages, startDate, endDate, eventId, pages.CurrentPageNumber, pages.PageSize);
           
            dv.Rows.Clear();

            for (int i = 0; i < RaceJurnal.Count; i++)
            {
                dv.Rows.Add();
                dv[0, i].Value = RaceJurnal[i]["id"];
                dv[1, i].Value = RaceJurnal[i]["tp"];
                dv[2, i].Value = GetDocName(Convert.ToInt32(RaceJurnal[i]["tp"]));
                dv[3, i].Value = RaceJurnal[i]["comment"];
                dv[4, i].Value = RaceJurnal[i]["race_id"];
                dv[5, i].Value = Convert.ToDateTime(RaceJurnal[i]["created"]).ToString("dd MMMM yyyy");
            }

        }

        // Показывает список лучших результатов
        public void ShowBestResults(DataGridView dv, int idTrack, LabelSmooth Records, bool ShowRaceID = false)
        {         
            List<Hashtable> Best = model.GetBestResults(idTrack, UniqueBestResult, DateTime.Now.AddYears(-1000), DateTime.Now.AddYears(1000), 40); // вдруг машину времени изобретут...

            dv.Rows.Clear();
            string pilotname = "";

            int L = Best.Count > 100 ? 100 : Best.Count;
            for (int i = 0; i < L; i++)
            {
                if (Best[i]["surname"].ToString().Length == 0 && Best[i]["name"].ToString().Length == 0)
                {
                    pilotname = "[" + Best[i]["nickname"].ToString() + "]";
                }
                else
                {
                    pilotname = Best[i]["surname"] + " " + Best[i]["name"] + " [" + Best[i]["nickname"].ToString() + "]";
                }

                pilotname = pilotname.Replace("[]", "").Trim();

                dv.Rows.Add();
                dv[9, i].Value = Best[i]["pilot_id"];
                dv[2, i].Value = pilotname;
                dv[3, i].Value = Best[i]["email"];
                dv[4, i].Value = Best[i]["tel"];
                dv[5, i].Value =  Convert.ToDouble(Best[i]["seconds"]).ToString("0.000").Replace(',', '.');
                dv[6, i].Value = Best[i]["track_name"];
                dv[7, i].Value = Convert.ToDateTime(Best[i]["created"]).ToString("dd MMMM yyyy");
                if (ShowRaceID)
                {
                    dv[8, i].Value = Best[i]["race_id"];
                }

                dv[0, i].Value = Best[i]["id"]; // id круга 
            }

            dv.Sort(dv.Columns[5], System.ComponentModel.ListSortDirection.Ascending);

            for (int i = 0; i < dv.Rows.Count; i++)
            {
                dv[1, i].Value = (i + 1).ToString();
            }

            if (idTrack > 0 && dv.Rows.Count > 0)
            {
                Records.Text = "Рекорд -   " + dv[2, 0].Value + "   " + dv[5, 0].Value + " ceк   " + dv[7, 0].Value;
            }
            else
            {
                Records.Text = String.Empty;
            }
        }

        // Показывает баланс пользователей
        public void ShowPilotsCash(DataGridView dv)
        {
            List<Hashtable> Cash = model.GetUsersBallans();
            dv.Rows.Clear();

            for (int i = 0; i < Cash.Count; i++)
            {
                dv.Rows.Add();
                dv[0, i].Value = Cash[i]["id"];

                dv[2, i].Value = Cash[i]["surname"] + " " + Cash[i]["name"];
                dv[3, i].Value = Cash[i]["email"];
                dv[4, i].Value = Cash[i]["tel"];
                dv[5, i].Value = Cash[i]["sum"];
                dv[6, i].Value = model.GetPilotRaceCount(Convert.ToInt32(Cash[i]["id"].ToString()), true);
                dv[7, i].Value = model.GetPilotRaceCount(Convert.ToInt32(Cash[i]["id"].ToString()), false);
            }

            dv.Sort(dv.Columns[5], System.ComponentModel.ListSortDirection.Descending);

            for (int i = 0; i < dv.Rows.Count; i++)
                dv[1, i].Value = (i + 1).ToString();
        }

        // Показывает список груп
        public void ShowGroups(DataGridView dv)
        {
            List<Hashtable> Groups = model.GetAllGroups();
            dv.Rows.Clear();

            for (int i = 0; i < Groups.Count; i++)
            {
                dv.Rows.Add();
                dv[0, i].Value = Groups[i]["id"];
                dv[1, i].Value = Groups[i]["name"];
                dv[2, i].Value = Groups[i]["sale"];
                dv[3, i].Value = Groups[i]["price"];
                dv[4, i].Value = model.GetCountPilotsOfGroup(Groups[i]["id"].ToString());
            }

        }

        // Показывает данные кассы за выбранную дату
        public void GetCassaReport(DataGridView dv, DateTime Date, int reportType, DateTime Date2, PageLister Pages)
        {
            // если наоборот даты, то поменяем местами
            if (Date2 < Date)
            {
                DateTime Date3 = Date;
                Date = Date2;
                Date2 = Date3;
            }

            Date = datetimeConverter.toStartDateTime(Date);
            Date2 = datetimeConverter.toEndDateTime(Date2);

            List<Hashtable> Cassa = model.GetCassaReport(Date, reportType, Date2, Pages);
            dv.Rows.Clear();

            int lastRaceId = -1; // между рейсами вставим чёрную строку, чтобы их разделять в списке
            bool isDivider = false;

            for (int i = 0; i < Cassa.Count; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dv);
                dr.Cells[0].Value = Cassa[i]["id"];
                dr.Cells[1].Value = GetDocName(Convert.ToInt32(Cassa[i]["tp"]));
                dr.Cells[2].Value = GetCassaUser(Convert.ToString(Cassa[i]["user_id"]));
                dr.Cells[3].Value = Double.Parse((Cassa[i]["sign"].ToString().Equals("1") ? "-" : "") + Cassa[i]["sum"].ToString().Replace(".", ",")).ToString();
                dr.Cells[4].Value = Cassa[i]["comment"];
                dr.Cells[5].Value = Cassa[i]["race_id"];
                dr.Cells[6].Value = Cassa[i]["date"];

                if (Cassa[i]["race_id"] != "")
                {
                    try
                    {
                        int currentIdRace = Convert.ToInt32(Cassa[i]["race_id"]);
                        if (currentIdRace != lastRaceId)
                        {
                            lastRaceId = currentIdRace;
                            isDivider = true;       // добавим разделительную строку         
                        }
                    }
                    catch (Exception ex)
                    {
                        isDivider = true;       // добавим разделительную строку
                        lastRaceId = -1;
                    }
                }
                else
                {
                    isDivider = true;       // добавим разделительную строку
                    lastRaceId = -1;
                }

                 if (isDivider == true)
                {                  
                    isDivider = false;
                    DataGridViewRow dr2 = new DataGridViewRow();
                    dr2.CreateCells(dv);
                    dr2.DefaultCellStyle.BackColor = Color.Silver;
                    dr2.Height = 2;
                    dv.Rows.Add(dr2);
                }

                dv.Rows.Add(dr);               
            }
        }

        // Показывает имя документа
        private string GetDocName(int DocNum)
        {
            string ret = String.Empty;

            switch (DocNum)
            {
                case 1: ret = "add_cassa"; break;
                case 4: ret = "get_cassa"; break;
                case 3: ret = "add_cassa_user"; break;
                case 2: ret = "get_cassa_user"; break;
                case 5: ret = "add_user"; break;
                case 6: ret = "get_user"; break;
                case 7: ret = "transf_cash"; break;
                case 10: ret = "start"; break;
                case 11: ret = "pause"; break;
                case 12: ret = "stop"; break;
                case 13: ret = "start_after_pause"; break;
                case 14: ret = "lap_is_deleted"; break;
                case 15: ret = "get_cash"; break;
                case 30: ret = "add_certificate"; break;
                case 31: ret = "activate_certificate"; break;
                case 32: ret = "deactivate_certificate"; break;
            }

            return ret;
        }

        // Показывает информацию о пользователе
        private string GetCassaUser(string ID)
        {
            string ret = String.Empty;


            if (ID == "")
            {
                ret = "Администратор";
            }
            else
            {
                Hashtable User = model.GetPilot(Convert.ToInt32(ID));
                if (User.Count == 0)
                {
                    ret = model.GetProgramUserName(ID.ToString());
                }
                else
                {
                    if (User["nickname"].ToString().Length > 0)
                    {
                        ret = "[" + User["nickname"].ToString() + "] ";
                    }

                    ret = ret + User["name"].ToString() + " " + User["surname"].ToString();
                }

            }
            return ret;
        }

        // Сохраняет цены
        public void SavePrices(DataGridView dv, int week, int raceModeId)
        {
            string DayPrices;
            string[] WeekPrices = new string[8];
            for (int i = 0; i < 7; i++)
            {
                DayPrices = String.Empty;
                for (int j = 0; j < dv.Rows.Count; j++)
                {
                    DayPrices += dv[i, j].Value.ToString() + "/";
                }

                WeekPrices[i + 1] = DayPrices;
            }

            model.SavePrices(week, WeekPrices, raceModeId);

            
            Prices = model.GetPrices();
        }

        // Показывает цену на текущий час, и номер недели ??
        // sgavrilenko - возвращает цену за час для указанного дня и режима
        public string GetPrice(int Day, int Hour, int idRaceMode)
        {
          //  MainForm.log("day: " + Day + ", hour: " + Hour + ", id race mode: " + idRaceMode);
 

            // получить массив цен для указанного режима
            PricesForRaceModes priceForSomeMode = null;

            for (int i = 0; i < Prices.Count; i++)
            {
                if (Prices[i].idRaceMode == idRaceMode)
                {
                    priceForSomeMode = Prices[i];
                    // MainForm.log("i =" + i);
 
                    break;
                }
            }
            
            // вернуть имеющуюся цену или цену по умолчанию
            string RetMess = "100";
            if (priceForSomeMode != null)
            {
                string[] PriceData = priceForSomeMode.Prices[Day].Split(new string[] { "/" }, StringSplitOptions.None);
                RetMess = PriceData[Hour];
            }
            else
            {
                MainForm.log("price is null");
 
            }

            return RetMess;
        }


         
      


        // Конструктор
        public AdminControl(int Year, int Month = 3, int Day = 1)
        {
            CurrentDay = Day;
            CurrentYear = Year;
            CurrentMonth = Month;

          //  MainForm.log("before prokard model init");

            model = new ProkardModel();

         //   MainForm.log("after prokard model init");
           

            List<string> SettFromFile = LoadSettings();

         //   MainForm.log("mysql settings are loaded");
            
            if (SettFromFile.Count > 0)
            {
              //  string[] MySQLData = ParseMySQLConfig(SettFromFile[0]);
                /*
                model.Server = MySQLData[0];
                model.Port = MySQLData[1];
                model.Uid = MySQLData[3];
                model.Password = MySQLData[4];
                model.Database = MySQLData[2];
                 */

            //    MainForm.log("before raceThread init");

                RaceTH = new RaceThread(this);

           //     MainForm.log("after raceThread init");

               
            }



            CurrDate = new DateTime(CurrentYear, CurrentMonth, CurrentDay);
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 6; j++) // TODO тут нужно ставить число TO, в зависимости от выбранного режима
                {
                    Races[i, j] = new RaceClass();
                    Races[i, j].RowPos = i;
                    Races[i, j].ColPos = j;
                    Races[i, j].ID = i.ToString() + "," + j.ToString();
                }
            }

            if (!model.Connect())
            {
                MessageBox.Show("can not connect"); 

                InError = true;
                TypeError = 1;
                ErrorMessage = model.LastError;
            }

        

        //    MainForm.log("before reload table");

            ReloadTable();

         //   MainForm.log("after reload table");

       

            Settings = model.LoadSettings();

         //   MainForm.log("after model.load settings");

     


            UniqueBestResult = Settings["uniquebestres"] == null ? false : Boolean.Parse(Settings["uniquebestres"].ToString());
            
            MaxKarts = model.GetMaxKarts();

       //     MainForm.log("after getMaxKarts");
     

            Prices = model.GetPrices();

         //   MainForm.log("after getPrices");

       
        }

        // Деструктор
        ~AdminControl()
        {
            model.Close();
        }

        // Получает список трасс 
        public void ShowAllTracks(DataGridView dv)
        {

            List<Hashtable> row = model.GetAllTracks();
            dv.Rows.Clear();

            for (int i = 0; i < row.Count; i++)
            {
                dv.Rows.Add();
                dv.Rows[i].Height = 30;

                dv[0, i].Value = row[i]["id"].ToString();
                dv[1, i].Value = row[i]["name"].ToString();
                dv[2, i].Value = row[i]["length"].ToString();
                dv[3, i].Value = row[i]["file"].ToString();
            }

        }

        // Присваивает матрице рейсов - данные за выбранный день
        private void LoadRaces(int Year, int Month, int Day)
        {
            int Minutes = 0;

            for (int i = 0; i < 24; i++)
            {
                Races[i, 0].Hour = i.ToString();

                Minutes = 0;
                for (int j = 1; j < 6; j++)
                {

                    Races[i, j].Minute = Minutes.ToString().Length == 1 ? "0" + Minutes.ToString() : Minutes.ToString();
                   if ( Races[i, j].Minute == "60")
                    {
                         Races[i, j].Minute = "59"; // классно придумано, и всё потому-что кто-то не знает, что данные бывают разных типов и использует string вместо int
                    }
                    Races[i, j].Hour = i.ToString();
                    Races[i, j].Date = new DateTime(CurrentYear, CurrentMonth, CurrentDay, i, Minutes >= 60 ? 59 : Minutes, 0);
                    model.GetRace(new DateTime(CurrentYear, CurrentMonth, CurrentDay), Races[i, j]);
                    
                    Races[i, j].Karts = model.GetRaceKarts(Races[i, j].RaceID);

                    Minutes += 15;

                    
                }
            }

           /*
            MessageBox.Show("before GC");
            GC.Collect();

            MessageBox.Show("after GC");
            */
        }

        // Обновляет таблицу с заданной датой
        public void ReloadTable()
        {
            CurrDate = new DateTime(CurrentYear, CurrentMonth, CurrentDay);

           // MessageBox.Show("ReloadTable 1");
            LoadRaces(CurrentYear, CurrentMonth, CurrentDay);
           // MessageBox.Show("ReloadTable 2");
        }

       

        // Выводит список найденных пилотов по фильтру ??
        public void ShowPilots(DataGridView dv, List<int> existPilots,  string filter = "")
        {
         //   MessageBox.Show("ShowPilots");
            dv.Rows.Clear();
                        
            Hashtable res = model.GetAllPilots(filter), row = new Hashtable();
            for (int i = 0; i < res.Count; i++)
            {
                 row = (Hashtable)res[i];

                // если в списке пилотов заезда уже есть этот пилот, не показываем в списке для фильтра
                if(existPilots.IndexOf(Convert.ToInt32(row["id"])) >= 0)
                {
                    continue;
                }

                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dv);

                dr.Cells[0].Value = row["id"].ToString();
                dr.Cells[1].Value = row["name"].ToString();
                dr.Cells[2].Value = row["surname"].ToString();
                dr.Cells[3].Value = row["nickname"].ToString();
                dr.Cells[4].Value = row["email"].ToString();
                dr.Cells[5].Value = Convert.ToDateTime(row["created"].ToString()).ToString("yyyy-MM-dd");
                dr.Cells[6].Value = row["banned"];
                dv.Rows.Add(dr);

                /*
                dv.Rows.Add();

            

                dv.Rows[i].Cells[0].Value = row["id"].ToString();
                dv.Rows[i].Cells[1].Value = row["name"].ToString();
                dv.Rows[i].Cells[2].Value = row["surname"].ToString();
                dv.Rows[i].Cells[3].Value = row["nickname"].ToString();
                dv.Rows[i].Cells[4].Value = row["email"].ToString();
                dv.Rows[i].Cells[5].Value = Convert.ToDateTime(row["created"].ToString()).ToString("yyyy-MM-dd");
                dv.Rows[i].Cells[6].Value = row["banned"];
                 */
            }
             
        }

        // Посмотреть всю статистику картов
        public void ShowAllKartStatistic(DataGridView dv, DateTime D1, DateTime D2)
        {
            List<Hashtable> row = model.GetAllKartsStatistic(Convert.ToDouble(Settings["track_length"]), D1, D2);
            dv.Rows.Clear();

            for (int i = 0; i < row.Count; i++)
            {
                dv.Rows.Add();

                dv[0, i].Value = row[i]["id"];
                dv[1, i].Value = row[i]["number"];
                dv[2, i].Value = row[i]["transponder"];
                dv[3, i].Value = row[i]["repairs"];

                dv[4, i].Value = row[i]["races_all"];
                dv[5, i].Value = Convert.ToDouble(row[i]["length_all"]) / 1000;
                dv[6, i].Value = row[i]["fuel_add"];
                dv[7, i].Value = Math.Abs(Convert.ToDouble(row[i]["fuel_rem"].ToString()));
            }
        }

        // Выводит список картов
        public void ShowKarts(DataGridView dv)
        {
            dv.Rows.Clear();

            Hashtable res = model.GetAllKarts(), row = new Hashtable();
            for (int i = 0; i < res.Count; i++)
            {
                dv.Rows.Add();

                row = (Hashtable)res[i];

                dv.Rows[i].Cells[0].Value = row["id"].ToString();
                dv.Rows[i].Cells[1].Value = row["name"].ToString();
                dv.Rows[i].Cells[2].Value = row["number"].ToString();
                dv.Rows[i].Cells[3].Value = row["transponder"].ToString();
                dv.Rows[i].Cells[4].Value = Convert.ToDateTime(row["created"]).ToString("yyyy-MM-dd");
                dv.Rows[i].Cells[5].Value = row["repair"].ToString();
                dv.Rows[i].Cells[6].Value = row["message_id"].ToString();
                dv.Rows[i].Cells[7].Value = row["wait"].ToString();
                dv.Rows[i].Cells[8].Value = model.GetKartFuel(row["id"].ToString());
            }
        }

        // Выводит свободные карты у текущего рейса
        public void ShowRaceKarts(ComboBox cb, RaceClass Race)
        {
            Hashtable res = model.GetAllKarts(), rw = new Hashtable();

            cb.BeginUpdate();
            cb.Items.Clear();
            cb.Items.Add("Без резервирования");

            for (int i = 0; i < res.Count; i++)
            {
                rw = (Hashtable)res[i];

                if (!KartInRace(Race, rw["number"].ToString()))
                {
                    cb.Items.Add(rw["number"].ToString());
                }
            }

            cb.SelectedIndex = 0;
            cb.EndUpdate();
        }

        // Прорисовка картов в рейсе
        public void ShowRaceKarts(DataGridView dv, int Repair = 0, int ColCount = 2)
        {
            dv.Rows.Clear();
            dv.Columns.Clear();
            int CellSize = 50;
            for (int i = 1; i <= ColCount; i++)
            {
                dv.Columns.Add("l" + i.ToString(), "Ряд " + i.ToString());
                dv.Columns[i - 1].Width = CellSize;
            }

            Hashtable res = model.GetAllKarts(Repair), rw = new Hashtable();

            int col = 0, row = 0;
            dv.Rows.Add();
            for (int i = 0; i < res.Count; i++)
            {
                rw = (Hashtable)res[i];

                if (col >= ColCount)
                {
                    col = 0;
                    dv.Rows.Add();
                    row++;
                }

                dv.Rows[row].Cells[col].Value = rw["number"].ToString();
                dv.Rows[row].Height = CellSize;
                col++;
            }
        }

        // Проверяет, используется данный карт в рейсе
        public bool KartInRace(int i, int j, string KartNum)
        {
            bool ret = false;

            if (Races[i, j].Karts.Count > 0)
                ret = Races[i, j].Karts.Contains(KartNum);
            return ret;
        }
        public bool KartInRace(RaceClass Race, string KartNum)
        {
            bool ret = false;

            if (Race.Karts.Count > 0)
                ret = Race.Karts.Contains(KartNum);

            return ret;
        }

        //Получает номер дня недели
        public int GetWeekDayNumber()
        {
            int ret = 1;
            DateTime date = new DateTime(CurrentYear, CurrentMonth, CurrentDay);

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: ret = 1; break;
                case DayOfWeek.Tuesday: ret = 2; break;
                case DayOfWeek.Wednesday: ret = 3; break;
                case DayOfWeek.Thursday: ret = 4; break;
                case DayOfWeek.Friday: ret = 5; break;
                case DayOfWeek.Saturday: ret = 6; break;
                case DayOfWeek.Sunday: ret = 7; break;
            }

            return ret;
        }


        //Получает номер дня недели
        public int GetWeekDayNumber(DateTime day)
        {
            int ret = 1;
           // DateTime date = new DateTime(CurrentYear, CurrentMonth, CurrentDay);

            switch (day.DayOfWeek)
            {
                case DayOfWeek.Monday: ret = 1; break;
                case DayOfWeek.Tuesday: ret = 2; break;
                case DayOfWeek.Wednesday: ret = 3; break;
                case DayOfWeek.Thursday: ret = 4; break;
                case DayOfWeek.Friday: ret = 5; break;
                case DayOfWeek.Saturday: ret = 6; break;
                case DayOfWeek.Sunday: ret = 7; break;
            }

            return ret;
        }
       


        // Получает минуты по этапу
        public static int GetStMinutesFromIndex(int i)
        {
            int Minutes = 0;
            for (int j = 1; j < i; j++)
            {
                Minutes += 15;
            }
            if (Minutes >= 60)
            {
                Minutes = 59;
            }
            
                return Minutes;
            
        }

        public RaceClass GetRace(int Hour, int Stage)
        {
            return Races[Hour, Stage];
        }

        public string[] ParseMySQLConfig(string s)
        {
            s = Decrypt(s);
            return s.Split(new string[] { "/" }, StringSplitOptions.None);
        }

        // чтение из файла
        public List<string> LoadSettings()
        {
            List<string> FileData = new List<string>();
            try
            {
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\settings.ini"))
                {
                    // создание объекта StreamReader
                    System.IO.StreamReader sr = new System.IO.StreamReader(Environment.CurrentDirectory + "\\settings.ini");
                    string input;
                    do
                    {
                        input = sr.ReadLine();
                        if (input != "")
                        {
                            FileData.Add(input);
                        }
                    } while (sr.Peek() != -1);
                    sr.Close();
                }
            }
            finally
            {


            }
            return FileData;
        }

        // запись в файл
        public void SaveSettings(List<string> FileData)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.CurrentDirectory + "\\settings.ini");
                for (int i = 0; i < FileData.Count; i++)
                {
                    sw.WriteLine(FileData[i]);
                }

                sw.Flush();
                sw.Close();
            }
            finally
            { }
        }


        public string Decrypt(string cipherString, bool useHashing = true)
        {
            byte[] keyArray;

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();

            string key = "yt83MJls993s1mldds1KKGWUnLjhsGGCC";

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string Encrypt(string toEncrypt, bool useHashing = true)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = "yt83MJls993s1mldds1KKGWUnLjhsGGCC";
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            
            string result = Convert.ToBase64String(resultArray, 0, resultArray.Length);

           // MessageBox.Show(result);
            return result;
        }

        
        /// <summary>
        /// Показывает все события
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="objectType"> -1 события от неизвестно кого ???, 0 - Kart, 1 - Pilot, 2 - Event</param>
        /// <param name="someDate"></param>
        /// <param name="eventType"> 0 = все, 1 = будущие, 2 = прошедшие, 3 - только на какую-то дату</param>
        /// <param name="ConvertDate"></param>
        public void ShowEvents(DataGridView dv, int objectType, DateTime someDate, int eventType, bool ConvertDate = false)
        {
            List<Hashtable> row = model.GetAllEvents(objectType, someDate, eventType);
            dv.Rows.Clear();

            for (int i = 0; i < row.Count; i++)
            {
                dv.Rows.Add();
                dv.Rows[i].Height = 40;

                dv[0, i].Value = row[i]["id"].ToString();
                if (ConvertDate)
                    dv[1, i].Value = Convert.ToDateTime(row[i]["date"].ToString()).ToString("dd MMMM");
                else
                    dv[1, i].Value = Convert.ToDateTime(row[i]["date"].ToString()).ToString("yyyy-MM-dd");

                dv[2, i].Value = row[i]["subject"].ToString();
                dv[3, i].Value = row[i]["message"].ToString();
            }
        }
    }
}
