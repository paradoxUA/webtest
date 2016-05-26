using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
namespace Rentix
{
    public partial class PilotInfo : Form
    {
        private int PilotID;
        AdminControl admin;
        public PilotInfo(int ID, AdminControl ad)
        {
            PilotID = ID;
            InitializeComponent();
            admin = ad;
            ShowInfoFromPilot();


            textBox1.Enabled = button5.Enabled = admin.IS_ADMIN;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PilotInfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        // Коррекция баланса
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CorrectBallance form = new CorrectBallance(admin,Convert.ToInt32(PilotID), false);
            form.Owner = this;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                labelSmooth18.Text = Math.Round(admin.model.GetUserBallans(PilotID), 3).ToString() + " грн";
            }
            form.Dispose();
        }


        private void ShowInfoFromPilot()
        {
            Hashtable User = admin.model.GetPilot(Convert.ToInt32(PilotID));

            if (User == null || User.Count == 0)
            {
                return;
            }

            // Информация
            labelSmooth13.Text = User["name"].ToString();
            labelSmooth15.Text = User["surname"].ToString();
            labelSmooth17.Text = User["nickname"].ToString();
            labelSmooth14.Text = admin.model.GetGroupName(User["gr"].ToString());
            labelSmooth16.Text = Convert.ToDateTime(User["birthday"]).ToString("dd MMMM yyyy");
            labelSmooth11.Text = (admin.model.GetGroupSale(User["gr"].ToString())).ToString() + "%";


            // Статистика
            labelSmooth18.Text = Math.Round(admin.model.GetUserBallans(PilotID), 3).ToString() + " грн";
            labelSmooth19.Text = admin.model.GetPilotRaceCount(PilotID, false).ToString();

            List<string> BestRes = admin.model.GetPilotBestResult(PilotID);
            labelSmooth21.Text = BestRes[0];
            labelSmooth23.Text = BestRes[1];

            labelSmooth20.Text = User["barcode"].ToString().Length == 0 ? "Карта не выдана" : User["barcode"].ToString();
            // Бан
            button4.Enabled = Convert.ToBoolean(User["banned"]);
            button3.Enabled = !Convert.ToBoolean(User["banned"]);

            // Текстовые сообщения
            richTextBox1.Text = String.Empty;
            richTextBox1.Text += admin.model.GetAllPilotsMessages(PilotID);

        }

        // Активировать
        private void button4_Click(object sender, EventArgs e)
        {

            admin.model.ActivatePilot(PilotID, "0", DateTime.Now, richTextBox2.Text);
            richTextBox2.Text = String.Empty;
            button3.Enabled = true;
            button4.Enabled = false;
            richTextBox1.Text = String.Empty;
            richTextBox1.Text += admin.model.GetAllPilotsMessages(PilotID);
        }

        // Забанить
        private void button3_Click(object sender, EventArgs e)
        {
            admin.model.ActivatePilot(PilotID, "1", DateTime.Now, richTextBox2.Text);
            richTextBox2.Text = String.Empty;
            button3.Enabled = false;
            button4.Enabled = true;
            richTextBox1.Text = String.Empty;
            richTextBox1.Text += admin.model.GetAllPilotsMessages(PilotID);
        }

        // Добавить сообщение
        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text.Trim().Length == 0)
            {
                MessageBox.Show("Необходимо указать текст заметки");
                return;
            }

            admin.model.AddMessage(PilotID, 1, 0, DateTime.Now, richTextBox2.Text);
            richTextBox2.Text = String.Empty;
            richTextBox1.Text = String.Empty;
            richTextBox1.Text = admin.model.GetAllPilotsMessages(PilotID);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            GiftCertificate form = new GiftCertificate(admin,false,PilotID);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            NewPilot form = new NewPilot(admin, PilotID, true);
            form.ShowDialog();
            form.Dispose();
            ShowInfoFromPilot();
        }
        
        private bool IsNumeric(string strTextEntry)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strTextEntry)
                 && (strTextEntry != "");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (IsNumeric(textBox1.Text))
            {
                string s = admin.model.DelRaceDataTimes(textBox1.Text,PilotID);

                MessageBox.Show(s); 
                textBox1.Text = String.Empty;
            }
            else MessageBox.Show("Не корректный ID заезда");
        }
    }
}
