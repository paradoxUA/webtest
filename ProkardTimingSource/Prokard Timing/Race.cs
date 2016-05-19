using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using Prokard_Timing.Controls;

namespace Prokard_Timing
{
    public partial class Race : Form
    {
        // Заглушки
        public RaceClass CurrentRace;
        private MainForm parent;
        private LastSelectClass SelKart = new LastSelectClass();
        private bool lv1_mdown = false; // Флаг нажатия при Drag&Drop
        private bool raceButtonsAreInactive = false;   // Флаг когда нельзя ничего изменить
        private int SelectedRow = -1;   // Последняя выбранная ячейка пилотов
        comboBoxItem ci = new comboBoxItem("", -1); // нестандартный combobox 


        private string RaceTime = "10:00"; // длительность заезда
        public Race(MainForm mainForm)
        {
            
            InitializeComponent();
            parent = mainForm; // this.Owner as MainForm;

            pilotsInRace_dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);
            raceTable_datagridview.DoubleBuffered(true);

            // время гонки по умолчанию
            // заполнить список с временами гонки
            fillRaceModes();

            comboBoxItem ci = new comboBoxItem("", -1);
            ci.selectComboBoxValueById(RaceMode_comboBox, Convert.ToInt32(parent.admin.Settings["default_race_mode_id"]));
            
        }

        // заполним список режимов заезда
        private void fillRaceModes()
        {
            RaceMode_comboBox.Items.Clear();

            List<Hashtable> data = parent.admin.model.GetAllRaceModes(" and is_deleted <> 1 ");

            for (int i = 0; i < data.Count; i++)
            {
                comboBoxItem someItem =
                    new comboBoxItem(Convert.ToString(data[i]["name"]),
                    Convert.ToInt32(data[i]["id"]));

                RaceMode_comboBox.Items.Add(someItem);
            }
        }


        // Передача в форму внешних данных
        public void SetRace(RaceClass Race)
        {
           
            CurrentRace = CopyHelper.CreateDeepCopy(Race);

            if (CurrentRace.Status == 2)
            {
                tabControl1.SelectedIndex = 1;
            }

            // Легкий режим
            lightMode_checkBox.Checked = CurrentRace.Light_mode == 1;
            lightMode_checkBox.BackColor = lightMode_checkBox.Checked ? Color.FromArgb(255, 128, 128) : Color.Transparent;

            this.Text = "Заезд №" + (CurrentRace.RaceNum > parent.admin.DopRace ? (CurrentRace.RaceNum - parent.admin.DopRace).ToString() + "a" : CurrentRace.RaceNum.ToString()) + " Отправление в " + CurrentRace.Hour + ":" + CurrentRace.Minute;
            idRaceLabel.Text = (CurrentRace.RaceNum > parent.admin.DopRace ? (CurrentRace.RaceNum - parent.admin.DopRace).ToString() + "a" : CurrentRace.RaceNum.ToString()) + "  ("+CurrentRace.RaceID.ToString()+")";

            if (CurrentRace.Date >= DateTime.Now.AddDays(1).Date || CurrentRace.Date <= DateTime.Now.AddDays(-1).Date)
            {
                label11.Text = CurrentRace.Date.ToString("dd MMMM, в ") + CurrentRace.Hour + ":" + CurrentRace.Minute;
            }
            else
            {

                label11.Text = CurrentRace.Hour + ":" + CurrentRace.Minute;
            }

            int RaceMaxTick = Convert.ToInt32(parent.admin.Settings["race_time"]);

            if (RaceMode_comboBox.SelectedItem == null)
            {
                MessageBox.Show("В настройках программы необходимо указать режим заезда по умолчанию");
                return;
            }


            string selectedTimeString = RaceMode_comboBox.SelectedItem.ToString(); // 10 минут 
            selectedTimeString = selectedTimeString.Substring(0,selectedTimeString.IndexOf(" "));
              
            RaceMaxTick = Convert.ToInt16(selectedTimeString.Trim()); // Convert.ToInt16(ci.getSelectedValue(RaceMode_comboBox));

            /*
            switch (RaceMode_comboBox.SelectedIndex)
            {

                case 0: RaceMaxTick = 5; break;

                case 2: RaceMaxTick = 15; break;

            }
            */
            RaceTime = new DateTime(new TimeSpan(0, RaceMaxTick, 0).Ticks).ToString("mm:ss");
            timeMode_label.Text = RaceTime;


            RefreshForm();

            labelSmooth1.Text = CurrentRace.TrackName;


        }

