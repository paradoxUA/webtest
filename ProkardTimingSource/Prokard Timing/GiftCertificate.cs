using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BarcodeLib;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing.Text;

namespace Rentix
{
    public partial class GiftCertificate : Form
    {
        AdminControl admin;
        string BarCode = String.Empty;
        int PilotID = -1;
        string Cash = "0";
        string Rcount = "0";
        bool NoN;
        string pName;
        public GiftCertificate(AdminControl ad, bool NoName, int PID = -1)
        {
            InitializeComponent();
            admin = ad;

            // 482  - Код страны
            // 0961 - Код производителя (с головы)
            // 34222 - Код продукта
            int Code = 34220 + admin.model.GetNextCertificateNum()-1;
            BarCode = "4820961" + Code.ToString();
            labelSmooth6.Text = BarCode;
            ShowCertificateType();
            radioButton4.Enabled = admin.IS_ADMIN;
            if (NoName)
            {
                anonymous_radioButton.Checked = true;
                named_radioButton1.Enabled = false;
                labelSmooth11.Text = "CrazyKart";
                NoN = false;
            }
            else
            {
                NoN = true;
                PilotID = PID;
                int uid = Convert.ToInt32(PID);
                if (uid > 0)
                {
                    Hashtable User = admin.model.GetPilot(uid);
                    labelSmooth11.Text = User["surname"] + " " + User["name"];
                    if (labelSmooth11.Text.Length < 3) labelSmooth11.Text = User["nickname"].ToString();

                }
                pName = labelSmooth11.Text;
            }

        
        }

        private void ShowCertificateType() {
            comboBox1.Items.Clear();

            List<string> CT = admin.model.GetCertificatesType(radioButton4.Checked ? "1" : "0");

         
            for (int i = 0; i < CT.Count; i++)
                comboBox1.Items.Add(CT[i]);

            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox1.SelectedIndex >= 0)
            {
                Hashtable ct = admin.model.GetCertificateType(admin.model.GetCertificateTypeID(comboBox1.Items[comboBox1.SelectedIndex].ToString()).ToString());

                Rcount = ct["nominal"].ToString();
                Cash = ct["cost"].ToString();

                labelSmooth7.Text = Cash + " грн";
                labelSmooth9.Text = Rcount;
            }
        
        }

        private void GiftCertificate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) MessageBox.Show("Не выбрат тип сертификата");
            else
            {
                if (admin.Settings["printer_result"].ToString().Length > 0)
                {
                    PrintCerificate(false);
                    printPreviewDialog1.Width = 600;
                    printPreviewDialog1.Height = 600;
                    ((ToolStrip)(printPreviewDialog1.Controls[1])).Items.RemoveAt(0);
                    PrinterSettings.StringCollection prnArray = PrinterSettings.InstalledPrinters;
                    printPreviewDialog1.Document.PrinterSettings.PrinterName = admin.Settings["printer_result"].ToString();
                    try
                    {
                        printPreviewDialog1.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не установлен принтер. Ошибка: " + ex.Message);
                    }
                }
                else MessageBox.Show("Не установлен принтер");
            }
        }

        Barcode bc;
        private void PrintCerificate(bool SaveToBase = true)
        {
            
                bc = new Barcode();
                bc.IncludeLabel = false;
                bc.LabelPosition = LabelPositions.BOTTOMCENTER;
                bc.Alignment = AlignmentPositions.CENTER;
                
                PrintDialog MyPrintDialog = new PrintDialog();
                MyPrintDialog.AllowCurrentPage = false;
                MyPrintDialog.AllowPrintToFile = false;
                MyPrintDialog.AllowSelection = false;
                MyPrintDialog.AllowSomePages = false;
                MyPrintDialog.PrintToFile = false;
                MyPrintDialog.ShowHelp = false;
                MyPrintDialog.ShowNetwork = false;

                printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;

                printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
                printDocument1.PrinterSettings.PrinterName = admin.Settings["printer_result"].ToString();

                printDocument1.DefaultPageSettings.Landscape = false;
                printDocument1.DefaultPageSettings.Color = false;
                printDocument1.PrinterSettings.Copies = 1;
                printDocument1.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                printDocument1.DocumentName = "Сертификат";




                //


                if (SaveToBase)
                {
                    if (radioButton4.Checked)
                    {
                        admin.model.AddCertificate((admin.model.GetCertificateTypeID(comboBox1.Items[comboBox1.SelectedIndex].ToString())).ToString(),
                            BarCode, anonymous_radioButton.Checked ? -1 :
                            PilotID, DateTime.Now.AddDays(365));
                    }
                    else
                    {
                        admin.model.AddCertificate((admin.model.GetCertificateTypeID(comboBox1.Items[comboBox1.SelectedIndex].ToString())).ToString(),
                            BarCode, anonymous_radioButton.Checked 
                            ? -1 : PilotID,
                            DateTime.Now.AddDays(Convert.ToInt32(admin.Settings["sertificat_day"])));
                    }
                }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            using (Font f1 = new Font("Calibri", 12))
            {
                e.Graphics.DrawString("Сертификат - " + comboBox1.Text, f1, Brushes.Black, new Point(20, 20));

                if (named_radioButton1.Checked)

                    e.Graphics.DrawString("Владелец - " + labelSmooth11.Text, f1, Brushes.Black, new Point(20, 40));

                e.Graphics.DrawString((radioButton4.Checked?"Cкидка- ":"Число заездов- ") + labelSmooth9.Text +(radioButton4.Checked?"%":""), f1, Brushes.Black, new Point(20, named_radioButton1.Checked ? 60 : 40));
                bc.RawData = BarCode;
                Image bimage = bc.Encode(BarcodeLib.TYPE.EAN13, BarCode, Color.Black, Color.White, 200, 70);
                e.Graphics.DrawImage(bimage, new Point(20, named_radioButton1.Checked ? 80 : 60));
                e.Graphics.DrawString(BarCode, f1, Brushes.Black, new Point(20, named_radioButton1.Checked ? 155 : 135));
        
            }
           }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) MessageBox.Show("Не выбран тип сертификата");
            else
            {
                if (admin.Settings["printer_result"].ToString().Length <= 2) MessageBox.Show("Принтер не установлен. Обрадитесь к администратору");
                else
                {

                    CertificateCash form = new CertificateCash(admin, Cash, PilotID);
                    if (Cash == "0" || form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						PrintCerificate();
						try
						{
							printDocument1.Print();
						}
						catch (Exception ex)
						{
							MessageBox.Show("Не удалось распечатать документ. Ошибка: " + ex.Message);
						}

						this.Close();
					}

                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                Hashtable ct = admin.model.GetCertificateType(admin.model.GetCertificateTypeID(comboBox1.Items[comboBox1.SelectedIndex].ToString()).ToString());

                Rcount = ct["nominal"].ToString();
                Cash = ct["cost"].ToString();

                labelSmooth7.Text = Cash + " грн";
                labelSmooth9.Text = Rcount;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            if (anonymous_radioButton.Checked)
            {
                labelSmooth11.Text = "CrazyKart";
            }
            else
            {
                if (NoN)
                    labelSmooth11.Text = pName;
                else
                    labelSmooth11.Text = "";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ShowCertificateType();
            labelSmooth10.Text = "Скидка %";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ShowCertificateType();
            labelSmooth10.Text = "Заездов";
        
        }
    }
}
