using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DateTimeExtensions;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Collections.Generic;
using Rentix.Controls;


namespace Rentix
{

 

    public partial class AddPilot : Form
    {

        comboBoxItem ci = new comboBoxItem(null, -1);

        RaceClass CurrRace;
        AdminControl admin;
        bool PressControlKey = false;
        string filter = "";
        string BarCode = String.Empty;
        bool FromCertificate = false;
        int SelectPilot = -1;
        CheckPrintData Data = new CheckPrintData();
       
        PilotFilter PF = new PilotFilter();

        bool OnCertif = false;

        public AddPilot(RaceClass Race, AdminControl ad)
        {
            InitializeComponent();

            CurrRace = Race;
            admin = ad;

            foundPilots_dataGridView3.DoubleBuffered(true);

            label3.Text = (CurrRace.RaceNum > ad.DopRace ? (CurrRace.RaceNum - ad.DopRace).ToString() + "a" : CurrRace.RaceNum.ToString());
            label5.Text = CurrRace.Date.ToString("dd MMMM");
            label4.Text = "Отправление в " + CurrRace.Hour + ":" + CurrRace.Minute;
            ad.ShowRacePilots(CurrRace.RaceID, otherRacers_dataGridView2); // Показать всех пилотов рейса

            if (foundPilots_dataGridView3.Rows.Count > 0) foundPilots_dataGridView3.Rows[0].Selected = false;

            //filter = PilotFilter();
            //Admin.ShowPilots(dataGridView3, filter); // Показать всех пилотов

            ad.ShowRaceKarts(comboBox1, CurrRace);
            label2.Visible = comboBox1.Visible = comboBox1.Items.Count > 1;

            // Скрываем форму с пилотами
            borderPanel2.Visible = toolStripButton3.Checked = otherRacers_dataGridView2.RowCount > 0;
            label12.Text = otherRacers_dataGridView2.RowCount.ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            toolStripButton4.PerformClick();
            ShowPilotName();

            checkBox2.Text = "Участник квалификации " + DateTime.Now.Last(DayOfWeek.Sunday).ToString("dd MMMM");
        }

        // Create new Pilot
        private void button1_Click(object sender, EventArgs e)
        {
            Hashtable data = new Hashtable();

            data["name"] = this.textBox2.Text;
            data["surname"] = this.textBox1.Text;
            data["nickname"] = this.textBox3.Text;
            data["birthday"] = "1971-01-01";
            data["gender"] = 0;
            data["email"] = this.textBox4.Text;
            data["tel"] = this.textBox5.Text;
            data["group"] = 0;
            admin.model.AddNewPilot(data);
            admin.ShowPilots(foundPilots_dataGridView3, getExistsPilots(), filter + PilotFilter());

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            button1.Enabled = foundPilots_dataGridView3.Rows.Count == 0;
            button6.Enabled = foundPilots_dataGridView3.Rows.Count > 0 && FromCertificate;
        }



        // Кнопка добавления пилота в рейс
        private void button2_Click(object sender, EventArgs e)
        {


            if (foundPilots_dataGridView3.Rows.Count > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                int pilot_id = Convert.ToInt32(foundPilots_dataGridView3.SelectedRows[0].Cells[0].Value ?? 0);

                //this.Visible = false;
                //if (this.Owner != null) this.Owner.Activate();
                bool ret = false;

                double paidAmount = 0;
                   int discountCardId = -1;
                   int idRaceMode = Convert.ToInt32(admin.Settings["default_race_mode_id"]);

                if (!checkBox1.Checked)
                {
                    // форма принятия денег
                    CashOperations form = new CashOperations(pilot_id, CurrRace, admin);
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (form.discountCard != null)
                        {
                            discountCardId = form.discountCard.Id;
                        }
                        ret = true;
                        paidAmount = Convert.ToDouble(form.priceForCurrentRace_textBox5.Text);
                        idRaceMode = ci.getSelectedValue(form.halfModes_comboBox);
                    }
                }
                else
                {
                    ret = true;
                }

                if (ret)
                {

                    int MonthRace = checkBox2.Checked ? 1 : 0;
                    int Reserv = checkBox1.Checked ? 1 : 0;
                    int Kart = comboBox1.SelectedIndex > 0 ? admin.model.GetKartID(comboBox1.Items[comboBox1.SelectedIndex].ToString()) : 0;
                    if (comboBox1.SelectedIndex > 0)
                    {
                        CurrRace.Karts.Add(comboBox1.Items[comboBox1.SelectedIndex].ToString());
                    }
                                                    

                    admin.model.AddPilotToRace(pilot_id, CurrRace.RaceID,   
                        CurrRace.Light_mode == 1, paidAmount, discountCardId, idRaceMode,
                        MonthRace, Reserv, Kart);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;


                }

                this.Close();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            borderPanel2.Visible = !borderPanel2.Visible;
            toolStripButton3.Checked = borderPanel2.Visible;
        }

