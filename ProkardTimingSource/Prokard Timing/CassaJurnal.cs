using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rentix
{
    public partial class CassaJurnal : Form
    {
        AdminControl admin;
        int GlobalRadio = 1; // 1 = реальные, 2 = виртуальные операции. есть ещё 3, но не работает, наверное
        PageLister Pages;
        int idRaceForCellColor; // будем подкрашивать разным фоном разные рейсы
        bool isWhiteBackground; // будем чередовать серый / белый фон для разных рейсов
        int _race_id; // фильтр кассы на определенную гонку

        public CassaJurnal(AdminControl adm, int race_id = 0)
        {
            InitializeComponent();
            admin = adm;
            Pages = new PageLister(toolStripComboBox1, toolStripButton11, toolStripButton12, toolStripButton9, toolStripButton10);
            _race_id = race_id;
            Pages.PageSize = 25;
            Pages.CurrentPageNumber = 1;
            // ShowAllPilots(dataGridView1);
            pageSize_toolStripComboBox.SelectedIndex = pageSize_toolStripComboBox.Items.IndexOf("25");

           // admin.GetCassaReport(dataGridView1, DateTime.Now, GlobalRadio, DateTime.Now, Pages);
            inCash_label2.Text = "В кассе:  " + admin.model.GetCashFromCassa(DateTime.Now, false, false, true, true, race_id) + " грн.";
            GetCashData();
          //  dateTimePicker1.Value = DateTime.Now;
          //  dateTimePicker2.Value = DateTime.Now;
            button2.Enabled = cassa_dataGridView1.Rows.Count > 0;

            if (!admin.IS_ADMIN)
            {
                this.Close();
            }
        }

        private void GetCashData()
        {

            double Credet = admin.model.GetCassaSumm(datetimeConverter.toStartDateTime(dateTimePicker1.Value),
                datetimeConverter.toEndDateTime(dateTimePicker2.Value), GlobalRadio, 1, _race_id);
            double Debet = admin.model.GetCassaSumm(datetimeConverter.toStartDateTime(dateTimePicker1.Value),
                datetimeConverter.toEndDateTime(dateTimePicker2.Value), GlobalRadio, 0, _race_id);
            /*  double Dt = 0, Ct = 0, Sl = 0, Tmp = 0;

              for (int i = 0; i < dataGridView1.Rows.Count; i++)
              {
                  Tmp = Double.Parse(dataGridView1[3, i].Value.ToString());
                  if (Tmp < 0)
                      Ct += Math.Abs(Tmp);
                  else
                      Dt += Tmp;
              }

              Sl = Dt - Ct;
              */
            label5.Text = "Сальдо:  " + (Debet - Credet).ToString() + " грн";
            label3.Text = "Дт:  " + Debet.ToString() + " грн";
            label4.Text = "Кт:  " + Credet.ToString() + " грн";

            button2.Enabled = cassa_dataGridView1.Rows.Count > 0;
        }

        private void CassaJurnal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            
            if (cassa_dataGridView1[3, e.RowIndex].Value != null && Double.Parse(cassa_dataGridView1[3, e.RowIndex].Value.ToString()) < 0)
            {
                cassa_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 229, 235);
            }

            /* тоже не оправдала надежд, пришлось вставлять строку - разделитель
            bool isRacesDivider = false; // if true then need to draw bold border line

            if (cassa_dataGridView1[5, e.RowIndex].Value != null && Int16.Parse(cassa_dataGridView1[5, e.RowIndex].Value.ToString()) != idRaceForCellColor)
            {
                isRacesDivider = true;
                isWhiteBackground = !isWhiteBackground;
                idRaceForCellColor = Int16.Parse(cassa_dataGridView1[5, e.RowIndex].Value.ToString());
            }

            if (isRacesDivider)
            {
                DataGridViewRow dividerRow = new DataGridViewRow();
                 dividerRow.DefaultCellStyle.BackColor = Color.Black;
                 dividerRow.Height = 2;
            
                this.cassa_dataGridView1.CellPainting -= new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            
                cassa_dataGridView1.Rows.Add(dividerRow);

                this.cassa_dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            
            }

             * /

            /* рекурсия получается

            if (e.RowIndex > -1)
            {
                if (isWhiteBackground)
                {
                    cassa_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    cassa_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                }
            }

            if (e.RowIndex >= 0 && Double.Parse(cassa_dataGridView1[3, e.RowIndex].Value.ToString()) < 0)
            {
                cassa_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 229, 235);
            }
             */
        }

        // изменён диапазон дат
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, GlobalRadio, dateTimePicker2.Value, Pages);
            GetCashData();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, 1, dateTimePicker2.Value, Pages);
            GlobalRadio = 1;
            GetCashData();
          
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, 2, dateTimePicker2.Value, Pages);
            GlobalRadio = 2;
            GetCashData();
          
        }

        // sgavrilenko опция не используется (visible = false)
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, 3, dateTimePicker2.Value, Pages);
            GlobalRadio = 3;
            GetCashData();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin.PrintDataGridView(cassa_dataGridView1, "Кассовая книга - " + dateTimePicker1.Value.ToString("yyyy-MM-dd") + " - " + dateTimePicker2.Value.ToString("yyyy-MM-dd"));
        }


        // выбрана другая страница в списке страниц
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex > 0)
            {
                if (!Pages.OnUpdate)
                {
                    Pages.ToPage(Convert.ToInt32(toolStripComboBox1.Items[toolStripComboBox1.SelectedIndex].ToString()));
                    // ShowAllPilots(dataGridView1);
                    admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, GlobalRadio, dateTimePicker2.Value, Pages);

                }
            }
        }

        // первая страница
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {

                Pages.First();
                //ShowAllPilots(dataGridView1);
                admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, GlobalRadio, dateTimePicker2.Value, Pages);

            }
        }

        // предыдущая страница
        private void toolStripButton12_Click(object sender, EventArgs e)
        {

            if (!Pages.OnUpdate)
            {
                Pages.Prev();
                //ShowAllPilots(dataGridView1);
                admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, GlobalRadio, dateTimePicker2.Value, Pages);

            }
        }

        // следующая страница
        private void toolStripButton9_Click(object sender, EventArgs e)
        {

            if (!Pages.OnUpdate)
            {
                Pages.Next();
                //ShowAllPilots(dataGridView1);
                admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, GlobalRadio, dateTimePicker2.Value, Pages);

            }
        }

        // последняя страница
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.Last();
                //ShowAllPilots(dataGridView1);
                admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, GlobalRadio, dateTimePicker2.Value, Pages);

            }
        }

        private void toolStripComboBox1_Click_1(object sender, EventArgs e)
        {

        }

        // изменён размер страницы
        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.CurrentPageNumber = 1;
               // MessageBox.Show("TODO переделать на stored proc чтобы работал pagelister ");

                if (pageSize_toolStripComboBox.Items[pageSize_toolStripComboBox.SelectedIndex].ToString() == "Все")
                {
                    Pages.PageSize = Int32.MaxValue;
                }
                else
                {
                    Pages.PageSize = Convert.ToInt32(pageSize_toolStripComboBox.Items[pageSize_toolStripComboBox.SelectedIndex].ToString());
                }
                
                Pages.OnChange = true;
                //ShowAllPilots(dataGridView1);
                admin.GetCassaReport(cassa_dataGridView1, dateTimePicker1.Value, GlobalRadio, dateTimePicker2.Value, Pages, _race_id);

                Pages.FillPageNumbers();

                /*
                if (Pages.CurrentPageNumber < toolStripComboBox1.Items.Count)
                {
                    toolStripComboBox1.SelectedIndex = Pages.PagesCount - 1;
                }
                 */
            }
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
