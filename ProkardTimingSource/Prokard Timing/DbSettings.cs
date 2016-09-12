using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rentix
{
    public partial class DbSettings : Form
    {
        static WebAnouncer anouncer = new WebAnouncer();
        public DbSettings()
        {
            InitializeComponent();
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            try
            {
                richTextBox1.Text = config.AppSettings.Settings["crazykartConnectionString"].Value;
            }
            catch (Exception exception)
            {
                config.AppSettings.Settings.Add("crazykartConnectionString", "");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // Properties.Settings.crazykartConnectionString = connectionString_textBox9.Text;
            config.AppSettings.Settings.Remove("crazykartConnectionString");
            config.AppSettings.Settings.Add("crazykartConnectionString", richTextBox1.Text);
            //Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //NetworkStream stream = null;
            // var text = richTextBox2.Text;
            //Echo newsend = new Echo();

            // MultiServer.SocketServer.WebSocketServices.Broadcast(text);

            startReadFromCom();

            startSendToCom();
            // MultiServer mserver = new MultiServer();
            //  byte[] msg = Encoding.UTF8.GetBytes(text);
            //stream = MultiServer.clientSocket.GetStream();
            //message = message.Substring(message.IndexOf(':') + 1).Trim().ToUpper();
            //msg = Encoding.Unicode.GetBytes(text);
            // Task.Factory.StartNew(startSendToCom);
        }

        private void startReadFromCom()
        {
            SerialPort RS232 = new SerialPort(Convert.ToString("COM3"), 9600, Parity.None, 8, StopBits.One);
            try
            {
                //RS232.WriteBufferSize = 1024 * 1024;
                RS232.ReadBufferSize = 32;
                RS232.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                //RS232.RtsEnable = true;
                RS232.Open();

            }
            catch (Exception ex)
            {
                //InError = true;
                Console.Write(ex.Message);
            }

        }

        static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {

            SerialPort sp = (SerialPort)sender;
            //sp.Write(GET_TIME, 0, GET_TIME.Length);
            //Thread.Sleep(500);//Даем больше времени на ожидание данных
            //string readex = newPort.ReadExisting();//Убираем эту строку и заменяем на
            int byteRecieved = sp.BytesToRead;
            byte[] messByte = new byte[byteRecieved];
            sp.Read(messByte, 0, byteRecieved);
            string indata = BitConverter.ToString(messByte);
            indata = indata.Replace('-', ' ');
           // MultiServer.SocketServer.WebSocketServices.Broadcast(indata);
            //Console.WriteLine("Data Received:");
          //  RaceThread thr = new RaceThread(new AdminControl(2016));
           // AMB20RX Res = thr.AMB20_Decode(indata);
           // Object obj = new Object("method": "data":Res);
            Webanounserdata data = new Webanounserdata { method = "newlap", data = indata };
             anouncer.action(data);
            //Console.WriteLine(Res.Transponder);
        }
//        № 15534 - Time19:3:25:263 Датчик 006063, уровень сигнала: хороший (123), (data: 00 14 01 0F 05 04 13 03 19 00 00 00 3F 00 00 3C 3F 00 00 02 3F 00 7B 00 00 00 00 00 02 3F 0D)


//№ 15545 - Time19:3:53:263 Датчик 006063, уровень сигнала: хороший (63), (data: 00 14 01 0F 05 04 13 03 35 00 00 00 3F 00 00 3C 3F 00 00 02 3F 00 3F 00 00 00 00 00 03 36 0D)

//№ 15555 - Time19:4:22:239 Датчик 006063, уровень сигнала: хороший (63), (data: 00 14 01 0F 05 04 13 04 16 00 00 00 3F 00 00 3C 3F 00 00 02 27 00 3F 00 00 00 00 00 02 3F 0D)
        //private void startSendToCom()
        //{
        //  //  sendToSocketServer(text);
        //}
        // 
        private void startSendToCom()
        {
            //SerialPort _serialPort = new SerialPort("COM3", 19200, Parity.None, 8, StopBits.One);

            //_serialPort.Handshake = Handshake.None;
            //_serialPort.WriteTimeout = 500;
            //_serialPort.Open();
            //_serialPort.Write("SI\r\n"); 



            SerialPort RS232 = new SerialPort(Convert.ToString("COM5"), 9600, Parity.None, 8, StopBits.One);
            //RS232 = new SerialPort(Convert.ToString("COM5"), 11500, Parity.None, 8, StopBits.One);
            //RS232.WriteBufferSize = 1024 * 1024;
            //RS232.ReadBufferSize = 1024 * 1024 * 2;
            //RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_Receive);
            //RS232.RtsEnable = true;
            RS232.Open();
            string st1 = "00 14 01 08 01 01 01 1C 27 00 00 00 31 00 00 3B A2 00 00 02 3F 00 67 00 00 00 00 00 02 19 0D 0A";
            string st2 = "00 14 01 08 01 01 01 1C 31 00 00 00 32 00 00 3B A2 00 00 02 AE 00 55 00 00 00 00 00 02 81 0D 0A";
            string st3 = "00 14 01 08 01 01 01 1C 36 00 00 00 33 00 00 3B A2 00 00 03 2B 00 45 00 00 00 00 00 01 F5 0D 0A";
            string st4 = "00 14 01 08 01 01 01 1C 3B 00 00 00 34 00 00 3B A2 00 00 00 91 00 42 00 00 00 00 00 02 5B 0D 0A";
            //byte[] data = { 0, 1, 2, 1, 0 };
            //RS232.Write(data, 0, data.Length);
            //  RS232.RtsEnable = true;
            // RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_DataReceived);
           // RS232.Open();
//            
//
//
            Random rnd = new Random();

            string systring = "00 15 01 08 01 01 01 1D 02 00 00 00 3A 00 00 00 00 8F 00 14 00 00 00 01 1D 0D 0A";
            //for (int i = 1; i < 10; i++)
            //{
                //RS232.Write("00 14 01 32 06 0B 08 02 09 00 00 00 36 00 00 3C C3 00 00 01 44 00 7A 00 00 00 00 00 02 5F 0D 0A");
                // string.Concat("0800".Select(c => ((int)c).ToString("x2")))
               // string str1 = "00 14 01 32 06 0B 08 02 09 00 00 00 36 00 00 3C C3 00 00 01 44 00 7A 00 00 00 00 00 02 5F 0D 0A";
                RS232.Write(StrToByteArr(st1), 0, st1.Split(' ').Length);
                RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
                int sleep = rnd.Next(4000, 6000);
                Thread.Sleep(sleep);
                // string str2 = "00 14 01 32 06 0B 08 02 09 00 00 00 36 00 00 3C C3 00 00 01 44 00 7A 00 00 00 00 00 02 5F 0D 0A";
               // Thread.Sleep(3000);
                //  RS232.WriteLine("");
                RS232.Write(StrToByteArr(st2), 0, st2.Split(' ').Length);
                RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
                 sleep = rnd.Next(4000,6000);
                Thread.Sleep(sleep);

                RS232.Write(StrToByteArr(st3), 0, st3.Split(' ').Length);
                RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
                 sleep = rnd.Next(4000, 6000);
                Thread.Sleep(sleep);

                RS232.Write(StrToByteArr(st4), 0, st4.Split(' ').Length);
                RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
                 sleep = rnd.Next(4000, 6000);
                Thread.Sleep(sleep);

             //   //Thread.Sleep(3000);
             //   str1 = "01 40 09 34 30 09 35 35 31 09 33 31 33 39 33 30 09 35 30 30 31 2E 39 36 32 09 31 32 09 31 31 39 09 30 32 09 78 41 30 33 45 0D 0A";
             //   RS232.Write(StrToByteArr(str1), 0, str1.Split(' ').Length);
             //   RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

             //   str2 = "01 40 09 34 30 09 35 35 31 09 33 31 33 39 33 30 09 35 30 30 31 2E 39 36 32 09 31 32 09 31 31 39 09 30 32 09 78 41 30 33 45 0D 0A";
             //   RS232.Write(StrToByteArr(str2), 0, str2.Split(' ').Length);
             //   RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

             // //  Thread.Sleep(3000);
             //   str1 = "01 40 09 34 30 09 35 35 31 09 33 31 33 39 33 30 09 35 30 30 31 2E 39 36 32 09 31 32 09 31 31 39 09 30 32 09 78 41 30 33 45 0D 0A";
             //   RS232.Write(StrToByteArr(str1), 0, str1.Split(' ').Length);
             //   RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
             //   str2 = "01 40 09 34 30 09 35 35 31 09 33 31 33 39 33 30 09 35 30 30 31 2E 39 36 32 09 31 32 09 31 31 39 09 30 32 09 78 41 30 33 45 0D 0A";
             //   RS232.Write(StrToByteArr(str2), 0, str2.Split(' ').Length);
             //   RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

             ////   Thread.Sleep(3000);
             //   str1 = "01 40 09 34 30 09 35 35 31 09 33 31 33 39 33 30 09 35 30 30 31 2E 39 36 32 09 31 32 09 31 31 39 09 30 32 09 78 41 30 33 45 0D 0A";
             //   RS232.Write(StrToByteArr(str1), 0, str1.Split(' ').Length);
             //   RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

             //   str2 = "01 40 09 34 30 09 35 35 31 09 33 31 33 39 33 30 09 35 30 30 31 2E 39 36 32 09 31 32 09 31 31 39 09 30 32 09 78 41 30 33 45 0D 0A";
             //   RS232.Write(StrToByteArr(str2), 0, str2.Split(' ').Length);
             //   RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
             //   Thread.Sleep(5000);
                //  testClass1.WriteLog(" startSendToCom ", "поток=" + Thread.CurrentThread.ManagedThreadId + " операция =" + i);
           // }
            RS232.Close();
        }

        public byte[] StrToByteArr(string hexStr)
        {
            hexStr = hexStr.Replace(" ", "");
            return Enumerable.Range(0, hexStr.Length)
                             .Where(a => a % 2 == 0)
                             .Select(a => Convert.ToByte(hexStr.Substring(a, 2), 16))
                             .ToArray();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //testClass1 wTestClass1testClass1 = new testClass1();
            //Thread rThread = new Thread(wTestClass1testClass1.startReadComDataFormDb);
            //rThread.Start();
             Task.Factory.StartNew(testClass1.startReadComDataFormDb);
            // ();
        }
    }

}
