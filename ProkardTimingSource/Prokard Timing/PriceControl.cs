using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Input;
using Rentix.Controls;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace Rentix
{
    public partial class PriceControl : Form
    {
        MainForm parent;
        int WeekNumber = 1;
        comboBoxItem ci = new comboBoxItem("", -1); // список режимов (нестандартный контрол)

        public PriceControl(MainForm P)
        {
            InitializeComponent();
            parent = P;

            // режимы заезда
            fillRaceModes();

            // режимы заезда
            fillGroups();

            CreateWeekGreed(dataGridView1);
            UnselectAllCells();
            
            if (dataGridView1.Rows.Count > DateTime.Now.Hour)
            {
                dataGridView1.Rows[DateTime.Now.Hour].Selected = true;
            }
           

            if (parent.admin.IS_ADMIN)
            {

                dataGridView1.ReadOnly = false;
                textBox2.Enabled = button2.Enabled = true;
            }
        }

        private void fillGroups()
        {
            List<Hashtable> data = parent.admin.model.GetAllGroups();

        var dict = new Dictionary<int, string>();

           for (int i = 0; i < data.Count; i++)
           {
               dict.Add(Convert.ToInt32(data[i]["id"]), data[i]["name"].ToString());

           }

        comboBox1.DataSource = new BindingSource(dict, null);
        comboBox1.DisplayMember = "Value";
        comboBox1.ValueMember = "Key";
        }


        private void fillRaceModes()
        {            
           List<Hashtable> data = parent.admin.model.GetAllRaceModes(" and is_deleted <> 1 ");

           for (int i = 0; i < data.Count; i++)
           {
               comboBoxItem someItem =
                   new comboBoxItem(Convert.ToString(data[i]["name"]),
                   Convert.ToInt32(data[i]["id"]));

               raceMode_comboBox1.Items.Add(someItem);
           }

           comboBoxItem ci = new comboBoxItem("", -1);
			comboBoxItem.selectComboBoxValueById(raceMode_comboBox1, Convert.ToInt32(parent.admin.Settings["default_race_mode_id"]));

        }

        private void PriceControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateWeekGreed(DataGridView dv)
        {
            dv.Rows.Clear();

            if(comboBoxItem.getSelectedValue(raceMode_comboBox1) == -1)
            {
                MessageBox.Show(@"Выберите тип заезда.");
                return;
            }

            int idRaceMode = comboBoxItem.getSelectedValue(raceMode_comboBox1);
            int idGroup = 1;
            if (comboBox1.SelectedItem != null)
            {
                KeyValuePair<int, string> item = (KeyValuePair<int, string>) comboBox1.SelectedItem;
                if (item.Key == 0)
                {
                    MessageBox.Show(@"Выберите группу.");
                    return;
                }

            }
            if (comboBox1.SelectedItem != null)
            {
                KeyValuePair<int, string> item = (KeyValuePair<int, string>)comboBox1.SelectedItem;
                idGroup = item.Key;
            }
            for (int i = 0; i < 24; i++)
            {

                dv.Rows.Add();
                if (checkBox1.Checked && (i < Convert.ToInt32(parent.admin.Settings["time_start"]) || i > Convert.ToInt32(parent.admin.Settings["time_end"]))) dv.Rows[i].Visible = false;
                else
                {
                    dv.Rows[i].Height = 30;
                    dv.Rows[i].HeaderCell.Value = i.ToString() + ":00";
                }
            }

            for (int i = 0; i < 7; i++)
                for (int j = 0; j < dv.Rows.Count; j++)
                {
                    dv[i, j].Value = parent.admin.GetPrice(i + 1, j, idRaceMode, idGroup);
                }

        }

        private void UnselectAllCells()
        {
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    dataGridView1[i, j].Selected = false;

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UnselectAllCells();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1[e.ColumnIndex, i].Selected = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.GetCellCount(DataGridViewElementStates.Selected); i++)
            {
                dataGridView1.SelectedCells[i].Value = textBox2.Text;
            }
            button3.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CreateWeekGreed(dataGridView1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CreateWeekGreed(dataGridView1);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CreateWeekGreed(dataGridView1);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            CreateWeekGreed(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            int idSelectedRaceMode = comboBoxItem.getSelectedValue(raceMode_comboBox1);

            if(idSelectedRaceMode == -1)
            {
                MessageBox.Show(@"Необходимо выбрать режим");
                return;
            }
            int idGroup = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            if (idGroup == 0)
            {
                MessageBox.Show(@"Необходимо выбрать группу");
                return;
            }
            parent.admin.SavePrices(dataGridView1, WeekNumber, idSelectedRaceMode, idGroup);
            button3.Enabled = false;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 5 || e.ColumnIndex == 6))
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromArgb(230, 230, 230);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CreateWeekGreed(dataGridView1);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            button3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parent.admin.PrintDataGridView(dataGridView1, "Цены");
        }

        private void raceMode_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateWeekGreed(dataGridView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateWeekGreed(dataGridView1);
        }
    }
}
