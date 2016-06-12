using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Rentix
{
    public partial class CheckSensors : Form
    {
        private SerialPort RS232;
        AdminControl admin;

        public CheckSensors(AdminControl adm)
        {
            InitializeComponent();
            this.admin = adm;           
        }


        public void ShowDiagForm()
        {
            if (string.IsNullOrEmpty(admin.Settings["rs232_port"].ToString()))
            {
                MessageBox.Show("Не выбран COM порт, к которому подключён преобразователь AMB-20.\r\nПожалуйста, укажите COM порт в настройках программы", "Ошибка COM порта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            RS232 = new SerialPort(Convert.ToString(admin.Settings["rs232_port"]), 9600, Parity.None, 8, StopBits.One);
            //RS232.RtsEnable = true;
           // RS232.ReadTimeout = 100;
            RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_DataReceived);
            RS232.Open();
            this.ShowDialog();
        }


        private void processLineFromComPort(string someLine)
        {
            string stringForLog = "";

              //string someAmbString = "@" + transponderNumber_textBox2.Text + DateTime.Now.ToString("HHmmssff") + rnd.Next(50).ToString("00");
            RaceThread rt = new RaceThread(admin);
             AMB20RX Res =  rt.AMB20_Decode(someLine);

             if (Res.Transponder.Length > 0 && Res.Transponder != "00")
             {
                 // we have got signal from transponder
                 #region processes signal from transponder

                 string signalLevel = "слабый";

                 if (Res.Hit >= 20)
                 {
                     signalLevel = "хороший";
                 }

                 stringForLog = "Time" + Res.Hour + ":" + Res.Minutes + ":" + Res.Seconds + ":" + Res.Millisecond + " Датчик " + Res.Transponder +
                     ", уровень сигнала: " + signalLevel + " (" + Res.Hit.ToString("00") + "), (data: " + someLine.Trim() + ")" + "\r\n" + "\r\n";                 //stringForLog = DateTime.Now.ToLongTimeString() + " Датчик " + Res.Transponder +
                     //", уровень сигнала: " + signalLevel + " (" + Res.Hit.ToString("00") + "), (data: " + someLine.Trim() + ")" + "\r\n" + "\r\n";


                 sensorsLog_richText.Invoke((MethodInvoker)delegate
             {
                 sensorsLog_richText.Text = stringForLog + sensorsLog_richText.Text;
             });

                 #endregion
             }
             else
             {
                
             }
        }
        // функция конвертации в хекс
        private String convertAsciiTextToHex(String i_asciiText)
        {
            StringBuilder sBuffer = new StringBuilder();
            for (int i = 0; i < i_asciiText.Length; i++)
            {
                sBuffer.Append(Convert.ToInt32(i_asciiText[i]).ToString("x2") + " ");
            }
            return sBuffer.ToString().ToUpper();
        }

        private void RS232_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s = RS232.ReadLine();
            //s = convertAsciiTextToHex(s); //вызов функция конвертации
           // if (s.Length >= 31 * 2 + 30 )
           // {
                processLineFromComPort(s);
           // }
        }

        private void CheckSensors_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (RS232 != null && RS232.IsOpen)
            {
                RS232.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
             string someAmbString = "@" + (rnd.Next(11) + 1).ToString("00") + DateTime.Now.ToString("HHmmssff") + rnd.Next(50).ToString("00");
             processLineFromComPort(someAmbString);
        }


        


    }
}
