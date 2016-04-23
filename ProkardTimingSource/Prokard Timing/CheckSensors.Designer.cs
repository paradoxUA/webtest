namespace Prokard_Timing
{
    partial class CheckSensors
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
            this.sensorsLog_richText = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sensorsLog_richText
            // 
            this.sensorsLog_richText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sensorsLog_richText.Location = new System.Drawing.Point(12, 63);
            this.sensorsLog_richText.Name = "sensorsLog_richText";
            this.sensorsLog_richText.ReadOnly = true;
            this.sensorsLog_richText.Size = new System.Drawing.Size(487, 208);
            this.sensorsLog_richText.TabIndex = 0;
            this.sensorsLog_richText.Text = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(488, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "Для проверки датчика, поднесите его к петле. В текстовом поле должна появиться за" +
    "пись с номером датчика и качеством сигнала (уровнем зарядки) ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CheckSensors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 283);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sensorsLog_richText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckSensors";
            this.Text = "Проверка датчиков";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckSensors_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox sensorsLog_richText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}