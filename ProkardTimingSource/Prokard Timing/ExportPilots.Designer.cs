namespace Prokard_Timing
{
    partial class ExportPilots
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.savedAmount_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.amountOfPilots_label = new System.Windows.Forms.Label();
            this.exportToXML_button = new System.Windows.Forms.Button();
            this.exportToExcel_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.withPhonesOnly_checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(324, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сохранено";
            // 
            // savedAmount_label
            // 
            this.savedAmount_label.BackColor = System.Drawing.Color.Transparent;
            this.savedAmount_label.Location = new System.Drawing.Point(76, 91);
            this.savedAmount_label.Name = "savedAmount_label";
            this.savedAmount_label.Size = new System.Drawing.Size(37, 12);
            this.savedAmount_label.TabIndex = 2;
            this.savedAmount_label.Text = "0";
            this.savedAmount_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(114, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "записей из";
            // 
            // amountOfPilots_label
            // 
            this.amountOfPilots_label.BackColor = System.Drawing.Color.Transparent;
            this.amountOfPilots_label.Location = new System.Drawing.Point(184, 91);
            this.amountOfPilots_label.Name = "amountOfPilots_label";
            this.amountOfPilots_label.Size = new System.Drawing.Size(37, 12);
            this.amountOfPilots_label.TabIndex = 4;
            this.amountOfPilots_label.Text = "0";
            this.amountOfPilots_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // exportToXML_button
            // 
            this.exportToXML_button.Location = new System.Drawing.Point(199, 158);
            this.exportToXML_button.Name = "exportToXML_button";
            this.exportToXML_button.Size = new System.Drawing.Size(142, 23);
            this.exportToXML_button.TabIndex = 5;
            this.exportToXML_button.Text = "Экспорт в XML";
            this.exportToXML_button.UseVisualStyleBackColor = true;
            this.exportToXML_button.Click += new System.EventHandler(this.exportToXML_button_Click);
            // 
            // exportToExcel_button
            // 
            this.exportToExcel_button.Location = new System.Drawing.Point(199, 198);
            this.exportToExcel_button.Name = "exportToExcel_button";
            this.exportToExcel_button.Size = new System.Drawing.Size(142, 23);
            this.exportToExcel_button.TabIndex = 6;
            this.exportToExcel_button.Text = "Экспорт в Excel";
            this.exportToExcel_button.UseVisualStyleBackColor = true;
            this.exportToExcel_button.Click += new System.EventHandler(this.exportToExcel_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(12, 158);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(120, 63);
            this.cancel_button.TabIndex = 7;
            this.cancel_button.Text = "Отмена";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Visible = false;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // withPhonesOnly_checkBox
            // 
            this.withPhonesOnly_checkBox.AutoSize = true;
            this.withPhonesOnly_checkBox.BackColor = System.Drawing.Color.Transparent;
            this.withPhonesOnly_checkBox.Location = new System.Drawing.Point(13, 124);
            this.withPhonesOnly_checkBox.Name = "withPhonesOnly_checkBox";
            this.withPhonesOnly_checkBox.Size = new System.Drawing.Size(323, 17);
            this.withPhonesOnly_checkBox.TabIndex = 8;
            this.withPhonesOnly_checkBox.Text = "выбрать только тех пилотов, для которых указан телефон";
            this.withPhonesOnly_checkBox.UseVisualStyleBackColor = false;
            this.withPhonesOnly_checkBox.CheckedChanged += new System.EventHandler(this.withPhonesOnly_checkBox_CheckedChanged);
            // 
            // ExportPilots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(353, 236);
            this.Controls.Add(this.withPhonesOnly_checkBox);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.exportToExcel_button);
            this.Controls.Add(this.exportToXML_button);
            this.Controls.Add(this.amountOfPilots_label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.savedAmount_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ExportPilots";
            this.Text = "Экспорт списка пилотов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label savedAmount_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label amountOfPilots_label;
        private System.Windows.Forms.Button exportToXML_button;
        private System.Windows.Forms.Button exportToExcel_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.CheckBox withPhonesOnly_checkBox;
    }
}