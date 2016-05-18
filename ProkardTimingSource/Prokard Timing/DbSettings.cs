using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prokard_Timing
{
    public partial class DbSettings : Form
    {
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
            Task.Factory.StartNew(startSendToCom);
        }


//        № 15534 - Time19:3:25:263 Датчик 006063, уровень сигнала: хороший (123), (data: 00 14 01 0F 05 04 13 03 19 00 00 00 3F 00 00 3C 3F 00 00 02 3F 00 7B 00 00 00 00 00 02 3F 0D)


//№ 15545 - Time19:3:53:263 Датчик 006063, уровень сигнала: хороший (63), (data: 00 14 01 0F 05 04 13 03 35 00 00 00 3F 00 00 3C 3F 00 00 02 3F 00 3F 00 00 00 00 00 03 36 0D)

//№ 15555 - Time19:4:22:239 Датчик 006063, уровень сигнала: хороший (63), (data: 00 14 01 0F 05 04 13 04 16 00 00 00 3F 00 00 3C 3F 00 00 02 27 00 3F 00 00 00 00 00 02 3F 0D)

        // 
        private void startSendToCom()
        {
            SerialPort RS232 = new SerialPort(Convert.ToString("COM5"), 9600, Parity.None, 8, StopBits.One);
           // RS232.ReadBufferSize = 32;
          //  RS232.RtsEnable = true;
           // RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_DataReceived);
            RS232.Open();
            string systring = "00 15 01 08 01 01 01 1D 02 00 00 00 3A 00 00 00 00 8F 00 14 00 00 00 01 1D 0D 0A";
            for (int i = 1; i < 10; i++)
            {
               // string.Concat("0800".Select(c => ((int)c).ToString("x2")))
                string str1 = "00 15 01 08 01 01 01 1D 02 00 00 00 3A 00 00 00 00 8F 00 14 00 00 00 01 1D 0D 0A";
               RS232.Write(StrToByteArr(str1), 0, str1.Split(' ').Length);
               RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
               string str2 = "00 14 01 08 01 01 01 1C 27 00 00 00 31 00 00 3B A4 00 00 02 3F 00 67 00 00 00 00 00 02 19 0D 0A";
               Thread.Sleep(3000);
              //  RS232.WriteLine("");
               RS232.Write(StrToByteArr(str2), 0, str2.Split(' ').Length);
               RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

               Thread.Sleep(3000);
               str1 = "00 14 01 08 01 01 01 1C 36 00 00 00 33 00 00 3B A2 00 00 03 2B 00 45 00 00 00 00 00 01 F5 0D 0A";
               RS232.Write(StrToByteArr(str1), 0, str1.Split(' ').Length);
               RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

               str2 = "00 14 01 08 01 01 01 1C 3B 00 00 00 34 00 00 3B A2 00 00 00 91 00 42 00 00 00 00 00 02 5B 0D 0A";
                RS232.Write(StrToByteArr(str2), 0, str2.Split(' ').Length);
                RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

                Thread.Sleep(3000);
                str1 = "00 14 01 08 01 01 01 1C 2B 00 00 00 34 00 00 5B A5 00 00 00 91 00 42 00 00 00 00 00 02 5B 0D 0A";
               RS232.Write(StrToByteArr(str1), 0, str1.Split(' ').Length);
               RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
               str2 = "00 14 01 08 01 01 01 1C 29 00 00 00 31 00 00 3B A2 00 00 02 03 00 67 00 00 00 00 00 02 19 0D 0A";
                RS232.Write(StrToByteArr(str2), 0, str2.Split(' ').Length);
                RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

                Thread.Sleep(3000);
                str1 = "00 14 01 08 01 01 01 1C 3B 00 00 00 34 00 00 5B A5 00 00 00 91 00 42 00 00 00 00 00 02 5B 0D 0A";
                RS232.Write(StrToByteArr(str1), 0, str1.Split(' ').Length);
                RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);

                str2 = "00 14 01 08 01 01 01 1C 30 00 00 00 31 00 00 3B A2 00 00 02 04 00 67 00 00 00 00 00 02 19 0D 0A";
                RS232.Write(StrToByteArr(str2), 0, str2.Split(' ').Length);
                RS232.Write(StrToByteArr(systring), 0, systring.Split(' ').Length);
                  Thread.Sleep(5000);
              //  testClass1.WriteLog(" startSendToCom ", "поток=" + Thread.CurrentThread.ManagedThreadId + " операция =" + i);
            }
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
