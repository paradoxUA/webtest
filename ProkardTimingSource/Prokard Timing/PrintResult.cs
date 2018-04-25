using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Collections.Generic;
using DGVPrinterHelper;
using System.Drawing.Text;
using System.Collections;
using System.Linq;
using DocumentPrinter.Services;
using DocumentPrinter;
using Rentix;
using DocumentPrinter.Models;

namespace Rentix
{
    public partial class PrintResult1 : Form
    {
        RaceClass CurrentRace = null;
        AdminControl admin;
        public PrintResult1(RaceClass Race, AdminControl ad)
        {
            InitializeComponent();
            CurrentRace = Race;
            admin = ad;
            labelSmooth1.Text = "Заезд №" + (Race.RaceNum > admin.DopRace ? (Race.RaceNum - admin.DopRace).ToString() + "a" : Race.RaceNum.ToString());
            labelSmooth2.Text = Race.Date.ToString("dd MMMM yyyy") + " " + Race.Hour + ":" + Race.Minute;
            labelSmooth4.Text = "Трасса - " + Race.TrackName;

            numericUpDown1.Value = admin.model.GetRacePilots(CurrentRace.RaceID).Count();
        }

        private void PrintResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
            if (e.KeyCode == Keys.Enter) button2.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) button3.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        
        public static bool Printing(RaceClass race, int trakId)
        {
            PrinterService ps = new PrinterService();
            PageService PageData = new PageService();


            //int trackId = 6668;

            ProkardModel pm = new ProkardModel();
            pm.Connect();
            //var race = pm.GetRace(,raceId);                                  // Получаем информацию о заезде
            var pilots = pm.GetFinallyRaceInfo(race.RaceID);                     // Получаем участников заезда

            var tempTime = pilots.FirstOrDefault().BestTime;
            var bestTime = pilots.FirstOrDefault(x => x.BestTime < tempTime).BestTime;

            AbsoluteRecordOfRace absoluteRaceRecord = null;
            try
            {
                absoluteRaceRecord = pm.GetAbsoluteRecordOfRace(trakId);     // Абсолютный рекорд трассы
            }
            catch
            {

            }
            PageData.Info = new RaceInfo();
            PageData.Info.Trek = race.TrackName;
            PageData.Info.DateNow = race.Date.ToString("dd MMMM yyyy");
            PageData.Info.RaceNumber = race.RaceNum.ToString();
            PageData.Info.Time = race.Hour + ":" + race.Minute;
            PageData.Info.RaceOfRecordUser = absoluteRaceRecord?.Pilot;
            PageData.Info.RaceOfRecordDate = absoluteRaceRecord?.Date.ToString("dd MMMM yyyy");
            PageData.Info.RaceOfRecordTime = absoluteRaceRecord?.RecordTime;
            PageData.Info.RecordOfDay = pm.GetBestPilots(trakId, true, DateTime.Today, DateTime.Now, 5);
            PageData.Info.RecordOfMounth = pm.GetBestPilots(trakId, true, DateTime.Today, DateTime.Now, 5);
            PageData.Info.RaceResults = pilots?.Select(x => new RaceResult
            {
                UserName = x.PilotName,
                Kart = x.CarNum,
                BestCheckIn = (x.BestTime - bestTime).ToString(),
                AmountTime = x.Times.Sum().ToString(),
                RaceTimes = RaceTimeConvert(x.Times)
            }).ToList();

            return true;
        }

        private static List<RaceTime> RaceTimeConvert(List<double> times)
        {
            List<RaceTime> temp = new List<RaceTime>();
            if (times == null || times.Count < 1) return temp;

            for (int i = 0; i < times.Count; i++)
            {
                temp.Add(new RaceTime() { Range = (i + 1).ToString(), Time = times[i].ToString() });
            }
            return temp;
        }
        private void button3_Click(object sender, EventArgs e)
        {

            if (Printing(CurrentRace, CurrentRace.TrackID)) return;//CurrentRace.TrackID)) return;


            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowNetwork = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.PrinterSettings.PrinterName = admin.Settings["printer_result"].ToString();

            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Landscape = true;
            printDocument1.DefaultPageSettings.Color = false;
            printDocument1.PrinterSettings.Copies = Convert.ToInt16(numericUpDown1.Value);
            printDocument1.PrinterSettings.Collate = true;
            printDocument1.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);