        // вернуть список уже добавленных пилотов в заезд 
        private List<int> getExistsPilots()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < otherRacers_dataGridView2.Rows.Count; i++)
            {
                result.Add(Convert.ToInt32(otherRacers_dataGridView2.Rows[i].Cells[1].Value));
            }

            return result;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text.Length >= 3 || textBox2.Text.Length >= 3 || textBox3.Text.Length >= 3 || textBox5.Text.Length >= 5)
            {
                
                filter = " and (surname like '%" + textBox1.Text + "%' and name like '%" + textBox2.Text + "%' and nickname like '%" + textBox3.Text + "%' and email like '%" + textBox4.Text + "%' and barcode like '%" + textBox5.Text + "%') " + PilotFilter();


                admin.ShowPilots(foundPilots_dataGridView3, getExistsPilots(), filter);
                button1.Enabled = foundPilots_dataGridView3.Rows.Count == 0;
                button6.Enabled = foundPilots_dataGridView3.Rows.Count > 0 && FromCertificate;
                ShowPilotName();
            }
        }



        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (otherRacers_dataGridView2[3, e.RowIndex].Value == null)
                {
                    otherRacers_dataGridView2[3, e.RowIndex].Value = "0";
                }

                if (otherRacers_dataGridView2[5, e.RowIndex].Value.Equals("True"))
                {
                    otherRacers_dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 253, 145);
                }

                if (otherRacers_dataGridView2[3, e.RowIndex].Value.Equals("0")) // без картинга
                {
                    otherRacers_dataGridView2.Rows[e.RowIndex].Cells[3].Value = "";
                    otherRacers_dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 196, 178);  //  Color.Tomato; // Color.FromArgb(255, 196, 178);
                }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            toolStripButton6.BackColor = checkBox1.BackColor = checkBox1.Checked ? Color.Yellow : Color.Transparent;
            toolStripButton6.Checked = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            toolStripButton7.BackColor = checkBox2.BackColor = checkBox2.Checked ? Color.SkyBlue : Color.Transparent;
            toolStripButton7.Checked = checkBox2.Checked;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!toolStripButton4.Checked)
            {
                borderPanel4.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.None;
                borderPanel4.BorderStyle = ButtonBorderStyle.None;
                borderPanel4.Refresh();
                panel2.Visible = false;
                borderPanel5.Dock = DockStyle.Fill;


            }
            else
            {
                borderPanel4.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Bottom;
                borderPanel4.BorderStyle = ButtonBorderStyle.Solid;
                borderPanel4.Refresh();
                borderPanel5.Dock = DockStyle.Bottom;
                panel2.Visible = true;
            }

            toolStripButton2.Visible = !toolStripButton4.Checked;
        }


        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {

            if (toolStripTextBox1.Text != "Имя \\Фамилия \\Никнейм \\Штрих код" && toolStripTextBox1.Text.Length >= 3)
            {


                string filter;
                if (toolStripTextBox1.Text.Length > 0)
                {
                    filter = toolStripTextBox1.Text;
                }
                else
                {
                     filter = "";
                }

                   // filter = " and (surname like '%" + toolStripTextBox1.Text + "%' or name like '%" + toolStripTextBox1.Text + "%' or nickname like '%" + toolStripTextBox1.Text + "%' or email like '%" + toolStripTextBox1.Text + "%' or barcode ='" + toolStripTextBox1.Text + "') ";
               

                admin.ShowPilots(foundPilots_dataGridView3, getExistsPilots(), filter + PilotFilter());
                button1.Enabled = foundPilots_dataGridView3.Rows.Count == 0;
                button6.Enabled = foundPilots_dataGridView3.Rows.Count > 0 && FromCertificate;
                ShowPilotName();
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowPilotName();
            SelectPilot = e.RowIndex;
        }

        private void ShowPilotName()
        {
            if (foundPilots_dataGridView3.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                label13.Text = "Пилот - " + foundPilots_dataGridView3.SelectedRows[0].Cells[2].Value + "  " + foundPilots_dataGridView3.SelectedRows[0].Cells[1].Value + " [" + foundPilots_dataGridView3.SelectedRows[0].Cells[3].Value + "]";
                button2.Enabled = foundPilots_dataGridView3.Rows.Count > 0 && !Convert.ToBoolean(foundPilots_dataGridView3.SelectedRows[0].Cells[6].Value);

                if (Convert.ToBoolean(foundPilots_dataGridView3.SelectedRows[0].Cells[6].Value))
                {

                    PilotInfo form = new PilotInfo(Convert.ToInt32(foundPilots_dataGridView3.SelectedRows[0].Cells[0].Value.ToString()), admin);
                    form.Owner = this;
                    form.ShowDialog();
                    form.Dispose();
                    admin.ShowPilots(foundPilots_dataGridView3, getExistsPilots(), filter);

                }
            }
            else
            {
                label13.Text = "Пилот не выбран";
            }


        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (button2.Enabled)
                button2.PerformClick();
        }


        private string PilotFilter()
        {
            string ret = String.Empty;
            // TODO починить, чтобы нельзя было повторно посадить того же пилота на этот рейс
            return ret;

            for (int i = 0; i < otherRacers_dataGridView2.Rows.Count; i++)
            {
                ret += " and id!='" + otherRacers_dataGridView2[1, i].Value.ToString() + "' ";

            }

            return ret;
        }

        private void AddPilot_KeyUp(object sender, KeyEventArgs e)
        {
            PressControlKey = false;
            if (e.KeyValue == 27) { PressControlKey = true; this.Close(); }

            if (e.KeyCode == Keys.F1) toolStripButton4.PerformClick();
            if (e.KeyCode == Keys.F2) toolStripButton3.PerformClick();
            if (e.KeyCode == Keys.F3) toolStripButton8.PerformClick();

            if (e.KeyCode == Keys.Insert)
            {
                PressControlKey = true;
                if (toolStripButton4.Checked) button5.PerformClick();
                else
                    toolStripButton2.PerformClick();
            }
            button2.Enabled = foundPilots_dataGridView3.Rows.Count > 0;
            button6.Enabled = foundPilots_dataGridView3.Rows.Count > 0 && FromCertificate;
            if (e.KeyCode == Keys.Enter && foundPilots_dataGridView3.Rows.Count > 0)
            {
                if (OnCertif) button6.PerformClick();
              //  else button2.PerformClick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = String.Empty;
            comboBox1.SelectedIndex = 0;
            admin.ShowPilots(foundPilots_dataGridView3,  getExistsPilots(), PilotFilter());
            button1.Enabled = false;
            checkBox1.Checked = checkBox2.Checked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            NewPilot form = new NewPilot(admin, PF, textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            form.Owner = this;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filter = PF.Filter;
                admin.ShowPilots(foundPilots_dataGridView3, getExistsPilots(), filter + PilotFilter());
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.SelectAll();
        }

        private void AddPilot_Activated(object sender, EventArgs e)
        {
            toolStripTextBox1.Control.Select();
            toolStripTextBox1.SelectAll();

        }

        private void AddPilot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (PressControlKey)

                e.Handled = true;


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NewPilot form = new NewPilot(admin, PF, textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            form.Owner = this;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filter = PF.Filter;
                admin.ShowPilots(foundPilots_dataGridView3, getExistsPilots(), filter + PilotFilter());
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            ShowPilotName();
        }

        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button2.PerformClick();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (foundPilots_dataGridView3.Rows.Count > 0)
            {
                PilotInfo form = new PilotInfo(Convert.ToInt32(
                    foundPilots_dataGridView3.SelectedRows[0].Cells[0].Value.ToString()), admin);
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
                admin.ShowPilots(foundPilots_dataGridView3, getExistsPilots(), filter);
            }
        }

        private void dataGridView3_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                if (Convert.ToBoolean(foundPilots_dataGridView3[6, e.RowIndex].Value))
                {
                    foundPilots_dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 198, 198);
                }


            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = toolStripButton6.Checked;
            toolStripButton6.BackColor = checkBox1.BackColor = checkBox1.Checked ? Color.Yellow : Color.Transparent;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = toolStripButton7.Checked;
            toolStripButton7.BackColor = checkBox2.BackColor = checkBox2.Checked ? Color.SkyBlue : Color.Transparent;

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            borderPanel6.Visible = !borderPanel6.Visible;
            toolStripButton8.Checked = borderPanel6.Visible;
            if (borderPanel6.Visible)
            {
                textBox6.Select();
                textBox6.SelectAll();
            }
            else
            {
                toolStripTextBox1.Control.Select();
                toolStripTextBox1.Select();
                toolStripTextBox1.SelectAll();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            button8.Enabled = textBox6.Text.Length > 9;
            if (textBox6.Text.Length < 9)
                labelSmooth2.Text = "Информация отсутствует";

            if (textBox6.Text.Length >= 13) button8.PerformClick();
            if (textBox6.Text.Length == 13) textBox6.SelectAll();
        }

        bool IsCertificateSale = false;
        double Sale = 0;

        private void button8_Click(object sender, EventArgs e)
        {
            Hashtable Certificate = admin.model.GetCertificate(textBox6.Text.Trim());
            BarCode = String.Empty;
            labelSmooth3.Text = "Информация отсутствует";
            FromCertificate = false;
            if (Certificate.Count == 0)
            {
                textBox6.SelectAll();
                button6.Enabled = false;
                labelSmooth2.Text = "Сертификат не найден";
                FromCertificate = false;

            }
            else
            {
                Hashtable ct = admin.model.GetCertificateType(Certificate["c_id"].ToString());

                if (Convert.ToBoolean(ct["c_type"]))
                {

                    IsCertificateSale = true;
                    Sale = Double.Parse(ct["nominal"].ToString());
                }
                else
                {
                    IsCertificateSale = false;
                    Sale = 0;
                }

                labelSmooth2.Text = "Тип - " + ct["name"].ToString() + "    |    Заездов - " + Certificate["count"].ToString() + "    |    Статус - ";

                string Stat = Convert.ToBoolean(Certificate["active"]) ? "Активен" : "Использован";

                if (Convert.ToDateTime(Certificate["date_end"]).Ticks < DateTime.Now.Ticks && Convert.ToBoolean(Certificate["active"]))
                {
                    Stat = "Просрочен";
                    FromCertificate = false;
                    admin.model.ActivateCertificate(textBox1.Text.Trim(), "0");
                }
                else
                {
                    if (Convert.ToBoolean(Certificate["active"]))
                    {
                        FromCertificate = true;
                        this.AcceptButton = button6;
                        BarCode = textBox6.Text.Trim();
                        int uid = Convert.ToInt32(Certificate["user_id"]);
                        if (uid > 0)
                        {
                            filter = " and id = " + uid.ToString() + " " + PilotFilter();
                            admin.ShowPilots(foundPilots_dataGridView3, getExistsPilots(), filter);
                            Hashtable User = admin.model.GetPilot(uid);
                            string PName = User["name"] + " " + User["surname"];
                            if (PName.Length < 3) PName = User["nickname"].ToString();
                       
                            labelSmooth3.Text = "Пилот - " + PName;
                        }
                        else
                        {
                            labelSmooth3.Text = "Сертификат годен для любого пилота";
                        }
                    }
                }

                labelSmooth2.Text += Stat;
                button6.Enabled = foundPilots_dataGridView3.Rows.Count > 0 && FromCertificate;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (BarCode.Length > 6)
            {
                Hashtable Certificate = admin.model.GetCertificate(BarCode);

                if (Certificate.Count > 0)
                {
                    int pilot_id = 0;
                    if (Convert.ToInt32(Certificate["user_id"]) == 0)
                        pilot_id = Convert.ToInt32(foundPilots_dataGridView3.SelectedRows[0].Cells[0].Value);
                    else
                        pilot_id = Convert.ToInt32(Certificate["user_id"]);

                    double paidAmount = 0;
                    CashOperations form = new CashOperations(pilot_id, CurrRace, admin, Sale);
                  
                    if ((pilot_id > 0 && !IsCertificateSale) || (IsCertificateSale && form.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                    {
                        paidAmount = Convert.ToDouble(form.priceForCurrentRace_textBox5.Text);
                        int MonthRace = checkBox2.Checked ? 1 : 0;
                        int Reserv = checkBox1.Checked ? 1 : 0;
                        int Kart = comboBox1.SelectedIndex > 0 ? admin.model.GetKartID(comboBox1.Items[comboBox1.SelectedIndex].ToString()) : 0;
                        if (comboBox1.SelectedIndex > 0)
                        {
                            CurrRace.Karts.Add(comboBox1.Items[comboBox1.SelectedIndex].ToString());
                        }
                        admin.model.AddPilotToRace(pilot_id, CurrRace.RaceID, 
                            CurrRace.Light_mode == 1, paidAmount, MonthRace, Reserv, Kart);

                        admin.model.DelRaceFromCertificate(textBox6.Text.Trim(), (Convert.ToInt32(Certificate["count"]) - 1));
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        
                        if (Convert.ToBoolean(admin.Settings["print_check"]) && !IsCertificateSale)
                        {
                            print_check(pilot_id, CurrRace, (Convert.ToInt32(Certificate["count"]) - 1));
							
							try
							{
								printDocument1.Print();
							}
							catch (Exception ex)
							{
								MessageBox.Show("Не удалось распечатать документ. Ошибка: " + ex.Message);
							}
							// printPreviewDialog1.ShowDialog();
      
                        }
                        this.Close();
                    }
                }
            }
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.SelectAll();
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.SelectAll();
        }

        bool toIngnore = true;
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 110 || e.KeyValue == 220) toIngnore = true; else toIngnore = false;

            if (!borderPanel6.Visible || !FromCertificate) this.AcceptButton = button2;
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (toIngnore) e.Handled = true;
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 110 || e.KeyValue == 220) toIngnore = true; else toIngnore = false;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (toIngnore) e.Handled = true;

        }

        private void borderPanel6_Leave(object sender, EventArgs e)
        {
            if (!FromCertificate || !borderPanel6.Visible)
            {
                OnCertif = false;
                this.AcceptButton = button2;
            }
        }

        private void borderPanel6_Enter(object sender, EventArgs e)
        {
            OnCertif = true;
            this.AcceptButton = button6;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            CreateCheck(e, Data);
      
        }

        private void print_check(int PilotID, RaceClass Race, int RCount)
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

            Hashtable User = admin.model.GetPilot(PilotID);
        

            Data.PilotID = User["id"].ToString();
            Data.Name = User["name"].ToString();
            Data.SurName = User["surname"].ToString();
            Data.Sum = RCount.ToString() + " заездов";
            Data.RaceTime = Race.Hour + ":" + Race.Minute;
            Data.RaceDate = new DateTime(Race.Date.Year, Race.Date.Month, Race.Date.Day, 0, 0, 0);
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
                e.Graphics.DrawString("Участвует как:  " + Data.NickName, f1, Brushes.Black, new Rectangle(0, 45 + 3, W, 20), drawFormat);

                e.Graphics.DrawString("Дата старта:", f1, Brushes.Black, new Rectangle(0, 60 + 3, W, 20), drawFormat);

                TW = W - datetimeConverter.toDateString(Data.RaceDate).Length;
                //e.Graphics.DrawString(Data.RaceDate, f1, Brushes.Black, TW, SP.Y + YStep * 3);
            }

            using (Font f1 = new Font("Calibri", 12))
            {

                e.Graphics.DrawString(Data.RaceDate + " в " + Data.RaceTime, f1, Brushes.Black, new Rectangle(0, 75 + 3, W, 20), drawFormat);
            }

            using (Font f1 = new Font("Calibri", 9))
            {
                e.Graphics.DrawString("--------------------------------------------------------------", f1, Brushes.Black, new Rectangle(0, 90 + 3, W, 20), drawFormat);
                e.Graphics.DrawString(datetimeConverter.toDateTimeString(DateTime.Now), f1, Brushes.Black, new Rectangle(0, 105 + 3, W, 20), drawFormat);
            }
        }

     


    }



}
