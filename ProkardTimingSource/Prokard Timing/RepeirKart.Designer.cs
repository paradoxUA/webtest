namespace Rentix
{
    partial class RepeirKart
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
            this.borderPanel1 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.borderPanel2 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.borderPanel1.SuspendLayout();
            this.borderPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // borderPanel1
            // 
            this.borderPanel1.BackgroundImage = global::Rentix.Properties.Resources.bg;
            this.borderPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel1.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel1.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel1.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Top;
            this.borderPanel1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel1.Controls.Add(this.button2);
            this.borderPanel1.Controls.Add(this.button1);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel1.Location = new System.Drawing.Point(0, 271);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(493, 40);
            this.borderPanel1.TabIndex = 0;
            this.borderPanel1.UseInnerColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(187, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(340, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // borderPanel2
            // 
            this.borderPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.borderPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel2.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.borderPanel2.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel2.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.None;
            this.borderPanel2.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.borderPanel2.Controls.Add(this.label4);
            this.borderPanel2.Controls.Add(this.label3);
            this.borderPanel2.Controls.Add(this.dateTimePicker1);
            this.borderPanel2.Controls.Add(this.textBox1);
            this.borderPanel2.Controls.Add(this.label2);
            this.borderPanel2.Controls.Add(this.comboBox1);
            this.borderPanel2.Controls.Add(this.label1);
            this.borderPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel2.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel2.Location = new System.Drawing.Point(0, 0);
            this.borderPanel2.Name = "borderPanel2";
            this.borderPanel2.Size = new System.Drawing.Size(493, 271);
            this.borderPanel2.TabIndex = 1;
            this.borderPanel2.UseInnerColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(184, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Номер карта - 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(337, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Дата";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(339, 43);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(142, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 85);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(469, 177);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Текст";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Поставить на ремонт",
            "Снять с ремонта",
            "Заметка"});
            this.comboBox1.Location = new System.Drawing.Point(12, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(321, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип заметки";
            // 
            // RepeirKart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 311);
            this.Controls.Add(this.borderPanel2);
            this.Controls.Add(this.borderPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RepeirKart";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Ремонт и заметки по карту";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RepeirKart_KeyDown);
            this.borderPanel1.ResumeLayout(false);
            this.borderPanel2.ResumeLayout(false);
            this.borderPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}