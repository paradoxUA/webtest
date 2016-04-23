using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Prokard_Timing
{
    public partial class AddRaceMode : Form
    {
        AdminControl admin;
        Int16 idCurrentMode;
        bool isEdit;

        public AddRaceMode(AdminControl admin, bool isEdit, Int16 raceModeId = -1)
        {
            InitializeComponent();
            this.admin = admin;
            this.isEdit = isEdit;

            if (isEdit && raceModeId > 0)
            {
                this.Text = "Редактирование режима заезда";

                List<Hashtable> data = admin.model.GetAllRaceModes(" and id = '" + raceModeId.ToString() + "'");

                if (data.Count > 0)
                {
                    name_textBox1.Text = Convert.ToString(data[0]["name"]);
                    length_numericUpDown1.Value =
                        Convert.ToDecimal(data[0]["length"]);
                    idCurrentMode = raceModeId;
                }
            }
            else
            {
                idCurrentMode = -1;  
            }

        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_button_Click(object sender, EventArgs e)
        {            
            
            if (name_textBox1.Text.Length < 1)
            {
                MessageBox.Show("Необходимо указать название");
                return;
            }

            if (length_numericUpDown1.Value < 1)
            {
                MessageBox.Show("Необходимо указать длительность заезда (1 минута или более)");
                return;
            }

            if (isEdit == false)
            {

                  admin.model.AddRaceMode(name_textBox1.Text, Convert.ToInt32(length_numericUpDown1.Value));
     
                   /*

                List<Hashtable> data = admin.model.GetAllRaceModes(" and length = '" + length_numericUpDown1.Value.ToString() + "'");
                
             
                
                if (data.Count == 0)
                {
                    admin.model.AddRaceMode(name_textBox1.Text, Convert.ToInt32(length_numericUpDown1.Value));
                }
                else
                {
                    MessageBox.Show("Уже существует заезд с указанной длительностью");
                    return;
                }
                 */
            }
            else
            {
                admin.model.EditRaceMode(idCurrentMode, name_textBox1.Text, Convert.ToInt32(length_numericUpDown1.Value));
            }

           
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            
        }
    }
}
