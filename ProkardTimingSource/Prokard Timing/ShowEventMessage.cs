using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Prokard_Timing
{
    public partial class ShowEventMessage : Form
    {

        public ShowEventMessage(string ID, AdminControl ad)
        {
            InitializeComponent();
            Hashtable Message = ad.model.GetMessageFromID(ID);
            
            label2.Text = Convert.ToDateTime(Message["date"]).ToString("dd MMMM");
            label4.Text = Message["subject"].ToString();

            richTextBox1.Text = Message["message"].ToString();
        }

        private void ShowEventMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
