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
    public partial class AddTrack : Form
    {
        AdminControl admin = null;
        string FilePath = String.Empty;
        bool tUpdate = false;
        string TrackID = String.Empty;

        public AddTrack(AdminControl ad, bool Updt = false, string ID="", string Name="", string FileName="", string Length="")
        {
            InitializeComponent();
            admin = ad;

            if (Updt)
            {
                tUpdate = true;
                TrackID = ID;
                textBox1.Text = Name;
                textBox2.Text = Length;
                FilePath = textBox3.Text = FileName;
                if (FileName.Length > 0 && System.IO.File.Exists (FileName))
                    pictureBox1.Image = Image.FromFile(string.Format(FileName));
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void AddTrack_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog1.FileName.Replace("\\", "\\\\");
                pictureBox1.Image = Image.FromFile(string.Format(FilePath));
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                textBox3.Text = FilePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) MessageBox.Show("Ошибка! Название не может быть пустым!");
            else
            {
                if (tUpdate)
                    admin.model.ChangeTrack(TrackID, textBox1.Text, textBox2.Text.Length == 0 ? "0" : textBox2.Text, FilePath);
                else
                    admin.model.AddTrack(textBox1.Text, textBox2.Text.Length == 0 ? "0" : textBox2.Text, FilePath);

                this.Close();
            }
        }
    }
}
