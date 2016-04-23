namespace Prokard_Timing
{
    partial class PetroleumSpend
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
            this.labelSmooth2 = new Prokard_Timing.LabelSmooth();
            this.price_numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.litres_numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.spendPetroleum_button1 = new System.Windows.Forms.Button();
            this.cancel_button2 = new System.Windows.Forms.Button();
            this.labelSmooth3 = new Prokard_Timing.LabelSmooth();
            this.labelSmooth1 = new Prokard_Timing.LabelSmooth();
            this.borderPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.price_numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.litres_numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // borderPanel1
            // 
            this.borderPanel1.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.borderPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel1.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel1.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel1.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.None;
            this.borderPanel1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel1.Controls.Add(this.labelSmooth2);
            this.borderPanel1.Controls.Add(this.price_numericUpDown2);
            this.borderPanel1.Controls.Add(this.litres_numericUpDown1);
            this.borderPanel1.Controls.Add(this.spendPetroleum_button1);
            this.borderPanel1.Controls.Add(this.cancel_button2);
            this.borderPanel1.Controls.Add(this.labelSmooth3);
            this.borderPanel1.Controls.Add(this.labelSmooth1);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel1.Location = new System.Drawing.Point(0, 0);
            this.borderPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(392, 156);
            this.borderPanel1.TabIndex = 27;
            this.borderPanel1.UseInnerColor = false;
            // 
            // labelSmooth2
            // 
            this.labelSmooth2.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth2.Location = new System.Drawing.Point(305, 50);
            this.labelSmooth2.Name = "labelSmooth2";
            this.labelSmooth2.Size = new System.Drawing.Size(70, 16);
            this.labelSmooth2.TabIndex = 32;
            this.labelSmooth2.Text = "грн";
            this.labelSmooth2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // price_numericUpDown2
            // 
            this.price_numericUpDown2.DecimalPlaces = 2;
            this.price_numericUpDown2.Location = new System.Drawing.Point(222, 46);
            this.price_numericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.price_numericUpDown2.Name = "price_numericUpDown2";
            this.price_numericUpDown2.Size = new System.Drawing.Size(77, 20);
            this.price_numericUpDown2.TabIndex = 31;
            this.price_numericUpDown2.Value = new decimal(new int[] {
            220,
            0,
            0,
            0});
            // 
            // litres_numericUpDown1
            // 
            this.litres_numericUpDown1.Location = new System.Drawing.Point(222, 19);
            this.litres_numericUpDown1.Name = "litres_numericUpDown1";
            this.litres_numericUpDown1.Size = new System.Drawing.Size(77, 20);
            this.litres_numericUpDown1.TabIndex = 30;
            this.litres_numericUpDown1.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // spendPetroleum_button1
            // 
            this.spendPetroleum_button1.Location = new System.Drawing.Point(109, 110);
            this.spendPetroleum_button1.Name = "spendPetroleum_button1";
            this.spendPetroleum_button1.Size = new System.Drawing.Size(137, 30);
            this.spendPetroleum_button1.TabIndex = 19;
            this.spendPetroleum_button1.Text = "Сохранить и закрыть";
            this.spendPetroleum_button1.UseVisualStyleBackColor = true;
            this.spendPetroleum_button1.Click += new System.EventHandler(this.spendPetroleum_button1_Click);
            // 
            // cancel_button2
            // 
            this.cancel_button2.Location = new System.Drawing.Point(252, 110);
            this.cancel_button2.Name = "cancel_button2";
            this.cancel_button2.Size = new System.Drawing.Size(123, 30);
            this.cancel_button2.TabIndex = 20;
            this.cancel_button2.Text = "Отмена";
            this.cancel_button2.UseVisualStyleBackColor = true;
            this.cancel_button2.Click += new System.EventHandler(this.cancel_button2_Click);
            // 
            // labelSmooth3
            // 
            this.labelSmooth3.AutoSize = true;
            this.labelSmooth3.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth3.Location = new System.Drawing.Point(12, 51);
            this.labelSmooth3.Name = "labelSmooth3";
            this.labelSmooth3.Size = new System.Drawing.Size(207, 14);
            this.labelSmooth3.TabIndex = 27;
            this.labelSmooth3.Text = "Стоимость за указанный объём";
            this.labelSmooth3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth1
            // 
            this.labelSmooth1.AutoSize = true;
            this.labelSmooth1.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth1.Location = new System.Drawing.Point(12, 25);
            this.labelSmooth1.Name = "labelSmooth1";
            this.labelSmooth1.Size = new System.Drawing.Size(53, 14);
            this.labelSmooth1.TabIndex = 9;
            this.labelSmooth1.Text = "Литров";
            this.labelSmooth1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // PetroleumSpend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 156);
            this.Controls.Add(this.borderPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PetroleumSpend";
            this.Text = "Списать бензин";
            this.borderPanel1.ResumeLayout(false);
            this.borderPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.price_numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.litres_numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private LabelSmooth labelSmooth2;
        private System.Windows.Forms.NumericUpDown price_numericUpDown2;
        private System.Windows.Forms.NumericUpDown litres_numericUpDown1;
        private System.Windows.Forms.Button spendPetroleum_button1;
        private System.Windows.Forms.Button cancel_button2;
        private LabelSmooth labelSmooth3;
        private LabelSmooth labelSmooth1;
    }
}