        // Обновляем данные формы
        private void RefreshForm()
        {
            parent.admin.ShowRacePilots(CurrentRace.RaceID, pilotsInRace_dataGridView1, 1);

            long t20 = DateTime.Now.AddMinutes(-parent.admin.TimeToCreate).Ticks;
            long tcurr = CurrentRace.Date.Ticks; // вероятно, прошедшее время заезда, в тиках
            
            // Условия блокирования формы
            // sngavrilenko: блокировать кнопки запуска рейса нужно в таких случаях:
            /* если заезд со статусом 0 (stop) и если нет пилотов в заезде и время старта заезда на 20 минут ранее текущего
             *  один раз я даже хотел навести тут порядок. сделать статус как enum, потратил часа 3 и без результата. лучше в эту функцию не лезть, так как тут полный капец.
             *  или потратить день и перелопатить весь проект. 
             * 
             */
                    
            if (CurrentRace.Status == 2  || CurrentRace.Date.DayOfYear < DateTime.Now.Date.DayOfYear ||
                (CurrentRace.Date.DayOfYear == DateTime.Now.Date.DayOfYear && tcurr < t20))
            {
               
                raceButtonsAreInactive = true;
            }
           

              if (CurrentRace.Date.DayOfYear < DateTime.Now.Date.DayOfYear || // если открыли форму в другой день
                (CurrentRace.Status == 1 && CurrentRace.Date.DayOfYear == DateTime.Now.Date.DayOfYear && tcurr < t20 // или день сегодняшний, и гонка запущена и время истекло                
                ))
            {               
                raceButtonsAreInactive = true;
            }



            // Установка свойств при завершении рейса
            if (CurrentRace.Status == 2 || raceButtonsAreInactive)
            {
                startCancel_toolStripMenuItem3.Enabled = false;
                
                dataGridView2.Visible = false;
                toolStrip6.Visible = false;
                borderPanel1.Visible = false;
                tabControl1.Dock = DockStyle.Fill;
                pilots_tableLayoutPanel1.ColumnCount = 1;
                // tabPage1.Parent = tabControl2;
                parent.admin.ShowFinnalyRaceResult(detailedTable_dataGridView4, CurrentRace.RaceID);
                
                /* не нужно скрывать панель пилотов никогда
                if (CurrentRace.Status == 2)
                {
                    панельПилотовToolStripMenuItem.Checked = RightPanels.Visible = false;
                }
                 */
                RaceMode_comboBox.Enabled = false;
                lightMode_checkBox.Enabled = false;
            }
            else
            {
                parent.admin.ShowRaceKarts(dataGridView2, 1);
            }


            if (parent.admin.RaceTH.RaceID == CurrentRace.RaceID && parent.admin.RaceTH.Status > 0)
            {
                RaceMode_comboBox.Enabled = false;
            }

            
            SetRunButton();

            label9.Text = "Max - " + parent.admin.MaxKarts.ToString();
            Repaint();
        }

