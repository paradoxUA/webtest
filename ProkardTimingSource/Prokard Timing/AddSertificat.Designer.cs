namespace Rentix
{
    partial class AddSertificat
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
            this.labelSmooth1 = new Rentix.LabelSmooth();
            this.borderPanel1 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.labelSmooth2 = new Rentix.LabelSmooth();
            this.labelSmooth3 = new Rentix.LabelSmooth();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.labelSmooth4 = new Rentix.LabelSmooth();
            this.borderPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSmooth1
            // 
            this.labelSmooth1.AutoSize = true;
            this.labelSmooth1.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth1.Location = new System.Drawing.Point(23, 50);
            this.labelSmooth1.Name = "labelSmooth1";
            this.labelSmooth1.Size = new System.Drawing.Size(65, 14);
            this.labelSmooth1.TabIndex = 0;
            this.labelSmooth1.Text = "Название";
            this.labelSmooth1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
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
            this.borderPanel1.Location = new System.Drawing.Point(0, 139);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(311, 40);
            this.borderPanel1.TabIndex = 1;
            this.borderPanel1.UseInnerColor = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(158, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(195, 20);
            this.textBox1.TabIndex = 2;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(94, 74);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(195, 20);
            this.numericUpDown1.TabIndex = 3;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 1;
            this.numericUpDown2.Location = new System.Drawing.Point(94, 100);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            65536});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(195, 20);
            this.numericUpDown2.TabIndex = 4;
            this.numericUpDown2.ThousandsSeparator = true;
            // 
            // labelSmooth2
            // 
            this.labelSmooth2.AutoSize = true;
            this.labelSmooth2.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth2.Location = new System.Drawing.Point(25, 74);
            this.labelSmooth2.Name = "labelSmooth2";
            this.labelSmooth2.Size = new System.Drawing.Size(63, 14);
            this.labelSmooth2.TabIndex = 5;
            this.labelSmooth2.Text = "Номинал";
            this.labelSmooth2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth3
            // 
            this.labelSmooth3.AutoSize = true;
            this.labelSmooth3.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth3.Location = new System.Drawing.Point(14, 101);
            this.labelSmooth3.Name = "labelSmooth3";
            this.labelSmooth3.Size = new System.Drawing.Size(74, 14);
            this.labelSmooth3.TabIndex = 6;
            this.labelSmooth3.Text = "Стоимость";
            this.labelSmooth3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Location = new System.Drawing.Point(94, 25);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(173, 17);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.Tag = "certificateType";
            this.radioButton1.Text = "Скидка на стоимость заезда";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(94, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(129, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            this.radioButton2.Tag = "certificateType";
            this.radioButton2.Text = "Подарочные заезды";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // labelSmooth4
            // 
            this.labelSmooth4.AutoSize = true;
            this.labelSmooth4.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth4.Location = new System.Drawing.Point(57, 14);
            this.labelSmooth4.Name = "labelSmooth4";
            this.labelSmooth4.Size = new System.Drawing.Size(31, 14);
            this.labelSmooth4.TabIndex = 9;
            this.labelSmooth4.Text = "Вид";
            this.labelSmooth4.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // AddSertificat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Rentix.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(311, 179);
            this.Controls.Add(this.labelSmooth4);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.labelSmooth3);
            this.Controls.Add(this.labelSmooth2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.borderPanel1);
            this.Controls.Add(this.labelSmooth1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSertificat";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сертификат";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddSertificat_KeyDown);
            this.borderPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelSmooth labelSmooth1;
        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private LabelSmooth labelSmooth2;
        private LabelSmooth labelSmooth3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private LabelSmooth labelSmooth4;
    }
}