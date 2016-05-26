using System.Windows.Forms;
namespace Rentix
{
    partial class PrintResult1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintResult1));
            this.borderPanel1 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dv3 = new DataGridView();
            this.Num2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestLap2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dv1 = new DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dv2 = new DataGridView();
            this.Num1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestLap1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelSmooth4 = new Rentix.LabelSmooth();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.labelSmooth3 = new Rentix.LabelSmooth();
            this.labelSmooth2 = new Rentix.LabelSmooth();
            this.labelSmooth1 = new Rentix.LabelSmooth();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.borderPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dv3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dv2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
            this.borderPanel1.Controls.Add(this.button3);
            this.borderPanel1.Controls.Add(this.button1);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel1.Location = new System.Drawing.Point(0, 80);
            this.borderPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(348, 40);
            this.borderPanel1.TabIndex = 0;
            this.borderPanel1.UseInnerColor = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 30);
            this.button3.TabIndex = 2;
            this.button3.Text = "Экспортировать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(195, 5);
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
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.labelSmooth4);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.labelSmooth3);
            this.panel1.Controls.Add(this.labelSmooth2);
            this.panel1.Controls.Add(this.labelSmooth1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 80);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dv3);
            this.panel2.Controls.Add(this.dv1);
            this.panel2.Controls.Add(this.dv2);
            this.panel2.Location = new System.Drawing.Point(12, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(856, 314);
            this.panel2.TabIndex = 8;
            // 
            // dv3
            // 
            this.dv3.AllowUserToAddRows = false;
            this.dv3.AllowUserToDeleteRows = false;
            this.dv3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dv3.BackgroundColor = System.Drawing.Color.White;
            this.dv3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dv3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dv3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dv3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dv3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num2,
            this.Date2,
            this.Name2,
            this.BestLap2});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dv3.DefaultCellStyle = dataGridViewCellStyle14;
            this.dv3.GridColor = System.Drawing.Color.Black;
            this.dv3.Location = new System.Drawing.Point(323, 3);
            this.dv3.Name = "dv3";
            this.dv3.ReadOnly = true;
            this.dv3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dv3.RowHeadersVisible = false;
            this.dv3.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dv3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dv3.ShowCellErrors = false;
            this.dv3.ShowCellToolTips = false;
            this.dv3.ShowEditingIcon = false;
            this.dv3.Size = new System.Drawing.Size(281, 74);
            this.dv3.TabIndex = 7;
            // 
            // Num2
            // 
            this.Num2.HeaderText = "№";
            this.Num2.Name = "Num2";
            this.Num2.ReadOnly = true;
            // 
            // Date2
            // 
            this.Date2.HeaderText = "Дата";
            this.Date2.Name = "Date2";
            this.Date2.ReadOnly = true;
            // 
            // Name2
            // 
            this.Name2.HeaderText = "Имя";
            this.Name2.Name = "Name2";
            this.Name2.ReadOnly = true;
            // 
            // BestLap2
            // 
            this.BestLap2.HeaderText = "Время";
            this.BestLap2.Name = "BestLap2";
            this.BestLap2.ReadOnly = true;
            // 
            // dv1
            // 
            this.dv1.AllowUserToAddRows = false;
            this.dv1.AllowUserToDeleteRows = false;
            this.dv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dv1.BackgroundColor = System.Drawing.Color.White;
            this.dv1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dv1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dv1.DefaultCellStyle = dataGridViewCellStyle16;
            this.dv1.GridColor = System.Drawing.Color.Black;
            this.dv1.Location = new System.Drawing.Point(3, 83);
            this.dv1.Name = "dv1";
            this.dv1.ReadOnly = true;
            this.dv1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dv1.RowHeadersVisible = false;
            this.dv1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dv1.ShowCellErrors = false;
            this.dv1.ShowCellToolTips = false;
            this.dv1.ShowEditingIcon = false;
            this.dv1.Size = new System.Drawing.Size(601, 249);
            this.dv1.TabIndex = 5;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // dv2
            // 
            this.dv2.AllowUserToAddRows = false;
            this.dv2.AllowUserToDeleteRows = false;
            this.dv2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dv2.BackgroundColor = System.Drawing.Color.White;
            this.dv2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dv2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dv2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dv2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num1,
            this.Date1,
            this.Name1,
            this.BestLap1});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dv2.DefaultCellStyle = dataGridViewCellStyle18;
            this.dv2.GridColor = System.Drawing.Color.Black;
            this.dv2.Location = new System.Drawing.Point(610, 3);
            this.dv2.Name = "dv2";
            this.dv2.ReadOnly = true;
            this.dv2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dv2.RowHeadersVisible = false;
            this.dv2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dv2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dv2.ShowCellErrors = false;
            this.dv2.ShowCellToolTips = false;
            this.dv2.ShowEditingIcon = false;
            this.dv2.Size = new System.Drawing.Size(234, 329);
            this.dv2.TabIndex = 6;
            // 
            // Num1
            // 
            this.Num1.HeaderText = "№";
            this.Num1.Name = "Num1";
            this.Num1.ReadOnly = true;
            // 
            // Date1
            // 
            this.Date1.HeaderText = "Дата";
            this.Date1.Name = "Date1";
            this.Date1.ReadOnly = true;
            this.Date1.Visible = false;
            // 
            // Name1
            // 
            this.Name1.HeaderText = "Имя";
            this.Name1.Name = "Name1";
            this.Name1.ReadOnly = true;
            // 
            // BestLap1
            // 
            this.BestLap1.HeaderText = "Время";
            this.BestLap1.Name = "BestLap1";
            this.BestLap1.ReadOnly = true;
            // 
            // labelSmooth4
            // 
            this.labelSmooth4.AutoSize = true;
            this.labelSmooth4.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth4.Location = new System.Drawing.Point(3, 58);
            this.labelSmooth4.Name = "labelSmooth4";
            this.labelSmooth4.Size = new System.Drawing.Size(89, 14);
            this.labelSmooth4.TabIndex = 4;
            this.labelSmooth4.Text = "Трасса Default";
            this.labelSmooth4.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown1.Location = new System.Drawing.Point(216, 45);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(52, 27);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Enter += new System.EventHandler(this.numericUpDown1_Enter);
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.button2.Image = global::Rentix.Properties.Resources.printer1;
            this.button2.Location = new System.Drawing.Point(278, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 56);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelSmooth3
            // 
            this.labelSmooth3.AutoSize = true;
            this.labelSmooth3.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth3.Location = new System.Drawing.Point(188, 16);
            this.labelSmooth3.Name = "labelSmooth3";
            this.labelSmooth3.Size = new System.Drawing.Size(80, 14);
            this.labelSmooth3.TabIndex = 2;
            this.labelSmooth3.Text = "Количество";
            this.labelSmooth3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth2
            // 
            this.labelSmooth2.AutoSize = true;
            this.labelSmooth2.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth2.Location = new System.Drawing.Point(4, 33);
            this.labelSmooth2.Name = "labelSmooth2";
            this.labelSmooth2.Size = new System.Drawing.Size(107, 14);
            this.labelSmooth2.TabIndex = 1;
            this.labelSmooth2.Text = "2012-01-23 10:43";
            this.labelSmooth2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelSmooth1
            // 
            this.labelSmooth1.AutoSize = true;
            this.labelSmooth1.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth1.Location = new System.Drawing.Point(3, 9);
            this.labelSmooth1.Name = "labelSmooth1";
            this.labelSmooth1.Size = new System.Drawing.Size(56, 14);
            this.labelSmooth1.TabIndex = 0;
            this.labelSmooth1.Text = "Заезд 54";
            this.labelSmooth1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(482, 45);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // PrintResult1
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(348, 120);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.borderPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "PrintResult1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Печать результатов";
            this.Activated += new System.EventHandler(this.PrintResult_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrintResult_KeyDown);
            this.borderPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dv3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dv2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private LabelSmooth labelSmooth2;
        private LabelSmooth labelSmooth1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private LabelSmooth labelSmooth3;
        private LabelSmooth labelSmooth4;
        private DataGridView dv1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private DataGridView dv2;
        private System.Windows.Forms.Panel panel2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private DataGridView dv3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name2;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestLap2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestLap1;
        private System.Windows.Forms.Button button4;
       }
}