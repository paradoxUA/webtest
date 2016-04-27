using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prokard_Timing
{
    public partial class DbSettings : Form
    {
        public DbSettings()
        {
            InitializeComponent();
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            richTextBox1.Text = config.AppSettings.Settings["crazykartConnectionString"].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // Properties.Settings.crazykartConnectionString = connectionString_textBox9.Text;
            config.AppSettings.Settings.Remove("crazykartConnectionString");
            config.AppSettings.Settings.Add("crazykartConnectionString", richTextBox1.Text);
            //Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            Application.Restart();
        }
    }
}
