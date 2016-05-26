using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
namespace Rentix
{
    public partial class AllPilots : Form
    {
        MainForm parent;
        PageLister Pages;
        string GroupFilter;
        
        public AllPilots(MainForm P)
        {
            InitializeComponent();
            parent = P;

            Pages = new PageLister(pageNumber_toolStripComboBox, firstPage_toolStripButton11, previousPage_toolStripButton12, nextPage_toolStripButton9, lastPage_toolStripButton10);


            if (parent.admin.IS_ADMIN)
            {

                deletePilot_toolStripButton2.Enabled = true;

            }

            if (parent.admin.IS_ADMIN || parent.admin.IS_SUPER_ADMIN)
            {
                exportPilots_toolStripButton1.Visible = true;
            }

            Pages.PageSize = 25;
            Pages.CurrentPageNumber = 1;
            // ShowAllPilots(dataGridView1);
            pageSize_toolStripComboBox2.SelectedIndex = pageSize_toolStripComboBox2.Items.IndexOf("25");

            ShowGroups();
        }

        private void ShowGroups()
        {
            groupFilter_toolStripComboBox.Items.Add("Любая");
            List<string> Groups = parent.admin.model.GetAllGroupsName();
            for (int i = 0; i < Groups.Count; i++)
            {
                groupFilter_toolStripComboBox.Items.Add(Groups[i]);
            }

            if (groupFilter_toolStripComboBox.Items.Count > 0)
            {
                groupFilter_toolStripComboBox.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // заполним грид со списком пилотов 
        private void ShowAllPilots(DataGridView dv)
        {
            dv.Rows.Clear();


            int gr = -1;

            if(groupFilter_toolStripComboBox.SelectedIndex > 0)
            {
                gr = parent.admin.model.GetGroupID(groupFilter_toolStripComboBox.Items[groupFilter_toolStripComboBox.SelectedIndex].ToString());
       
            }

            string filter = filter_TextBox.Text;
            
           // Pages.CurrentPageNumber = Pages.CurrentPageNumber;
            //ShowAllPilots(dataGridView1);

            Hashtable res = parent.admin.model.GetAllPilots(Pages, gr, filter, false);
            Hashtable row = new Hashtable();

          // dv.Visible = false;
            //dv.SuspendLayout();
                      
            for (int i = 0; i < res.Count; i++)
            {
                dv.Rows.Add();

                row = (Hashtable)res[i];

                dv.Rows[i].Cells[0].Value = row["id"].ToString();
                dv.Rows[i].Cells[1].Value = row["name"].ToString();
                dv.Rows[i].Cells[2].Value = row["surname"].ToString();
                dv.Rows[i].Cells[3].Value = row["nickname"].ToString();
                dv.Rows[i].Cells[4].Value = row["gender"].ToString().Contains("True") ? "м." : "ж.";
                dv.Rows[i].Cells[5].Value = row["email"].ToString();
                dv.Rows[i].Cells[6].Value = row["tel"].ToString();
                dv.Rows[i].Cells[7].Value = parent.admin.model.GetGroupName(row["gr"].ToString());
                dv.Rows[i].Cells[8].Value = Convert.ToDateTime(row["birthday"]).ToString("yyyy-MM-dd");
                dv.Rows[i].Cells[9].Value = Convert.ToDateTime(row["created"]).ToString("yyyy-MM-dd");
                dv.Rows[i].Cells[10].Value = Math.Round(parent.admin.model.GetUserBallans(Convert.ToInt32(row["id"].ToString())), 2).ToString() + " грн.";
                dv.Rows[i].Cells[11].Value = row["banned"];
                dv.Rows[i].Cells[12].Value = row["barcode"];
            }

          //  dv.Visible = true;
          //  dv.ResumeLayout();

           prepareCertificate_toolStripButton8.Enabled = ballanceCorrection_toolStripButton4.Enabled = pilotDetails_toolStripButton3.Enabled = editPilot_toolStripButton5.Enabled = button2.Enabled = pilotsList_dataGridView1.Rows.Count > 0;
        }

        // добавить пилота
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewPilot form = new NewPilot(parent.admin, -1, false);
            form.ShowDialog();
          //  form.Dispose();
            ShowAllPilots(pilotsList_dataGridView1);
        }

        // удалить пилота
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (pilotsList_dataGridView1.Rows.Count > 0)
            {
                parent.admin.model.DelPilot(pilotsList_dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (pilotsList_dataGridView1.Rows.Count > 0)
            {
                PilotInfo form = new PilotInfo(
                    Convert.ToInt32(
                    pilotsList_dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), parent.admin);
                form.Owner = this;
                form.ShowDialog();
               // form.Dispose();
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PilotInfo form = new PilotInfo(Convert.ToInt32(
                    pilotsList_dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), parent.admin);
                form.Owner = this;
                form.ShowDialog();
                form.Dispose();
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

        private void AllPilots_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CorrectBallance form = new CorrectBallance(parent.admin, 
                Int32.Parse(pilotsList_dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), false);
            form.ShowDialog();
           // form.Dispose();
            ShowAllPilots(pilotsList_dataGridView1);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (pilotsList_dataGridView1.Rows.Count > 0)
            {
                NewPilot form = new NewPilot(parent.admin, Convert.ToInt32(
                    pilotsList_dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), true);
                form.ShowDialog();
                form.Dispose();
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            DayStatistic form = new DayStatistic(parent.admin, 3);
            form.Owner = this;
            form.ShowDialog();
            form.Dispose();
            this.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            pilotDetails_toolStripButton3.PerformClick();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (Convert.ToBoolean(pilotsList_dataGridView1[11, e.RowIndex].Value))
                {
                    pilotsList_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 198, 198);
                }
            }
        }

        // Changes how cells are displayed depending on their columns and values.
        private void dataGridView1_CellFormatting(object sender,
            System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            // Set the background to red for negative values in the Balance column.
            if (e.RowIndex > -1 && Convert.ToBoolean(pilotsList_dataGridView1[11, e.RowIndex].Value))
            {
                pilotsList_dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 198, 198);
            }
        }


        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.ToPage(Convert.ToInt32(pageNumber_toolStripComboBox.Items[pageNumber_toolStripComboBox.SelectedIndex].ToString()));
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

        // первая страница
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {

                Pages.First();
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

        // страница назад
        private void toolStripButton12_Click(object sender, EventArgs e)
        {

            if (!Pages.OnUpdate)
            {
                Pages.Prev();
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

            if (!Pages.OnUpdate)
            {
                Pages.Next();
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

        // последняя страница
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.Last();
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }

       
        // изменён размер страницы
        private void pageSizeComboboxIndexChanged(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                if (pageSize_toolStripComboBox2.Items[pageSize_toolStripComboBox2.SelectedIndex].ToString() == "Все")
                {
                    Pages.PageSize = Int32.MaxValue;
                }
                else
                {
                    Pages.PageSize = Convert.ToInt32(pageSize_toolStripComboBox2.Items[pageSize_toolStripComboBox2.SelectedIndex].ToString());
                }
                Pages.OnChange = true;
                Pages.CurrentPageNumber = 1;
                ShowAllPilots(pilotsList_dataGridView1);
                Pages.FillPageNumbers();

               // if (Pages.CurrentPageNumber > pageNumber_toolStripComboBox.Items.Count) pageNumber_toolStripComboBox.SelectedIndex = Pages.PagesCount - 1;
            }
        }
               
        // выбрана группа
        private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
                if (groupFilter_toolStripComboBox.SelectedIndex > 0)
                {
                    //int gr = parent.admin.model.GetGroupID(groupFilter_toolStripComboBox.Items[groupFilter_toolStripComboBox.SelectedIndex].ToString());

              //      GroupFilter = " and gr='" + gr.ToString() + "'";
                    Pages.CurrentPageNumber = 1;
                    ShowAllPilots(pilotsList_dataGridView1);

                }
                else
                {
                    Pages.CurrentPageNumber = 1;
                    GroupFilter = String.Empty;
                    ShowAllPilots(pilotsList_dataGridView1);
                }
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            groupFilter_toolStripComboBox.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.admin.PrintDataGridView(pilotsList_dataGridView1, "Пилоты");
        }
        
        // применить фильтр
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (!Pages.OnUpdate)
            {
                Pages.CurrentPageNumber = 1;

                // Pages.SecondFilter = " and (surname like '%" + filter_TextBox.Text + "%' or name like '%" + filter_TextBox.Text + "%' or nickname like '%" + filter_TextBox.Text + "%' or email like '%" + filter_TextBox.Text + "%' or tel like '%" + filter_TextBox.Text + "%' or barcode like '%" + filter_TextBox.Text + "%') ";
                ShowAllPilots(pilotsList_dataGridView1);
            }
        }
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) applyFilter_button.PerformClick();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            GiftCertificate form = new GiftCertificate(parent.admin, false, Convert.ToInt32(
                    pilotsList_dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            form.ShowDialog();
            form.Dispose();
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (filter_TextBox.Text.Length == 13)
            {
                filter_TextBox.SelectAll();
                applyFilter_button.PerformClick();
            }
        }

        private void exportPilots_toolStripButton1_Click(object sender, EventArgs e)
        {
            int gr = -1;

            if (groupFilter_toolStripComboBox.SelectedIndex > 0)
            {
                gr = parent.admin.model.GetGroupID(groupFilter_toolStripComboBox.Items[groupFilter_toolStripComboBox.SelectedIndex].ToString());

            }

            ExportPilots exportPilotsForm = new ExportPilots(parent, gr, filter_TextBox.Text);
            exportPilotsForm.ShowDialog();
        }


    }
}
