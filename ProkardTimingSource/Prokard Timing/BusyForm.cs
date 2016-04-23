using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Prokard_Timing
{
    public partial class BusyForm : Form
    {
        public bool isCancelled { private set; get; }

        public BusyForm(string name, int maximumValue)
        {
            InitializeComponent();
            this.name_label.Text = name;
            progressBar.Maximum = maximumValue;
            records_label.Text = maximumValue.ToString();           
        }
               
        public bool SetProgressValue(int currentRecordNumber)
        {
            progressBar.Value = currentRecordNumber;
            processed_label.Text = currentRecordNumber.ToString();
            Application.DoEvents();
            return isCancelled;
        }

        public void CloseForm()
        {
            name_label.Text = "Завершено...";
            Application.DoEvents();
            Thread.Sleep(300);
            Application.DoEvents();
            this.Close();
        }

        public void CancelForm()
        {
            name_label.Text = "Отмена...";
            Application.DoEvents();
            Thread.Sleep(300);
            Application.DoEvents();
            this.Close();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            isCancelled = true;
        }

        private void BusyForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
        }
    }
}
