using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using Rentix.model;
using Timer = System.Threading.Timer;

namespace Rentix
{
    public class RaceThread
    {
        AdminControl admin;

        /*
         * Status = 0 - Гонка не запущена, активна кнопка запуска
         * Status = 1 - Гонка запущена, активна кнопки Пауза, Стоп
         * Status = 2 - Гонка приостановлена, активны кнопки Запуск, Стоп
         */
        public int Status = 0;  // Статус рейса 0= stop, 1 = заезд, 2 - пауза

        private bool EmulatorOn = false;

        public bool InError = false;
        public string ErrorLine = String.Empty;

        public RaceClass Race = null;

        public int RaceID = 0;  // ID заезда
        public string lastReceive = "";
        public long StartTick = 0;  // Время запуска гонки, в тиках
        public long PauseTick = 0;  // Общее время паузы, в тиках
        public long TempPauseTick = 0; // Текущее время паузы
        public long MaxTicksForRace;       // Конечная метка времени, при котором завершится рейс
        private long WarmTicks;     // количество тиков в прогревочном круге
        private long NoiseTime = 0; // Шумовое время - минимальное время круга
        //private long CorrectTime = 0; // Коорекция времени
        public List<RaceMember> Members = new List<RaceMember>();
        private ProkardModel model = new ProkardModel();
        private SerialPort RS232;
        private Hashtable Settings = new Hashtable();
        private bool IsWarmSubtracted = false; //Было ли отсечено время прогревочного круга
        private bool WarmLapShouldBeSubtracted = false; //Нужно ли отсекать время прогревочного круга
        private bool StartAfterDetection = false;

        // Пробуем эмулировать стек LIFO ?? sgavrilenko: в этой версии так и не попробовали...
        // private Queue StackReceive = new Queue();

        // принятая строка от amb20
        public string stringForLogWindow = "";

        public Emulator em = new Emulator();
        public object[] decoderSetts = null;

        public void Nextval()
        {
            if (EmulatorOn)
            {
                if (Status == 1 || Status == 2)
                {
                    string val = em.AMB20_GetNextEmulatedValue();
                    AMB20_SaveData(val);
                }
            }
        }

