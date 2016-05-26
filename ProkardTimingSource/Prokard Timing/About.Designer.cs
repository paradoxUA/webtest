namespace Rentix
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.borderPanel1 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.labelSmooth2 = new Rentix.LabelSmooth();
            this.labelSmooth1 = new Rentix.LabelSmooth();
            this.borderPanel2 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.borderPanel1.SuspendLayout();
            this.borderPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // borderPanel1
            // 
            this.borderPanel1.BackgroundImage = global::Rentix.Properties.Resources.bg;
            this.borderPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel1.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel1.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel1.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Bottom;
            this.borderPanel1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel1.Controls.Add(this.labelSmooth2);
            this.borderPanel1.Controls.Add(this.labelSmooth1);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel1.Location = new System.Drawing.Point(0, 0);
            this.borderPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(395, 40);
            this.borderPanel1.TabIndex = 0;
            this.borderPanel1.UseInnerColor = false;
            // 
            // labelSmooth2
            // 
            this.labelSmooth2.AutoSize = true;
            this.labelSmooth2.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth2.Location = new System.Drawing.Point(351, 10);
            this.labelSmooth2.Name = "labelSmooth2";
            this.labelSmooth2.Size = new System.Drawing.Size(41, 19);
            this.labelSmooth2.TabIndex = 1;
            this.labelSmooth2.Text = "v 1.4";
            this.labelSmooth2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth1
            // 
            this.labelSmooth1.AutoSize = true;
            this.labelSmooth1.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth1.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth1.Location = new System.Drawing.Point(3, 3);
            this.labelSmooth1.Name = "labelSmooth1";
            this.labelSmooth1.Size = new System.Drawing.Size(92, 33);
            this.labelSmooth1.TabIndex = 0;
            this.labelSmooth1.Text = "Rentix ";
            this.labelSmooth1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // borderPanel2
            // 
            this.borderPanel2.BackgroundImage = global::Rentix.Properties.Resources.bg;
            this.borderPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel2.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel2.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel2.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Top;
            this.borderPanel2.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel2.Controls.Add(this.button1);
            this.borderPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderPanel2.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel2.Location = new System.Drawing.Point(0, 233);
            this.borderPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel2.Name = "borderPanel2";
            this.borderPanel2.Size = new System.Drawing.Size(395, 40);
            this.borderPanel2.TabIndex = 1;
            this.borderPanel2.UseInnerColor = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(241, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Oк";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::Rentix.Properties.Resources.bg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 193);
            this.panel1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(12, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(371, 168);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private LabelSmooth labelSmooth1;
        private LabelSmooth labelSmooth2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}