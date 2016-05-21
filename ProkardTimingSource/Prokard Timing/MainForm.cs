using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DateTimeExtensions;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Prokard_Timing.model;
using FontStyle = System.Drawing.FontStyle;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;


namespace Prokard_Timing
{
    public partial class MainForm : Form
    {
        public AdminControl admin;
        public LastSelectClass LastSelect = new LastSelectClass();
        private string GlobalTime = String.Empty;
        public string NextRaceNum;
        public string NextRaceTime;
        int NextRaceI = 0, NextRaceJ = 0;
        crazykartContainer db = new crazykartContainer();

        public MainForm()
        {
           // MessageBox.Show("MainForm"); 
            log("checkDb");
           // return;
            log("MainForm");

            InitializeComponent();

            log("after components init");

          //  MessageBox.Show("after init"); 

            //Устанавливаем БолдерДатес
            monthCalendar1.BoldedDates = null;


            //Создаем строку для датагрида
          //  dataGridView1.DoubleBuffered(false);
          //  dataGridView2.DoubleBuffered(true);
          //  dataGridView3.DoubleBuffered(true);

         //   MessageBox.Show("b"); 
            // Создаем класс обработчик

         //   log("before admin init");
            admin = new AdminControl(monthCalendar1.TodayDate.Year, monthCalendar1.TodayDate.Month, monthCalendar1.TodayDate.Day);

         //   log("after admin init");

         //   MessageBox.Show("c"); 
            if (Convert.ToBoolean(admin.Settings["enter_password"]))
            {
                войтиВРежимToolStripMenuItem.PerformClick();
            }
            else
            {
                admin.IS_ADMIN = admin.IS_SUPER_ADMIN = false;
                admin.IS_USER = true;
                admin.USER_ID = 0;
            }
          //  DataTable admins = localSetts.GetAdmins();

            List<Hashtable> pu = admin.model.GetAllPgrogramUsers(0);//localSetts.GetAdmins();


            if (pu.Count == 0)
            {
                string password = admin.Encrypt("1111");
               // localSetts.addSysUser("test", password, "2", "test", "test", "");
                admin.model.AddProgramUser("test", password, "2", "test", "test", "");
            }

            ShowGridTable();
            ShowEvents();

      //      MessageBox.Show("d"); 
            toolStripLabel1.Text = monthCalendar1.SelectionStart.Date.ToString("dd MMMM");

            ShowError();

            string YesterdaySumm = admin.model.GetCashFromCassa(DateTime.Now.AddDays(-1),true,false,false);
            double YSumm = -1;
            if (YesterdaySumm.Length > 0)
                YSumm = Double.Parse(YesterdaySumm);

        //  MessageBox.Show(YesterdaySumm);

            if (YSumm > 0) {


               // MessageBox.Show(YesterdaySumm);
                admin.model.Jurnal_Cassa("7", -1, -1, YesterdaySumm, "0", "Добавление кассового остатка за прошлые дни");
                admin.model.Jurnal_Cassa("7", -1, -1, YesterdaySumm, "1", "Перенос кассы на следующий день", true);
            }


        //    MessageBox.Show("MainForm inited");

           
        }
        private void ShowSportModeGridTable()
        {
            
          

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = startDate.AddDays(1).AddMilliseconds(-1);

            // получили набор заездов за выбранный день
            List<races> someRaces = db.races.Where(m => m.is_race == true).Where(m => m.created >= startDate).Where(m => m.created <= endDate).OrderBy(m => m.created).ToList();

            DateTime startTime = DateTime.Now;
            racesList_dataGridView1.SuspendLayout();

            racesList_dataGridView1.Rows.Clear();
            int i, j, num_race = 0, dayNumber = admin.GetWeekDayNumber();
            
            // узнаем количество строк
            double rowsCount = someRaces.Count() / 4; // по 4 ячейки в строке и добавим справа дополнительную
            rowsCount = Math.Ceiling(rowsCount); // 

            int currentRaceNumber = 0;

                          
            for (i = 0; i < rowsCount; i++)
            {
                racesList_dataGridView1.Rows.Add();
              //  racesList_dataGridView1.Rows[i].Cells[0].Value = someRaces.ElementAt(currentRaceNumber).created.Hour; // дата создания? // admin.Races[i, 0].Hour;
             //   racesList_dataGridView1.Rows[i].Height = 70;


                

                /*

                if (i < Convert.ToInt32(admin.Settings["time_start"]) || i > Convert.ToInt32(admin.Settings["time_end"]))
                {
                    racesList_dataGridView1.Rows[i].Visible = false;
                }
                 */
                /*
                if (admin.CurrentDay == DateTime.Now.Day && Convert.ToBoolean(admin.Settings["time_wrap"]))
                {
                    if (((i + Convert.ToInt16(admin.Settings["wrap_pos"])) < DateTime.Now.Hour) && !показатьПрошедшиеЧасыToolStripMenuItem.Checked)
                    {
                        racesList_dataGridView1.Rows[i].Visible = false;
                    }
                }
                 */

                for (j = 0; j < 5; j++)
                {
                  /*
                    if (i >= Convert.ToInt32(admin.Settings["time_start"]) && i <= Convert.ToInt32(admin.Settings["time_end"]) && j != 5)
                    {
                        num_race++;
                    }
                   */
                    
                    racesList_dataGridView1.Rows[i].Cells[j].Value = someRaces.ElementAt(currentRaceNumber).stat;// admin.Races[i, j].Status;

                    /*
                    if (j == 5)
                    {
                        admin.Races[i, j].RaceNum = admin.DopRace + num_race;
                    }
                    else
                    {*/

             //       admin.SportRaces.ElementAt(currentRaceNumber).RaceNum = currentRaceNumber;
                      //  admin.Races[i, j].RaceNum = num_race;
                   // }
                    if (racesList_dataGridView1.Rows[i].Visible)
                    {
                        //admin.Races[i, j].RaceSum =
                      
                        //  double.Parse(admin.GetPrice(dayNumber, i, 1)); // TODO режим гонки
                    }

                    currentRaceNumber++;
                }

            }


            racesList_dataGridView1.Refresh();
            racesList_dataGridView1.ResumeLayout(false);

            labelSmooth1.Text = "Трек - " + admin.model.GetTrackName(admin.Settings["default_track"].ToString());
            SetControlButton();

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ShowGridTable", Logger.LogType.info, executionTime);


        }

