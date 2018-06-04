using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Text;
using Rentix.Controls;
using Rentix.Extensions;

namespace Rentix
{
    public partial class CashOperations : Form
    {

        Hashtable User;
        bool OnNumber = false;
        RaceClass Race;
        int PilotID = -1;
        double Sum = 0;      // Полная стоимость 
        double groupDiscount; // скидка на группу
//        double Sale = 0;     // Скидка %
        double certificateDiscount = 0;    // Скидка от сертификата
        double finalSum = 0;  // Итоговая сумма к оплате
        double UserCash = 0; // Бабло на счету у пилота 
        CheckPrintData Data = new CheckPrintData();
        AdminControl admin;
        public model.DiscountCard discountCard;
        comboBoxItem ci = new comboBoxItem("", -1);

        int sale_onelap = 0;
        int sale_half = 0;
        bool InSale = false;
        bool IsHalfModesEnabled = false; // разрешены ли заезды на половину времени заезда

        public CashOperations(int ID, RaceClass R, AdminControl ad, double sale = 0)
        {

            InitializeComponent();
              admin = ad;

            discountCard = null;
           
            PilotID = ID;
            User = admin.model.GetPilot(PilotID);

            int idDefaultRaceMode = Convert.ToInt32(admin.Settings["default_race_mode_id"]);

               Race = R;
            int idGroup = Convert.ToInt32(User["gr"]);
            Sum = Convert.ToDouble(admin.GetPrice(admin.GetWeekDayNumber(R.Date),
               Convert.ToInt32(R.Hour), idDefaultRaceMode, idGroup));//  R.RaceSum;
         

     //       ci = new comboBoxItem(null, -1);
     //       ci.selectComboBoxValueById(userSelectedMode_comboBox1, idDefaultRaceMode);
            // userSelectedMode_comboBox1.SelectedIndex = 0;

            labelSmooth1.Text = "Полная стоимость билета - " + Sum.ToString() + " грн";
            cashFromPilot_textBox3.Text = priceForCurrentRace_textBox5.Text = Sum.ToString();
            certificateDiscount = sale;
          
            LoadData();


            InSale = labelSmooth5.Visible = userSelectedMode_comboBox1.Visible = Convert.ToBoolean(admin.Settings["racesale"] ?? false);
            sale_onelap = Convert.ToInt32(admin.Settings["sale_onelap"] ?? 0);
            sale_half = Convert.ToInt32(admin.Settings["sale_half"] ?? 0);

			priceForHalfMode_label.Visible = halfModes_comboBox.Visible = true;
			//IsHalfModesEnabled = priceForHalfMode_label.Visible = halfModes_comboBox.Visible = Convert.ToBoolean(admin.Settings["halfModesEnabled"] ?? false);


            List<Hashtable> data = admin.model.GetAllRaceModes(" and is_deleted <> 1 ");
            halfModes_comboBox.Items.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                comboBoxItem someItem =
                    new comboBoxItem(Convert.ToString(data[i]["name"]),
                    Convert.ToInt32(data[i]["id"]));

                halfModes_comboBox.Items.Add(someItem);
            }

			partnerComboBox.Items.Clear();
			var partners = ad.model.GetPartners(true).ToArray();
			partnerComboBox.Items.Add(new comboBoxItem("", -1));
			for (int i = 0; i < partners.Length; i++)
			{
				var someItem = new comboBoxItem(Convert.ToString(partners[i][1]), Convert.ToInt32(partners[i][0]));
				partnerComboBox.Items.Add(someItem);
			}
        }

        // если введён номер карты, найти её и пересчитать сумму к оплате
        private void findCard(string cardNumber)
        {
             discountCard = admin.model.getCardByNumber(cardNumber);
           if (discountCard != null)
           {
               if (discountCard.IsBlocked == true)
               {
                   discountPercentValue_label.Text = "заблокирована";
                   discountCard = null;
               }
               else
               {
                   if (admin.model.isCardUsedInRaceData(discountCard.Id,
                       Convert.ToInt32(Race.RaceID), PilotID) == true)
                   {
                       discountPercentValue_label.Text = "Карта уже используется в этом заезде";
                       discountCard = null;
                   }
                   else
                   {
                       discountCard_textBox1.Text = discountCard.Number;
                       discountPercentValue_label.Text =
                           discountCard.DiscountCardGroup.PercentOfDiscount.ToString();
                       discountPercent_label.Visible = true;
                   }
               }
           }
           else
           {
               discountPercentValue_label.Text = "";
           }

           CalculateSumForPayment();
        }