        public RaceThread(AdminControl admin)
        {
            this.admin = admin;

            /*
            model.Server = MySQLData[0];
            model.Port = MySQLData[1];
            model.Uid = MySQLData[3];
            model.Password = MySQLData[4];
            model.Database = MySQLData[2];
             */
            IsWarmSubtracted = false;
            model.Connect(); // Создаем дополнительное подключение с базой
            Settings = model.LoadSettings();
            // настройки транспондеров
            string path = "transetts.xml";
            if (File.Exists(path))
            {
                DataSet ds = new DataSet();

                ds.ReadXml(path);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row.ItemArray[0].ToString().Trim() == Settings["decoder"].ToString().Trim())
                    {
                        decoderSetts = row.ItemArray;
                    }
                }

            }

        }

        // Добавляет пилотов в рейс
        public void AddPilot(RaceMember Pilot)
        {
            Members.Add(Pilot);
        }

        // Очищает пилотов
        public void ClearPilot()
        {
            Members.Clear();
        }

        // Запуск приема данных
        public void ThreadStart(int raceLengthInMinutes)
        {
            stringForLogWindow = "";
            IsWarmSubtracted = false;
            // Сброс счетчиков
            InError = false;
            ErrorLine = String.Empty;
            StartTick = 0;
            PauseTick = 0;
            TempPauseTick = 0;
            Settings = model.LoadSettings();




            /*
            int RaceMaxTick = Convert.ToInt32(Settings["race_time"]);

            switch (raceModeId)
            {

                case 0: RaceMaxTick = 5; break;

                case 2: RaceMaxTick = 15; break;

            }
             */

            StartAfterDetection = Convert.ToBoolean(Settings["start_after_detection"] ?? false);

            // Старт времени независимо от первого прохождения карта
            if (!StartAfterDetection)
            {
                StartTick = DateTime.Now.Ticks;
            }
            MaxTicksForRace = new TimeSpan(0, raceLengthInMinutes, 0).Ticks;    // Устанавливается время 
            WarmTicks = new TimeSpan(0, 0, Convert.ToInt32(Settings["warm_time"])).Ticks;    // Метка прогревочного круга
            WarmLapShouldBeSubtracted = Convert.ToBoolean(Settings["warm_subtract"] ?? false);

            // Сохранение значения шумового времени
            NoiseTime = new TimeSpan(0, 0, 0, Convert.ToInt32(Settings["noise_time"]), 0).Ticks;
            // NoiseTime += new TimeSpan(0, 0, 0, 0, 0).Ticks;
            // CorrectTime = new TimeSpan(0, 0, 0, 0, 390).Ticks;

            if (EmulatorOn)
            {
                string[] st = new string[Members.Count];
                for (int i = 0; i < Members.Count; i++)
                {
                    if (Members[i].CarTransponder == "")
                    {
                        Members[i].CarTransponder = (i + 1).ToString("00");
                    }

                    st[i] = Members[i].CarTransponder;

                }
                em.SetTransponders(st);
            }

            if (Settings["rs232_port"].ToString().Length > 2)
            {
                int portSpeed = Convert.ToInt32(decoderSetts[8]);
                int bits = Convert.ToInt32(decoderSetts[9]);
                // Открываем порт для чтения
                try
                {
                    RS232 = new SerialPort(Settings["rs232_port"].ToString(), portSpeed, Parity.None, bits, StopBits.One);
                    RS232.WriteBufferSize = 1024 * 1024;
                    RS232.ReadBufferSize = 1024 * 1024 * 2;
                    RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_Receive);
                    RS232.RtsEnable = true;
                    RS232.Open();

                }
                catch (Exception ex)
                {
                    //InError = true;
                    ErrorLine = ex.Message;
                }
            }
            else MessageBox.Show("Не установлен COM-порт, программа запускается без детекции");
        }

        private void FuelControl()
        {
            double TrackLength = model.GetTrackLength(Race.TrackID.ToString());
            double Length, Fuel;
            for (int i = 0; i < Members.Count; i++)
            {
                Length = Members[i].Laps * TrackLength;
                Fuel = (Length / 1000) * Convert.ToDouble(Settings["fuel_on_lap"]);

                model.DelFuel(Members[i].CarID.ToString(), Fuel.ToString().Replace(",", "."), "Кругов - " + Members[i].Laps.ToString());
            }
        }

        public void ThreadStop()
        {
            FuelControl();
            model.AddToJurnal("12", -1, Race.RaceID, "Завершение рейса " + Race.RaceNum.ToString() + " в " + DateTime.Now.ToString("HH:mm:ss"));
            Status = 0;
            model.StopRace(RaceID.ToString());
            Race.Status = 2;
            if (Settings["rs232_port"].ToString().Length > 1)
            {
                RS232.Close();
                RS232.Dispose();
            }
        }

        public void ThreadCancel()
        {

            model.AddToJurnal("12", -1, Race.RaceID, "Отмена рейса " + Race.RaceNum.ToString() + " в " + DateTime.Now.ToString("HH:mm:ss"));
            Status = 0;
            Race.Status = 1;
            if (Settings["rs232_port"].ToString().Length > 1)
            {
                RS232.Close();
                RS232.Dispose();
            }
        }


        // Функция приема данных с RS232 
        void RS232_Receive(object sender, SerialDataReceivedEventArgs e)
        {

            if (Status == 1 || Status == 2)
            {
                AMB20_SaveData(RS232.ReadLine());

            }
        }

        // функция конвертации в хекс
        private String convertAsciiTextToHex(String i_asciiText)
        {
            string hexOutput = "";
            foreach (char letter in i_asciiText)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                hexOutput += value.ToString("x2") + " ";
                //Console.WriteLine("Hexadecimal value of {0} is {1}", letter, hexOutput);
            }
            return hexOutput;
            //StringBuilder sBuffer = new StringBuilder();
            //for (int i = 0; i < i_asciiText.Length; i++)
            //{
            //    sBuffer.Append(Convert.ToInt16(i_asciiText[i]).ToString("x2") + " ");
            //}
            //return sBuffer.ToString().ToUpper();
        }

        // Сохраняет декодированные данные // TODO public for tests
        public void AMB20_SaveData(string s)
        {
            // получаем порцию данных от прибора. 
            // Это может быть пустая порция @00000036
            // или в момент прихода сигнала от датчика @050000059140
            // @05-00-00-05-91-40		05 - датчик, 00 - часы, 00 - минуты, 05 - секунды, 91 - миллисекунды, 40 - хит - по непроверенной информации, это количество срабатываний прибора в момент прохода датчика над петлёй. то есть, если сигнал хороший (возможно и скорость меньше?), то число должно быть больше


            //    MainForm.log("AMB20_SaveData: " + s + " at system time: " + datetimeConverter.toDateTimeString(DateTime.Now) + "." + DateTime.Now.Millisecond.ToString());

            if (Race == null)
            {
                return;
            }

            //if (bytesToRec != null)
            //{
            //    AMB20RX Res = AMB20_Decode(s);
            //}
            //else
            //{
            // if (s.Length > 30) { 
            AMB20RX Res = AMB20_Decode(s);
            // }

            //if (Res.Transponder.Length > 0 && Res.Transponder != "00")
            if (Res.Transponder != null && Res.Transponder.Length > 0)
            {
                // we have got signal from transponder

                #region processes signal from transponder

                stringForLogWindow += s + "\r\n";
                lastReceive = s;

                int pilotInRaceIndex;

                // когда может прийти такая строка?
                if (stringForLogWindow.Length > 1024)
                {
                    stringForLogWindow = "";
                }

                // узнали индекс пилота в заезде.  
                pilotInRaceIndex = FindMemberFromTransponder(Res.Transponder);

                // если это свободный режим, и пилоту едет на неназначенном карте, то назначим пилоту карт
                if (pilotInRaceIndex < 0 && Race.Light_mode == 1)
                {
                    pilotInRaceIndex = AddKartToPilot(Res.Transponder);
                }


                //if (true)
                if (pilotInRaceIndex >= 0)
                {

                    // создали тик из полученного времени
                    long TickWhenSensorSignalReceived =
                        new TimeSpan(0, Res.Hour, Res.Minutes, Res.Seconds, Res.Millisecond).Ticks;

                    //       MainForm.log("receivedTick: " + TickWhenSensorSignalReceived.ToString());

                    // приготовим строку для лога, который показывается на закладке Полученные данные
                    stringForLogWindow += ">+ " + TickWhenSensorSignalReceived.ToString() + "  " +
                                          new TimeSpan(Math.Abs(TickWhenSensorSignalReceived -
                                                                Members[pilotInRaceIndex].LastTick)).TotalSeconds
                                              .ToString() + "\r\n";
                    stringForLogWindow += ">- " + Res.Hour.ToString("00") + ":" + Res.Minutes.ToString("00") +
                                          ":" + Res.Seconds.ToString("00") + "." + Res.Millisecond.ToString("000") +
                                          "/" +
                                          Res.Hit.ToString("000")
                                          + "\r\n\r\n";

                    // Условие шумового времени
                    // если надо вычитать время прогревочного круга
                    if (WarmLapShouldBeSubtracted)
                    {
                        // и если оно не было вычтено ?? для всех или для первого? муть какая-то
                        if (!IsWarmSubtracted)
                        {
                            MaxTicksForRace = MaxTicksForRace - WarmTicks;
                            IsWarmSubtracted = true;
                        }
                    }

                    // Глобальная метка времени первого пересечения
                    if (StartAfterDetection && StartTick == 0)
                    {
                        StartTick = DateTime.Now.Ticks;
                    }

                    #region sgavrilenko: непонятно, чего убрано

                    // 1 - Узнаем разницу времени от последнего пересечения круга
                    /*
                if (Members[index].LastTick == 0)
                {
                    T = 0;
                }
                else
                {
                    T = NowT - Members[index].LastTick;
                }

                // 2 - Определение дельты времени
                if (Members[index].LapTime < (T - CorrectTime) && (T - CorrectTime) != 0)
                {
                    Members[index].DeltaTime = "+";
                }
                else
                {
                    Members[index].DeltaTime = "-";
                }
                Members[index].DeltaTime += Math.Round(new TimeSpan(Math.Abs(Members[index].LapTime - (T - CorrectTime))).TotalSeconds, 2).ToString();

                // 3 - Если это первый круг то запоминаем время
                //            if (Members[index].Laps == 0) Members[index].FirstTick = receivTick;
                 */

                    #endregion

                    // 5 - Устанавливаем время круга
                    long lapTime = Math.Abs(TickWhenSensorSignalReceived - Members[pilotInRaceIndex].LastTick);

                    if (lapTime >= NoiseTime) // если время круга больше минимального, то...
                    {

                        if (Members[pilotInRaceIndex].LastTick > 0)
                        {
                            Members[pilotInRaceIndex].LapTime = lapTime < 0 ? 0 : lapTime;

                            // 6 - Сохраняем общее время пилота
                            Members[pilotInRaceIndex].TotalPilotTimeOfThisRace += lapTime;

                            // 7 - Счетчик кругов
                            Members[pilotInRaceIndex].Laps++;

                            // 8 - Запись лучшего времени круга
                            if (Members[pilotInRaceIndex].BestLapTime <= 0 ||
                                Members[pilotInRaceIndex].BestLapTime > Members[pilotInRaceIndex].LapTime)
                            {
                                Members[pilotInRaceIndex].BestLapTime = Members[pilotInRaceIndex].LapTime;
                            }

                            // 9 - Сохраняем данные в базу
                            if (Members[pilotInRaceIndex].Laps > 0)
                            {
                                //    MainForm.log("lapTime: " + lapTime.ToString());

                                decimal pilotLapTime = Convert.ToDecimal(new TimeSpan(lapTime).TotalSeconds);

                                //    MainForm.log("AddTimeStamp: " + pilotLapTime.ToString());

                                model.AddTimeStamp(Members[pilotInRaceIndex].MemberID,
                                    Members[pilotInRaceIndex].Laps, pilotLapTime);
                            }
                        }
                        // 4 - Запоминаем время последнего тика
                        Members[pilotInRaceIndex].LastTick = TickWhenSensorSignalReceived;

                    }
                }

                #endregion
            }

            //}
        }

        // Добавляет случайному пилоту случаный карт при LightMode
        public int AddKartToPilot(string Transponder)
        {
            bool EmptMembers = false;
            int index = -1;
            for (int i = 0; i < Members.Count; i++)
            {
                if (Members[i].LightMode && Members[i].IsNoKart)
                {
                    Hashtable Kart = model.GetKartFromTransponder(Convert.ToInt32(Transponder).ToString("000000"));

                    if (Kart.Count > 0)
                    {
                        Members[i].IsNoKart = false;
                        Members[i].CarID = Convert.ToInt32(Kart["id"]);
                        Members[i].CarNum = Kart["number"].ToString();
                        Members[i].CarTransponder = Convert.ToInt32(Transponder).ToString("000000");
                        EmptMembers = true;
                        index = i;
                        model.AddKartToRace(Members[i].MemberID.ToString(), Members[i].CarNum, true);

                        break;
                    }
                }
            }


            if (!EmptMembers)
            {
                model.AddKartNoRace(Transponder, Race.RaceID.ToString());

            }

            return index;
        }


        // Поиск пилота по принимаемому транспондеру
        private int FindMemberFromTransponder(string transponder)
        {
            int index = -1;

            for (int i = 0; i < Members.Count; i++)
            {
                if (Members[i].CarTransponder == transponder)
                {
                    index = i;
                    break;
                }
                /*

                if ((Members[i].CarTransponder.Length == 1 ? "0" : "") + Members[i].CarTransponder == transponder)
                {
                    index = i;
                    break;
                }
                 */
            }

            return index;
        }


        /// <summary>
        /// распарсить строку в структуру данных
        /// </summary>
        /// <param name="s"></param>

        /// <returns></returns>

        public AMB20RX AMB20_Decode(string s)
        {
            AMB20RX Ret = new AMB20RX();

            //if (bytesToRec != null && bytesToRec.Length == 32)
            //{
            //    string hexstringFromCom = BitConverter.ToString(bytesToRec);
            //    string[] hexValuesSplit = hexstringFromCom.Split('-');

            //    string trans = String.Concat(hexValuesSplit[13], hexValuesSplit[14], hexValuesSplit[15],
            //        hexValuesSplit[16]);
            //    string hour = hexValuesSplit[6];
            //    string min = hexValuesSplit[7];
            //    string sec = hexValuesSplit[8];
            //    string msec = String.Concat(hexValuesSplit[17], hexValuesSplit[18], hexValuesSplit[19],
            //        hexValuesSplit[20]);
            //    string hit = String.Concat(hexValuesSplit[21], hexValuesSplit[22]);

            //    Ret.Transponder = Convert.ToInt16(trans, 16).ToString();
            //    Ret.Hour = Convert.ToInt16(hour, 16);
            //    Ret.Minutes = Convert.ToInt16(min, 16);
            //    Ret.Seconds = Convert.ToInt16(sec, 16);
            //    Ret.Millisecond = Convert.ToInt16(msec, 16);
            //    Ret.Hit = Convert.ToInt16(hit, 16);
            //}
            //else
            //{
            if (Settings["decoder"].ToString().Trim() == "AMBrc")
            {
                if (s.Length > 40)
                {
                    // @\t40\t551\t313930\t5001.962\t12\t119\t02\txA03E\r
                    s = s.Replace("\r", " ").Replace('@', ' ').Trim();
                    string[] elemStrings = s.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    TimeSpan time = TimeSpan.FromSeconds(Double.Parse(elemStrings[4].Split('.')[0].ToString()));
                    Ret.Transponder = elemStrings[3];
                    Ret.Hour = time.Hours;
                    Ret.Minutes = time.Minutes;
                    Ret.Seconds = time.Seconds;
                    Ret.Millisecond = Convert.ToInt16(elemStrings[4].Split('.')[1]);
                    Ret.Hit = Convert.ToInt16(elemStrings[5], 16);
                }
            }
            else
            {
                s = convertAsciiTextToHex(s); //вызов функция конвертации
                // parses the string

                if (s.Length >= 31 * 2 + 30)
                {
                    s = s.Replace("\r\n", " ").Trim();
                    string[] hexBits = s.Split(new[] { ' ' });
                    string[] hexValuesSplit = hexBits;
                    //try
                    //{
                    //    for (int i = 0; i < hexBits.Length; i++)
                    //    {
                    //        hexValuesSplit[i] =
                    //            int.Parse(hexBits[i], System.Globalization.NumberStyles.HexNumber).ToString();
                    //        //Console.WriteLine(i + ". " + hexBits[i] + " = " + decBits[i]);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(@"String can not be parsed." + ex.Message);
                    //}

                    //Console.WriteLine(sett["decoder"]);
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
                    int hourByte = Convert.ToInt32(decoderSetts[2].ToString().Trim());
                    int minuteByte = Convert.ToInt32(decoderSetts[3].ToString().Trim());
                    int secondByte = Convert.ToInt32(decoderSetts[4].ToString().Trim());

                    //int[] transBytes = new int[decoderSetts[5].ToString().Trim().Split(',').Length];

                    int index = 0;
                    string trans = "";
                    foreach (string transByte in decoderSetts[5].ToString().Trim().Split(','))
                    {
                        trans += hexValuesSplit[Convert.ToInt32(transByte) - 1];
                    }
                    string msec = "";
                    foreach (string transByte in decoderSetts[6].ToString().Trim().Split(','))
                    {
                        msec += hexValuesSplit[Convert.ToInt32(transByte) - 1];
                    }
                    string hit = "";
                    foreach (string transByte in decoderSetts[7].ToString().Trim().Split(','))
                    {
                        hit += hexValuesSplit[Convert.ToInt32(transByte) - 1];
                    }


                    //  String.Concat(hexValuesSplit[13],hexValuesSplit[14],hexValuesSplit[15],hexValuesSplit[16]);
                    string hour = hexValuesSplit[hourByte - 1];
                    string min = hexValuesSplit[minuteByte - 1];
                    string sec = hexValuesSplit[secondByte - 1];
                    // string[] hexValuesSplit = s.Split(' ');

                    //string trans = String.Concat(hexValuesSplit[13], hexValuesSplit[14], hexValuesSplit[15],
                    //    hexValuesSplit[16]);
                    //string hour = hexValuesSplit[6];
                    //string min = hexValuesSplit[7];
                    //string sec = hexValuesSplit[8];
                    //string msec = String.Concat(hexValuesSplit[17], hexValuesSplit[18], hexValuesSplit[19],
                    //    hexValuesSplit[20]);
                    //string hit = String.Concat(hexValuesSplit[21], hexValuesSplit[22]);

                    Ret.Transponder = Convert.ToInt16(trans, 16).ToString();
                    Ret.Hour = Convert.ToInt16(hour, 16);
                    Ret.Minutes = Convert.ToInt16(min, 16);
                    Ret.Seconds = Convert.ToInt16(sec, 16);
                    Ret.Millisecond = Convert.ToInt16(msec, 16);
                    Ret.Hit = Convert.ToInt16(hit, 16);

                    //Ret.Transponder = String.Concat(decBits[14 - 1], decBits[15 - 1], decBits[16 - 1], decBits[17 - 1]);
                    //Ret.Hour = Convert.ToInt16(decBits[7 - 1]);
                    //Ret.Minutes = Convert.ToInt16(decBits[8 - 1]);
                    //Ret.Seconds = Convert.ToInt16(decBits[9 - 1]);
                    //Ret.Millisecond =
                    //    Convert.ToInt16(String.Concat(decBits[18 - 1], decBits[19 - 1], decBits[20 - 1], decBits[21 - 1]));

                    //Ret.Hit = Convert.ToInt16(String.Concat(decBits[22 - 1], decBits[23 - 1]));
                    //Ret.receivedString = s;
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

            }
            return Ret;
        }



        public static string connectionString = "";
        private static SqlConnection db, db1, conn;


        private static int flagComRead = 0;
        private static Timer Timer1;
        public static Dictionary<long, string> dic = new Dictionary<long, string>();


        public void RS232_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            //  Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //  connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;

            //  db = new SqlConnection(connectionString);
            //  db.Open();

            if (RS232.IsOpen)
            {
                flagComRead = 1;
                if (Timer1 == null)
                {
                    startReadComDataFormDb();
                }
                else
                {
                    Timer1.Change(1000, 1000);
                }
                string s = RS232.ReadLine();
                // s = convertAsciiTextToHex(s);
                dic.Add(DateTime.Now.Ticks, s);
                //testClass1.WriteLog(" RS232_DataReceived ", "поток=" + Thread.CurrentThread.ManagedThreadId + " s =" + s);

                // if (s.Length >= 31 * 2 + 30)
                // {
                //     var res = AMB20_Decode(s);
                //     Console.WriteLine(res.receivedString);
                //     using (SqlCommand cmd = new SqlCommand("insert into comlogs (comdata, created) values ('" + res.receivedString + "',GETDATE())", db))
                //     {
                //        cmd.ExecuteNonQuery();
                //        testClass1.WriteLog(" RS232_DataReceived ", "поток=" + Thread.CurrentThread.ManagedThreadId + " cmd =" + cmd.CommandText);
                //    }

                //    writetext.WriteLine(res.receivedString);
                // }
                //string code = RS232.ReadExisting();
                //Task.Factory.StartNew(new Action(startParser));
                // db.Close();
                //  Console.WriteLine(code);
            }
        }

        internal void startReadComDataFormDb()
        {
            int num = 1;
            TimerCallback tm = new TimerCallback(ReadComDataFormDb);
            Timer1 = new Timer(tm, num, 0, 2000);
            //testClass1.WriteLog(" startReadComDataFormDb ", "поток=" + Thread.CurrentThread.ManagedThreadId + " START ");
        }

        private void ReadComDataFormDb(object sender)
        {
            //testClass1.WriteLog(" readComDataFormDb ", "поток=" + Thread.CurrentThread.ManagedThreadId + " flagComRead = " + flagComRead);

            foreach (var obj in dic)
            {
                // AMB20_SaveData(obj.Value);
                Thread.Sleep(500);
                dic.Remove(obj.Key);
            }

            //if (flagComRead == 1)
            //{
            //   string StringNotOperated = getStringNotOperatedFromDb();

            //    conn = new SqlConnection(connectionString);
            //    conn.Open();
            //    try
            //    {
            //        using (SqlCommand cmd = new SqlCommand("select top 1 * from comlogs where operated = 0", db1))
            //        {
            //            testClass1.WriteLog(" readComDataFormDb ",
            //                "поток=" + Thread.CurrentThread.ManagedThreadId + " cmd = " + cmd.CommandText);
            //            lock (cmd)
            //            {
            //                var res = cmd.ExecuteScalar();
            //                if (res == null)
            //                {
            //                    flagComRead = 0;
            //                }

            //                SqlDataReader reader = cmd.ExecuteReader();

            //                while (reader.Read())
            //                {
            //                    testClass1.WriteLog(" readComDataFormDb ",
            //                        "поток=" + Thread.CurrentThread.ManagedThreadId + " line = " + reader[1].ToString());

            //                    AMB20_SaveData(reader[2].ToString());
            //                    if (conn.State == ConnectionState.Open)
            //                    {
            //                        lock (conn)
            //                        {

            //                            using (
            //                                SqlCommand cmd1 =
            //                                    new SqlCommand(
            //                                        "update comlogs set operated = 1 where id = " + reader[0].ToString(),
            //                                        conn))
            //                            {
            //                                testClass1.WriteLog(" readComDataFormDb ",
            //                                    "поток=" + Thread.CurrentThread.ManagedThreadId + " cmd = " +
            //                                    cmd.CommandText);

            //                                cmd1.ExecuteNonQuery();

            //                                testClass1.WriteLog(" readComDataFormDb ",
            //                                    "поток=" + Thread.CurrentThread.ManagedThreadId + " cmd =" +
            //                                    cmd1.CommandText);
            //                            }
            //                        }
            //                    }
            //                }
            //                reader.Close();

            //            }
            //        }
            //        conn.Close();
            //        db1.Close();
            //    }
            //    catch (Exception es)
            //    {
            //        Console.WriteLine(es.Message);
            //    }
            //}
            //else
            //{
            //    Timer1.Change(60 * 1000, 60 * 1000 * 5);
            //}
        }

        private string getStringNotOperatedFromDb()
        {
            string res = "";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;

            db1 = new SqlConnection(connectionString);
            db1.Open();
            if (db1.State == ConnectionState.Open)
            {

            }
            return res;
        }





    }

    public class Emulator
    {
        private int TickCount = 0;
        private string[] Transponders;

        public void Clear()
        {
            TickCount = 0;
        }

        public void SetTransponders(string[] Tr)
        {
            Transponders = Tr;
        }

        public string AMB20_GetNextEmulatedValue()
        {
            string ret = String.Empty, transp = String.Empty;
            Random rnd = new Random();
            int num;
            TickCount++;
            ret = TickCount.ToString();

            num = rnd.Next(0, Transponders.Length);

            if (num >= Transponders.Length)
            {
                transp = "00";
            }
            else
            {
                transp = Transponders[num].Length == 1 ? "0" + Transponders[num] : Transponders[num];
            }
            /*
            ret = "0101"; // ms And Hits
            while (ret.Length < 10)
            {
                ret = "0" + ret;
            }
            */
            Thread.Sleep(rnd.Next(0, 101));



            return "@" + transp + DateTime.Now.ToString("HHmmssff") + "01";
        }

    }

    public struct AMB20RX
    {
        public string Transponder;
        public int Hour;
        public int Minutes;
        public int Seconds;
        public int Millisecond;
        public int Hit;
        public string receivedString;
    }

    public struct LapTime
    {

        public int Lap;
        public int Seconds;
    }

    public class RaceMember
    {
        public int RaceID;
        public int PilotID;
        public int CarID;
        public string CarNum;
        public string CarTransponder;
        public int MemberID;

        public bool LightMode;
        public bool IsNoKart;

        public string PilotName;
        public string PilotLastName;

        public string PilotNickName;

        public long FirstTick = 0;
        public long LastTick = 0;
        public long TimeST = 0;
        public long LapTime = 0;
        public long TotalPilotTimeOfThisRace = 0;

        public long BestLapTime = -1;
        public string DeltaTime = String.Empty;

        public int Laps = 0;
        public int Warm = 0;
    }

    public struct LapResult
    {
        public int RaceID;
        public int PilotID;
        public int MemberID;
        public int MaxLap;

        public string CarNum;
        public string PilotName;
        public string PilotNickName;

        public bool Light;

        public int BestTimeLap;
        public double BestTime;
        public double AverageTime;

        public List<double> Times;
    }
}
