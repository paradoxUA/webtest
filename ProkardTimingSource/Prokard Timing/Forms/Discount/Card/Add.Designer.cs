namespace Prokard_Timing.Forms.Discount.Card
{
    partial class Discount_Card_Add
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
            this.cardNumber_textBox1 = new System.Windows.Forms.TextBox();
            this.assigned_dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cardGroup_comboBox1 = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.owner_comboBox1 = new System.Windows.Forms.ComboBox();
            this.borderPanel1 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.labelSmooth1 = new Prokard_Timing.LabelSmooth();
            this.labelSmooth3 = new Prokard_Timing.LabelSmooth();
            this.labelSmooth2 = new Prokard_Timing.LabelSmooth();
            this.labelSmooth4 = new Prokard_Timing.LabelSmooth();
            this.borderPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardNumber_textBox1
            // 
            this.cardNumber_textBox1.Location = new System.Drawing.Point(108, 23);
            this.cardNumber_textBox1.Name = "cardNumber_textBox1";
            this.cardNumber_textBox1.Size = new System.Drawing.Size(264, 20);
            this.cardNumber_textBox1.TabIndex = 5;
            // 
            // assigned_dateTimePicker1
            // 
            this.assigned_dateTimePicker1.Location = new System.Drawing.Point(108, 101);
            this.assigned_dateTimePicker1.Name = "assigned_dateTimePicker1";
            this.assigned_dateTimePicker1.Size = new System.Drawing.Size(264, 20);
            this.assigned_dateTimePicker1.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 30);
            this.button1.TabIndex = 19;
            this.button1.Text = "Сохранить и закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(252, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 30);
            this.button2.TabIndex = 20;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cardGroup_comboBox1
            // 
            this.cardGroup_comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cardGroup_comboBox1.FormattingEnabled = true;
            this.cardGroup_comboBox1.Location = new System.Drawing.Point(108, 74);
            this.cardGroup_comboBox1.Name = "cardGroup_comboBox1";
            this.cardGroup_comboBox1.Size = new System.Drawing.Size(121, 21);
            this.cardGroup_comboBox1.TabIndex = 23;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 200);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(387, 22);
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // owner_comboBox1
            // 
            this.owner_comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.owner_comboBox1.FormattingEnabled = true;
            this.owner_comboBox1.Location = new System.Drawing.Point(108, 49);
            this.owner_comboBox1.Name = "owner_comboBox1";
            this.owner_comboBox1.Size = new System.Drawing.Size(264, 21);
            this.owner_comboBox1.TabIndex = 25;
            // 
            // borderPanel1
            // 
            this.borderPanel1.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.borderPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel1.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel1.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel1.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.None;
            this.borderPanel1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel1.Controls.Add(this.button1);
            this.borderPanel1.Controls.Add(this.labelSmooth4);
            this.borderPanel1.Controls.Add(this.labelSmooth2);
            this.borderPanel1.Controls.Add(this.button2);
            this.borderPanel1.Controls.Add(this.labelSmooth3);
            this.borderPanel1.Controls.Add(this.labelSmooth1);
            this.borderPanel1.Controls.Add(this.assigned_dateTimePicker1);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel1.Location = new System.Drawing.Point(0, 0);
            this.borderPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(387, 222);
            this.borderPanel1.TabIndex = 26;
            this.borderPanel1.UseInnerColor = false;
            // 
            // labelSmooth1
            // 
            this.labelSmooth1.AutoSize = true;
            this.labelSmooth1.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth1.Location = new System.Drawing.Point(12, 25);
            this.labelSmooth1.Name = "labelSmooth1";
            this.labelSmooth1.Size = new System.Drawing.Size(91, 14);
            this.labelSmooth1.TabIndex = 9;
            this.labelSmooth1.Text = "Номер карты";
            this.labelSmooth1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth3
            // 
            this.labelSmooth3.AutoSize = true;
            this.labelSmooth3.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth3.Location = new System.Drawing.Point(12, 51);
            this.labelSmooth3.Name = "labelSmooth3";
            this.labelSmooth3.Size = new System.Drawing.Size(70, 14);
            this.labelSmooth3.TabIndex = 27;
            this.labelSmooth3.Text = "Владелец";
            this.labelSmooth3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth2
            // 
            this.labelSmooth2.AutoSize = true;
            this.labelSmooth2.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth2.Location = new System.Drawing.Point(12, 76);
            this.labelSmooth2.Name = "labelSmooth2";
            this.labelSmooth2.Size = new System.Drawing.Size(68, 14);
            this.labelSmooth2.TabIndex = 28;
            this.labelSmooth2.Text = "% скидки";
            this.labelSmooth2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth4
            // 
            this.labelSmooth4.AutoSize = true;
            this.labelSmooth4.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth4.Location = new System.Drawing.Point(12, 104);
            this.labelSmooth4.Name = "labelSmooth4";
            this.labelSmooth4.Size = new System.Drawing.Size(56, 14);
            this.labelSmooth4.TabIndex = 29;
            this.labelSmooth4.Text = "Выдана";
            this.labelSmooth4.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // Discount_Card_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 222);
            this.Controls.Add(this.owner_comboBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cardGroup_comboBox1);
            this.Controls.Add(this.cardNumber_textBox1);
            this.Controls.Add(this.borderPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "Discount_Card_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить скидочную карту";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Discount_Card_Add_KeyDown);
            this.borderPanel1.ResumeLayout(false);
            this.borderPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cardNumber_textBox1;
        private System.Windows.Forms.DateTimePicker assigned_dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cardGroup_comboBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox owner_comboBox1;
        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private LabelSmooth labelSmooth4;
        private LabelSmooth labelSmooth2;
        private LabelSmooth labelSmooth3;
        private LabelSmooth labelSmooth1;
    }
}