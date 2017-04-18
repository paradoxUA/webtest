using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;
using System.Drawing.Printing;
using Rentix.Controls;
using System.Configuration;

namespace Rentix
{
    public partial class Settings : Form
    {
        MainForm parent;
        private SerialPort RS232;
        bool InSave = false; //Флаг сохранялись ли настройки

        public Settings(MainForm P)
        {
            InitializeComponent();
            parent = P;


            if (parent.admin.InError && parent.admin.TypeError == 1)
            {
                tabPage1.Parent = tabControl2;
                tabPage3.Parent = tabControl2;
                tabPage4.Parent = tabControl2;
            }

            string[] RS232 = System.IO.Ports.SerialPort.GetPortNames();

            for (int i = 0; i < RS232.Length; i++)
            {
                comboBox1.Items.Add(RS232[i]);
            }

            if (!parent.admin.InError)
            {

                List<Hashtable> Tracks = parent.admin.model.GetAllTracks();
                for (int i = 0; i < Tracks.Count; i++)
                    comboBox2.Items.Add(Tracks[i]["name"]);
            }
            try
            {
                PrinterSettings.StringCollection sc = PrinterSettings.InstalledPrinters;
                for (int i = 0; i < sc.Count; i++)
                {
                    comboBox4.Items.Add(sc[i]);
                    comboBox5.Items.Add(sc[i]);
                }


            }
            catch (Exception exp)
            {
                
            }
            LoadSettings();

        }
        
        // покажем настройки на форме
        private void LoadSettings()
        {
            fillRaceModes();

            // настройки транспондеров
            string path = "transetts.xml";
            if (File.Exists(path))
            {
                DataSet ds = new DataSet();

                ds.ReadXml(path);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataGridView1.Rows.Add(row.ItemArray);
                    if (row.ItemArray[0].ToString().Trim().Length > 0)
                    {
                        comboBox3.Items.Add(row.ItemArray[0]);
                    }
                }

            }

            loadDbList();

            Hashtable sett = parent.admin.model.LoadSettings();

            // Закладка Общие настройки
            numericUpDown4.Value = Convert.ToInt32(sett["time_start"]);
            numericUpDown5.Value = Convert.ToInt32(sett["time_end"]);
            checkBox1.Checked = Convert.ToBoolean(sett["time_wrap"]);
            numericUpDown1.Value = Convert.ToInt32(sett["wrap_pos"]);
            checkBox8.Checked = Convert.ToBoolean(sett["show_events"]);
            numericUpDown1.Enabled = checkBox1.Checked;
            checkBox6.Checked = Convert.ToBoolean(sett["enter_password"]);
            numericUpDown7.Value = Convert.ToInt32(sett["sertificat_day"]);
            checkBox9.Checked = Convert.ToBoolean(sett["card_user"]);
            checkBox10.Checked = Convert.ToBoolean(sett["beep_system"] ?? false);
            checkBox11.Checked = Convert.ToBoolean(sett["beep_windows"] ?? false);
            showUniqueBestResults_checkBox.Checked = Convert.ToBoolean(sett["uniquebestres"] ?? false);


            // Закладка оборудование
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(sett["rs232_port"]);
            comboBox3.SelectedIndex = comboBox3.Items.IndexOf(sett["decoder"]);
            comboBox4.SelectedIndex = comboBox4.Items.IndexOf(sett["printer_result"]);
            comboBox5.SelectedIndex = comboBox5.Items.IndexOf(sett["printer_check"]);
            checkBox2.Checked = Convert.ToBoolean(sett["print_check"]);

            // Закладка Гонка
            checkBox3.Checked = Convert.ToBoolean(sett["stop_on_pause"]);
            checkBox4.Checked = Convert.ToBoolean(sett["start_after_detection"]);
            checkBox5.Checked = Convert.ToBoolean(sett["print_result"]);
            checkBox7.Checked = Convert.ToBoolean(sett["show_zero_lap"]);
            minimumLapTime_numericUpDown2.Value = Convert.ToInt32(sett["noise_time"]);
            numericUpDown3.Value = Convert.ToInt32(sett["track_length"]);
            //numericUpDown6.Value = Convert.ToInt32(sett["race_time"]);

            // время гонки по умолчанию
            // заполнить список с временами гонки

            if (sett["default_race_mode_id"] != null)
            {
                comboBoxItem ci = new comboBoxItem("", -1);
                ci.selectComboBoxValueById(defaultRaceMode_comboBox6, Convert.ToInt32(sett["default_race_mode_id"]));
            }
                // defaultRaceMode_comboBox6.SelectedValue = sett["default_race_mode_id"]; 