        // таблица на главной форме, где указана цена за час и запланированные/ совершённые заезды
        private void ShowGridTable()
        {
            if (competitionMode_menuItem.Checked)
            {
                // режим соревнований без привязки к пятнадатиминутной сетке
                ShowSportModeGridTable();
            }
            else
            {

                DateTime startTime = DateTime.Now;
                racesList_dataGridView1.SuspendLayout();

                racesList_dataGridView1.Rows.Clear();
                int i, j, num_race = 0, dayNumber = admin.GetWeekDayNumber();
                for (i = 0; i < 24; i++)
                {
                    racesList_dataGridView1.Rows.Add();
                    racesList_dataGridView1.Rows[i].Cells[0].Value = admin.Races[i, 0].Hour;
                    racesList_dataGridView1.Rows[i].Height = 70;

                    if (i < Convert.ToInt32(admin.Settings["time_start"]) || i > Convert.ToInt32(admin.Settings["time_end"])) racesList_dataGridView1.Rows[i].Visible = false;

                    if (admin.CurrentDay == DateTime.Now.Day && Convert.ToBoolean(admin.Settings["time_wrap"]))
                        if (((i + Convert.ToInt16(admin.Settings["wrap_pos"])) < DateTime.Now.Hour) && !показатьПрошедшиеЧасыToolStripMenuItem.Checked) racesList_dataGridView1.Rows[i].Visible = false;

                    for (j = 1; j < 6; j++)
                    {
                        if (i >= Convert.ToInt32(admin.Settings["time_start"]) && i <= Convert.ToInt32(admin.Settings["time_end"]) && j != 5)
                        {
                            num_race++;
                        }

                        racesList_dataGridView1.Rows[i].Cells[j].Value = admin.Races[i, j].Status;

                        if (j == 5)
                        {
                            admin.Races[i, j].RaceNum = admin.DopRace + num_race;
                        }
                        else
                        {
                            admin.Races[i, j].RaceNum = num_race;
                        }
                        if (racesList_dataGridView1.Rows[i].Visible)
                        {
                            admin.Races[i, j].RaceSum =
                                double.Parse(admin.GetPrice(dayNumber, i, 1, 1)); // TODO режим гонки
                        }
                    }

                }


                racesList_dataGridView1.Refresh();
                racesList_dataGridView1.ResumeLayout(false);

                labelSmooth1.Text = "Трек - " + admin.model.GetTrackName(admin.Settings["default_track"].ToString());
                SetControlButton();

                TimeSpan executionTime = DateTime.Now - startTime;
                Logger.AddRecord("ShowGridTable", Logger.LogType.info, executionTime);

                //dataGridView1.Rows[LastSelect.Row].Cells[LastSelect.Col].Selected = true;
                //dataGridView1.CurrentCell = dataGridView1.SelectedCells[0];
            }
        }



        public static void log(string message)
        {
            File.AppendAllText("C:\\log.txt", message + "\r\n");
        }








        private void ShowError()
        {
            if (admin.InError)
            {
                string ErrType = "Ошибка - ", Message;

                switch (admin.TypeError)
                {
                    case 1:
                        {
                            ErrType = "Ошибка с базой - "; MessageBox.Show("Ошибка в подключении к базе данных. \r\nПрограмма работает в ограниченном режиме ");

                            for (int i = 0; i < menuStrip1.Items.Count; i++)
                            {
                                menuStrip1.Items[i].Enabled = false;
                            }

                            окнаToolStripMenuItem.Visible = администраторToolStripMenuItem.Visible = статистикаToolStripMenuItem.Visible = кассаToolStripMenuItem.Visible = пилотыToolStripMenuItem.Visible = false;
                            GlobalTimer.Enabled = false;

                            группыToolStripMenuItem.Visible = ценыToolStripMenuItem.Visible = событияToolStripMenuItem.Visible = трассыToolStripMenuItem1.Visible = управлениеToolStripMenuItem.Visible = false;

                            MainPanel.Enabled = false;
                            управлениеToolStripMenuItem1.Enabled = true;
                            RaceModesToolStripMenuItem.Enabled = true;
                            настройкиToolStripMenuItem.Enabled = true;
                            справкаToolStripMenuItem.Enabled = true;


                        } break;
                }

                Message = admin.ErrorMessage;

                ErrorString.Text = ErrType + Message;
            }
        }

        // Показываем события на форме
        private void ShowEvents()
        {
            admin.ShowEvents(eventsAnonse_dataGridView3, 2, DateTime.Now, 3, true);

            eventsAnonse_dataGridView3.Visible = label3.Visible = eventsAnonse_dataGridView3.Rows.Count > 0;

            //Установка болдерДатес

            DateTime[] BolderDates = new DateTime[eventsAnonse_dataGridView3.Rows.Count + 2];
            int i = 0;

            if (Convert.ToBoolean(admin.Settings["show_events"]))
                for (i = 0; i < eventsAnonse_dataGridView3.Rows.Count; i++)
                    BolderDates[i] = Convert.ToDateTime(eventsAnonse_dataGridView3[1, i].Value.ToString());

            BolderDates[i] = DateTime.Now;
            BolderDates[i + 1] = DateTime.Now.Last(DayOfWeek.Sunday);
            monthCalendar1.BoldedDates = BolderDates;
        }

        private bool CreateRace(int i, int j)
        {
            return admin.CreateRace(i, j);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.ColumnIndex > 0)
            {
                if (CreateRace(e.RowIndex, e.ColumnIndex))
                {
                    admin.ReloadTable();
                    ShowGridTable();
                }

                LastSelect.Row = e.RowIndex;
                LastSelect.Col = e.ColumnIndex;

                if (admin.Races[LastSelect.Row, LastSelect.Col].Status > 0)
                    ShowRaceForm();
            }
            */
            ShowRaceForm();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            SetDate();
        }

