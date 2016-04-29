using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prokard_Timing
{
    public partial class AddGroup : Form
    {
        AdminControl admin;
        bool sUpdate = false;
        string GroupID = String.Empty;
        public AddGroup(AdminControl ad, bool updt = false, string GrID = "",string Name = "" , string Sale = "")
        {
            InitializeComponent();
            admin = ad;

            if (updt)
            {
                sUpdate = true;
                GroupID = GrID;

                textBox1.Text = Name;
                textBox2.Text = Sale;
            }


        }

        private void AddGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show(@"Ошибка! Название не может быть пустым");
            }
            else
            {
                int numVal;
                Int32.TryParse(textBox2.Text.Split(',')[0], out numVal);
                if (numVal > 100)
                {
                    MessageBox.Show(@"Ошибка! Скидка не может быть более 100%!");
                }
                else
                {
                    if (sUpdate)
                        admin.model.ChangeGroup(textBox1.Text, textBox2.Text, GroupID);
                    else
                        admin.model.AddGroup(textBox1.Text, textBox2.Text);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