        // Установка активности кнопок запуска // new
        private void SetRunButton()
        {

            int status = 0;

            if (parent.admin.RaceTH.RaceID == CurrentRace.RaceID) status = parent.admin.RaceTH.Status;
            else if (parent.admin.RaceTH.Status > 0) status = 3;

            if (pilotsInRace_dataGridView1.Rows.Count == 0 || ((CurrentRace.Karts.Count != pilotsInRace_dataGridView1.Rows.Count) && !lightMode_checkBox.Checked) || !parent.admin.EnabledRaceStart(CurrentRace) || (GetCountReserv() != 0)) status = 3;

            if (raceButtonsAreInactive)
            {
                status = 3;
            }

            switch (status)
            {
                case 0:
                    {
                        Run_Race_button.Enabled = true;
                        PauseRace_button.Enabled = Stop_Race_button.Enabled = false;
                        
                    } break;

                case 1:
                    {
                        Run_Race_button.Enabled = false;
                        PauseRace_button.Enabled = Stop_Race_button.Enabled = true;
                        заездToolStripMenuItem.Enabled = true;
                    } break;

                case 2:
                    {
                        Run_Race_button.Enabled = Stop_Race_button.Enabled = true;
                        заездToolStripMenuItem.Enabled = true;
                        PauseRace_button.Enabled = false;
                    } break;
                case 3:
                    {
                        заездToolStripMenuItem.Enabled = false;
                        Run_Race_button.Enabled = PauseRace_button.Enabled = 
                            Stop_Race_button.Enabled = false;
                    } break;
            }


            //Run_Race.BackColor = Run_Race.Enabled ? Color.Lime : Color.LightGreen;
            //Pause_Race.BackColor = Pause_Race.Enabled ? Color.Yellow : Color.LightYellow;
            //Stop_Race.BackColor = Stop_Race.Enabled ? Color.Red : Color.LightPink;

            стартToolStripMenuItem1.Enabled = Run_Race_button.Enabled;
            паузаToolStripMenuItem1.Enabled = PauseRace_button.Enabled;
            финишToolStripMenuItem.Enabled = Stop_Race_button.Enabled;
            SetControlButton();

            /*
            int status = 0;

            // если открыта форма с текущим рейсом
            if (parent.admin.RaceTH.RaceID == CurrentRace.RaceID)
            {
                status = parent.admin.RaceTH.Status;
            }
            else if (parent.admin.RaceTH.Status > 0)
            { // открыта форма с каким-то другим рейсом, запрещаем повторный запуск гонки
                status = 3;
            }

            // если режим "не свободный" и имеются пилоты без картов, запрещаем запуск гонки
            if (pilotsInRace_dataGridView1.Rows.Count == 0 || 
                ((CurrentRace.Karts.Count != pilotsInRace_dataGridView1.Rows.Count) && !lightMode_checkBox.Checked) ||
                !parent.admin.EnabledRaceStart(CurrentRace) || 
                (GetCountReserv() != 0))
            {
                status = 3;
            }

            if (raceButtonsAreInactive)
            {
                status = 3;
            }

            switch (status)
            {
                case 0: // гонка не запущена
                    {
                        Run_Race_button.Enabled = true;
                        PauseRace_button.Enabled = Stop_Race_button.Enabled = false;
                        
                    } break;

                case 1: // гонка запущена
                    {
                        Run_Race_button.Enabled = false;
                        PauseRace_button.Enabled = Stop_Race_button.Enabled = true;
                        startCancel_toolStripMenuItem3.Enabled = true;
                    } break;

                case 2: // пауза
                    {
                        Run_Race_button.Enabled = Stop_Race_button.Enabled = true;
                        startCancel_toolStripMenuItem3.Enabled = true;
                        PauseRace_button.Enabled = false;
                    } break;
                case 3: // не текущий заезд или нет картов у пилотов в режиме "не свободный" 
                    {
                        startCancel_toolStripMenuItem3.Enabled = false;
                        Run_Race_button.Enabled = PauseRace_button.Enabled = Stop_Race_button.Enabled = false;
                    } break;
            }


            //Run_Race.BackColor = Run_Race.Enabled ? Color.Lime : Color.LightGreen;
            //Pause_Race.BackColor = Pause_Race.Enabled ? Color.Yellow : Color.LightYellow;
            //Stop_Race.BackColor = Stop_Race.Enabled ? Color.Red : Color.LightPink;

            стартToolStripMenuItem1.Enabled = Run_Race_button.Enabled;
            паузаToolStripMenuItem1.Enabled = PauseRace_button.Enabled;
            финишToolStripMenuItem.Enabled = Stop_Race_button.Enabled;
            SetControlButton();
             */

        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((((parent.admin.RaceTH.Status == 0 || parent.admin.RaceTH.Status == 2) && CurrentRace.RaceID == parent.admin.RaceTH.RaceID) || CurrentRace.RaceID != parent.admin.RaceTH.RaceID && !raceButtonsAreInactive) && !lightMode_checkBox.Checked)
                if (!lv1_mdown) lv1_mdown = true;
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (lv1_mdown)
            {
                if (e.Data.GetDataPresent(DataFormats.Text))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            lv1_mdown = false;
        }

        private void dataGridView2_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Point p = dataGridView2.PointToClient(new Point(e.X, e.Y));
                int row = dataGridView2.HitTest(e.X, e.Y).RowIndex;
                int col = dataGridView2.HitTest(e.X, e.Y).ColumnIndex;


                if (row >= 0 && col >= 0 && dataGridView2.Rows[row].Cells[col].Value != null)
                {

                    string str = dataGridView2.Rows[row].Cells[col].Value.ToString();
                    if (str == "") return;

                    if (parent.admin.KartInRace(CurrentRace, str)) return;

                    dataGridView2.DoDragDrop(str, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {

            Point p = pilotsInRace_dataGridView1.PointToClient(new Point(e.X, e.Y));
            int row = pilotsInRace_dataGridView1.HitTest(p.X, p.Y).RowIndex;

            if (row <= pilotsInRace_dataGridView1.Rows.Count && row >= 0)
            {
                if (e.Effect == DragDropEffects.Move || e.Effect == DragDropEffects.Copy)
                {
                    string kart_num = e.Data.GetData(DataFormats.Text).ToString();

                    if (pilotsInRace_dataGridView1.Rows[row].Cells[3].Value.ToString().Length > 0)
                    {
                        CurrentRace.Karts.Remove(pilotsInRace_dataGridView1.Rows[row].Cells[3].Value.ToString());
                    }

                    pilotsInRace_dataGridView1.Rows[row].Cells[3].Value = kart_num;
                    CurrentRace.Karts.Add(kart_num);
                    parent.admin.model.AddKartToRace(pilotsInRace_dataGridView1.Rows[row].Cells[4].Value.ToString(), kart_num, false);
                    SetRunButton();
                    dataGridView2.Refresh();
                }
            }
        }

        // Прорисовка картов
        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Color col = new Color();
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                if (e.Value != null && parent.admin.KartInRace(CurrentRace, e.Value.ToString()))
                    col = Color.FromArgb(144, 144, 144);
                else
                    col = Color.FromArgb(244, 244, 244);

                using (Brush brush = new SolidBrush(col))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    e.Graphics.DrawRectangle(Pens.DarkGray, e.CellBounds);
                }

                if ((e.State & DataGridViewElementStates.Selected) != 0)
                {
                    col = Color.FromArgb(33, 0, 0, 255);
                    using (Brush brush = new SolidBrush(col))
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                if (e.Value != null)
                    using (Font f1 = new Font("Calibri", 14))
                    {
                        string content = e.Value.ToString();
                        e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                        StringFormat drawFormat = new System.Drawing.StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;
                        drawFormat.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(content, f1, parent.admin.KartInRace(CurrentRace, e.Value.ToString()) ? Brushes.White : Brushes.Black, e.CellBounds, drawFormat);
                    }

            }


            e.Handled = true;
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1.PerformClick();
        }

        // ДОбавление пилота в рейс
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            AddPilot form = new AddPilot(CurrentRace, parent.admin);

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RefreshForm();

            }

