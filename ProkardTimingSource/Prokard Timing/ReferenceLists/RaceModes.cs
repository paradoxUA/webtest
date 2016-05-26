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
    public partial class RaceModes : Form
    {
        AdminControl admin;

        public RaceModes(AdminControl admin)
        {
            InitializeComponent();
            this.admin = admin;
            fillRaceModes();
        }

        private void fillRaceModes()
        {
            raceModes_dataGridView1.Rows.Clear();

            List<Hashtable> data = admin.model.GetAllRaceModes("");

            for (int i = 0; i < data.Count; i++)
            {
                raceModes_dataGridView1.Rows.Add();
                raceModes_dataGridView1[0, i].Value = data[i]["id"];
                raceModes_dataGridView1[1, i].Value = data[i]["name"];
                raceModes_dataGridView1[2, i].Value = data[i]["length"];
                if (data[i]["is_deleted"].ToString().Length > 0 && data[i]["is_deleted"].ToString().ToLower() == "true")
                {
                    raceModes_dataGridView1[3, i].Value = "да";
                }
                else
                {
                     raceModes_dataGridView1[3, i].Value = "нет";
                }
            }

        }




        private void addRaceMode_toolStripButton_Click(object sender, EventArgs e)
        {
            AddRaceMode addRace = new AddRaceMode(admin, false);
            DialogResult dr = addRace.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                fillRaceModes();
            }

            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteRaceMode_toolStripButton2_Click(object sender, EventArgs e)
        {
            if (raceModes_dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            bool isDeleted = admin.model.DelRaceMode(Convert.ToInt16(
                raceModes_dataGridView1.SelectedRows[0].Cells[0].Value));

            if (isDeleted == false)
            {
                admin.model.markRaceModeAsDeleted(Convert.ToInt16(
                raceModes_dataGridView1.SelectedRows[0].Cells[0].Value));
                MessageBox.Show("Невозможно удалить запись, так как на неё имеются ссылки в других таблицах. Однако, запись будет помечена как 'удалённая' и будет скрыта во всех списках");
            }         
                fillRaceModes();
        }

        private void editRaceMode_toolStripButton3_Click(object sender, EventArgs e)
        {
            if (raceModes_dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            Int16 idRaceMode = Convert.ToInt16(
                raceModes_dataGridView1.SelectedRows[0].Cells[0].Value);


            AddRaceMode addRace = new AddRaceMode(admin, true, idRaceMode);
            DialogResult dr = addRace.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                fillRaceModes();
            }

           
        }
    }
}
