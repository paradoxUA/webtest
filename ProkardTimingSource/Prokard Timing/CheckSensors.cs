using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Collections;
using System.Xml;
using WebSocketSharp;

namespace Rentix
{
    public partial class CheckSensors : Form
    {
        private SerialPort RS232;
        AdminControl admin;
        public object[] decoderSetts = null;
        private ProkardModel model = new ProkardModel();
        private Hashtable Settings = new Hashtable();
        private Hashtable bytesHashtable = new Hashtable();
        //private byte[] messByte = new byte[32]; // входной буфер, который будем обрабатывать в процессе
        public CheckSensors(AdminControl adm)
        {
            InitializeComponent();
            this.admin = adm;
            model.Connect(); // Создаем дополнительное подключение с базой
            Settings = model.LoadSettings();
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


        public void ShowDiagForm()
        {
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
                    RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_DataReceived);
                    RS232.RtsEnable = true;
                    RS232.Open();
                }
                catch (Exception ex)
                {
                    //InError = true;
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Не установлен COM-порт, программа запускается без детекции");

            this.ShowDialog();
        }


        private void processLineFromComPort(string someLine)
        {
            string stringForLog = "";

            //string someAmbString = "@" + transponderNumber_textBox2.Text + DateTime.Now.ToString("HHmmssff") + rnd.Next(50).ToString("00");
            RaceThread rt = new RaceThread(admin);
            AMB20RX Res = rt.AMB20_Decode(someLine);
            if (Res.Hit == 0)
            {
                return;
            }
            string someLine1 = convertAsciiTextToHex(someLine);
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
                    ", уровень сигнала: " + signalLevel + " (" + Res.Hit.ToString("00") + "), (data: " + someLine1.Trim() + ")" + "\r\n" + "\r\n";                 //stringForLog = DateTime.Now.ToLongTimeString() + " Датчик " + Res.Transponder +
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

        bool mIsConnected;

        char[] inputBuffer = new char[45];

        ulong mRxCounter;
        ulong bytesCounter;
        ulong mTxCounter;
        private bool boolRead = true;
        List<byte> sBuffer = new List<byte>();
        List<byte> tempBytesList = new List<byte>();
        private void RS232_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            while (boolRead)
            {
                try
                {

                    string tempStr = "";
                    //побайтовое чтение того, что порт посылает
                    if (!sp.IsOpen) { return; }
                    byte newByte = (byte)sp.BaseStream.ReadByte();
                    tempBytesList.Add(newByte);
                    byte[] tempBytes = tempBytesList.ToArray();
                    tempStr = Encoding.ASCII.GetString(tempBytes);
                    tempBytesList.Clear();
                    // Encoding.ASCII.GetChars()
                    sBuffer.Add(newByte);
                    if (sBuffer.Count > 0)
                    {
                        //преобразование их в массив
                        if (tempStr == "\n")
                        {
                            byte[] recivedByte = sBuffer.ToArray();
                            string recivedStr = Encoding.ASCII.GetString(recivedByte);
                            processLineFromComPort(recivedStr);
                            this.sBuffer.Clear();
                        }

                        // FullMessageRecived(recivedStr);
                    }
                }
                //если кончилось время ожидания..
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }

            // this.spPort.Close();
            //this.Invoke(new EventHandler(displayText));

            // string s = RS232.ReadLine();
            //s = convertAsciiTextToHex(s); //вызов функция конвертации
            // if (s.Length >= 31 * 2 + 30 )
            // {

            //try
            //{
            //    mRxCounter += (ulong)sp.Read(inputBuffer, 0, inputBuffer.Length);

            //    Array.Clear(inputBuffer, 0, inputBuffer.Length);
            //}
            //catch (System.TimeoutException)
            //{
            //    MessageBox.Show("Time out Failure");
            //}
            //catch (System.ArgumentException)
            //{
            //    MessageBox.Show("Argument Exception");
            //}
            //catch (System.Exception)
            //{
            //    MessageBox.Show("Exception");
            //}
            //byte[] buffer = new byte[blockLimit];
            //Action kickoffRead = null;
            //kickoffRead = delegate
            //{
            //    sp.BaseStream.BeginRead(buffer, 0, buffer.Length, delegate(IAsyncResult ar)
            //    {
            //        try
            //        {
            //            int actualLength = port.BaseStream.EndRead(ar);
            //            byte[] received = new byte[actualLength];
            //            Buffer.BlockCopy(buffer, 0, received, 0, actualLength);
            //            raiseAppSerialDataEvent(received);
            //        }
            //        catch (IOException exc)
            //        {
            //            handleAppSerialError(exc);
            //        }
            //        kickoffRead();
            //    }, null);
            //};
            //kickoffRead();
            //sp.Write(GET_TIME, 0, GET_TIME.Length);
            //Thread.Sleep(500);//Даем больше времени на ожидание данных
            //string readex = newPort.ReadExisting();//Убираем эту строку и заменяем на
            // int byteRecieved = sp.BytesToRead;
            //byte[] messByte = new byte[sp.BytesToRead];
            //sp.Read(messByte, 0, sp.BytesToRead);
            //Console.WriteLine(BitConverter.ToString(messByte));
            //foreach (Byte b in messByte)
            //{
            //  //  Console.Write(b.ToString() + @" ");
            //}
            //bytesHashtable.Add(DateTime.UtcNow.Ticks, messByte);
            // processData(messByte);
            // }
        }

        //private void displayText(object sender, EventArgs e)
        //{

        //    foreach (char c in inputBuffer)
        //    {
        //       // richTextBox1.SelectionColor = color;
        //        if ((c >= ' ' && c <= '~') || c == '\n' || c == '\r' || c == '\t')
        //        {
        //            //richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
        //            Console.Write(c.ToString());
        //        }
        //        else
        //        {
        //            //richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Italic);
        //            Console.Write("{" + ((int)c).ToString("X2") + "}");
        //        }
        //    }
        //   // string s = new string(inputBuffer);
        //   // Console.WriteLine(@"------------------");
        //   // Console.WriteLine(DateTime.Now.TimeOfDay+@"---"+s);
        //}

        //private void processData(byte[] messByte)
        //{
        //    //byte[] currentMessageBytes = new byte[48];

        //    //do currentMessageBytes[i]


        //    //bytesHashtable.GetEnumerator().MoveNext();
        //    //            foreach (long key in bytesHashtable.Keys)
        //    //            {
        //    //                byte[] val = bytesHashtable[key];
        //    //  string indata = (byte[])BitConverter.ToString();
        //    //indata = indata.Replace('-', ' ');
        //    //Console.WriteLine(indata);
        //    //    bytesHashtable.Remove();.Values[b].

        //    //}

        //    //foreach (byte b in messByte)
        //    //{

        //    //  string indata = BitConverter.ToChar(b, 0);
        //    //indata = indata.Replace('-', ' ');
        //    //Console.WriteLine(indata);
        //    //    bytesHashtable.Remove();.Values[b].

        //    //}
        //    //processLineFromComPort(indata);
        //}

        private void CheckSensors_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (RS232 != null && RS232.IsOpen)
            {
                RS232.DiscardInBuffer();
                RS232.Dispose();
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
