namespace Rentix
{
    partial class AddPartner
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.labelSmooth3 = new Rentix.LabelSmooth();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.labelSmooth1 = new Rentix.LabelSmooth();
			this.borderPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// borderPanel1
			// 
			this.borderPanel1.BackgroundImage = global::Rentix.Properties.Resources.bg;
			this.borderPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.borderPanel1.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.borderPanel1.BorderPadding = new System.Windows.Forms.Padding(0);
			this.borderPanel1.BorderSides = ((Microsoft.TeamFoundation.Client.BorderPanel.Sides)((((Microsoft.TeamFoundation.Client.BorderPanel.Sides.Top | Microsoft.TeamFoundation.Client.BorderPanel.Sides.Bottom) 
            | Microsoft.TeamFoundation.Client.BorderPanel.Sides.Left) 
            | Microsoft.TeamFoundation.Client.BorderPanel.Sides.Right)));
			this.borderPanel1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
			this.borderPanel1.Controls.Add(this.button2);
			this.borderPanel1.Controls.Add(this.button1);
			this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
			this.borderPanel1.Location = new System.Drawing.Point(0, 80);
			this.borderPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.borderPanel1.Name = "borderPanel1";
			this.borderPanel1.Size = new System.Drawing.Size(324, 40);
			this.borderPanel1.TabIndex = 1;
			this.borderPanel1.UseInnerColor = false;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(5, 5);
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
			this.button1.Location = new System.Drawing.Point(168, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(150, 30);
			this.button1.TabIndex = 0;
			this.button1.Text = "Закрыть";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = global::Rentix.Properties.Resources.bg;
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.labelSmooth3);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.labelSmooth1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(324, 80);
			this.panel1.TabIndex = 2;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(92, 45);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(219, 20);
			this.textBox2.TabIndex = 3;
			// 
			// labelSmooth3
			// 
			this.labelSmooth3.AutoSize = true;
			this.labelSmooth3.BackColor = System.Drawing.Color.Transparent;
			this.labelSmooth3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelSmooth3.Location = new System.Drawing.Point(12, 47);
			this.labelSmooth3.Name = "labelSmooth3";
			this.labelSmooth3.Size = new System.Drawing.Size(74, 14);
			this.labelSmooth3.TabIndex = 2;
			this.labelSmooth3.Text = "Стоимость";
			this.labelSmooth3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(93, 20);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(219, 20);
			this.textBox1.TabIndex = 1;
			// 
			// labelSmooth1
			// 
			this.labelSmooth1.AutoSize = true;
			this.labelSmooth1.BackColor = System.Drawing.Color.Transparent;
			this.labelSmooth1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelSmooth1.Location = new System.Drawing.Point(12, 22);
			this.labelSmooth1.Name = "labelSmooth1";
			this.labelSmooth1.Size = new System.Drawing.Size(65, 14);
			this.labelSmooth1.TabIndex = 0;
			this.labelSmooth1.Text = "Название";
			this.labelSmooth1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			// 
			// AddPartner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 120);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.borderPanel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddPartner";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Группа";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddGroup_KeyDown);
			this.borderPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

     

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private LabelSmooth labelSmooth1;
        private LabelSmooth labelSmooth3;
		private System.Windows.Forms.TextBox textBox2;
	}
}