        private void DrawRaceInfo(DataGridViewCellPaintingEventArgs e)
        {
            Rectangle rec = ResizeRect(e.CellBounds, -2);

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (admin.Races[e.RowIndex, e.ColumnIndex].Status != 0 )
                {
                    int Pilots = admin.model.GetRacePilots(admin.Races[e.RowIndex, e.ColumnIndex].RaceID).Count();
                    int Max = admin.MaxKarts;

                    double Kof = (Pilots * 100) / (Max == 0 ? 1 : Max);

                    int width = (int)Math.Round((rec.Width * Kof) / 100) - 5;

                    Color col = new Color();
                    if (Kof <= 30) col = Color.Green;
                    if (Kof <= 50 && Kof > 30) col = Color.YellowGreen;
                    if (Kof <= 90 && Kof > 50) col = Color.Yellow;
                    if (Kof > 90) col = Color.Red;

                    /*
                    using (Brush br = new LinearGradientBrush(new Point(5, 0), new Point(rec.Left + width, 0), Color.LightBlue, col))
                    {
                        if (width > 0)
                        {
                            e.Graphics.FillRectangle(br, rec.Left + 5, rec.Bottom - 40, width - 5, 6);
                        }
                        e.Graphics.DrawRectangle(Pens.DarkGray, rec.Left + 5, rec.Bottom - 40, width - 5, 6);
                    }
                     */

                    // отрисовка шкалы загрузки рейса
                    try
                    {
                      //   Brush br = new LinearGradientBrush(new Point(5, 0), new Point(rec.Left + width, 0), Color.LightBlue, col);
                       
                        Brush br = new LinearGradientBrush(new Point(5, 0), new Point(rec.Left + width, 0), Color.LightSeaGreen, col);
                        if (width > 0)
                        {
                            e.Graphics.FillRectangle(br, rec.Left + 5, rec.Bottom - 40, width - 5, 6);
                            e.Graphics.DrawRectangle(Pens.DarkGray, rec.Left + 5, rec.Bottom - 40, width - 5, 6);
                    
                        }
                         
                      //  e.Graphics.DrawRectangle(Pens.DarkGray, rec.Left + 5, rec.Bottom - 40, width - 5, 6);
                    }
                    catch (Exception ex)
                    {
                       // MessageBox.Show(ex.Message);
                    }

                    using (Font f2 = new Font("Tahoma", 10))
                    {
                        string content = Pilots.ToString() + "/" + Max.ToString();
                        e.Graphics.DrawString(content, f2, e.ColumnIndex == 5 ? Brushes.DarkGray : Brushes.Black, rec.Left + 1, rec.Bottom - 17);
                    }


                }
            }
        }

        private Rectangle ResizeRect(Rectangle Rect, int Size)
        {
            // Rect.Height += Size-1;
            //Rect.Width += Size;

            Rect.Y += Size + 1;
            return Rect;
        }

        private Rectangle ResizeRect(Rectangle Rect, int H, int W)
        {
            Rect.Height += H;
            Rect.Width += W;
            Rect.X += W;
            Rect.Y += H;
            return Rect;
        }

        // Прорисовка ячеек заезда
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            int clwidth = 820 - 70 - 150;
            int mint = DateTime.Now.Minute;
            int LinePos = Convert.ToInt32((mint * clwidth) / 60) + 72;


            Color col = new Color();
            Rectangle Rect = ResizeRect(e.CellBounds, -2);
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            // Прорисовка зебры
            if (e.RowIndex % 2 == 0)
            {
                e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
            }
            // else using (Brush br = new SolidBrush(Color.FromArgb(233, 233, 233))) e.Graphics.FillRectangle(br, e.CellBounds);

            else
            {
                using (Brush br = new LinearGradientBrush(new Point(0, 0), new Point(e.ClipBounds.Width, 0), Color.FromArgb(233, 233, 233), Color.FromArgb(233, 233, 233))) //  серый фон для нечётных строк Color.FromArgb(233, 233, 233) Color.Beige
                {
                    e.Graphics.FillRectangle(br, e.CellBounds);
                }
            }

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int Y = e.RowIndex != DateTime.Now.Hour && admin.CurrentDay == DateTime.Now.Day ? 200 : 255;

                // Прорисовка заездов
                long tk20 = DateTime.Now.AddMinutes(-admin.TimeToCreate).Ticks;
                if (admin.CurrentDay == DateTime.Now.Day && 
                    admin.CurrentMonth == DateTime.Now.Month && 
                    admin.CurrentYear == DateTime.Now.Year)
                {
                    if (admin.Races[e.RowIndex, e.ColumnIndex].Status == 1 && (
                        //(DateTime.Now.Hour > e.RowIndex) ||
                        // (DateTime.Now.Hour == e.RowIndex && (DateTime.Now.Minute - admin.TimeToCreate) > Convert.ToInt32(admin.Races[e.RowIndex, e.ColumnIndex].Minute)))
                       (admin.Races[e.RowIndex, e.ColumnIndex].Date.Ticks < tk20))
                        )
                    {
                        col =Color.FromArgb(Y, 255, 209, 209); // проваленные заезды Color.FromArgb(Y, 255, 209, 209);  
                    }
                    else
                    {
                        if (admin.Races[e.RowIndex, e.ColumnIndex].Status == 2)
                        {
                            col = Color.FromArgb(Y, 207, 255, 178); // завершён или отменён
                        }
                    }
                    using (Brush brush = new SolidBrush(col))
                    {
                        e.Graphics.FillRectangle(brush, Rect);
                    }

                }
                /*
            if (admin.Races[e.RowIndex, e.ColumnIndex].Status == 1)
                using (Brush brush = new LinearGradientBrush(new Point(0, e.CellBounds.Top), new Point(0, e.CellBounds.Bottom), col, Color.Beige)) e.Graphics.FillRectangle(brush, Rect);
            */
                e.Graphics.DrawLine(Pens.Red, LinePos, 0, LinePos, 140);

                // заполнить информацию по заезду - сколько пилотов там
                DrawRaceInfo(e);

                // Прорисовка выбранной ячейки
                if ((e.State & DataGridViewElementStates.Selected) != 0 && e.ColumnIndex > 0)
                {
                    Color c = Color.FromArgb(33, 0, 0, 200);
                    using (Brush b = new SolidBrush(c)) e.Graphics.FillRectangle(b, Rect);
                }

                // Прорисовка бордюра
                if (e.RowIndex == DateTime.Now.Hour && admin.CurrentDay == DateTime.Now.Day)
                {
                    e.Graphics.DrawRectangle(Pens.DarkGray, Rect);
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.DarkGray, Rect);
                }



                // Прорисовка ячейки над которым находится курсор
                if (e.RowIndex == LastSelect.RowHit && 
                    e.ColumnIndex == LastSelect.ColHit && e.ColumnIndex > 0)
                {
                    Color c = Color.FromArgb(33, 150, 200, 200);
                    using (Brush b = new SolidBrush(c)) e.Graphics.FillRectangle(b, Rect);
                }

                string content = "";
                if (e.ColumnIndex == 0)
                {
                    // Рисуем Время (первый столбец)
                    using (Font f1 = new Font("Calibri", 20))
                    {
                        content = admin.Races[e.RowIndex, e.ColumnIndex].Hour;
                        e.Graphics.DrawString(content, f1, Brushes.Black, Rect.X + 20, Rect.Y + 18);
                    }
                }
                else
                // if (e.ColumnIndex != 5)
                {
                    // Рисуем остальную информацию 

                    // Часы
                    using (Font f1 = new Font("Tahoma", 12))
                    {
                        if (e.ColumnIndex == 5)
                            content = admin.Races[e.RowIndex, 0].Hour + ":**";
                        else
                            content = admin.Races[e.RowIndex, 0].Hour + ":" + admin.Races[e.RowIndex, e.ColumnIndex].Minute;
                        e.Graphics.DrawString(content, f1, e.ColumnIndex == 5 ? Brushes.DarkGray : Brushes.Black, Rect);
                    }

                    // Номер рейса
                    using (Font f2 = new Font("Tahoma", 10))
                    {
                        if (e.ColumnIndex == 5)
                            content = (admin.Races[e.RowIndex, e.ColumnIndex].RaceNum - admin.DopRace).ToString() + "a";
                        else
                            content = admin.Races[e.RowIndex, e.ColumnIndex].RaceNum.ToString();
                        e.Graphics.DrawString(content, f2, e.ColumnIndex == 5 ? Brushes.DarkGray : Brushes.Black, e.CellBounds.Right - 25, e.CellBounds.Top);
                    }

                    // Стоимость рейса
                    using (Font f2 = new Font("Tahoma", 10))
                    {
                        System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                        drawFormat.Alignment = StringAlignment.Far;
                        content = admin.GetPrice(admin.GetWeekDayNumber(), e.RowIndex, 1, 1) + " грн"; //TODO режим гонки
                        e.Graphics.DrawString(content, f2, Brushes.DarkSlateBlue, Rect.Right, Rect.Bottom - 17, drawFormat);
                        drawFormat.Dispose();
                    }
                }

            }
            e.Handled = true;

        }

        private void SetDate()
        {

            admin.CurrentYear = monthCalendar1.SelectionStart.Year;
            admin.CurrentMonth = monthCalendar1.SelectionStart.Month;
            admin.CurrentDay = monthCalendar1.SelectionStart.Day;
            admin.ReloadTable();

            ShowGridTable();

            toolStripLabel1.Text = monthCalendar1.SelectionStart.Date.ToString("dd MMMM");

            SetControlButton();
        }

        //One month back
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            monthCalendar1.SelectionStart = monthCalendar1.SelectionStart.AddMonths(-1);
            SetDate();
        }


        // Отображает следующий рейс на боковой панеле
        private void SetNextRace()
        {
            if (admin.RaceTH != null && admin.RaceTH.Status == 0)
            {
                int H, M, I, J;

                admin.GetNextRaceTime(out H, out M, out I, out J);

                if (admin.Races[I, J].Status == 2)
                    admin.GetNextRaceTime(out H, out M, out I, out J, true);

                if (H <= Convert.ToInt32(admin.Settings["time_end"]))
                {
                    label4.Text = "Заезд №" + (admin.Races[I, J].RaceNum > admin.DopRace ? (admin.Races[I, J].RaceNum - admin.DopRace).ToString() + "a" : admin.Races[I, J].RaceNum.ToString()) + " Отправление в " + admin.Races[I, J].Hour + ":" + admin.Races[I, J].Minute;
                    label2.Text = H.ToString() + ":" + (M.ToString().Length == 1 ? "0" + M.ToString() : M.ToString());

                    NextRaceTime = label2.Text;
                    NextRaceNum = (admin.Races[I, J].RaceNum > admin.DopRace ? (admin.Races[I, J].RaceNum - admin.DopRace).ToString() + "a" : admin.Races[I, J].RaceNum.ToString());

                    NextRaceI = I;
                    NextRaceJ = J;
                }
                else
                {

                    label4.Text = label2.Text = String.Empty;
                    NextRaceI = NextRaceJ = -1;
                }
            }
        }



        // Установка кнопок
        private void SetControlButton()
        {
            long ntick = DateTime.Now.Ticks;
            long n20ticks = DateTime.Now.AddMinutes(-admin.TimeToCreate).Ticks;

            bool RacesEnd =
                admin.Races[LastSelect.Row, LastSelect.Col].Status == 2 ||
            admin.Races[LastSelect.Row, LastSelect.Col].Date < DateTime.Now.Date ||
                (admin.Races[LastSelect.Row, LastSelect.Col].Date.DayOfYear == DateTime.Now.Date.DayOfYear &&
                // (Convert.ToInt32(admin.Races[LastSelect.Row, LastSelect.Col].Hour) < DateTime.Now.Hour ||

                  (admin.Races[LastSelect.Row, LastSelect.Col].Date.Ticks <= n20ticks
                //                  (Convert.ToInt32(admin.Races[LastSelect.Row, LastSelect.Col].Hour) == DateTime.Now.Hour && Convert.ToInt32(admin.Races[LastSelect.Row, LastSelect.Col].Minute) < (DateTime.Now.Minute - 20))


                   ));


            if (RacesEnd || pilotsList_dataGridView2.Rows.Count >= admin.MaxKarts)
                toolStripMenuItem1.Enabled = toolStripButton6.Enabled = false;
            else

                toolStripMenuItem1.Enabled = toolStripButton6.Enabled = true;

            // Кнопка на боковой панели которая открывает рейс
            toolStripButton11.Enabled = перейтиКЗаездуToolStripMenuItem1.Enabled = (!RacesEnd && racesList_dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0) || (RacesEnd && admin.Races[LastSelect.Row, LastSelect.Col].Status > 0);


            if (!RacesEnd && pilotsList_dataGridView2.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int SelectedRow = pilotsList_dataGridView2.SelectedRows[0].Index;

                delPilot_toolStripButton7.Enabled = racesList_dataGridView1.Rows[SelectedRow].Cells[1].Value != null;
                toolStripButton5.Enabled = Convert.ToBoolean(pilotsList_dataGridView2[5, SelectedRow].Value);
            }
            else
                delPilot_toolStripButton7.Enabled = toolStripButton5.Enabled = false;
            SetNextRace();
        }


        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                LastSelect.Row = e.RowIndex;
                LastSelect.Col = e.ColumnIndex;

                if (e.Button == MouseButtons.Right)
                {
                    racesList_dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    contextMenuStrip1.Location = e.Location;
                    contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
                }
                else
                    if (e.Button == MouseButtons.Left)
                    {
                        if (admin.Races[e.RowIndex, e.ColumnIndex].RaceID > 0)
                            admin.ShowRacePilots(admin.Races[e.RowIndex, e.ColumnIndex].RaceID, pilotsList_dataGridView2);
                        else
                            pilotsList_dataGridView2.Rows.Clear();

                        pilotsList_dataGridView2.Visible = pilotsList_dataGridView2.Rows.Count > 0;
                    }

                toolStripLabel3.Text = admin.Races[e.RowIndex, e.ColumnIndex].Hour + ":" + admin.Races[e.RowIndex, e.ColumnIndex].Minute;

                SetControlButton();
            }
        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
            // LastSelect.ColHit = -1;
            // LastSelect.RowHit = -1;
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            LastSelect.RowHit = e.RowIndex;
            LastSelect.ColHit = e.ColumnIndex;
            //dataGridView1.Refresh();
        }

        private void перейтиКЗаездуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            admin.ReloadTable();
            ShowGridTable();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LastSelect.Col > 0 || LastSelect.Row > 0)
            {
                if (CreateRace(LastSelect.Row, LastSelect.Col))
                    admin.ReloadTable();
                // GlobalTimer.Enabled = false;
                AddPilot form = new AddPilot(admin.Races[LastSelect.Row, LastSelect.Col], admin);
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
                //GlobalTimer.Enabled = true;
                admin.ShowRacePilots(admin.Races[LastSelect.Row, LastSelect.Col].RaceID, pilotsList_dataGridView2);
            }
        }

        private void перейтиКЗаездуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowRaceForm();

        }

        //Показывает форму рейса
        private void ShowRaceForm()
        {
            DateTime startTime = DateTime.Now;

            bool is_not_open = true;

            bool RacesEnd =
                admin.Races[LastSelect.Row, LastSelect.Col].Status == 2 ||
            admin.Races[LastSelect.Row, LastSelect.Col].Date < DateTime.Now.Date ||
                (admin.Races[LastSelect.Row, LastSelect.Col].Date == DateTime.Now.Date &&
                  (Convert.ToInt32(admin.Races[LastSelect.Row, LastSelect.Col].Hour) < DateTime.Now.Hour ||
                  (Convert.ToInt32(admin.Races[LastSelect.Row, LastSelect.Col].Hour) == DateTime.Now.Hour &&
                   Convert.ToInt32(admin.Races[LastSelect.Row, LastSelect.Col].Minute) < (DateTime.Now.Minute - 5))));

            if (admin.Races[LastSelect.Row, LastSelect.Col].Status > 0)
            {
                for (int i = 0; i < this.OwnedForms.Length; i++)
                {
                    if (this.OwnedForms[i].GetType().ToString().Equals("Prokard_Timing.Race"))
                    {
                        RaceClass rc = (this.OwnedForms[i] as Race).CurrentRace;
                        {

                            if (rc.Date == admin.Races[LastSelect.RowHit, LastSelect.ColHit].Date &&
                                 rc.ID == admin.Races[LastSelect.RowHit, LastSelect.ColHit].ID)
                            {
                                this.OwnedForms[i].Activate();

                                this.OwnedForms[i].WindowState = this.OwnedForms[i].WindowState == FormWindowState.Minimized ? FormWindowState.Normal : this.OwnedForms[i].WindowState;
                                this.OwnedForms[i].Left = (Screen.PrimaryScreen.Bounds.Width - this.OwnedForms[i].Width) / 2;
                                this.OwnedForms[i].Top = (Screen.PrimaryScreen.Bounds.Height - this.OwnedForms[i].Height) / 2;
                                is_not_open = false;
                                break;
                            }
                        }
                    }
                }

                }

            if (is_not_open)
            {
                bool can_open = false;
                if (!RacesEnd && admin.Races[LastSelect.Row, LastSelect.Col].Status == 0 && CreateRace(LastSelect.Row, LastSelect.Col))
                {
                    int R = LastSelect.Row, C = LastSelect.Col;

                    admin.ReloadTable();
                    ShowGridTable();

                    LastSelect.Row = R;
                    LastSelect.Col = C;

                    if (racesList_dataGridView1[LastSelect.Col, LastSelect.Row].Visible)
                    {
                        racesList_dataGridView1[LastSelect.Col, LastSelect.Row].Selected = true;
                        racesList_dataGridView1.CurrentCell = racesList_dataGridView1.SelectedCells[0];
                    }
                    can_open = true;
                }
                else
                {
                    if (admin.Races[LastSelect.Row, LastSelect.Col].Status > 0)
                    {
                        can_open = true;
                    }
                }

                if (can_open)
                {
                    Race form = new Race(this);
                    // form.Owner = this;

                    form.SetRace(admin.Races[LastSelect.Row, LastSelect.Col]);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Не было заездов в это время");
                
                   // MessageBox.Show("Убедитесь, что существует по крайней мере один пилот в списке пилотов");
                }
            }
        }

        private void ShowRaceForm(RaceClass Race)
        {
            DateTime startTime = DateTime.Now;

            bool is_not_open = true;

            for (int i = 0; i < this.OwnedForms.Length; i++)
            {
                if (this.OwnedForms[i].GetType().ToString().Equals("Prokard_Timing.Race"))
                {
                    RaceClass rc = (this.OwnedForms[i] as Race).CurrentRace;
                    {

                        if (rc.Date == Race.Date &&
                             rc.ID == Race.ID)
                        {
                            this.OwnedForms[i].Activate();

                            this.OwnedForms[i].WindowState = this.OwnedForms[i].WindowState == FormWindowState.Minimized ? FormWindowState.Normal : FormWindowState.Maximized;
                            this.OwnedForms[i].Left = (Screen.PrimaryScreen.Bounds.Width - this.OwnedForms[i].Width) / 2;
                            this.OwnedForms[i].Top = (Screen.PrimaryScreen.Bounds.Height - this.OwnedForms[i].Height) / 2;
                            is_not_open = false;
                            break;
                        }
                    }
                }

            }

            if (is_not_open)
            {
                Race form = new Race(this);
                //form.Owner = this;
                form.SetRace(admin.RaceTH.Race);
                form.Show();
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ShowRaceForm", Logger.LogType.info, executionTime);

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ShowRaceForm();
        }

        private void создатьКарточкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPilot form = new NewPilot(admin, -1, false);
            form.Owner = this;

            form.ShowDialog();
            form.Dispose();
        }

        private void просмотрВсехToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllPilots form = new AllPilots(this);
            //form.Owner = this;
            form.ShowDialog(this);
            //form.Dispose();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings form = new Settings(this);
            form.Owner = this;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                admin.Settings = admin.model.LoadSettings();
                labelSmooth1.Text = "Трек - " + admin.model.GetTrackName(admin.Settings["default_track"].ToString());
                ShowEvents();
            }
            form.Dispose();
        }

        private void картыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (pilotsList_dataGridView2[3, e.RowIndex].Value == null) pilotsList_dataGridView2[3, e.RowIndex].Value = "0";

                if (pilotsList_dataGridView2[3, e.RowIndex].Value.Equals("0"))
                {
                    pilotsList_dataGridView2.Rows[e.RowIndex].Cells[3].Value = "";
                    pilotsList_dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 196, 178);
                }

                if (Convert.ToBoolean(pilotsList_dataGridView2[5, e.RowIndex].Value))
                {
                    pilotsList_dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 253, 145);

                    pilotsList_dataGridView2.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(pilotsList_dataGridView2.RowsDefaultCellStyle.Font, FontStyle.Bold);
                }

            }
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            monthCalendar1.SelectionStart = monthCalendar1.SelectionStart.AddMonths(1);
            SetDate();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            monthCalendar1.SelectionStart = monthCalendar1.TodayDate;
            SetDate();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                racesList_dataGridView1[LastSelect.Col, LastSelect.Row].Selected = true;

                if (LastSelect.Col > 0)
                {
                    ShowRaceForm();
                }


            }
            if (e.KeyValue == 45)
            {
                SetControlButton();
                toolStripMenuItem1.PerformClick();
            }

        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            using (SolidBrush br = new SolidBrush(Color.Transparent))
                e.Graphics.FillRectangle(br, e.ClipRectangle);

            using (SolidBrush br = new SolidBrush(Color.DarkGray))
            {
                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();

                drawFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(GlobalTime, label1.Font, br, e.Graphics.ClipBounds, drawFormat);
                drawFormat.Dispose();
            }


        }

        bool InAlert = false;
        bool OnBgRed = false;
        private void GlobalTimer_Tick(object sender, EventArgs e)
        {
            if (admin != null && admin.RaceTH != null)
            {
                admin.RaceTH.Nextval();
            }
            if (admin != null && (admin.IS_ADMIN || admin.IS_USER))
                {

                    GlobalTime = DateTime.Now.ToString("HH:mm:ss");
                    label1.Refresh();

                    if (admin.RaceTH != null && admin.RaceTH.Race != null && admin.RaceTH.Race.Status == 2 && admin.RaceTH.Status == 0)
                    {
                        label2.Text = "Финиш";
                        InAlert = !InAlert;
                    }
                    else InAlert = false;

                    // Обработка отображения времени рейса
                    if (admin.RaceTH != null && (admin.RaceTH.Status == 1 || admin.RaceTH.Status == 2))
                    {
                        InAlert = false;
                        long ticks = admin.RaceTH.MaxTicksForRace - (DateTime.Now.Ticks - admin.RaceTH.StartTick - admin.RaceTH.PauseTick - admin.RaceTH.TempPauseTick);

                        if (admin.RaceTH.StartTick > 0 && ticks <= 0)
                        {
                            admin.RaceTH.ThreadStop();
                            admin.RaceTH.RaceID = -1;
                            admin.ReloadTable();
                            ShowGridTable();
                            InAlert = false;
                        }

                        if (ticks > 0 && (admin.RaceTH.Status == 1 || (admin.RaceTH.Status == 2 && !Convert.ToBoolean(admin.Settings["stop_on_pause"]))))
                        {
                            int percent = Math.Abs((int)((ticks * 100) / admin.RaceTH.MaxTicksForRace - 100));

                            if (percent > 0)
                            {
                                progressBar1.Value = percent > 100 ? progressBar1.Value : percent;
                            }

                            label2.Text = new DateTime(ticks).ToString("mm:ss");
                            label4.Text = "Заезд №" + (admin.RaceTH.Race.RaceNum > admin.DopRace ? (admin.RaceTH.Race.RaceNum - admin.DopRace).ToString() + "a" : admin.RaceTH.Race.RaceNum.ToString()) + " Отправление в " + admin.RaceTH.Race.Hour + ":" + admin.RaceTH.Race.Minute;
                            InAlert = false;
                        }
                        else
                        {
                            label2.Text = "Пауза";
                        }

                        InAlert = false;
                    }

                }
            if (InAlert)
            {
                if (!OnBgRed)
                {
                    borderPanel5.BackgroundImage = global::Prokard_Timing.Properties.Resources.redbg;
                    OnBgRed = true;
                    if (admin.Settings["beep_system"] != null && Boolean.Parse(admin.Settings["beep_system"].ToString())) Console.Beep(1000, 200);
                    if (admin.Settings["beep_windows"] != null && Boolean.Parse(admin.Settings["beep_windows"].ToString())) System.Media.SystemSounds.Asterisk.Play();
                }
            }
            else
            {
                if (OnBgRed)
                {
                    borderPanel5.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
                    OnBgRed = false;
                }
            }
        }

        private void управлениеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            KartsControl form = new KartsControl(this);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();

            admin.MaxKarts = admin.model.GetMaxKarts();
        }

        private void трассыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TrackControl form = new TrackControl(this);
            form.ShowDialog();
            form.Dispose();
        }

        private void ценыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PriceControl form = new PriceControl(this);
            form.ShowDialog();
            form.Dispose();
        }

        private void событияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventControl form = new EventControl(this);
            form.ShowDialog();
            ShowEvents();
          //  form.Dispose();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowEventMessage form = new ShowEventMessage(eventsAnonse_dataGridView3.SelectedRows[0].Cells[0].Value.ToString(), admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void borderPanel5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bool OverPrint = true;
                if (admin.RaceTH.Race != null && admin.RaceTH.Race.Status == 2)
                {


                    PrintResult1 form = new PrintResult1(admin.RaceTH.Race, admin);
                    form.Owner = this;
                    form.ShowDialog();
                    if (admin.RaceTH.Status == 0)
                    admin.RaceTH.Race = null;
                    progressBar1.Value = 0;
                    SetNextRace();
                    OverPrint = false;

                }
                else
                {
                    progressBar1.Value = 0;
                }

                if (OverPrint)
                {
                    if (admin.RaceTH.Status == 1 || admin.RaceTH.Status == 2)
                    {
                        ShowRaceForm(admin.RaceTH.Race);
                    }
                    else
                    {
                        if (NextRaceI >= 0 && NextRaceJ > 0)
                        {
                            LastSelect.Row = NextRaceI;
                            LastSelect.Col = NextRaceJ;

                            ShowRaceForm();
                        }
                    }
                }
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {

            for (int i = 0; i < this.OwnedForms.Length; i++)
            {
                if (this.OwnedForms[i].GetType().ToString().Equals("Prokard_Timing.Race"))
                {

                    this.OwnedForms[i].WindowState = FormWindowState.Minimized;
                }

            }
            toolStripStatusLabel1.Text = "Текущий пользователь - " + admin.User_Name;
        }


        private void журналToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CassaJurnal form = new CassaJurnal(admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void закрытьКассуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CassaClose form = new CassaClose(admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void панельИнструментовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = панельИнструментовToolStripMenuItem.Checked;
        }

        private void toolStripButton11_Click_1(object sender, EventArgs e)
        {
            ShowRaceForm();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.PerformClick();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            перейтиКЗаездуToolStripMenuItem.PerformClick();
        }

        //Удаление пилота из заезда
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            bool ret = false;

            if (pilotsList_dataGridView2.Rows.Count > 0 && pilotsList_dataGridView2.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int SelectedRow = pilotsList_dataGridView2.SelectedRows[0].Index;
                if (Convert.ToBoolean(pilotsList_dataGridView2.Rows[SelectedRow].Cells[5].Value)) ret = true;
                else
                {
                    CorrectBallance form = new CorrectBallance(admin, Int32.Parse(pilotsList_dataGridView2.Rows[SelectedRow].Cells[4].Value.ToString()), 
                        true, admin.Races[LastSelect.Row, LastSelect.Col]);
                    form.Owner = this;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK) ret = true;
                }

                if (ret)
                {
                    if (pilotsList_dataGridView2.Rows[SelectedRow].Cells[3].Value != null)
                        admin.Races[LastSelect.Row, LastSelect.Col].Karts.Remove(pilotsList_dataGridView2.Rows[SelectedRow].Cells[3].Value.ToString());

                    admin.model.DelPilotFromRace(pilotsList_dataGridView2.Rows[SelectedRow].Cells[4].Value.ToString());
                    admin.ShowRacePilots(admin.Races[LastSelect.Row, LastSelect.Col].RaceID, pilotsList_dataGridView2);
                    SetControlButton();
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            bool ret = false;
            if (pilotsList_dataGridView2.Rows.Count > 0 && pilotsList_dataGridView2.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int SelectedRow = pilotsList_dataGridView2.SelectedRows[0].Index;

                CashOperations form = new CashOperations(Int32.Parse(pilotsList_dataGridView2.Rows[SelectedRow].Cells[1].Value.ToString()), admin.Races[LastSelect.Row, LastSelect.Col], admin);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK) ret = true;
                form.Dispose();

                if (ret)
                {
                    admin.model.ChangePilotReservStatus(pilotsList_dataGridView2.Rows[SelectedRow].Cells[4].Value.ToString());
                    admin.ShowRacePilots(admin.Races[LastSelect.Row, LastSelect.Col].RaceID, pilotsList_dataGridView2);
                    pilotsList_dataGridView2.Rows[SelectedRow].Selected = true;
                    SetControlButton();
                }
            }
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            SetControlButton();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pilotsList_dataGridView2.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                PilotInfo form = new PilotInfo(Convert.ToInt32(
                    pilotsList_dataGridView2.SelectedRows[0].Cells[1].Value.ToString()), admin);
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
            }
        }

        private void группыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupControl form = new GroupControl(admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void участникиМесячнойГонкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PilotMonthRace form = new PilotMonthRace(admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void заСегодняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayStatistic form = new DayStatistic(admin, 0);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void журналРейсовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayStatistic form = new DayStatistic(admin, 1);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void лучшееВремяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayStatistic form = new DayStatistic(admin, 2);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void поКартамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayStatistic form = new DayStatistic(admin, 4);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void поПилотамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayStatistic form = new DayStatistic(admin, 3);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void балансПользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCash form = new AddCash(admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void показатьПрошедшиеЧасыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGridTable();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            создатьКарточкуToolStripMenuItem.PerformClick();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            просмотрВсехToolStripMenuItem.PerformClick();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            участникиМесячнойГонкиToolStripMenuItem.PerformClick();
        }

        private void войтиВРежимToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(admin.Settings["card_user"]))
            {
                LoginUser form = new LoginUser(admin);

                form.ShowDialog();
                form.Dispose();
                SetAdmin();
            }
            else
            {
                LoginForm form = new LoginForm(admin);

                form.ShowDialog();
                form.Dispose();
                SetAdmin();
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            admin.IS_USER = admin.IS_SUPER_ADMIN = admin.IS_ADMIN = false;
            admin.model.LogOut(admin.USER_ID.ToString());
            LoginForm form = new LoginForm(admin);
            form.ShowDialog();
            form.Dispose();
            SetAdmin();
        }


        private void SetAdmin()
        {
            testData_ToolStripMenuItem.Visible = admin.IS_ADMIN || admin.IS_SUPER_ADMIN;
            войтиВРежимToolStripMenuItem.Enabled = !(admin.IS_ADMIN && admin.IS_USER);
            выйтиToolStripMenuItem.Enabled = admin.IS_ADMIN || admin.IS_USER;
            RaceModesToolStripMenuItem.Enabled = admin.IS_ADMIN || admin.IS_SUPER_ADMIN;
            настройкиToolStripMenuItem.Enabled = кассаToolStripMenuItem.Enabled = admin.IS_ADMIN;
            пользователиToolStripMenuItem.Enabled = admin.IS_SUPER_ADMIN;
            поКартамToolStripMenuItem.Enabled = admin.IS_SUPER_ADMIN;
            сертификатыToolStripMenuItem.Enabled = admin.IS_ADMIN;
            скидочныеКартыToolStripMenuItem.Enabled = admin.IS_ADMIN;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hashtable data = new Hashtable();
            data["name"] = "test";
            data["surname"] = "test";
            data["nickname"] = "test";
            data["birthday"] = "2010-02-22";
            data["gender"] = 1;
            data["email"] = "test";
            data["tel"] = "test";
            data["group"] = 0;


            for (int i = 1; i <= 10000; i++)
            {

                admin.model.AddNewPilot(data);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query;
            Random w_rnd = new Random();
            for (int i = 0; i <= 600; i++) {

                query = "insert into race_times (member_id,lap,seconds,created) values('"+w_rnd.Next(910,931)+"','" + i.ToString() + "','" + ((w_rnd.Next(10,50))*w_rnd.NextDouble()).ToString() +"',now())";
                admin.model.ExecuteQuery(query);
            
            }
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramUsers form = new ProgramUsers(admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (admin.RaceTH != null && admin.RaceTH.Status > 0)
            {
                if (MessageBox.Show("В данный момент запущен рейс.\r\nВы уверены что хотите его прервать?", "Выход с программы", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {

                    admin.model.LogOut(admin.USER_ID.ToString());
                    e.Cancel = false;

                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
                admin.model.LogOut(admin.USER_ID.ToString());
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (racesList_dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                LastSelect.Col = racesList_dataGridView1.SelectedCells[0].ColumnIndex;
                LastSelect.Row = racesList_dataGridView1.SelectedCells[0].RowIndex;
                SetControlButton();
            }
        }

        private void сертификатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sertificat form = new Sertificat(admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void поискСертификатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveCertificate form = new ActiveCertificate(admin);
            form.ShowDialog();
            form.Dispose();
        }

        private void выдатьСертификатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GiftCertificate form = new GiftCertificate(admin, true);
            form.ShowDialog();
            form.Dispose();
        }

        private void анонсерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            Anonser fr2 = new Anonser(admin, this);
            fr2.Left = sc[sc.Length - 1].Bounds.Width;
            fr2.Top = sc[sc.Length - 1].Bounds.Height;
            fr2.StartPosition = FormStartPosition.Manual;
            fr2.Location = sc[sc.Length - 1].Bounds.Location;
            Point p = new Point(sc[sc.Length - 1].Bounds.Location.X, sc[sc.Length - 1].Bounds.Location.Y);
            fr2.Location = p;
            fr2.WindowState = FormWindowState.Maximized;
            fr2.Show();
        }

       

       

        private void скидочныеКартыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Функция пока не реализована");
           // return;
            Discount_Card dc = new Discount_Card(admin);
            dc.ShowDialog();
        }

        private void RaceModesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RaceModes modesForm = new RaceModes(admin);
            modesForm.ShowDialog();
        }

        private void testData_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestDataFiller dataFiller = new TestDataFiller(admin);
            dataFiller.ShowDialog();
        }

        private void CheckSensorsMenuItem_Click(object sender, EventArgs e)
        {
            if (admin.RaceTH != null && admin.RaceTH.Status > 0)
            {
                MessageBox.Show("В данный момент идёт заезд. Во время заезда режим проверки датчиков использовать нельзя");
                return;
            }

            CheckSensors checkSensorsForm = new CheckSensors(admin);
            checkSensorsForm.ShowDiagForm();
        }

        private void petroleumSpend_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PetroleumSpend petroleumForm = new PetroleumSpend(admin);
            petroleumForm.ShowDialog();
        }

        private void competitionMode_menuItem_Click(object sender, EventArgs e)
        {
            admin.isSportMode = competitionMode_menuItem.Checked;
            ShowGridTable();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            admin.isSportMode = competitionMode_menuItem.Checked;
        }
    }


    public class LastSelectClass
    {
        public int Row = 0;
        public int Col = 0;

        public int RowHit = -1;
        public int ColHit = -1;
    }

    // Расширяет класс DataGridView для включения DoubleBuffer
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }


    public class LabelSmooth : Label
    {
        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;

        public TextRenderingHint TextRenderingHint
        {
            get { return _textRenderingHint; }
            set { _textRenderingHint = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = _textRenderingHint;
            base.OnPaint(e);
        }
    }


    public class PageLister
    {
        public int CurrentPageNumber;
        public int PagesCount;
        public int PageSize;

        public int Rows;
        public int RowsMax;
        public bool OnUpdate = false;
        public bool OnChange = false;
        public string Filter;
        public string SecondFilter;

        public void setPageListerButtonsEnability()
        {

            /* http://justgeeks.blogspot.com/2007/02/sql-2005-rownumber.html
             * http://weblogs.asp.net/mschwarz/archive/2006/03/15/SQL-Server-2005-and-Ajax.NET-Professional-_2D00_-RowNumber-Example.aspx
             * DECLARE @PageNum AS INT;
DECLARE @PageSize AS INT;
SET @PageNum = 2;
SET @PageSize = 2;

WITH OrdersRN AS
(
    SELECT ROW_NUMBER() OVER(ORDER BY id, name) AS RowNum,
          *         
      FROM users
)

SELECT * 
  FROM OrdersRN
 WHERE RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 
                  AND @PageNum * @PageSize
 ORDER BY name   ,id;


             */

            int Start = (CurrentPageNumber - 1) * PageSize;
            //Filter = SecondFilter + "  limit " + Start.ToString() + "," + (PageSize).ToString();

            if (CurrentPageNumber == 1)
            {
                sl.Enabled = ssl.Enabled = false;
            }
            else
            {
                sl.Enabled = ssl.Enabled = true;
            }

            if (CurrentPageNumber == PagesCount)
            {
                sr.Enabled = ssr.Enabled = false;
            }
            else
            {
                sr.Enabled = ssr.Enabled = true;
            }


            if (PagesCount == 0)
            {
                sr.Enabled = ssr.Enabled = sl.Enabled = ssl.Enabled = false;
            }

            if (pageNumbers_comboBox.SelectedIndex != pageNumbers_comboBox.Items.IndexOf(CurrentPageNumber))
            {
                OnUpdate = true;
                pageNumbers_comboBox.SelectedIndex = pageNumbers_comboBox.Items.IndexOf(CurrentPageNumber);
                OnUpdate = false;
            }

           // pageNumbers_comboBox.SelectedIndex = pageNumbers_comboBox.Items.IndexOf(CurrentPageNumber);
        }

        ToolStripButton sl, sr, ssl, ssr;
        ToolStripComboBox pageNumbers_comboBox;

        public PageLister(ToolStripComboBox cb, 
            ToolStripButton sl, ToolStripButton ssl, ToolStripButton sr, 
            ToolStripButton ssr)
        {
            this.pageNumbers_comboBox = cb;
            this.sl = sl;
            this.ssl = ssl;
            this.sr = sr;
            this.ssr = ssr;
            FillPageNumbers();
        }

        public void FillPageNumbers()
        {
            if (PagesCount < 2)
            {
                pageNumbers_comboBox.Items.Clear();
                pageNumbers_comboBox.Enabled = false;
                /*
                this.sl.Enabled = false;
                this.ssl.Enabled = false;
                this.sr.Enabled = false;
                this.ssr.Enabled = false;
                 */
                setPageListerButtonsEnability();
                return;
            }
            else
            {
                pageNumbers_comboBox.Enabled = true;
                /*
                this.sl.Enabled = true;
                this.ssl.Enabled = true;
                this.sr.Enabled = true;
                this.ssr.Enabled = true;
                 */
            }

            if (pageNumbers_comboBox.Items.Count == PagesCount)
            {
                if (pageNumbers_comboBox.SelectedIndex != pageNumbers_comboBox.Items.IndexOf(CurrentPageNumber))
                {
                    pageNumbers_comboBox.SelectedIndex = pageNumbers_comboBox.Items.IndexOf(CurrentPageNumber);       
                }

                setPageListerButtonsEnability();
                return;
            }

            pageNumbers_comboBox.Items.Clear();
            pageNumbers_comboBox.BeginUpdate();

            for (int i = 1; i <= PagesCount; i++)
            {
                pageNumbers_comboBox.Items.Add(i);
            }

            pageNumbers_comboBox.EndUpdate();
            pageNumbers_comboBox.SelectedIndex = pageNumbers_comboBox.Items.IndexOf(CurrentPageNumber);
            setPageListerButtonsEnability();
        }

        public void Next()
        {
            if (CurrentPageNumber < PagesCount) CurrentPageNumber++;
            setPageListerButtonsEnability();
        }

        public void Prev()
        {
            if (CurrentPageNumber > 1) CurrentPageNumber--;
            setPageListerButtonsEnability();

        }

        public void First()
        {
            CurrentPageNumber = 1;
            setPageListerButtonsEnability();
        }

        public void Last()
        {
            CurrentPageNumber = PagesCount;
            setPageListerButtonsEnability();
        }

        public void ToPage(int sPage)
        {
            CurrentPageNumber = sPage;
            setPageListerButtonsEnability();
        }



    }
}


