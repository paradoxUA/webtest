namespace Prokard_Timing
{
    partial class AddRaceMode
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
            this.add_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.label7 = new Prokard_Timing.LabelSmooth();
            this.labelSmooth1 = new Prokard_Timing.LabelSmooth();
            this.name_textBox1 = new System.Windows.Forms.TextBox();
            this.length_numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelSmooth2 = new Prokard_Timing.LabelSmooth();
            this.borderPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.length_numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // borderPanel1
            // 
            this.borderPanel1.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.borderPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel1.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel1.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel1.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Top;
            this.borderPanel1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel1.Controls.Add(this.add_button);
            this.borderPanel1.Controls.Add(this.cancel_button);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel1.Location = new System.Drawing.Point(0, 178);
            this.borderPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(411, 40);
            this.borderPanel1.TabIndex = 1;
            this.borderPanel1.UseInnerColor = false;
            // 
            // add_button
            // 
            this.add_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add_button.Location = new System.Drawing.Point(101, 5);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(150, 30);
            this.add_button.TabIndex = 36;
            this.add_button.Text = "Сохранить и закрыть";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel_button.Location = new System.Drawing.Point(257, 5);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(150, 30);
            this.cancel_button.TabIndex = 40;
            this.cancel_button.Text = "Отмена";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(41, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 14);
            this.label7.TabIndex = 32;
            this.label7.Text = "Название";
            this.label7.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth1
            // 
            this.labelSmooth1.AutoSize = true;
            this.labelSmooth1.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelSmooth1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSmooth1.Location = new System.Drawing.Point(41, 103);
            this.labelSmooth1.Name = "labelSmooth1";
            this.labelSmooth1.Size = new System.Drawing.Size(97, 14);
            this.labelSmooth1.TabIndex = 33;
            this.labelSmooth1.Text = "Длительность";
            this.labelSmooth1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // name_textBox1
            // 
            this.name_textBox1.Location = new System.Drawing.Point(144, 37);
            this.name_textBox1.Name = "name_textBox1";
            this.name_textBox1.Size = new System.Drawing.Size(255, 20);
            this.name_textBox1.TabIndex = 34;
            this.name_textBox1.Text = "10 минут";
            // 
            // length_numericUpDown1
            // 
            this.length_numericUpDown1.Location = new System.Drawing.Point(144, 101);
            this.length_numericUpDown1.Name = "length_numericUpDown1";
            this.length_numericUpDown1.Size = new System.Drawing.Size(74, 20);
            this.length_numericUpDown1.TabIndex = 35;
            // 
            // labelSmooth2
            // 
            this.labelSmooth2.AutoSize = true;
            this.labelSmooth2.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelSmooth2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSmooth2.Location = new System.Drawing.Point(224, 103);
            this.labelSmooth2.Name = "labelSmooth2";
            this.labelSmooth2.Size = new System.Drawing.Size(45, 14);
            this.labelSmooth2.TabIndex = 36;
            this.labelSmooth2.Text = "минут";
            this.labelSmooth2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // AddRaceMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(411, 218);
            this.Controls.Add(this.labelSmooth2);
            this.Controls.Add(this.length_numericUpDown1);
            this.Controls.Add(this.name_textBox1);
            this.Controls.Add(this.labelSmooth1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.borderPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRaceMode";
            this.ShowIcon = false;
            this.Text = "Добавить режим заезда";
            this.borderPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.length_numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private System.Windows.Forms.Button cancel_button;
        private LabelSmooth label7;
        private LabelSmooth labelSmooth1;
        private System.Windows.Forms.TextBox name_textBox1;
        private System.Windows.Forms.NumericUpDown length_numericUpDown1;
        private LabelSmooth labelSmooth2;
        private System.Windows.Forms.Button add_button;
    }
}