        // при открытии формы, если у пилота есть карта, показать её
        private void loadDiscountCard()
        {
               #region если у пилота есть скидочная карта, покажем её

            discountPercentValue_label.Text = ""; 
            discountPercent_label.Visible = false;

           discountCard = admin.model.getCardByUserId(PilotID);
           if (discountCard != null)
           {
               if (discountCard.IsBlocked == true)
               {
                   discountPercentValue_label.Text = "заблокирована";
                   discountCard = null;
               }
               else
               {
                   discountCard_textBox1.Text = discountCard.Number;
                   discountPercentValue_label.Text =
                       discountCard.DiscountCardGroup.PercentOfDiscount.ToString();
                   discountPercent_label.Visible = true;
               }
           }
           else
           {
               discountPercentValue_label.Text = "";
           }

            #endregion

           CalculateSumForPayment();
        }


        private void LoadData()
        {

            loadDiscountCard();

            checkBox2.Visible = Convert.ToBoolean(admin.Settings["print_check"]);
            User = admin.model.GetPilot(PilotID);
            groupDiscount = 0;
            if (User.Count > 0)
            {
                string pilotName = "";

                if (User["nickname"].ToString().Length > 0)
                {
                    pilotName = "[" + User["nickname"].ToString() + "]";
                }

                pilotName = pilotName + " " + User["name"].ToString() + " " + User["surname"].ToString();

                pilotNameForCashOperations_textBox1.Text = pilotName.Trim();
                UserCash = admin.model.GetUserBallans(PilotID);
                groupDiscount = admin.model.GetGroupSale(User["gr"].ToString());
               // Sale = Usale + sSale;
                labelSmooth2.Text = "Группа - " + admin.model.GetGroupName(User["gr"].ToString());
            }
            else
            {
                UserCash = 0;
          //      Sale = sSale;
                labelSmooth2.Text = "Группа - Default";
            }
            textBox2.Text = UserCash.ToString() + " грн";

            CalculateSumForPayment();
            cassaRadioButton.Checked = true;
            labelSmooth3.Text = "Скидка группы - " + groupDiscount.ToString() + "%";
            if (certificateDiscount == 0)
            {
                labelSmooth4.Text = "";
            }
            else
            {
                labelSmooth4.Text = "Скидка сертификата - " + certificateDiscount.ToString() + "%";
            }
            label1.Text = "Заезд №" + (Race.RaceNum > admin.DopRace ? (Race.RaceNum - admin.DopRace).ToString() + "a" : Race.RaceNum.ToString());
            startTimeLabel_label3.Text = "Старт в " + Race.Hour + ":" + Race.Minute;
            label2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private double SimpleRound(double Summ)
        {
            double result = Summ / (double)5;
            result = Math.Ceiling(result);
            result = result * 5;
            return result;

           // return (Summ + 5) - (Summ + 5) % 5;
        }

        private void CalculateSumForPayment()
        {



             comboBoxItem ci = new comboBoxItem("", -1);
             int idGroup = Convert.ToInt32(User["gr"]);

            // узнали полную цену для этого времени, с учётом указанного количества кругов
            Sum = Convert.ToDouble(admin.GetPrice(admin.GetWeekDayNumber(Race.Date),
                Convert.ToInt16(Race.Hour),
                halfModes_comboBox.SelectedIdx(), idGroup));

            
            /* есть такие скидки - certificateSale - скидка по сертификату
             *  скидка на группу
             *  и скидка по скидочной карте.
             *  в расчёте должна быть учтена одна, максимальная
             */

            double discountCardDiscount = 0;
            if (discountCard != null && discountCard.IsBlocked == false)
            {
                discountCardDiscount = ((finalSum * (double)((double)discountCard.DiscountCardGroup.PercentOfDiscount / 100)));
            }
        
          double maximumDiscount = discountCardDiscount;
          if (certificateDiscount > discountCardDiscount)
          {
              maximumDiscount = certificateDiscount;
          }

          if (groupDiscount > maximumDiscount)
          {
              maximumDiscount = groupDiscount;
          }


            /*
            priceForCurrentRace_textBox5.Text = priceForHalfMode;
            cashFromPilot_textBox3.Text = priceForHalfMode;
            finalSum = Convert.ToInt16(priceForHalfMode);
             */
        

            finalSum = (Math.Round(Sum - (Sum * (maximumDiscount / 100)), 2));

            /*
            if (InSale && userSelectedMode_comboBox1.SelectedIndex > 0)
            {

                switch (userSelectedMode_comboBox1.SelectedIndex)
                {
                    case 1: 
                        if (sale_onelap > 0)
                        {
                            finalSum = (finalSum * (double)((double)sale_onelap / 100));
                        } 
                        break;
                    case 2: if (sale_half > 0)
                        {
                            finalSum = (finalSum * (double)((double)sale_half / 100));
                        }
                        break;
                }

            }
             */

           

        //    if (Sale != 0)
       //     {
                finalSum = SimpleRound(finalSum); // округлить до 5 грн вверх
       //     }
            
            if (finalSum < 0)
            {
                finalSum = 0;
            }

            priceForCurrentRace_textBox5.Text = cashFromPilot_textBox3.Text = finalSum.ToString();
            
            userCashRadioButton.Enabled = UserCash >= finalSum;

             refreshRestMoney();
        }


        private void CashOperations_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        bool InUserCash = false;

        // нажато Оплатить (при посадке пилота)
        private void button2_Click(object sender, EventArgs e)
        {
            string SaleComment = String.Empty;
            switch (userSelectedMode_comboBox1.SelectedIndex)
            {

                case 1: SaleComment = "За один круг"; break;
                case 2: SaleComment = "За половину заезда"; break;
            }

            // Добавление денег в кассу, если оплата не идет через счет пользователя
            if (!userCashRadioButton.Checked)
            {
                admin.model.Jurnal_Cassa(terminalRadioButton.Checked  ? "33" : "1", Convert.ToInt32(PilotID), Race.RaceID, priceForCurrentRace_textBox5.Text, "0", "Оплата участия в рейсе." + SaleComment, partnerComboBox.SelectedIdx(), refCodeTextBox.Text, false);
            }
            else
            {
                admin.model.Jurnal_UserCashByUserId("6", Convert.ToInt32(PilotID),
                    priceForCurrentRace_textBox5.Text, "1", "Оплата участия в рейсе." + SaleComment
                    + " Cъём со счета пользователя.", Race.RaceID, partnerComboBox.SelectedIdx(), refCodeTextBox.Text);
                InUserCash = true;
            }

            if (moveRestToUserAccount_checkBox1.Checked && moveRestToUserAccount_checkBox1.Enabled)
            {
                admin.model.Jurnal_AddToUserCash("3", Convert.ToInt32(PilotID), textBox4.Text, "0", "Коррекция баланса пользователя.", partnerComboBox.SelectedIdx(), refCodeTextBox.Text);
            }

            if (Convert.ToBoolean(admin.Settings["print_check"]))
            {
                if (checkBox2.Checked)
                {
                    print_check();
                    try
                    {
                        printDocument1.Print();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось распечатать чек. Ошибка: " + ex.Message);
                    }
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void CashOperations_Activated(object sender, EventArgs e)
        {
            cashFromPilot_textBox3.Select();
            cashFromPilot_textBox3.SelectAll();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            OnNumber = false;

            if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9))
                if (/*e.KeyValue != 190 && */e.KeyValue != 188 && e.KeyCode != Keys.Back)
                {
                    OnNumber = true;
                }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (OnNumber) e.Handled = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CashOperations_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        // пересчитать сдачу
        private void refreshRestMoney()
        {
            try
            {
                double s1 = Double.Parse(cashFromPilot_textBox3.Text);
                double s2 = Math.Round(Math.Abs(finalSum - s1), 2);
                moveRestToUserAccount_checkBox1.Enabled = s2 > 0 && s1 >= finalSum;
                textBox4.Text = moveRestToUserAccount_checkBox1.Enabled ? s2.ToString() : "";

                button2.Enabled = s1 >= finalSum;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }


        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            refreshRestMoney();
        }

        // взять с личного счёта
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cashFromPilot_textBox3.Enabled = !userCashRadioButton.Checked;
            moveRestToUserAccount_checkBox1.Enabled = !userCashRadioButton.Checked;
        }


        private void print_check()
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;
            MyPrintDialog.PrinterSettings.PrinterName = admin.Settings["printer_check"].ToString();

            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Landscape = false;
            printDocument1.DefaultPageSettings.Color = false;
            printDocument1.PrinterSettings.Copies = 1;
            printDocument1.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            printDocument1.DocumentName = "Чек";



            Data.PilotID = User["id"].ToString();
            Data.Name = User["name"].ToString();
            Data.SurName = User["surname"].ToString();
            if (InUserCash)
                Data.Sum = "Со счета:" + priceForCurrentRace_textBox5.Text + " грн";
            else
                Data.Sum = priceForCurrentRace_textBox5.Text + " грн";
            Data.RaceTime = Race.Hour + ":" + Race.Minute;
            Data.RaceDate = Race.Date;
            Data.NickName = User["nickname"].ToString();
            Data.OrgName = "CrazyKarting";
            Data.RaceNum = "Заезд " + (Race.RaceNum > admin.DopRace ? (Race.RaceNum - admin.DopRace).ToString() + "a" : Race.RaceNum.ToString());
            Data.PrintSum = true;


        }

        private void CreateCheck(PrintPageEventArgs e, CheckPrintData Data)
        {

            Rectangle Rect = e.MarginBounds;
            Point SP = new Point(Rect.X, Rect.Y);
            int W = Rect.Width - 36;
            int YStep;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();

            using (Font f1 = new Font("Calibri", 10))
            {
                YStep = f1.Height;
                int TW = 0;
                drawFormat.Alignment = StringAlignment.Center;
                using (Font f2 = new Font("Calibri", 12, FontStyle.Bold))
                    e.Graphics.DrawString(Data.OrgName, f2, Brushes.Black, new Rectangle(0, 0, W, 20), drawFormat);

                TW = W - Data.RaceNum.Length;

                drawFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(Data.RaceNum, f1, Brushes.Black, new Rectangle(0, 15 + 3, W, 20), drawFormat);


                if (Data.PrintSum)
                {
                    drawFormat.Alignment = StringAlignment.Far;
                    TW = W - Data.NickName.Length;
                    e.Graphics.DrawString(Data.Sum, f1, Brushes.Black, new Rectangle(0, 15 + 3, W, 20), drawFormat);
                }


                drawFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(Data.SurName + " " + Data.Name, f1, Brushes.Black, new Rectangle(0, 30 + 3, W, 20), drawFormat);
                e.Graphics.DrawString(halfModes_comboBox.SelectedItem?.ToString(), f1, Brushes.Black, new Rectangle(0, 45 + 3, W, 20), drawFormat);

                e.Graphics.DrawString("Дата старта:", f1, Brushes.Black, new Rectangle(0, 60 + 3, W, 20), drawFormat);

                TW = W - datetimeConverter.toDateString(Data.RaceDate).Length;
                //e.Graphics.DrawString(Data.RaceDate, f1, Brushes.Black, TW, SP.Y + YStep * 3);
            }

            using (Font f1 = new Font("Calibri", 12))
            {

                e.Graphics.DrawString(Data.RaceDate.ToString("yyyy-MM-dd") + " в " + Data.RaceTime, f1, Brushes.Black, new Rectangle(0, 75 + 3, W, 20), drawFormat);
            }

            using (Font f1 = new Font("Calibri", 9))
            {
                e.Graphics.DrawString("--------------------------------------------------------------", f1, Brushes.Black, new Rectangle(0, 90 + 3, W, 20), drawFormat);
			    e.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), f1, Brushes.Black, new Rectangle(0, 105 + 3, W, 20), drawFormat);
				e.Graphics.DrawString("www.crazykarting.com.ua", f1, Brushes.Black, new Rectangle(0, 160 + 3, W, 20), drawFormat);

				//e.Graphics.DrawString("www.crazykarting.com.ua", f1, Brushes.Black, new Rectangle(0, 105 + 3, W, 20), drawFormat);

			}
		}

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            CreateCheck(e, Data);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            print_check();

            printPreviewDialog1.Document = printDocument1;

            // Show PrintPreview Dialog
            printPreviewDialog1.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateSumForPayment();
        }



        private void halfModes_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateSumForPayment();

            /*

            comboBoxItem ci = new comboBoxItem("", -1);           

            string priceForHalfMode = admin.GetPrice(admin.GetWeekDayNumber(Race.Date),
                Convert.ToInt16(Race.Hour), 
                comboBoxItem.getSelectedValue(halfModes_comboBox));

            priceForCurrentRace_textBox5.Text = priceForHalfMode;
            cashFromPilot_textBox3.Text = priceForHalfMode;
            finalSum = Convert.ToInt16(priceForHalfMode);
            refreshRestMoney();
             */

        }

        private void discountCard_textBox1_TextChanged(object sender, EventArgs e)
        {
            if (discountCard_textBox1.Text.Length != 8)
            {
                discountPercentValue_label.Text = "номер не содержит 8 цифр";
            }
            else
            {
                findCard(discountCard_textBox1.Text);

            }
        }

		private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			refCodeTextBox.Enabled = partnerComboBox.SelectedIdx() != -1;
			UpdateCostByPartner();
		}

		private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
		{
		}

		private void UpdateCostByPartner()
		{
			var partnerId = partnerComboBox.SelectedIdx();
			if (partnerId == -1)
			{
				CalculateSumForPayment();
				return;
			}
			var partnerCash = admin.model.GetPartnerCash(partnerId);
			if (partnerCash == -1)
			{
				return;
			}
			cashFromPilot_textBox3.Text = priceForCurrentRace_textBox5.Text = partnerCash.ToString("F0");
		}
	}

	public struct CheckPrintData
    {
        public string NickName;
        public string Name;
        public string SurName;
        public string RaceNum;
        public string RaceTime;
        public DateTime RaceDate;
        public string CheckNum;
        public string Sum;
        public bool PrintSum;
        public string PilotID;
        public string OrgName;
    }
}
