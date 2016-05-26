using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Rentix.model;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Rentix
{
    class testClass1
    {
        private static crazykartContainer edb;
        public static string connectionString = "";
        private static SqlConnection db, db1, conn;
        private static SerialPort RS232;

        private static Dictionary<int, string> dict = new Dictionary<int, string>();

        private static int flagComRead = 0;
        private static Timer Timer1;

        private static object[] decoderSetts = null;


        public  void RS232_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;

            db = new SqlConnection(connectionString);
            db.Open();

            if (RS232.IsOpen)
            {

                SerialPort spL = (SerialPort)sender;

                //spL.ReadChar();
                string s = spL.ReadLine();
                // @\t40\t551\t313930\t5001.962\t12\t119\t02\txA03E\r
                //s = s.Replace("\r\n", " ").Trim();
                s = convertAsciiTextToHex(s);
                string[] hexBits = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] hexValuesSplit = new string[hexBits.Length];
                try
                {
                    for (int i = 0; i < hexBits.Length; i++)
                    {
                        hexValuesSplit[i] = int.Parse(hexBits[i], System.Globalization.NumberStyles.HexNumber).ToString();
                        //Console.WriteLine(i + ". " + hexBits[i] + " = " + decBits[i]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"String can not be parsed." + ex.Message);
                }

                // 2 - hours 1
            // 3 - minutes 1
            // 4 - seconds 1
            // 5 - transponder
            // 6 - mseconds
            // 7 - hits
                int hourByte = Convert.ToInt32(decoderSetts[2].ToString().Trim());
                int minuteByte = Convert.ToInt32(decoderSetts[3].ToString().Trim());
                int secondByte = Convert.ToInt32(decoderSetts[4].ToString().Trim());

                //int[] transBytes = new int[decoderSetts[5].ToString().Trim().Split(',').Length];

                int index=0;
                string trans = "";
                foreach (string transByte in decoderSetts[5].ToString().Trim().Split(','))
                {
                   trans += hexValuesSplit[Convert.ToInt32(transByte)];
                }
                string msec = "";
                foreach (string transByte in decoderSetts[6].ToString().Trim().Split(','))
                {
                   msec += hexValuesSplit[Convert.ToInt32(transByte)];
                }
                string hit = "";
                foreach (string transByte in decoderSetts[7].ToString().Trim().Split(','))
                {
                   hit += hexValuesSplit[Convert.ToInt32(transByte)];
                }
                

              //  String.Concat(hexValuesSplit[13],hexValuesSplit[14],hexValuesSplit[15],hexValuesSplit[16]);
                string hour = hexValuesSplit[hourByte];
                string min = hexValuesSplit[minuteByte];
                string sec = hexValuesSplit[secondByte];
                //string msec = String.Concat(hexValuesSplit[17],hexValuesSplit[18],hexValuesSplit[19],hexValuesSplit[20]);
                //string hit = String.Concat(hexValuesSplit[21],hexValuesSplit[22]);

                var Transponder = Convert.ToInt16(trans, 16);
                var Hour = Convert.ToInt16(hour, 16);
                var Minutes = Convert.ToInt16(min, 16);
                var Seconds = Convert.ToInt16(sec, 16);
                var Millisecond = Convert.ToInt16(msec, 16);

                var Hit = Convert.ToInt16(hit, 16);
               // var receivedString = s;




                Console.WriteLine(Transponder.ToString());
                Console.WriteLine(Hour.ToString());
                Console.WriteLine(Minutes.ToString());
                Console.WriteLine(Seconds.ToString());
                Console.WriteLine(Millisecond.ToString());
                Console.WriteLine(Hit.ToString());

                //var hexchars = "";
                //var i = 0;
                //while (i != bytesToRec.Length)
                //{
                //    hexchars += (bytesToRec[i]).ToString("X") + " ";
                //    i++;
                //}
                //var existing = spL.ReadExisting();

                //existing = convertAsciiTextToHex(existing);
                //RS232.DiscardInBuffer();
                //RS232.DiscardOutBuffer();
                //string filepath = @"KMTU_" + DateTime.Now.ToString("MMM-d-yyyy") + ".hex";
                //FileStream LogFile = new FileStream(filepath, FileMode.Create);

                //LogFile.Write(bytesToRec, 0, bytesToRec.Length);
                //LogFile.Close();

           //     flagComRead = 1;
           //     if (Timer1 == null)
           //     {
           //         startReadComDataFormDb();
           //     }
           //     else
           //     {
           //         Timer1.Change(1000, 1000);
           //     }
                
           //     //{
           //     //    Task.Factory.StartNew(startReadComDataFormDb);
           //     //}
                
           //   //  StreamWriter writetext = new StreamWriter("write.txt");
           // string s = RS232.ReadLine();
           // testClass1.WriteLog(" RS232_DataReceived ", "поток=" + Thread.CurrentThread.ManagedThreadId + " s =" + s);
           //// s = convertAsciiTextToHex(s); //вызов функция конвертации

           // if (s.Length >= 31 * 2 + 30)
           // {
           //     var res = AMB20_Decode(s);
           //     Console.WriteLine(res.receivedString);
           //     using (SqlCommand cmd = new SqlCommand("insert into comlogs (comdata, created) values ('" + res.receivedString + "',GETDATE())", db))
           //     {
           //         cmd.ExecuteNonQuery();
           //         testClass1.WriteLog(" RS232_DataReceived ", "поток=" + Thread.CurrentThread.ManagedThreadId + " cmd =" + cmd.CommandText);
           //     }

           //     //    writetext.WriteLine(res.receivedString);
           // }
           //     //string code = RS232.ReadExisting();
           //     //Task.Factory.StartNew(new Action(startParser));
           //     db.Close();
           //   //  Console.WriteLine(code);
            }
        }

        internal static void startReadComDataFormDb()
        {
            int num = 1;
            TimerCallback tm = new TimerCallback(readComDataFormDb);
            Timer1 = new Timer(tm, num, 0, 2000);
            testClass1.WriteLog(" startReadComDataFormDb ", "поток=" + Thread.CurrentThread.ManagedThreadId + " START ");
        }

        private static void readComDataFormDb(object sender)
        {
            testClass1.WriteLog(" readComDataFormDb ", "поток=" + Thread.CurrentThread.ManagedThreadId + " flagComRead = " + flagComRead);

            if (flagComRead == 1)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;

                db1 = new SqlConnection(connectionString);
                db1.Open();

                conn = new SqlConnection(connectionString);
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("select * from comlogs where operated = 0", db1))
                {
                    testClass1.WriteLog(" readComDataFormDb ",
                        "поток=" + Thread.CurrentThread.ManagedThreadId + " cmd = " + cmd.CommandText);
                    //cmd.ExecuteScalar();
                    var res = cmd.ExecuteScalar();
                    if(res==null) { flagComRead = 0; }
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        testClass1.WriteLog(" readComDataFormDb ",
                            "поток=" + Thread.CurrentThread.ManagedThreadId + " line = " + reader[1].ToString());
                        //RaceThread raceThread = new RaceThread(new AdminControl(2016, 3, 1));
                        //raceThread.AMB20_SaveData(reader[2].ToString());
                        using (
                            SqlCommand cmd1 =
                                new SqlCommand("update comlogs set operated = 1 where id = " + reader[0].ToString(),
                                    conn))
                        {
                            testClass1.WriteLog(" readComDataFormDb ",
                                "поток=" + Thread.CurrentThread.ManagedThreadId + " cmd = " + cmd.CommandText);

                            cmd1.ExecuteNonQuery();

                            testClass1.WriteLog(" readComDataFormDb ",
                                "поток=" + Thread.CurrentThread.ManagedThreadId + " cmd =" + cmd1.CommandText);
                        }

                    }
                    reader.Close();
                }


            }
            else
            {
                Timer1.Change(60*1000, 60*1000*5);
            }
        }

        private static object sync = new object();
        public static void WriteLog(string method, string text)
        {
            try
            {
                // Путь .\\Log
                string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog); // Создаем директорию, если нужно
                string filename = Path.Combine(pathToLog, string.Format("{0}_{1:dd.MM.yyy}.log",
                AppDomain.CurrentDomain.FriendlyName, DateTime.Now));
                string fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] [{1}] {2}\r\n",
                DateTime.Now, method, text);
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
                // Перехватываем все и ничего не делаем
            }
        }
        private String convertAsciiTextToHex(String i_asciiText)
        {
            StringBuilder sBuffer = new StringBuilder();
            for (int i = 0; i < i_asciiText.Length; i++)
            {
                sBuffer.Append(Convert.ToInt32(i_asciiText[i]).ToString("x2") + " ");
            }
            return sBuffer.ToString().ToUpper();
        }
        public AMB20RX AMB20_Decode(string s)
        {
            AMB20RX Ret;
            //if (s[0] == '@')
            if (s.Length >= 31 * 2 + 30)
            {
                //s = s.Replace("\r\n", " ").Trim();
                //Ret.Transponder = String.Concat(s[1], s[2]);
                //Ret.Hour = Convert.ToInt16(String.Concat(s[3],s[4]));
                //Ret.Minutes = Convert.ToInt16(String.Concat(s[5],s[6]));
                //Ret.Seconds = Convert.ToInt16(String.Concat(s[7],s[8]));
                //if (s.Length > 9)
                //{
                //    Ret.Millisecond = Convert.ToInt16(String.Concat(s[9],s[10]));
                //    Ret.Millisecond = Ret.Millisecond * 10; // потому, что прибор присылает не 3 разряда, а только 2. а должно быть три
                //    Ret.Hit = Convert.ToInt16(String.Concat(s[11],s[12]));
                //}
                //else
                //{
                //    Ret.Hit = Ret.Millisecond = 0;
                //}
                //Ret.receivedString = s;
                s = s.Replace("\r\n", " ").Trim();
                string[] hexBits = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] decBits = new string[hexBits.Length];
                try
                {
                    for (int i = 0; i < hexBits.Length; i++)
                    {
                        decBits[i] = int.Parse(hexBits[i], System.Globalization.NumberStyles.HexNumber).ToString();
                        //Console.WriteLine(i + ". " + hexBits[i] + " = " + decBits[i]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("String can not be parsed." + ex.Message);
                }

                Ret.Transponder = String.Concat(decBits[14 - 1], decBits[15 - 1], decBits[16 - 1], decBits[17 - 1]);
                Ret.Hour = Convert.ToInt16(decBits[7 - 1]);
                Ret.Minutes = Convert.ToInt16(decBits[8 - 1]);
                Ret.Seconds = Convert.ToInt16(decBits[9 - 1]);
                Ret.Millisecond = Convert.ToInt16(String.Concat(decBits[18 - 1], decBits[19 - 1], decBits[20 - 1], decBits[21 - 1]));

                Ret.Hit = Convert.ToInt16(String.Concat(decBits[22 - 1], decBits[23 - 1]));
                Ret.receivedString = s;
            }
            else
            {
                Ret.Transponder = String.Empty;
                Ret.Hit = 0;
                Ret.Hour = 0;
                Ret.Minutes = 0;
                Ret.Seconds = 0;
                Ret.Millisecond = 0;
                Ret.receivedString = String.Empty;
            }
            return Ret;

        }
 
        private void startParser()
        {

        }

        public void testClass11()
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
                    //comboBox6.
                }
                short dbID = database.Field<short>("dbid");
                DateTime creationDate = database.Field<DateTime>("create_date");
            }




            ProkardModel form = new ProkardModel();

            Hashtable sett = form.LoadSettings();
            //Console.WriteLine(sett["decoder"]);
            // настройки транспондеров
            string path = "transetts.xml";
            if (File.Exists(path))
            {
                DataSet ds = new DataSet();

                ds.ReadXml(path);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row.ItemArray[0].ToString().Trim() == sett["decoder"].ToString().Trim())
                    {
                        decoderSetts = row.ItemArray;
                    }
                }

            }
            // 0 - name protokol
            // 1 - bytes (32)
            // 2 - hours 1
            // 3 - minutes 1
            // 4 - seconds 1
            // 5 - transponder
            // 6 - mseconds
            // 7 - hits
            // 8 - port speed
            // 9 - bits
            // 10 - StopBits
            int portSpeed = Convert.ToInt32(decoderSetts[8]);
            int bits = Convert.ToInt32(decoderSetts[9]);
           // var stopBits = null;
            //if (Convert.ToInt32(decoderSetts[9]) == 1)
           // {
            //    stopBits = StopBits.One;
          //  }
          //  string stopBits = decoderSetts[10].ToString();

            RS232 = new SerialPort(Convert.ToString("COM4"), portSpeed, Parity.None, bits, StopBits.One);
           // RS232.ReadBufferSize = 32;
          //  RS232.RtsEnable = true;
            RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_DataReceived);
            RS232.Open();

        }

        public void startQueue(string param)
        {

            Thread.CurrentThread.Suspend();

        }

        public void testClass()
        {


           // Thread thread = new Thread();







            int idTrack = 9;
            int amountOfRecords = 20;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;
            db = new SqlConnection(connectionString);
            db.Open();
            db1 = new SqlConnection(connectionString);
            db1.Open();

            conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand SqlCommand = conn.CreateCommand();

            SqlCommand.CommandText = "" +
                        " select race_data.*, users.id as user_id, users.name as username " +
                        " from race_data " +
                        " join races on races.id = race_data.race_id " +
                        " join users on race_data.pilot_id = users.id " +
                        " where races.track_id = @TRACK_ID ";
            SqlCommand.Parameters.AddWithValue("@TRACK_ID", idTrack);
            da.SelectCommand = SqlCommand;
            DataSet ds = new DataSet();

            conn.Open();
            da.Fill(ds);
            conn.Close();

            List<race_times> resObjects11 = new List<race_times>();

            List<int> listUsersInts = new List<int>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                race_data rdData = new race_data();
                rdData.pilot_id = Convert.ToInt32(row[2]);
                // rdData.car_id = Convert.ToInt32(res["car_id"]);
                rdData.id = Convert.ToInt32(row[0]);
                rdData.race_id = Convert.ToInt32(row[1]);
                rdData.created = Convert.ToDateTime(row[4]);

                using (SqlCommand newmCommand
                    = new SqlCommand(" select race_times.*,race_data.*, users.name " +
                     " from race_times " +
                     " join race_data on race_times.member_id = race_data.id " +
                     " left join users on users.id = race_data.pilot_id " +
                     " where race_times.seconds in( " +
                     " select min(race_times.seconds) " +
                     " from race_times " +
                     " join race_data on race_times.member_id = race_data.id " +
                     " where race_data.pilot_id = @USER_ID) " +
                     " and race_data.pilot_id = @USER_ID " +
                     "", db1))
                {
                    newmCommand.Parameters.AddWithValue("@USER_ID", row[2]);
                    if (listUsersInts.Contains(Convert.ToInt32(row[2])))
                    {
                        continue;
                    }
                    else
                    {
                        listUsersInts.Add(Convert.ToInt32(row[2]));
                    }
                    
                    Console.WriteLine(@"@USER_ID===" + row[2]);
                    using (SqlDataReader res1 = newmCommand.ExecuteReader())
                    {
                        while (res1.Read())
                        {
                            race_times rsTimes = new race_times();
                            rsTimes.id = Convert.ToInt32(res1[0]);
                            rsTimes.lap = Convert.ToInt32(res1[1]);
                            rsTimes.seconds = Convert.ToDecimal(res1[2]);
                            rsTimes.created = Convert.ToDateTime(res1[3]);
                            rsTimes.race_data = (race_data)(rdData);
                            resObjects11.Add(rsTimes);
                        }
                    }
                    //  Console.WriteLine(res["id"]);
                }

            }

            foreach (object resObject in resObjects11)
            {
                Console.WriteLine(resObject);
            }





            List<race_times> resObjects = new List<race_times>();
            using (SqlCommand cmd
                    = new SqlCommand("" +
                        " select race_data.*, users.id as user_id, users.name as username " +
                        " from race_data " +
                        " join races on races.id = race_data.race_id " +
                        " join users on race_data.pilot_id = users.id " +
                        " where races.track_id = @TRACK_ID ", db))
            {
                cmd.Parameters.AddWithValue("@TRACK_ID", idTrack);
                Console.WriteLine(@"@TRACK_ID===" + idTrack);
                using (SqlDataReader res = cmd.ExecuteReader())
                {
                    while (res.Read())
                    {
                        race_data rdData = new race_data();
                        rdData.pilot_id = Convert.ToInt32(res[2]);
                       // rdData.car_id = Convert.ToInt32(res["car_id"]);
                        rdData.id = Convert.ToInt32(res[0]);
                        rdData.race_id = Convert.ToInt32(res[1]);
                        rdData.created = Convert.ToDateTime(res[4]);
                        using (SqlCommand newmCommand
                            = new SqlCommand(" select race_times.*,race_data.*, users.name " +
                                             " from race_times " +
                                             " join race_data on race_times.member_id = race_data.id " +
                                             " left join users on users.id = race_data.pilot_id " +
                                             " where race_times.seconds in( " +
                                             " select min(race_times.seconds) " +
                                             " from race_times " +
                                             " join race_data on race_times.member_id = race_data.id " +
                                             " where race_data.pilot_id = @USER_ID) " +
                                             " and race_data.pilot_id = @USER_ID " +
                                             "", db1))
                        {
                            newmCommand.Parameters.AddWithValue("@USER_ID", res[2]);
                            Console.WriteLine(@"@USER_ID===" + res[2]);
                            if (listUsersInts.Contains(Convert.ToInt32(res[2])))
                            {
                                //break;
                            }
                            else
                            {
                                listUsersInts.Add(Convert.ToInt32(res[2]));
                            }
                            
                            using (SqlDataReader res1 = newmCommand.ExecuteReader())
                            {
                                while (res1.Read())
                                {
                                    race_times rsTimes = new race_times();
                                    rsTimes.id = Convert.ToInt32(res1[0]);
                                    rsTimes.lap = Convert.ToInt32(res1[1]);
                                    rsTimes.seconds = Convert.ToDecimal(res1[2]);
                                    rsTimes.created = Convert.ToDateTime(res1[3]);
                                    rsTimes.race_data = (race_data)(rdData);
                                    resObjects.Add(rsTimes);
                                }
                            }
                          //  Console.WriteLine(res["id"]);
                        }
                        //Console.WriteLine(res["id"]);
                        //  string val = res[0].ToString();
                        // ret = val == String.Empty ? 0.0 : Double.Parse(val);
                    }
                }
            }

            foreach (object resObject in resObjects)
            {
                Console.WriteLine(resObject);
            }



            


        }


    }
}