            timer1.Enabled = true;
        }

        // Событие вызываемое при закрытии формы
        private void Race_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.admin.ReloadTable();
            parent.Activate();
           // this.Dispose();
        }

        // Запуск рейса
        private void Run_Race_Click(object sender, EventArgs e)
        {
            receivedData_textBox1.Clear();
            RaceMode_comboBox.Enabled = false;
            if (панельПилотовToolStripMenuItem.Checked) панельПилотовToolStripMenuItem.PerformClick();
            parent.admin.RaceTH.RaceID = CurrentRace.RaceID;

            if (parent.admin.RaceTH.Status == 2)
            {
                parent.admin.RaceStart(CurrentRace.RaceID, 1, 0); // продолжение после паузы

                if (Convert.ToBoolean(parent.admin.Settings["stop_on_pause"]))
                {
                    parent.admin.RaceTH.PauseTick += DateTime.Now.Ticks - parent.admin.RaceTH.TempPauseTick;
                    parent.admin.RaceTH.TempPauseTick = 0;
                }
                //  parent.admin.model.AddToJurnal("13", "0", CurrentRace.RaceID.ToString(), "Запуск рейса " + (CurrentRace.RaceNum > parent.admin.DopRace ? (CurrentRace.RaceNum - parent.admin.DopRace).ToString() + "a" : CurrentRace.RaceNum.ToString()) + " после паузы  в " + DateTime.Now.ToString("HH:mm:ss"));

                parent.admin.model.AddToJurnal("13", -1, CurrentRace.RaceID, "Запуск рейса " + (CurrentRace.RaceNum > parent.admin.DopRace ? (CurrentRace.RaceNum - parent.admin.DopRace).ToString() + "a" : CurrentRace.RaceNum.ToString()) + " после паузы  в " + DateTime.Now.ToString("HH:mm:ss"));
            }
            else
            {
                parent.admin.RaceStart(CurrentRace.RaceID);
                parent.admin.RaceTH.Race = CurrentRace;

                string selectedTimeString = RaceMode_comboBox.SelectedItem.ToString(); // 10 минут 
                selectedTimeString = selectedTimeString.Substring(0, selectedTimeString.IndexOf(" "));
                //DbSettings dbsettings = new DbSettings();
                //dbsettings.Show();

                //TODO вынести в глобальный метод
                parent.admin.RaceTH.ThreadStart(Convert.ToInt16(selectedTimeString));

                if (parent.admin.RaceTH.InError)
                {

                    MessageBox.Show(parent.admin.RaceTH.ErrorLine);
                }


             //   timeMode_label.Text = RaceTime;

                if (!parent.admin.RaceTH.InError)
                {
                    parent.admin.model.AddToJurnal("10", -1, CurrentRace.RaceID, "Запуск рейса " + (CurrentRace.RaceNum > parent.admin.DopRace ? (CurrentRace.RaceNum - parent.admin.DopRace).ToString() + "a" : CurrentRace.RaceNum.ToString()) + " в " + DateTime.Now.ToString("HH:mm:ss"));
                }
            }
            if (!parent.admin.RaceTH.InError)
            {

                parent.admin.RaceTH.Status = 1;
            }
            SetRunButton();
        }

        // Приостановка рейса
        private void Pause_Race_Click(object sender, EventArgs e)
        {
            if (!панельПилотовToolStripMenuItem.Checked)
            {
                панельПилотовToolStripMenuItem.PerformClick();
            }
            if (parent.admin.RaceTH.RaceID == CurrentRace.RaceID && parent.admin.RaceTH.Status == 1)
            {
                parent.admin.model.AddToJurnal("11", -1, CurrentRace.RaceID, "Приостановка рейса " + (CurrentRace.RaceNum > parent.admin.DopRace ? (CurrentRace.RaceNum - parent.admin.DopRace).ToString() + "a" : CurrentRace.RaceNum.ToString()) + " в " + DateTime.Now.ToString("HH:mm:ss"));
                parent.admin.RaceTH.Status =  2; 
            }
            parent.admin.RaceTH.TempPauseTick = DateTime.Now.Ticks;
            SetRunButton();
        }

        // Остановка рейса
        private void Stop_Race_Click(object sender, EventArgs e)
        {
            if (parent.admin.RaceTH.RaceID == CurrentRace.RaceID)
            {
           //     parent.admin.RaceTH.Status = 3;
               parent.admin.RaceTH.Status = 0;
                parent.admin.RaceTH.ThreadStop();
                parent.admin.RaceTH.RaceID = -1;
               CurrentRace.Status = 2; 
            }

            RefreshForm();
        }

        private void Race_Activated(object sender, EventArgs e)
        {
            SetRunButton();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (parent.admin.RaceTH.Race != null && parent.admin.RaceTH.Race.Status == 2 && parent.admin.RaceTH.Race.RaceID == CurrentRace.RaceID && parent.admin.RaceTH.StartTick > 0 && !raceButtonsAreInactive)
            {
                CurrentRace.Status = parent.admin.RaceTH.Race.Status;
                RefreshForm();
            }

            if (parent.admin.RaceTH.RaceID == CurrentRace.RaceID && parent.admin.RaceTH.StartTick > 0 && (parent.admin.RaceTH.Status == 1 || parent.admin.RaceTH.Status == 2))
            {
                long ticks = parent.admin.RaceTH.MaxTicksForRace - (DateTime.Now.Ticks - parent.admin.RaceTH.StartTick - parent.admin.RaceTH.PauseTick);

                if (ticks > 0 && (parent.admin.RaceTH.Status == 1 || (parent.admin.RaceTH.Status == 2 && !Convert.ToBoolean(parent.admin.Settings["stop_on_pause"]))))
                {
                    int percent = Math.Abs((int)((ticks * 100) / parent.admin.RaceTH.MaxTicksForRace - 100));

                    if (percent > 0)
                    {
                        progressBar1.Value = percent;
                        label7.Text = percent.ToString() + " %";
                        
                    }
                    receivedData_textBox1.Text = parent.admin.RaceTH.stringForLogWindow;
                    label4.Text = parent.admin.RaceTH.lastReceive;
                    parent.admin.ShowRaceResults(raceTable_datagridview);
                    parent.admin.ShowFinnalyRaceResult(detailedTable_dataGridView4, CurrentRace.RaceID);




                  timeMode_label.Text = new DateTime(ticks).ToString("mm:ss");

                  

                    /*
                    richTextBox1.Text = "";
                    for (int i = 0; i < parent.admin.RaceTH.ReceivData.Count; i++)
                    {
                        richTextBox1.SelectedText = parent.admin.RaceTH.ReceivData[i].RX + "\r\n";

                    }*/

                }

            }

            if (parent.admin.RaceTH.Status == 2 && parent.admin.RaceTH.RaceID == CurrentRace.RaceID)
            {
                pauseTime_label8.Text = new DateTime(parent.admin.RaceTH.PauseTick + (DateTime.Now.Ticks - parent.admin.RaceTH.TempPauseTick)).ToString("mm:ss");
            }
            else if (parent.admin.RaceTH.Status == 1 && parent.admin.RaceTH.RaceID == CurrentRace.RaceID)
            {
                pauseTime_label8.Text = new DateTime(parent.admin.RaceTH.PauseTick + parent.admin.RaceTH.TempPauseTick).ToString("mm:ss");
            }
            else
            {
                pauseTime_label8.Text = "00:00";
            }
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Race_Load(object sender, EventArgs e)
        {
            if (parent.admin.IS_SUPER_ADMIN)
            {
                transponderNumber_textBox2.Visible = true;
                sendTransponder_button.Visible = true;
            }

            timer1.Enabled = true;
        }

        // Прорисовка пилотов
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (pilotsInRace_dataGridView1[3, e.RowIndex].Value == null)
                {
                    pilotsInRace_dataGridView1[3, e.RowIndex].Value = "0";
                }

                if (pilotsInRace_dataGridView1[3, e.RowIndex].Value.Equals("0") || pilotsInRace_dataGridView1[3, e.RowIndex].Value.ToString() == "")
                {
                    pilotsInRace_dataGridView1.Rows[e.RowIndex].Cells[3].Value = "";
                    pilotsInRace_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 196, 178); // Color.AntiqueWhite; // пилоту необходимо указать карт Color.MediumSeaGreen;  // Color.FromArgb(192, 255, 191); 
                }
                else
                {
                    //pilotsInRace_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen; // карт указан Color.FromArgb(255, 253, 145); //  Color.FromArgb(255, 196, 178);
                }

                if (Convert.ToBoolean(pilotsInRace_dataGridView1[5, e.RowIndex].Value))
                {
          //          pilotsInRace_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue; // Color.FromArgb(255, 196, 178); //  Color.FromArgb(255, 253, 145);

            //        pilotsInRace_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(pilotsInRace_dataGridView1.RowsDefaultCellStyle.Font, FontStyle.Bold);
                }

            }
            /*
            if (e.RowIndex >= 0)
            {
                if (dataGridView1[3, e.RowIndex].Value == null) dataGridView1[3, e.RowIndex].Value = "0";

                if (dataGridView1[3, e.RowIndex].Value.Equals("0") || dataGridView1[3, e.RowIndex].Value.ToString() == "")
                {
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value = "";
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 191);
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 196, 178);
                }

                if (Convert.ToBoolean(dataGridView1[5, e.RowIndex].Value))
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 253, 145);
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dataGridView1.RowsDefaultCellStyle.Font, FontStyle.Bold);
                }

            }*/
        }

        // Обновляет цвета рейса
        private void Repaint()
        {
            parent.admin.ShowRacePilots(CurrentRace.RaceID, pilotsInRace_dataGridView1, 1);
            if (SelectedRow >= 0)
            {
                pilotsInRace_dataGridView1.Rows[SelectedRow].Selected = true;
            }
        }

        // Установка активных кнопок рейса
        private void SetControlButton()
        {
            bool SelPilotRow = pilotsInRace_dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0;
            bool SetKartRow = dataGridView2.GetCellCount(DataGridViewElementStates.Selected) > 0;

            if (raceButtonsAreInactive)
            {
                toolStripButton1.Enabled = false; // Добавить пилота
                toolStripButton2.Enabled = false; // Удалить пилота
                toolStripButton6.Enabled = false; // Перевод с резерва
                toolStripButton7.Enabled = false; // Удаление карта
                toolStripButton11.Enabled = false; // Ремонт карта
                toolStripButton13.Enabled = false; // Освобождение карта
                lightMode_checkBox.Enabled = false; // Режим свободной езды


                if (DateTime.Now.Date.DayOfYear == CurrentRace.Date.DayOfYear && CurrentRace.Status != 2 && raceButtonsAreInactive && pilotsInRace_dataGridView1.Rows.Count > 0)
                {
                    toolStripButton2.Enabled = true; // Удалить пилота
                }

            }
            else
            {
                // Добавление пилота
                toolStripButton1.Enabled = добавитьToolStripMenuItem.Enabled = pilotsInRace_dataGridView1.Rows.Count < parent.admin.MaxKarts;

                // Удаление пилота
                toolStripButton2.Enabled = SelPilotRow && SelectedRow >= 0;

                // Удаление карта у пилота
                toolStripButton7.Enabled = SelPilotRow && SelectedRow >= 0 && pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value != null && pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value.ToString() != "";

                // Резерв места
                toolStripButton6.Enabled = SelPilotRow && SelectedRow >= 0 && pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[5].Value != null && Convert.ToBoolean(pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[5].Value);
            }

            if (parent.admin.RaceTH.Status > 0 && CurrentRace.RaceID == parent.admin.RaceTH.RaceID)
            {

                // Во время паузы
                if (parent.admin.RaceTH.Status == 1)
                {
                    toolStripButton1.Enabled = false; // Добавить пилота
                    toolStripButton2.Enabled = false; // Удалить пилота
                    toolStripButton6.Enabled = false; // Перевод с резерва
                    toolStripButton7.Enabled = false; // Удаление карта
                    toolStripButton11.Enabled = false; // Ремонт карта
                    toolStripButton13.Enabled = false; // Освобождение карта
                    lightMode_checkBox.Enabled = false; // Режим свободной езды
                }
                else
                    if (parent.admin.RaceTH.Status == 2)
                    {

                        // Удаление карта у пилота
                        toolStripButton7.Enabled = SelPilotRow && SelectedRow >= 0 && pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value != null && pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value.ToString() != "";
                        toolStripButton2.Enabled = false; // Удалить пилота
                        toolStripButton1.Enabled = false; // Добавить пилота
                        toolStripButton6.Enabled = false; // Перевод с резерва
                        lightMode_checkBox.Enabled = false; // Режим свободной езды

                    }
            }

            // Информационные клавиши для пилотов
            toolStripButton10.Enabled = SelPilotRow && SelectedRow >= 0;
            // Информационные клавиши для картов
            toolStripButton12.Enabled = SetKartRow;

            // Просчет готовых пилотов
            if (pilotsInRace_dataGridView1.Rows.Count == 0) label6.Text = String.Empty;
            else
                label6.Text = "Готово " + CurrentRace.Karts.Count.ToString() + " из " + pilotsInRace_dataGridView1.Rows.Count.ToString();
            int CountReserv = GetCountReserv();
            if (CountReserv > 0) label13.Text = "Неоплаченых мест - " + CountReserv.ToString();
            else label13.Text = String.Empty;

            toolStripButton16.Enabled = завершитьГонкуToolStripMenuItem.Enabled = CurrentRace.Status == 2;

            добавитьToolStripMenuItem.Enabled = toolStripButton1.Enabled;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (SelectedRow >= 0)
                pilotsInRace_dataGridView1[0, e.RowIndex].Selected = true;
            SetControlButton();
        }

        // Отключение статуа резерв
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            bool ret = false;
            CashOperations form = new CashOperations(Int32.Parse(pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[1].Value.ToString()), CurrentRace, parent.admin);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK) ret = true;
            form.Dispose();

            if (ret)
            {
                parent.admin.model.ChangePilotReservStatus(pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[4].Value.ToString());
                pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[5].Value = "False";
                RefreshForm();
            }
        }

        // Удаление машины
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value != null && pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value.ToString().Length > 0)
            {
                CurrentRace.Karts.Remove(pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value.ToString());
                parent.admin.model.DelKartFromRace(pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[4].Value.ToString());
                pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value = "";
                dataGridView2.Refresh();
                SetRunButton();
            }
        }

        // Удаление пилота
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        // Получает количество зарезервированных мест
        private int GetCountReserv()
        {
            int ret = 0;

            for (int i = 0; i < pilotsInRace_dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(pilotsInRace_dataGridView1[5, i].Value)) ret++;

            }

            return ret;
        }

        private void стартToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Run_Race_button.PerformClick();
        }

        private void паузаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PauseRace_button.PerformClick();
        }

        private void финишToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop_Race_button.PerformClick();
        }

        private void закрытьОкноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (pilotsInRace_dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                PilotInfo form = new PilotInfo(Convert.ToInt32(
                    pilotsInRace_dataGridView1.SelectedRows[0].Cells[8].Value.ToString()), parent.admin);
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pilotsInRace_dataGridView1.Rows.Count > 0 && pilotsInRace_dataGridView1.SelectedRows[0].Cells[1].Value != null && pilotsInRace_dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                PilotInfo form = new PilotInfo(Convert.ToInt32(
                    pilotsInRace_dataGridView1.SelectedRows[0].Cells[1].Value.ToString()), parent.admin);
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (SelKart.Col > -1 && SelKart.Row > -1)
            {
                Kartinfo form = new Kartinfo(dataGridView2[SelKart.Col, SelKart.Row].Value.ToString(), parent.admin);
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {

            RepeirKart form = new RepeirKart(dataGridView2[SelKart.Col, SelKart.Row].Value.ToString(), parent.admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            parent.admin.MaxKarts = parent.admin.model.GetMaxKarts();
            RefreshForm();

            SelKart.Row = -1;
            SelKart.Col = -1;
            toolStripButton11.Enabled = toolStripButton12.Enabled = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (((parent.admin.RaceTH.Status == 0 || parent.admin.RaceTH.Status == 2) && CurrentRace.RaceID == parent.admin.RaceTH.RaceID) || CurrentRace.RaceID != parent.admin.RaceTH.RaceID && !raceButtonsAreInactive)
            {
                if (dataGridView2[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    SelKart.Row = e.RowIndex;
                    SelKart.Col = e.ColumnIndex;
                    toolStripButton11.Enabled = toolStripButton12.Enabled = true;
                }
                else
                {
                    SelKart.Row = -1;
                    SelKart.Col = -1;
                    toolStripButton11.Enabled = toolStripButton12.Enabled = false;
                }

                if (dataGridView2[e.ColumnIndex, e.RowIndex].Value != null)
                    toolStripButton11.Enabled = !parent.admin.KartInRace(CurrentRace, dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString());

                toolStripButton13.Enabled = !toolStripButton11.Enabled;
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton12.PerformClick();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
           
            if (SelKart.Col == -1 || SelKart.Row == -1)
            {
                MessageBox.Show("Выберите карт, который необходимо освободить");
                return;
            }

            if (dataGridView2[SelKart.Col, SelKart.Row].Value != null && dataGridView2[SelKart.Col, SelKart.Row].Value.ToString().Length > 0)
            {
              
                for (int i = 0; i < pilotsInRace_dataGridView1.Rows.Count; i++)
                {
                    if (pilotsInRace_dataGridView1.Rows[i].Cells[3].Value.ToString() == dataGridView2[SelKart.Col, SelKart.Row].Value.ToString())
                    {
                        parent.admin.model.DelKartFromRace(pilotsInRace_dataGridView1.Rows[i].Cells[4].Value.ToString());
                        pilotsInRace_dataGridView1.Rows[i].Cells[3].Value = "";
                        CurrentRace.Karts.Remove(dataGridView2[SelKart.Col, SelKart.Row].Value.ToString());
                        break;
                    }
                }
            }


            dataGridView2.Refresh();
            SetRunButton();
        }

        private void Race_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void панельПилотовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            панельПилотовToolStripMenuItem.Checked = RightPanels.Visible = !RightPanels.Visible;
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;

            PrintResult1 form = new PrintResult1(CurrentRace, parent.admin);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            if (parent.admin.RaceTH.Race != null && parent.admin.RaceTH.Race.RaceID == CurrentRace.RaceID && parent.admin.RaceTH.Status == 0)
            {
                parent.admin.RaceTH.Race = null;
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("toolStripButton16_Click (print race)", Logger.LogType.info, executionTime);
            //GC.Collect();
        }

        private void завершитьГонкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton16.PerformClick();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            lightMode_checkBox.BackColor = lightMode_checkBox.Checked ? Color.FromArgb(255, 128, 128) : Color.Transparent;
            parent.admin.model.ChangeRaceLightMode(lightMode_checkBox.Checked ? "1" : "0", CurrentRace.RaceID.ToString());
            SetRunButton();

            CurrentRace.Light_mode = lightMode_checkBox.Checked ? 1 : 0;
        }

        // удалить пилота из заезда
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ret = false;
            if (pilotsInRace_dataGridView1.SelectedRows.Count > 0)
            {
                SelectedRow = pilotsInRace_dataGridView1.SelectedRows[0].Index;
                if (pilotsInRace_dataGridView1.SelectedRows[0].Cells[5].Value.Equals("True"))
                {
                    ret = true;
                }
                else
                {
                    int idRaceData =  Int32.Parse(pilotsInRace_dataGridView1.SelectedRows[0].Cells[4].Value.ToString());

                    CorrectBallance form = new CorrectBallance(parent.admin, 
                        idRaceData, true,
                        CurrentRace);
                    form.Owner = this;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ret = true;
                    }
                }

                if (ret)
                {
                    if (pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value != null)
                    {
                        CurrentRace.Karts.Remove(pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value.ToString());
                    }
                    dataGridView2.Refresh();
                    bool isDeleted = parent.admin.model.DelPilotFromRace(pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[4].Value.ToString());

                    if (isDeleted)
                    {
                        pilotsInRace_dataGridView1.Rows.Remove(pilotsInRace_dataGridView1.Rows[SelectedRow]);
                        SelectedRow = -1;
                    }
                    SetRunButton();

                }
            }
        }

        private void переместитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedRow >= 0)
            {
                ChangeRace form = new ChangeRace(parent.admin, pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[1].Value.ToString(), pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[4].Value.ToString(), CurrentRace);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value != null)
                        CurrentRace.Karts.Remove(pilotsInRace_dataGridView1.Rows[SelectedRow].Cells[3].Value.ToString());
                    dataGridView2.Refresh();
                    pilotsInRace_dataGridView1.Rows.Remove(pilotsInRace_dataGridView1.Rows[SelectedRow]);
                    SelectedRow = -1;
                    SetRunButton();
                }
                form.Dispose();
            }
        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentRace == null)
            {
                timeMode_label.Text = "";
                return;
            }

            timeMode_label.Text = parent.admin.model.GetRaceModeById(ci.getSelectedValue(RaceMode_comboBox)).length.ToString() + ":00";
            
        }
         

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            if (parent.admin.RaceTH.RaceID == CurrentRace.RaceID)
            {
                parent.admin.RaceTH.Status = 0;
                parent.admin.RaceTH.ThreadCancel();
                parent.admin.RaceTH.RaceID = -1;
                CurrentRace.Status = 1;
                панельПилотовToolStripMenuItem.PerformClick();
            }

            RefreshForm();
        }

        // сымитировать сигнал от указанного датчика
        private void sendTransponder_button_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            string someAmbString = "@" + transponderNumber_textBox2.Text + DateTime.Now.ToString("HHmmssff") + rnd.Next(50).ToString("00");
            parent.admin.RaceTH.AMB20_SaveData(someAmbString);

            /*http://www.hobbytalk.com/bbs1/archive/index.php/t-73738.html
             * 
             * Anyone have what the full translation of the hex that is sent? someone out there must have the translation. This is an exchange of data between the decoder and the PC with the racing app and I believe is AMBrc3

From decoder to desktop
8e:02:33:00:cf:02:00:00:01:00:01:04:b2:9b:01:00:03 :04:27:fc:70:00:04:08:e8:19:e6:bd:8a:75:04:00:05:0 2:33:00:06:02:10:00:08:02:00:00:81:04:fc:05:04:00: 8f

desktop to decoder
an ACK

decoder to desktop
8e:02:1f:00:3d:27:00:00:02:00:01:02:1b:00:07:02:21 :00:0c:01:7a:06:01:00:81:04:fc:05:04:00:8f

desktop to decoder
an ACK

decoder to desktop
8e:02:1f:00:98:e8:00:00:02:00:01:02:1b:00:07:02:22 :00:0c:01:7a:06:01:00:81:04:fc:05:04:00:8f

desktop to decoder
an ACK

Thanks for any help -r

I have stared att these dumps for a while and have done a lot of assumptions. I do appreciate all kind of help with calculating the "checksum" or what it is in the beginning of the message, whithout this the decoder seems to reject all commands.

First: the decoder seems to use big endian (i think it's the correct term), the data fields should be read from right to left.

Let's take a look at your example:

8e:02:33:00:cf:02:00:00:01:00:01:04:b2:9b:01:00:03 :04:27:fc:70:00:04:08:e8:19:e6:bd:8a:75:04:00:05:0 2:33:00:06:02:10:00:08:02:00:00:81:04:fc:05:04:00: 8f

8e: start of message
02: unknown, a minute ago i thought it was 00 for TO the decoder and 01 FROM the decoder
33:00: the length of the message in hex, 0033=51 bytes* read from L to R
cf:02:00:00 the evil checksum I can't figure out 000002cf=719
01: type of message, this is a passing
00: guess: the data length for the passing is 0 bytes
01: sequence number
04: length 4 bytes
b2:9b:01:00 value of sequence number 00019bb2=105394 (seems to have great uptime?)
03: transponder number
04: length 4 bytes
27:fc:70:00 value of transponder number 0070fc27=7404583
04: lap time
08: length 8 bytes
e8:19:e6:bd:8a:75:04:00 lap time in seconds if you divide by one million, 1255138658.753000 hmm... never seen this huge values on my system. Hope it got natural causes...
05: signal strength
02: lengt 2 bytes
33:00: value of signal strength 0x0033=51
06: hits
02: length 2 bytes
10:00: 0010=16 hits
08: unknown
02: length of this unknown
00:00: value of unknown: zero (doesn't make it easier...)
81: ID of the decoder (the last four bytes of the mac address, guess the first two is the IANA assigned number for AMB.it)
04: length of ID 4 byted
fc:05:04:00: xx:xx:00:04:05:fc
8f: end of message

*If the value is in the range 8A to 8F (could be the range 80-8F i guess) the value is prefixed/escaped by 8D followed by the byte value plus 0x20. The prefix/escape is NOT counted in the length of the message.
Example: signal strength whith the value of 142 0x8E: 05:02:8D:AE:00
Action: subtract AE whith 0x20 => 8E and delete the byte containing 8D.
Result: 05:02:8e:00 

Now: please help me find out how to calculate the "checksum" or what it is.
If someone do I will post all the keys I've found.

             */

        }

        // отправить сигнал от всех датчиков
        private void sendDataFromAllSensors_button_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= pilotsInRace_dataGridView1.RowCount; i++)
            {
                Random rnd = new Random();
                string someAmbString = "@" + i.ToString("00") + DateTime.Now.AddSeconds(rnd.Next(i)).ToString("HHmmssff") + rnd.Next(50).ToString("00");

              //  string someAmbString = "@" + i.ToString("00") + "0000000001";
             //   MainForm.log("generated someAmbString: " + someAmbString);

                parent.admin.RaceTH.AMB20_SaveData(someAmbString);

            }
        }






    }

    public class RowComparer : System.Collections.IComparer
    {
        private static int sortOrderModifier = 1;

        public RowComparer(SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;
            int CompareResult = -1;
            if (DataGridViewRow1.Cells[4].Value.ToString() != "")
            {
                int Num1 = Convert.ToInt32(DataGridViewRow1.Cells[4].Value.ToString());
                int Num2 = Convert.ToInt32(DataGridViewRow2.Cells[4].Value.ToString());


                if (Num1 == Num2) CompareResult = 0;
                else
                    if (Num1 < Num2) CompareResult = -1; else CompareResult = 1;

                if (CompareResult == 0)
                {

                    Double D1 = Convert.ToDouble(DataGridViewRow1.Cells[8].Value.ToString());
                    Double D2 = Convert.ToDouble(DataGridViewRow2.Cells[8].Value.ToString());

                    if (D1 == D2) CompareResult = 0;
                    else
                        if (D1 < D2) CompareResult = 1; else CompareResult = -1;
                }
            }
            return CompareResult * sortOrderModifier;
        }
    }
    public class RowComparer2 : System.Collections.IComparer
    {
        private static int sortOrderModifier = 1;
        private int MaxColumns = 0;
        public RowComparer2(SortOrder sortOrder, int MaxCells)
        {
            MaxColumns = MaxCells - 2;
            if (sortOrder == SortOrder.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;
            int CompareResult = 0;
            int Columns = MaxColumns;
            if (MaxColumns > 4)
            {
            f2:
                string S1 = DataGridViewRow1.Cells[Columns].Value == null ? String.Empty : DataGridViewRow1.Cells[Columns].Value.ToString().Replace(".", ",");
            string S2 = DataGridViewRow2.Cells[Columns].Value == null ? String.Empty : DataGridViewRow2.Cells[Columns].Value.ToString().Replace(".", ",");

                if (S1.Length == 0 && S2.Length == 0)
                {
                    Columns--;
                    if (Columns > 4)
                        goto f2;
                }
                Double D1 = S1.Length == 0 ? Double.MaxValue : Convert.ToDouble(S1);
                Double D2 = S2.Length == 0 ? Double.MaxValue : Convert.ToDouble(S2);

                if (D1 == D2) CompareResult = 0; else if (D1 < D2) CompareResult = 1; else CompareResult = -1;
            }
            return CompareResult * sortOrderModifier;
        }
    }
    public class RowComparer3 : System.Collections.IComparer
    {
        private static int sortOrderModifier = 1;
        private int Cell;
        public RowComparer3(SortOrder sortOrder, int C)
        {
            Cell = C;
            if (sortOrder == SortOrder.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;
            int CompareResult = 0;


            Double D1 = DataGridViewRow1.Cells[Cell].Value == null ? Double.MaxValue : Convert.ToDouble(DataGridViewRow1.Cells[Cell].Value.ToString().Replace('.', ','));
            Double D2 = DataGridViewRow2.Cells[Cell].Value == null ? Double.MaxValue : Convert.ToDouble(DataGridViewRow2.Cells[Cell].Value.ToString().Replace('.', ','));

            if (D1 == D2) CompareResult = 0;
            else
                if (D1 < D2) CompareResult = 1; else CompareResult = -1;


            return CompareResult * sortOrderModifier;
        }
    }
}