            try
            {
                textBox1.Text = sett["fuel_on_lap"].ToString();
            }
            catch (Exception ex)
            {

            }

            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(parent.admin.model.GetTrackName(sett["default_track"].ToString()));
                      

             if(Convert.ToBoolean(sett["warm_subtract"]) == true)
            {
            checkBox14.Checked =  true;
            }
            else
            {
                checkBox14.Checked = false;
            }

            numericUpDown8.Value = Convert.ToInt32(sett["warm_time"]??0);

            // connection string
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            connectionString_textBox9.Text = config.AppSettings.Settings["crazykartConnectionString"].Value;

            //Закладка временных скидок
            isTimeDiscountsAvailable_checkBox13.Checked = Convert.ToBoolean(sett["racesale"] ?? false);
            numericUpDown9.Value = Convert.ToInt32(sett["sale_onelap"] ?? 0);
            numericUpDown10.Value = Convert.ToInt32(sett["sale_half"] ?? 0);

            isHalfModesEnabled_checkBox.Checked = Convert.ToBoolean(sett["halfModesEnabled"] ?? false);
            
            // Анонсер
            if (sett["images_for_anonser"] != null)
            {
                anonserImagesFolder_textBox9.Text = sett["images_for_anonser"].ToString();
               
            }
            else
            {
                anonserImagesFolder_textBox9.Text = "";
            }

             if (sett["sponsors_images_for_anonser"] != null)
            {
               sponsorsImagesFolder_textBox9.Text = sett["sponsors_images_for_anonser"].ToString();
               
            }
            else
            {
                sponsorsImagesFolder_textBox9.Text = "";
            }

            nextRacePilotsTimer_numericUpDown11.Value =
                Convert.ToInt32(sett["anonser_nextRacePilotsTimer"]); // Показывает пилотов следующего рейса
            
            raceLoadTimer_numericUpDown12.Value = Convert.ToInt32(sett["anonser_raceLoadTimer"]); // Показывает загрузку рейса
            bestResultTimer_numericUpDown13.Value = Convert.ToInt32(sett["anonser_bestResultTimer"]);  // Показывает лучший результат
            currentResultTimer_numericUpDown14.Value = Convert.ToInt32(sett["anonser_currentResultTimer"]); // время по кругам
            anonserImageRotator_numericUpDown2.Value = Convert.ToInt32(sett["anonser_imageRotateTimer"]);  // смена картинки, когда нет заезда         

            sponsorsImagesRotator_numericUpDown2.Value = Convert.ToInt32(sett["sponsors_images_rotate_timer"]);            
            sponsorsImagesFolder_textBox9.Text = Convert.ToString(sett["sponsors_images_for_anonser"]);