            if (printDocument1.PrinterSettings.IsValid)
            {
                DateTime startTime = DateTime.Now;

                PrintRaceResult(CurrentRace.RaceID.ToString());

                TimeSpan executionTime = DateTime.Now - startTime;
                Logger.AddRecord("PrintRaceResult", Logger.LogType.info, executionTime);

                //MessageBox.Show(printDocument1.DefaultPageSettings.PaperSize.Width.ToString() + " " + printDocument1.DefaultPageSettings.PaperSize.Height.ToString());

                try
                {
                    printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось распечатать документ. Ошибка: " + ex.Message);
                }
                //  printPreviewDialog1.ShowDialog();

                MyPrintDialog.Dispose();
                this.Close();
            }
            else
            {
                MessageBox.Show("Не указан принтер для печати отчётов. Перейдите в раздел Управление - Настройки - Оборудование и выберите принтер в списке 'Печать результатов'");
            }
        }

        private void PrintResult_Activated(object sender, EventArgs e)
        {
            numericUpDown1.Select();
            numericUpDown1.Select(0, 10);
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, 10);
        }

        // DataGridViewPrinter MD1, MD2, MD3;

        // Печатает результаты гонки 
        private void PrintRaceResult(string RaceID)
        {
            //CurrentRace.Date.ToString("yyyy-MM-dd");
            admin.ShowFinnalyRaceResult(dv1, CurrentRace.RaceID);
            admin.ShowBestDMResult(dv2, DateTime.Now.ToString("yyyy-MM-dd"), CurrentRace.TrackID, true, "1");
            admin.ShowBestDMResult(dv3, DateTime.Now.ToString("yyyy-MM-dd"), CurrentRace.TrackID, false, "2");
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            DrawDatagrid1(dv1, 40, 300, e.Graphics);
            DrawDatagrid2(dv2, 950, 160, e.Graphics);
            DrawDatagrid3(dv3, 655, 160, e.Graphics);
            DrawOtherInfo(e.Graphics);
        }

        private void DrawOtherInfo(Graphics g)
        {
            using (Pen p = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(p, new Rectangle(654, 140, 289, 145));
                g.DrawRectangle(p, new Rectangle(949, 140, 179, ((dv1.Rows.Count + 9) * 20) + 5));
                g.DrawRectangle(p, new Rectangle(39, 300, 905, ((dv1.Rows.Count + 1) * 20) + 5));
                //g.DrawRectangle(p, new Rectangle(39, 745, 905, 40));
            }

            using (Font f = new Font("Calibri", 11, FontStyle.Bold))
            {
                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;

                // g.DrawString("Тип:    Квалификация", f, Brushes.Black, 50, 755);

                g.DrawString("Лучший результат месяца", f, Brushes.Black, new Rectangle(654, 140, 289, 20), drawFormat);
                g.DrawString("Лучший результат дня", f, Brushes.Black, new Rectangle(949, 140, 179, 20), drawFormat);



                drawFormat.Alignment = StringAlignment.Far;
                g.DrawString("Трек:    " + CurrentRace.TrackName, f, Brushes.Black, 650, 195, drawFormat);

                g.DrawString("Дата:  " + CurrentRace.Date.ToString("dd MMMM yyyy"), f, Brushes.Black, 650, 235, drawFormat);
                g.DrawString("Время:  " + CurrentRace.Hour + ":" + CurrentRace.Minute, f, Brushes.Black, 650, 175, drawFormat);
                g.DrawString("Номер заезда:  " + (CurrentRace.RaceNum > admin.DopRace ? (CurrentRace.RaceNum - admin.DopRace).ToString() + "a" : CurrentRace.RaceNum.ToString()), f, Brushes.Black, 650, 215, drawFormat);

                g.DrawString("Абсолютный рекорд трассы", f, Brushes.Black, 650, 255, drawFormat);
                g.DrawString(admin.model.GetRecord(CurrentRace.TrackID), f, Brushes.Black, 650, 275, drawFormat);
            }
        }

        private void DrawDatagrid1(DataGridView dv, int ML, int MT, Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            int margintop = MT; // Смещение ячейки по Y
            int marginleft = ML; // Смещение ячейки по X
            int cellheight = 20; // Высота ячейки
            int ccount = 30; // Общее количество ячеек
            int Lap = 1;
            string val = String.Empty;

            Hashtable BestLaps = FindBestLap(dv);

            // Отрисовка шапки
            for (int j = 0; j < ccount; j++)
            {
                Font font = new Font("Calibri", 8, FontStyle.Bold);

                if (j != 1)
                {
                    using (Pen p = new Pen(Color.Black, 2)) g.DrawRectangle(p, marginleft - 1, margintop, DV1width(j), 25);

                    // Отрисовка данных круга
                    if (j < dv.Columns.Count && dv.Columns[j].Visible)
                    {
                        if (j < dv.Columns.Count && j != (dv.Columns.Count - 1))
                        {
                            switch (j)
                            {
                                case 0: val = "Поз"; break;
                                case 2: val = "Имя"; break;
                                case 3: val = "Карт"; break;
                                case 4: val = "Лучший круг"; break;
                                default: val = String.Empty; break;
                            }

                            if (j > 4 && val.Length > 5) val.Remove(val.Length - 1);

                            g.DrawString(val, font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), 25), drawFormat);
                        }
                    }

                    // Отрисовка других данных
                    if (j == (ccount - 1))
                    {
                        val = "Сред";
                        g.DrawString(val, font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), 25), drawFormat);
                    }
                    else
                        if (j > 4)
                    {
                        g.DrawString((Lap++).ToString(), font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), 25), drawFormat);
                    }

                    marginleft += DV1width(j);
                }
                font.Dispose();
            }

            margintop += 5;

            for (int i = 0; i < dv.Rows.Count; i++)
            {
                margintop += cellheight;
                marginleft = ML;


                for (int j = 0; j < ccount; j++)
                {
                    if (j != 1)
                    {
                        if (j == 2 || j == 4) drawFormat.Alignment = StringAlignment.Near; else drawFormat.Alignment = StringAlignment.Center;

                        if (Convert.ToInt32(BestLaps[i]) == j && j > 0)
                        {
                            g.FillRectangle(Brushes.LightGray, marginleft - 1, margintop, DV1width(j), cellheight);
                        }

                        g.DrawRectangle(Pens.Black, marginleft - 1, margintop, DV1width(j), cellheight);

                        // Отрисовка данных круга
                        if (j < dv.Columns.Count && dv.Columns[j].Visible)
                        {
                            if (j < dv.Columns.Count && j != (dv.Columns.Count - 1) && dv.Rows[i].Cells[j].Value != null)
                            {
                                Font font;
                                switch (j)
                                {
                                    case 2:
                                    case 0: font = new Font("Calibri", 9); break;
                                    default: font = new Font("Calibri", 7, FontStyle.Bold); break;
                                }

                                val = dv.Rows[i].Cells[j].Value.ToString();

                                if (j > 4 && val.Length > 5) val = val.Remove(val.Length - 1, 1);

                                g.DrawString(val, font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), cellheight), drawFormat);


                                font.Dispose();
                            }
                        }

                        // Отрисовка среднего значения
                        if ((i - 1) < dv.Rows.Count && j == ccount - 1 && dv.Rows[i].Cells[dv.Columns.Count - 1].Value != null)
                        {
                            Font font;

                            switch (j)
                            {
                                case 2:
                                case 0: font = new Font("Calibri", 9); break;
                                default: font = new Font("Calibri", 7, FontStyle.Bold); break;
                            }

                            val = dv.Rows[i].Cells[dv.Columns.Count - 1].Value.ToString();

                            //if (j > 4 && val.Length > 5) val.Remove(val.Length);

                            g.DrawString(val, font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), cellheight), drawFormat);

                            font.Dispose();
                        }

                        marginleft += DV1width(j);
                    }
                }
            }
        }

        private void DrawDatagrid2(DataGridView dv, int ML, int MT, Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            int margintop = MT; // Смещение ячейки по Y
            int marginleft = ML; // Смещение ячейки по X
            int cellheight = 20; // Высота ячейки
            int ccount = 4; // Общее количество ячеек
            string val = String.Empty;

            // Отрисовка шапки
            for (int j = 0; j < ccount; j++)
            {
                Font font = new Font("Calibri", 8, FontStyle.Bold);

                if (j != 1)
                {
                    using (Pen p = new Pen(Color.Black, 2)) g.DrawRectangle(p, marginleft - 1, margintop, DV1width(j), 25);

                    if (j < dv.Columns.Count && dv.Columns[j].Visible)
                    {
                        switch (j)
                        {
                            case 2: val = "Имя"; break;
                            case 3: val = "Время"; break;
                            default: val = String.Empty; break;
                        }

                        g.DrawString(val, font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), 25), drawFormat);

                    }



                    marginleft += DV1width(j);
                }
                font.Dispose();
            }

            margintop += 5;

            for (int i = 0; i < dv1.Rows.Count + 7; i++)
            {
                margintop += cellheight;
                marginleft = ML;


                for (int j = 0; j < ccount; j++)
                {
                    if (j != 1)
                    {
                        if (j == 2 || j == 4) drawFormat.Alignment = StringAlignment.Near; else drawFormat.Alignment = StringAlignment.Center;

                        g.DrawRectangle(Pens.Black, marginleft - 1, margintop, DV1width(j), cellheight);

                        // Отрисовка данных круга
                        if (j < dv.Columns.Count && dv.Columns[j].Visible)
                        {
                            if (j < dv.Columns.Count && dv.Rows[i].Cells[j].Value != null)
                            {
                                Font font = new Font("Calibri", 9);

                                val = dv.Rows[i].Cells[j].Value.ToString();

                                if (j > 4 && val.Length > 5) val = val.Remove(val.Length - 1, 1);

                                g.DrawString(val, font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), cellheight), drawFormat);


                                font.Dispose();
                            }
                        }

                        marginleft += DV1width(j);
                    }
                }
            }
        }

        private void DrawDatagrid3(DataGridView dv, int ML, int MT, Graphics g)
        {

            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            int margintop = MT; // Смещение ячейки по Y
            int marginleft = ML; // Смещение ячейки по X
            int cellheight = 20; // Высота ячейки
            int ccount = 4; // Общее количество ячеек
            string val = String.Empty;

            // Отрисовка шапки
            for (int j = 0; j < ccount; j++)
            {
                Font font = new Font("Calibri", 8, FontStyle.Bold);
                using (Pen p = new Pen(Color.Black, 2)) g.DrawRectangle(p, marginleft - 1, margintop, DV1width(j), 25);

                if (j < dv.Columns.Count && dv.Columns[j].Visible)
                {
                    switch (j)
                    {
                        case 1: val = "Дата"; break;
                        case 2: val = "Имя"; break;
                        case 3: val = "Время"; break;
                        default: val = String.Empty; break;
                    }

                    g.DrawString(val, font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), 25), drawFormat);

                }
                marginleft += DV1width(j);

                font.Dispose();
            }

            margintop += 5;

            for (int i = 0; i < dv.Rows.Count; i++)
            {
                margintop += cellheight;
                marginleft = ML;


                for (int j = 0; j < ccount; j++)
                {
                    if (j == 2 || j == 4) drawFormat.Alignment = StringAlignment.Near; else drawFormat.Alignment = StringAlignment.Center;

                    g.DrawRectangle(Pens.Black, marginleft - 1, margintop, DV1width(j), cellheight);

                    // Отрисовка данных круга
                    if (j < dv.Columns.Count && dv.Columns[j].Visible)
                    {
                        if (j < dv.Columns.Count && dv.Rows[i].Cells[j].Value != null)
                        {
                            Font font = new Font("Calibri", 9);

                            val = dv.Rows[i].Cells[j].Value.ToString();

                            if (j > 4 && val.Length > 5) val = val.Remove(val.Length - 1, 1);

                            g.DrawString(val, font, Brushes.Black, new RectangleF(marginleft - 1, margintop + 1, DV1width(j), cellheight), drawFormat);


                            font.Dispose();
                        }
                    }

                    marginleft += DV1width(j);

                }
            }
        }


        private Hashtable FindBestLap(DataGridView dv)
        {
            Hashtable ret = new Hashtable();
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                if (dv[4, i].Value != null)
                {
                    string[] TL = dv[4, i].Value.ToString().Split(new string[] { "/" }, StringSplitOptions.None);

                    int val = Convert.ToInt16(TL[1]);

                    if (val > 0)
                    {
                        ret[i] = val + 4;
                    }
                }
            }

            return ret;
        }

        private int DV1width(int tp)
        {
            int width = 0;

            switch (tp)
            {
                case 0: width = 24; break;
                case 1: width = 110; break;
                case 2: width = 110; break;
                case 3: width = 45; break;
                case 4: width = 45; break;
                case 29: width = 32; break;
                default: width = 27; break;
            }

            return width;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            admin.ShowBestDMResult(dv2, CurrentRace.Date.ToString("yyyy-MM-dd"), CurrentRace.TrackID, true, "1");
            admin.ShowBestDMResult(dv3, CurrentRace.Date.ToString("yyyy-MM-dd"), CurrentRace.TrackID, false, "2");

        }


    }


}
