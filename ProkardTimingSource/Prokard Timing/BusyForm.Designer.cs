namespace Rentix
{
    partial class BusyForm
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
            this.name_label = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.currentStatus_label = new System.Windows.Forms.Label();
            this.cancel_button = new System.Windows.Forms.Button();
            this.records_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.processed_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name_label
            // 
            this.name_label.BackColor = System.Drawing.Color.Transparent;
            this.name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name_label.Location = new System.Drawing.Point(16, 13);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(587, 49);
            this.name_label.TabIndex = 0;
            this.name_label.Text = "Обработка...";
            this.name_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.Yellow;
            this.progressBar.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progressBar.Location = new System.Drawing.Point(17, 73);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(583, 23);
            this.progressBar.TabIndex = 1;
            // 
            // currentStatus_label
            // 
            this.currentStatus_label.AutoSize = true;
            this.currentStatus_label.BackColor = System.Drawing.Color.Transparent;
            this.currentStatus_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentStatus_label.Location = new System.Drawing.Point(38, 107);
            this.currentStatus_label.Name = "currentStatus_label";
            this.currentStatus_label.Size = new System.Drawing.Size(61, 13);
            this.currentStatus_label.TabIndex = 2;
            this.currentStatus_label.Text = "Записей:";
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(475, 107);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(125, 33);
            this.cancel_button.TabIndex = 3;
            this.cancel_button.Text = "Отмена";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // records_label
            // 
            this.records_label.AutoSize = true;
            this.records_label.BackColor = System.Drawing.Color.Transparent;
            this.records_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.records_label.Location = new System.Drawing.Point(105, 107);
            this.records_label.Name = "records_label";
            this.records_label.Size = new System.Drawing.Size(14, 13);
            this.records_label.TabIndex = 4;
            this.records_label.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(20, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Обработано:";
            // 
            // processed_label
            // 
            this.processed_label.AutoSize = true;
            this.processed_label.BackColor = System.Drawing.Color.Transparent;
            this.processed_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.processed_label.Location = new System.Drawing.Point(105, 127);
            this.processed_label.Name = "processed_label";
            this.processed_label.Size = new System.Drawing.Size(14, 13);
            this.processed_label.TabIndex = 6;
            this.processed_label.Text = "0";
            // 
            // BusyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Rentix.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(612, 149);
            this.ControlBox = false;
            this.Controls.Add(this.processed_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.records_label);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.currentStatus_label);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.name_label);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BusyForm";
            this.Text = "Пожалуйста, подождите...";
            this.Shown += new System.EventHandler(this.BusyForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Label currentStatus_label;
        private System.Windows.Forms.Button cancel_button;
        public System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label records_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label processed_label;
    }
}