            List<string> SettFromFile = parent.admin.LoadSettings();
            if (SettFromFile.Count > 0)
            {
                string[] MySQLData = parent.admin.ParseMySQLConfig(SettFromFile[0]);

                textBox2.Text = MySQLData[0];
                textBox3.Text = MySQLData[1];
                textBox4.Text = MySQLData[2];
                textBox5.Text = MySQLData[3];
                textBox6.Text = MySQLData[4];
            }
            
        }
      
       
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (InSave) this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }


        
        // Сохранение настроек с формы в БД
        private void button2_Click(object sender, EventArgs e)
        {
            Hashtable sett = new Hashtable();
            InSave = true;
            // Закладка Общие настройки
            sett["time_start"] = numericUpDown4.Value;
            sett["time_end"] = numericUpDown5.Value;
            sett["time_wrap"] = checkBox1.Checked;
            sett["wrap_pos"] = numericUpDown1.Value;
            sett["show_events"] = checkBox8.Checked;
            sett["enter_password"] = checkBox6.Checked;
            sett["sertificat_day"] = numericUpDown7.Value;
            sett["card_user"] = checkBox9.Checked;
            sett["beep_system"] = checkBox11.Checked;
            sett["beep_windows"] = checkBox10.Checked;
            sett["uniquebestres"] = showUniqueBestResults_checkBox.Checked;

            // Закладка оборудование
            if (comboBox1.SelectedIndex == -1)
                sett["rs232_port"] = comboBox1.Items.Count == 0 ? "" : parent.admin.Settings["rs232_port"];
            else
                sett["rs232_port"] = comboBox1.Items[comboBox1.SelectedIndex];
            sett["decoder"] = comboBox3.Items[comboBox3.SelectedIndex];

            sett["printer_check"] = comboBox5.SelectedIndex >= 0 ? comboBox5.Items[comboBox5.SelectedIndex] : "";
            sett["printer_result"] = comboBox4.SelectedIndex >= 0 ? comboBox4.Items[comboBox4.SelectedIndex] : "";
            sett["print_check"] = checkBox2.Checked;


            // Закладка Гонка
            sett["stop_on_pause"] = checkBox3.Checked;
            sett["start_after_detection"] = checkBox4.Checked;
            sett["print_result"] = checkBox5.Checked;
            sett["show_zero_lap"] = checkBox7.Checked;
            sett["noise_time"] = minimumLapTime_numericUpDown2.Value;
            sett["track_length"] = numericUpDown3.Value;
            sett["fuel_on_lap"] = textBox1.Text;
            sett["default_track"] = comboBox2.SelectedIndex >= 0 ? parent.admin.model.GetTrackID(comboBox2.Items[comboBox2.SelectedIndex].ToString()) : 0;
          //  sett["race_time"] = numericUpDown6.Value;

            // время гонки по умолчанию
            comboBoxItem ci = new comboBoxItem("", -1);
            sett["default_race_mode_id"] = ci.getSelectedValue(defaultRaceMode_comboBox6); // defaultRaceMode_comboBox6.SelectedValue;

            sett["warm_subtract"] = checkBox14.Checked;
            sett["warm_time"] = numericUpDown8.Value;


            // connection string
           // ConfigurationManager.AppSettings.Add("crazykartConnectionString", connectionString_textBox9.Text);//.ConnectionStrings["crazykartConnectionString"].ConnectionString = connectionString_textBox9.Text;
           // Configuration config =
           //     ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
          //  config.AppSettings.Settings.Remove("crazykartConnectionString");
           // config.AppSettings.Settings.Add("crazykartConnectionString", connectionString_textBox9.Text);
          //  ConfigurationManager.ConnectionStrings.Clear();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
           // Properties.Settings.crazykartConnectionString = connectionString_textBox9.Text;
            config.AppSettings.Settings.Remove("crazykartConnectionString");
            config.AppSettings.Settings.Add("crazykartConnectionString", connectionString_textBox9.Text);
            //Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings"); 

            
            // Save the configuration file.
          //  config.Save(ConfigurationSaveMode.Modified, true);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
            // Settings.Default.crazykartConnectionString = MyNewValue;
          //  Settings.Default.Save();

            //Закладка временных скидок
            sett["racesale"] = isTimeDiscountsAvailable_checkBox13.Checked;
            sett["halfModesEnabled"] = isHalfModesEnabled_checkBox.Checked;
            sett["sale_onelap"] = numericUpDown9.Value;
            sett["sale_half"] = numericUpDown10.Value;

            // анонсер
            sett["anonser_nextRacePilotsTimer"] = nextRacePilotsTimer_numericUpDown11.Value; // Показывает пилотов следующего рейса
            sett["anonser_raceLoadTimer"] = raceLoadTimer_numericUpDown12.Value; // Показывает загрузку рейса
            sett["anonser_bestResultTimer"] = bestResultTimer_numericUpDown13.Value;  // Показывает лучший результат
            sett["anonser_currentResultTimer"] = currentResultTimer_numericUpDown14.Value; // время по кругам
            sett["anonser_imageRotateTimer"] = anonserImageRotator_numericUpDown2.Value;  // смена картинки, когда нет заезда         
            sett["images_for_anonser"] = anonserImagesFolder_textBox9.Text;
            sett["sponsors_images_for_anonser"] = sponsorsImagesFolder_textBox9.Text;
            sett["sponsors_images_rotate_timer"] = sponsorsImagesRotator_numericUpDown2.Text;



            parent.admin.model.SaveSettings(sett);

        }

        private void Settings_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }
              

        // заполним список режимов заезда
        private void fillRaceModes()
        {
            defaultRaceMode_comboBox6.Items.Clear();

             List<Hashtable> data = parent.admin.model.GetAllRaceModes(" and is_deleted <> 1 ");

             for (int i = 0; i < data.Count; i++)
             {
                 comboBoxItem someItem = 
                     new comboBoxItem(Convert.ToString(data[i]["name"]), 
                     Convert.ToInt32(data[i]["id"]));

                 defaultRaceMode_comboBox6.Items.Add(someItem);
             }            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {


                RS232 = new SerialPort(comboBox1.Items[comboBox1.SelectedIndex].ToString(), 9600, Parity.None, 8, StopBits.One);
                RS232.RtsEnable = true;
                RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_DataReceived);
                RS232.Open();
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = true;

            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            RS232.Close();
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = false;
        }

        List<byte> bBuffer = new List<byte>();
        string sBuffer = String.Empty;

        void RS232_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s = RS232.ReadLine();
            sensorsLog_richTextBox1.Invoke((MethodInvoker)delegate { sensorsLog_richTextBox1.SelectedText = s; sensorsLog_richTextBox1.ScrollToCaret(); }, s);

        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (RS232 != null && RS232.IsOpen) RS232.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBox1.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string CodeString = textBox2.Text + "/" + textBox3.Text + "/" + textBox4.Text + "/" + textBox5.Text + "/" + textBox6.Text;

            string Code = parent.admin.Encrypt(CodeString);

            List<string> Sett = new List<string>();

            Sett.Add(Code);

            parent.admin.SaveSettings(Sett);
        }

        private void labelSmooth12_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parent.admin.model.ExecuteQuery(textBox7.Text);
            string password = parent.admin.Encrypt("1111");
            parent.admin.model.AddProgramUser("test", password, "2", "test", "test", "");


        }

        private void button5_Click(object sender, EventArgs e)
        {
            parent.admin.model.ExecuteQuery(textBox8.Text);

        }

        private void isTimeDiscountsAvailable_checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (isTimeDiscountsAvailable_checkBox13.Checked)
            {
                isHalfModesEnabled_checkBox.Checked = false;
            }
        }

        private void isHalfModesEnabled_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isHalfModesEnabled_checkBox.Checked)
            {
                isTimeDiscountsAvailable_checkBox13.Checked = false;
            }
        }

        // выбрать путь к картинкам для анонсера
        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            DialogResult dr = fd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                anonserImagesFolder_textBox9.Text = fd.SelectedPath;
            }
        }

        private void setSponsorsImagesFolder_button_Click(object sender, EventArgs e)
        {
             FolderBrowserDialog fd = new FolderBrowserDialog();
            DialogResult dr = fd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                sponsorsImagesFolder_textBox9.Text = fd.SelectedPath;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            createDbBackup();
        }

        private void createDbBackup()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string connStr = config.AppSettings.Settings["crazykartConnectionString"].Value;
            SqlConnection myConnection = new SqlConnection(connStr);
            string dbname = myConnection.Database;
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"Бэкап базы данных (*.bak)|*.bak|Все файлы (*.*)|*.*";
            saveFileDialog.FileName = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" +
                                      DateTime.Now.Day.ToString()+ "_" + dbname;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                SqlCommand com =
                    new SqlCommand(
                        String.Format(
                            @"BACKUP DATABASE {0} TO DISK = '{1}' WITH INIT , NOUNLOAD ,  NOSKIP , STATS = 10, NOFORMAT",
                            dbname, saveFileDialog.FileName ), myConnection);

                SqlDataReader myreader = com.ExecuteReader();
                Thread.Sleep(2000);
                MessageBox.Show(@"Резервная копия базы успешно сохранена!");
            }
        }

        private void loadDbList()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string connStr = config.AppSettings.Settings["crazykartConnectionString"].Value;
            string[] connstStrings = connStr.Trim().Split(';');
            string connNew = "";
            if (connstStrings.Length == 4)
            {
                connNew = String.Format("{0};{1};", connstStrings[0], connstStrings[2]);
            }
            SqlConnection myConnection = new SqlConnection(connNew);
            string dbname = myConnection.Database;
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            var dblist = myConnection.GetSchema("Databases");
            string[] sysdbs = new[] {"master", "model", "msdb", "tempdb"};
            foreach (DataRow database in dblist.Rows)
            {
                String databaseName = database.Field<String>("database_name");
                if (!sysdbs.Contains(databaseName))
                {
                    comboBox6.Items.Add(databaseName);
                    comboBox7.Items.Add(databaseName);
                }
            }

        }


        private void button8_Click(object sender, EventArgs e)
        {
            // сохранение настроек транспондеров в файл xml
            string path = "transetts.xml";

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            //Adding columns to datatable

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {

                dt.Columns.Add(col.Name);

            }

            //adding new rows

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                DataRow row1 = dt.NewRow();

                for (int i = 0; i < dataGridView1.ColumnCount; i++)


                    row1[i] = (row.Cells[i].Value ?? DBNull.Value);

                dt.Rows.Add(row1);

            }

            ds.Tables.Add(dt);

            ds.WriteXml(path);

            label29.Visible = true;
            Thread.Sleep(2000);
            label29.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            restoreDb();
        }

        public string restoreDbName = "";
        public string restoreDbFile = "";
        private void restoreDb()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string connStr = config.AppSettings.Settings["crazykartConnectionString"].Value;
            SqlConnection myConnection = new SqlConnection(connStr);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            try
            {
                string sqlStmt2 = string.Format("ALTER DATABASE [" + restoreDbName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, myConnection);
                bu2.ExecuteNonQuery();

                string sqlStmt3 = "USE MASTER; RESTORE DATABASE [" + restoreDbName + "] FROM DISK='" + restoreDbFile + "'WITH REPLACE;";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, myConnection);
                bu3.ExecuteNonQuery();

                string sqlStmt4 = string.Format("ALTER DATABASE [" + restoreDbName + "] SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, myConnection);
                bu4.ExecuteNonQuery();

                MessageBox.Show(@"Восстановление прошло успешно!");
                myConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                restoreDbFile = dlg.FileName;
                restoreButton.Enabled = true;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox) sender;
            restoreDbName = box.SelectedItem.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DateTime DtFrom = dateTimePicker1.Value;
            DateTime DtTo = dateTimePicker2.Value;
            startClearDb(DtFrom, DtTo);
        }

        private void startClearDb(DateTime DtFrom, DateTime DtTo)
        {

           this.Cursor = Cursors.WaitCursor;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string connStr = config.AppSettings.Settings["crazykartConnectionString"].Value;
            SqlConnection myConnection = new SqlConnection(connStr);
            SqlConnection delConnection = new SqlConnection(connStr);
            Console.WriteLine(DtFrom.ToString());
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            try
            {
                string sqlStmt2 =
                    string.Format(
                        "SELECT id FROM races WHERE convert(date,created) >= '{0}' and convert(date,created) <= '{1}'",
                       DtFrom.Year + "." + DtFrom.Month + "." + DtFrom.Day + " " + DtFrom.TimeOfDay, DtTo.Year + "." + DtTo.Month + "." + DtTo.Day + " " + DtTo.TimeOfDay);
                SqlCommand bu2 = new SqlCommand(sqlStmt2, myConnection);
                SqlDataReader readerselect = bu2.ExecuteReader();
                while (readerselect.Read())
                {
                    var id_race = readerselect.GetValue(0);
                    delConnection.Open();
                    string cassaDelString = string.Format("delete from cassa where doc_id in(select id from jurnal where race_id = {0})", id_race);
                    SqlCommand cassaCommand = new SqlCommand(cassaDelString, delConnection);
                    cassaCommand.ExecuteNonQuery();

                    string user_cashDelString = string.Format("delete from user_cash where doc_id in(select id from jurnal where race_id = {0})", id_race);
                    SqlCommand user_cashCommand = new SqlCommand(user_cashDelString, delConnection);
                    user_cashCommand.ExecuteNonQuery();

                    string jurnalDelString = string.Format("delete from jurnal where race_id = {0}", id_race);
                    SqlCommand jurnalCommand = new SqlCommand(jurnalDelString, delConnection);
                    jurnalCommand.ExecuteNonQuery();

                    string race_timesDelString = string.Format("delete from race_times where member_id in (select id from race_data where race_id = {0})", id_race);
                    SqlCommand race_timesCommand = new SqlCommand(race_timesDelString, delConnection);
                    race_timesCommand.ExecuteNonQuery();

                    string race_dataDelString = string.Format("delete from race_data where race_id = {0}", id_race);
                    SqlCommand race_dataCommand = new SqlCommand(race_dataDelString, delConnection);
                    race_dataCommand.ExecuteNonQuery();

                    string noracekartDelString = string.Format("delete from noracekart where race_id = {0}", id_race);
                    SqlCommand noracekartCommand = new SqlCommand(noracekartDelString, delConnection);
                    noracekartCommand.ExecuteNonQuery();

                    string raceDelString = string.Format("delete from races where id = {0}", id_race);
                    SqlCommand raceCommand = new SqlCommand(raceDelString, delConnection);
                    raceCommand.ExecuteNonQuery();
                    delConnection.Close();
                }
                this.Cursor = Cursors.Default;
                MessageBox.Show(@"Очистка прошла успешно!");
                myConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == @"159753")
            {
                panel2.Visible = true;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var t = textBox10.Text;
            //MultiServer serv = new MultiServer();
            Echo ec = new Echo();
            MultiServer.SocketServer.WebSocketServices.Broadcast(t);
        }
    }
}
