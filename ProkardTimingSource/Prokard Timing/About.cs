using Rentix.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Rentix
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
			var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			var dateOfBuild = Assembly.GetExecutingAssembly().GetLinkerTime();
			label2.Text = $"{assemblyVersion}    {dateOfBuild}";
		}

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
