using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.IO;

namespace Rentix
{
    public partial class Anonser : Form
    {
        AdminControl admin;
        MainForm mf;
        int hourWhenDateUpdated; // запомним в каком часу обновляли дату, и если сейчас другой час, обновим опять



        private bool isImageShows; // if true, then now images shown insteat laps time
        int currentTickNumber; // image will changed every NN seconds


        public Anonser(AdminControl ad, MainForm f)
        {
            admin = ad;
            mf = f;
            InitializeComponent();

            // если не были установлены параметры анонсера, загрузим значения по умолчанию
            if (admin.Settings["anonser_nextRacePilotsTimer"] == null)
            {
                admin.Settings = admin.model.DefaultAnonserSettings(admin.Settings);               
            }
              
            showImageOrLapsTime();
            showNewSponsorImage();
        }

        private void Anonser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.X || (e.KeyCode == Keys.Escape) || (e.KeyCode == Keys.F9))
            {
                this.Close();
            }
            if (e.KeyCode == Keys.F1)
            {
                if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
                {
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                }
                else
                {
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                }
            }
        }


        int periodType = 4;

        private void ShowBestRes()
        {
            if (periodType > 3)
            {
                periodType = 1;
            }

            switch (periodType)
            {

                case 1: labelSmooth4.Text = "Лучший результат дня"; break;
                case 2: labelSmooth4.Text = "Лучший результат недели"; break;
                case 3: labelSmooth4.Text = "Лучший результат"; break;
              /*
                case 3: labelSmooth4.Text = "Лучший результат месяца"; break;
                case 4: labelSmooth4.Text = "Лучший результат года"; break;
               */


            }
            admin.ShowAnonserBestResult(bestResult_dataGridView1, periodType, Convert.ToInt32(admin.Settings["default_track"]));

            periodType++;
        }


        bool TimeSeparator = false;

        private void setCurrentDate()
        {
            if (hourWhenDateUpdated != DateTime.Now.Hour)
            {
                date_labelSmooth7.Text = DateTime.Now.ToString("dd.MM.yyyy") + " г.";
                hourWhenDateUpdated = DateTime.Now.Hour;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            setCurrentDate();

            if (TimeSeparator)
            {
                LH.Text = DateTime.Now.ToString("HH.mm");
            }
            else
            {
                LH.Text = DateTime.Now.ToString("HH:mm");
            }

            TimeSeparator = !TimeSeparator;

            // Показывает пилотов следующего рейса
            if ((DateTime.Now.Second % Convert.ToInt32(admin.Settings["anonser_nextRacePilotsTimer"])) == 0)
            {
                admin.ShowNextRacePilots(dataGridView2, labelSmooth5, labelSmooth6);
            }

            
            // Показывает загрузку рейса
            if ((DateTime.Now.Second % Convert.ToInt32(admin.Settings["anonser_raceLoadTimer"])) == 0)
            {
                admin.ShowRaceLoad(dataGridView4);
            }

            // Показывает лучший результат
            if ((DateTime.Now.Second % Convert.ToInt32(admin.Settings["anonser_bestResultTimer"])) == 0)
            {
                ShowBestRes();
            }

            if(admin.Settings["sponsors_images_rotate_timer"] != null && Convert.ToInt32(admin.Settings["sponsors_images_rotate_timer"]) > 0 && DateTime.Now.Second % Convert.ToInt32(admin.Settings["sponsors_images_rotate_timer"]) == 0)
            {
                showNewSponsorImage();
            }

            // Обработка отображения времени рейса
            if (admin.RaceTH.Status == 1 || admin.RaceTH.Status == 2)
            {

                long ticks = admin.RaceTH.MaxTicksForRace - (DateTime.Now.Ticks - admin.RaceTH.StartTick - admin.RaceTH.PauseTick - admin.RaceTH.TempPauseTick);

                if (ticks > 0 && (admin.RaceTH.Status == 1 || (admin.RaceTH.Status == 2 && !Convert.ToBoolean(admin.Settings["stop_on_pause"]))))
                {
                    showImageOrLapsTime();                 

                    int percent = Math.Abs((int)((ticks * 100) / admin.RaceTH.MaxTicksForRace - 100));

                    labelSmooth1.Text = ticks>0? new DateTime(ticks).ToString("mm:ss"):"00:00";
                    labelSmooth3.Text = "Идет заезд №" + (admin.RaceTH.Race.RaceNum > admin.DopRace ? (admin.RaceTH.Race.RaceNum - admin.DopRace).ToString() + "a" : admin.RaceTH.Race.RaceNum.ToString());


                    // Показывает текущие результаты
                    if ((DateTime.Now.Second % Convert.ToInt32(admin.Settings["anonser_currentResultTimer"])) == 
                        1 || (DateTime.Now.Second %
                        Convert.ToInt32(admin.Settings["anonser_currentResultTimer"])) == 0)
                    {
                        admin.ShowRaceResults(anonserLaps_dataGridView3, true);
                    }
                }
                else
                {
                    if (admin.RaceTH.Status == 2)
                    {
                        labelSmooth1.Text = "Пауза";
                    }
                    else
                    {
                        labelSmooth1.Text = "Старт";
                    }
                }
            }
            else
            { // заезда сейчас нет
                anonserLaps_dataGridView3.Rows.Clear();

                showImageOrLapsTime(); // покажем картинку вместо времени по кругам
                
                labelSmooth1.Text = mf.NextRaceTime;
                labelSmooth3.Text = "Следующий заезд №" + mf.NextRaceNum;
            }
        }


        private void showImageOrLapsTime()
        {
            if (admin.RaceTH.Status == 1 || admin.RaceTH.Status == 2)
            {
                // идёт заезд, должно показываться время по кругам
                if (isImageShows) // а показывается картинка
                {
                    tableLayoutPanel1.SetRow(anonserLaps_dataGridView3, 1);
                    tableLayoutPanel1.SetRow(anonserImage_pictureBox, 0);
                    isImageShows = false;
                }              
                
            }
            else
            {
                // заезда нет, должна показываться картинка
                if (!isImageShows) // а она не показывается
                {
                    tableLayoutPanel1.SetRow(anonserLaps_dataGridView3, 0);
                    tableLayoutPanel1.SetRow(anonserImage_pictureBox, 1);
                    isImageShows = true;
                    currentTickNumber = 500;
                }
                else
                {
                    currentTickNumber++;
                                      
                    if (currentTickNumber > (Convert.ToInt32(admin.Settings["anonser_nextRacePilotsTimer"])))
                    {
                        currentTickNumber = 0;
                        showNewImage(); // показать другую картинку
                    }

                    /*
                    Random rnd = new Random();
                    int someInt = rnd.Next(10);

                    if (currentTickNumber > (Convert.ToInt32(admin.Settings["anonser_nextRacePilotsTimer"])) + someInt))
                    {
                        currentTickNumber = 0;
                        showNewImage(); // показать другую случайную картинку
                    }
                     */
                }
            }

        }

        // показать случайную картинку в анонсере
        private void showNewImage()
        {
            if (admin.Settings["images_for_anonser"] == null || admin.Settings["images_for_anonser"].ToString().Length == 0 )
            {
                return;
            }

            if (!Directory.Exists((string)admin.Settings["images_for_anonser"]))
            {
                return;
            }

            
            string[] imageFiles = Directory.GetFiles((string)admin.Settings["images_for_anonser"], "*.jpg");
            imageFiles = imageFiles.Concat(Directory.GetFiles((string)admin.Settings["images_for_anonser"], "*.bmp")).ToArray();
            imageFiles = imageFiles.Concat(Directory.GetFiles((string)admin.Settings["images_for_anonser"], "*.png")).ToArray();
            imageFiles = imageFiles.Concat(Directory.GetFiles((string)admin.Settings["images_for_anonser"], "*.gif")).ToArray();
            
            if (imageFiles.Length == 0)
            {
                return;
            }

            Random rnd = new Random();
            string someImage = imageFiles[rnd.Next(imageFiles.Length)];

            if (File.Exists(someImage))
            {
                anonserImage_pictureBox.ImageLocation = someImage;
            }

        }

          // показать случайную картинку в анонсере
        private void showNewSponsorImage()
        {
            if (admin.Settings["sponsors_images_for_anonser"] == null || admin.Settings["sponsors_images_for_anonser"].ToString().Length == 0)
            {                
                return;
            }

            if(!Directory.Exists((string)admin.Settings["sponsors_images_for_anonser"]))
            {
                return;
            }

            string[] imageFiles = Directory.GetFiles((string)admin.Settings["sponsors_images_for_anonser"], "*.jpg");
            imageFiles = imageFiles.Concat(Directory.GetFiles((string)admin.Settings["sponsors_images_for_anonser"], "*.bmp")).ToArray();
            imageFiles = imageFiles.Concat(Directory.GetFiles((string)admin.Settings["sponsors_images_for_anonser"], "*.png")).ToArray();
            imageFiles = imageFiles.Concat(Directory.GetFiles((string)admin.Settings["sponsors_images_for_anonser"], "*.gif")).ToArray();
            
            if (imageFiles.Length == 0)
            {
                return;
            }

            Random rnd = new Random();
            string someImage = imageFiles[rnd.Next(imageFiles.Length)];

            if (File.Exists(someImage))
            {
                sponsor_pictureBox1.ImageLocation = someImage;
            }

        }



        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                if (e.RowIndex % 2 == 0) e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
                else using (Brush br = new LinearGradientBrush(new Point(0, 0), new Point(e.ClipBounds.Width, 0), Color.FromArgb(0, 0, 0), Color.FromArgb(253, 0, 0))) e.Graphics.FillRectangle(br, e.CellBounds);
                string content = dataGridView2[e.ColumnIndex, e.RowIndex].Value == null ? " * " : dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString(); ;

                string[] data = content.Split(new string[] { "*" }, StringSplitOptions.None);


                // Никнейм пилота
                using (Font f2 = new Font("Tahoma", 21))
                {
                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                    drawFormat.Alignment = StringAlignment.Near;
                    content = data.Length > 0 ? data[0] : "";
                    e.Graphics.DrawString(content, f2, Brushes.White, e.CellBounds.X, e.CellBounds.Y, drawFormat);
                    drawFormat.Dispose();
                }

                // Имя пилота
                using (Font f2 = new Font("Tahoma", 11))
                {

                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                    drawFormat.Alignment = StringAlignment.Near;

                    content = data.Length > 1 ? data[1] : "";
                    e.Graphics.DrawString(content, f2, Brushes.DarkGray, e.CellBounds.X + 6, e.CellBounds.Bottom - 17, drawFormat);
                    drawFormat.Dispose();
                }
            }
            e.Handled = true;


        }

        private void dataGridView4_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                if (e.RowIndex % 2 == 0) e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
                else using (Brush br = new LinearGradientBrush(new Point(0, 0), new Point(e.ClipBounds.Width, 0), Color.FromArgb(0, 0, 0), Color.FromArgb(253, 0, 0))) e.Graphics.FillRectangle(br, e.CellBounds);
                string content = dataGridView4[e.ColumnIndex, e.RowIndex].Value == null ? "" : dataGridView4[e.ColumnIndex, e.RowIndex].Value.ToString(); ;

                using (Pen p = new Pen(Color.FromArgb(68, 31, 31)))
                    e.Graphics.DrawRectangle(p, e.CellBounds);
                // Прорисовка данных
                using (Font f2 = new Font("Tahoma", 21))
                {
                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                    drawFormat.LineAlignment = StringAlignment.Center;
                    if (e.ColumnIndex == 0)
                        drawFormat.Alignment = StringAlignment.Near;
                    else
                        drawFormat.Alignment = StringAlignment.Center;

                    e.Graphics.DrawString(content, f2, Brushes.White, e.CellBounds, drawFormat);
                    drawFormat.Dispose();
                }

            }
            e.Handled = true;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex == -1)
            {
                using (Brush br = new SolidBrush(Color.FromArgb(253, 14, 0)))
                {
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.Paint(e.ClipBounds, (DataGridViewPaintParts.All & ~DataGridViewPaintParts.Background));
                }
            }

            if (e.RowIndex >= 0)
            {

                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                if (e.RowIndex % 2 == 0) e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
                else using (Brush br = new LinearGradientBrush(new Point(0, 0), new Point(e.ClipBounds.Width, 0), Color.FromArgb(0, 0, 0), Color.FromArgb(253, 0, 0))) e.Graphics.FillRectangle(br, e.CellBounds);
                string content = bestResult_dataGridView1[e.ColumnIndex, e.RowIndex].Value == null ? "" : bestResult_dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(); ;


                using (Pen p = new Pen(Color.FromArgb(68, 31, 31)))
                    e.Graphics.DrawRectangle(p, e.CellBounds);
                // Прорисовка данных
                int th = 20;
                if (e.ColumnIndex >= 2) th = 15;

                using (Font f2 = new Font("Tahoma", th))
                {
                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                    drawFormat.LineAlignment = StringAlignment.Center;
                    if (e.ColumnIndex != 2)
                        drawFormat.Alignment = StringAlignment.Near;
                    else
                        drawFormat.Alignment = StringAlignment.Center;

                    e.Graphics.DrawString(content, f2, Brushes.White, e.CellBounds, drawFormat);
                    drawFormat.Dispose();
                }

            }
            e.Handled = true;
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
        }

        private void dataGridView3_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex == -1)
            {
                using (Brush br = new SolidBrush (Color.FromArgb(253,14,0))){
                e.Graphics.FillRectangle(br, e.CellBounds);
                e.Paint(e.ClipBounds, (DataGridViewPaintParts.All & ~DataGridViewPaintParts.Background));
                }
            }

            if (e.RowIndex >= 0)
            {

                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                if (e.RowIndex % 2 == 0) e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
                else using (Brush br = new LinearGradientBrush(new Point(0, 0), new Point(e.ClipBounds.Width, 0), Color.FromArgb(0, 0, 0), Color.FromArgb(253,0,0))) e.Graphics.FillRectangle(br, e.CellBounds);
                string content = anonserLaps_dataGridView3[e.ColumnIndex, e.RowIndex].Value == null ? "" : anonserLaps_dataGridView3[e.ColumnIndex, e.RowIndex].Value.ToString(); ;

                using (Pen p = new Pen(Color.FromArgb(68,31,31)))
                e.Graphics.DrawRectangle(p, e.CellBounds);
                // Прорисовка данных
                int th = 22;

                using (Font f2 = new Font("Tahoma", th))
                {
                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                    drawFormat.LineAlignment = StringAlignment.Center;
                    if (e.ColumnIndex == 1 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
                        drawFormat.Alignment = StringAlignment.Near;
                    else
                        drawFormat.Alignment = StringAlignment.Center;

                    e.Graphics.DrawString(content, f2, Brushes.White, e.CellBounds, drawFormat);
                    drawFormat.Dispose();
                }

            }
            e.Handled = true;

        }


    }
}
