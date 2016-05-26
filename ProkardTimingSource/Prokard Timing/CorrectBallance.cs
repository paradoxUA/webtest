using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Rentix
{
    public partial class CorrectBallance : Form
    {
        bool OnNumber = false;
        RaceClass Race;
        int idRecordInRaceData = -1;
        double Sum = 0;
        double MaxSum = 0;
        AdminControl admin;
        bool DelP = false;

        // бывает необходимость просто положить деньги на счёт пользователя или снять. А бывает, что нужно вернуть столько, сколько он заплатил за заезд
        // поэтому иногда надо знать id race data. для этого придуман флаг convertToIdUser. Если да, то из idRaceData надо получить id пилота 
        public CorrectBallance(AdminControl ad,int idRaceData, bool DelPilotFromRace, RaceClass R = null)
        {
            InitializeComponent();
            idRecordInRaceData = idRaceData;
            admin = ad;
            Race = R;

            if (DelPilotFromRace)
            {
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                radioButton1.Visible = false;
                radioButton2.Visible = false;

                radioButton3.Checked = true;
                this.DelP = DelPilotFromRace;
                textBox3.ReadOnly = true;
                Sum = admin.model.getPaidAmount(idRecordInRaceData);

                button2.Enabled = true;
            }
            else
            {
                Sum = 0;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton1.Visible = true;
                radioButton2.Visible = true;

                radioButton1.Checked = true;
            }
            textBox3.Text = Sum.ToString();
            admin.model.Connect();
            LoadData(DelPilotFromRace);
        }

        private void LoadData(bool convertToIdUser)
        {
            int idPilot = idRecordInRaceData;

            if (convertToIdUser)
            {
                idPilot = admin.model.getUserByIdRaceData(idRecordInRaceData).id;
            }

            Hashtable User = admin.model.GetPilot(idPilot);
            if (User.Count > 0)
            {
                textBox1.Text = User["surname"].ToString() + " " + User["name"].ToString();
            }
            double user_cash = Math.Round(admin.model.GetUserBallans(idPilot) ,3);
            MaxSum = user_cash;
            textBox2.Text = user_cash.ToString() + " грн.";

            if (DelP)
            {
                textBox3.Text = Sum.ToString(); // (Sum - Sum * (Convert.ToDouble(admin.model.GetGroupSale(User["gr"].ToString())) / 100)).ToString();
            
            }
        }


        private void CorrectBallance_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CorrectBallance_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool ret = false;

            if (radioButton3.Checked)
            { // Отказ от участия в заезде
                ret = true;
                admin.model.Jurnal_Cassa("4", admin.model.getUserByIdRaceData(idRecordInRaceData).id, Race.RaceID, textBox3.Text, "1", "Отказ от участия в рейсе. Выдача денег наличными");
            }
            else
            {
                if (radioButton2.Checked) // Съем денег со счета пользователя
                {
                    if (Double.Parse(textBox3.Text) <= MaxSum)
                    {
                        ret = true;
                        admin.model.Jurnal_AddToUserCash("2", Convert.ToInt32(idRecordInRaceData), textBox3.Text, "1", "Съем денег со счета пользователя");
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно средств на счету пользователя");
                    }
                }
                else
                {
                    if (radioButton1.Checked) // Добавление денег на счет пользователя
                    {
                        admin.model.Jurnal_AddToUserCash("3", Convert.ToInt32(idRecordInRaceData), textBox3.Text, "0", "Добавление денег на счет пользователя");
                        ret = true;
                    }
                    else
                    {
                        if (radioButton4.Checked) // Добавление виртуальных денег на счет пользователя
                        {
                            admin.model.Jurnal_UserCash("5",
                                Convert.ToInt32(idRecordInRaceData),
                                textBox3.Text, "0",
                                "Отказ от участия в рейсе. Перевод денег на счет пользователя",
                                Race.RaceID);
                            ret = true;
                        }
                    }
                }
            }

            if (ret)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
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

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            double Summ = 0;
            try
            {
                Summ = Double.Parse(textBox3.Text);
            }
            catch (Exception ex)
            {
                Summ = 0;
                string exm = ex.Message;
            }

            if (Summ <= 0 || (radioButton2.Checked && (Summ > MaxSum)))
                button2.Enabled = false;
            else button2.Enabled = true;

        }
    }
}
