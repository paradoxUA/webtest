namespace Prokard_Timing
{
    partial class AddDiscountCard
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
            this.label1 = new System.Windows.Forms.Label();
            this.percentOfDiscount_numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ok_button1 = new System.Windows.Forms.Button();
            this.cancel_button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.percentOfDiscount_numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "% скидки";
            // 
            // percentOfDiscount_numericUpDown1
            // 
            this.percentOfDiscount_numericUpDown1.Location = new System.Drawing.Point(98, 69);
            this.percentOfDiscount_numericUpDown1.Name = "percentOfDiscount_numericUpDown1";
            this.percentOfDiscount_numericUpDown1.Size = new System.Drawing.Size(45, 20);
            this.percentOfDiscount_numericUpDown1.TabIndex = 1;
            // 
            // ok_button1
            // 
            this.ok_button1.Location = new System.Drawing.Point(113, 140);
            this.ok_button1.Name = "ok_button1";
            this.ok_button1.Size = new System.Drawing.Size(75, 23);
            this.ok_button1.TabIndex = 2;
            this.ok_button1.Text = "OK";
            this.ok_button1.UseVisualStyleBackColor = true;
            this.ok_button1.Click += new System.EventHandler(this.ok_button1_Click);
            // 
            // cancel_button2
            // 
            this.cancel_button2.Location = new System.Drawing.Point(205, 140);
            this.cancel_button2.Name = "cancel_button2";
            this.cancel_button2.Size = new System.Drawing.Size(75, 23);
            this.cancel_button2.TabIndex = 3;
            this.cancel_button2.Text = "Отмена";
            this.cancel_button2.UseVisualStyleBackColor = true;
            this.cancel_button2.Click += new System.EventHandler(this.cancel_button2_Click);
            // 
            // AddDiscountCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 174);
            this.Controls.Add(this.cancel_button2);
            this.Controls.Add(this.ok_button1);
            this.Controls.Add(this.percentOfDiscount_numericUpDown1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDiscountCard";
            this.Text = "Добавление типа скидочных карт";
            ((System.ComponentModel.ISupportInitialize)(this.percentOfDiscount_numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown percentOfDiscount_numericUpDown1;
        private System.Windows.Forms.Button ok_button1;
        private System.Windows.Forms.Button cancel_button2;
    }
}