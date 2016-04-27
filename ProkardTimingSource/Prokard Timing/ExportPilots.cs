using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Data.Common;
using System.IO;

namespace Prokard_Timing
{
    public partial class ExportPilots : Form
    {
        MainForm parent;
        int selectedGroupId;
        string filter;
        int amountOfPilots;
        static bool isExportCancelled; // if user has pressed Cancel

        public ExportPilots(MainForm mainForm, int idGroup, string filter)
        {
            InitializeComponent();

            selectedGroupId = idGroup;
            this.filter = filter;
            parent = mainForm;
            setAmountOfpilots(idGroup, filter, withPhonesOnly_checkBox.Checked);
            amountOfPilots_label.Text = amountOfPilots.ToString();
        }

        private void setAmountOfpilots(int idGroup, string filter, bool withPhonesOnly)
        {
            amountOfPilots = parent.admin.model.GetPilotsCount(idGroup, filter, withPhonesOnly);
            amountOfPilots_label.Text = amountOfPilots.ToString();    

        }

        private void setCancelButtonActive(bool isActive)
        {
            cancel_button.Visible = isActive;
            exportToExcel_button.Enabled = !isActive;
            exportToXML_button.Enabled = !isActive;
        }

        private void exportToXML_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "XML файлы (*.xml)|*.xml|Все файлы(*.*)|(*.*)";
            DialogResult dr = sd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {

                setCancelButtonActive(true);
                isExportCancelled = false;
                exportContactsToXML(sd.FileName, withPhonesOnly_checkBox.Checked);
                setCancelButtonActive(false);
                this.Close();
            }
        }
        
        private void exportToExcel_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "MS Excel файлы (*.xls)|*.xls|Все файлы(*.*)|(*.*)";
            DialogResult dr = sd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                setCancelButtonActive(true);
                isExportCancelled = false;
                exportContactsToExcel(sd.FileName, withPhonesOnly_checkBox.Checked);
                setCancelButtonActive(false);
                this.Close();
            }
           
        }
        
        private void exportContactsToXML(string fileName, bool withPhonesOnly)
        {
            Hashtable pilots = parent.admin.model.GetAllPilots(filter, selectedGroupId, withPhonesOnly);
            if (pilots.Count == 0)
            {
                MessageBox.Show("Список пилотов пустой, файл не будет создан");
                return;
            }

            XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteStartElement("pilots");
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = '\t';

            progressBar1.Minimum = 0;
            progressBar1.Maximum = pilots.Count;

            for (int i = 0; i < pilots.Count; i++)
            {
                Hashtable somePilot = (Hashtable) pilots[i];

                writer.WriteStartElement("pilot");

                writer.WriteStartAttribute("lastName");
                writer.WriteString(Convert.ToString(somePilot["surname"]));
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("firstName");
                writer.WriteString(Convert.ToString(somePilot["name"]));
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("phone");
                writer.WriteString(Convert.ToString(somePilot["tel"]));
                writer.WriteEndAttribute();

                writer.WriteEndElement();

                savedAmount_label.Text = (i+1).ToString();
                Application.DoEvents();

                progressBar1.Value = i;

                if (isExportCancelled)
                {
                    break;
                }
            }
            progressBar1.Value = 0;
            writer.WriteEndElement();
            writer.Close();
        }

        private void exportContactsToExcel(string fileName, bool withPhonesOnly)
        {
            Hashtable pilots = parent.admin.model.GetAllPilots(filter, selectedGroupId, withPhonesOnly);
            
            if (pilots.Count == 0)
            {
                MessageBox.Show("Список пилотов пустой, файл не будет создан");
                
                return;
            }

            progressBar1.Minimum = 0;
            progressBar1.Maximum = pilots.Count;

            string templateLocation = Program.ProgramFolder + "\\ExportPilotsTemplate\\pilotsTemplate.xlst";

            if (File.Exists(templateLocation) == false)
            {
                MessageBox.Show("Файл '" + templateLocation + "' с шаблоном отчёта не найден. Пожалуйста скопируйте файл в указанный каталог из установочного пакета");
                return;
            }

            File.Copy(templateLocation, fileName, true);

            string lConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName +
                ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
            
            DbProviderFactory lFactory = DbProviderFactories.GetFactory("System.Data.OleDb");
            int lSequence = 0;
            using (DbConnection lConnection = lFactory.CreateConnection())
            { 
                lConnection.ConnectionString = lConnectionString; 
                lConnection.Open();

                for (int i = 0; i < pilots.Count; i++) // pilots.Count
            {
                Hashtable somePilot = (Hashtable)pilots[i];
            
                    lSequence++;
                    using (DbCommand lCommand = lConnection.CreateCommand()) 
                    {
                        lCommand.CommandText = "INSERT INTO [pilots$] ";
                        lCommand.CommandText += "(LastName, FirstName, Phone) ";
                        lCommand.CommandText += "VALUES(";                        
                       lCommand.CommandText += "\"" + somePilot["surname"] + "\",";
                        lCommand.CommandText += "\"" + somePilot["name"] + "\",";
                        lCommand.CommandText += "\"" + somePilot["tel"] + "\" )";
                        lCommand.ExecuteNonQuery();

                      
                        savedAmount_label.Text = (i + 1).ToString();
                        progressBar1.Value = i;

                          Application.DoEvents();

                        if (isExportCancelled)
                        {
                            break;
                        }
                    } 
                }

                progressBar1.Value = 0;
                lConnection.Close(); 
            }            
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            isExportCancelled = true;
        }

        private void withPhonesOnly_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            setAmountOfpilots(selectedGroupId, this.filter, withPhonesOnly_checkBox.Checked);
        }
    }
}
