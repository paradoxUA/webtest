using System.Windows.Forms;
namespace Rentix
{
    partial class CassaJurnal
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CassaJurnal));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.borderPanel1 = new Microsoft.TeamFoundation.Client.BorderPanel();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.borderPanel2 = new Microsoft.TeamFoundation.Client.BorderPanel();
			this.moneyTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.partnerComboBox = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.userGroupsComboBox = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.raceTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.pageSize_toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
			this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
			this.label7 = new System.Windows.Forms.Label();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.label6 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.borderPanel3 = new Microsoft.TeamFoundation.Client.BorderPanel();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.inCash_label2 = new System.Windows.Forms.Label();
			this.cassa_dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.borderPanel1.SuspendLayout();
			this.borderPanel2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.borderPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cassa_dataGridView1)).BeginInit();
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
			this.borderPanel1.Controls.Add(this.button2);
			this.borderPanel1.Controls.Add(this.button1);
			this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
			this.borderPanel1.Location = new System.Drawing.Point(0, 441);
			this.borderPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.borderPanel1.Name = "borderPanel1";
			this.borderPanel1.Size = new System.Drawing.Size(1138, 40);
			this.borderPanel1.TabIndex = 2;
			this.borderPanel1.UseInnerColor = false;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(828, 6);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(150, 30);
			this.button2.TabIndex = 1;
			this.button2.Text = "Печать";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(984, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(150, 30);
			this.button1.TabIndex = 0;
			this.button1.Text = "Закрыть";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// borderPanel2
			// 
			this.borderPanel2.BackgroundImage = global::Rentix.Properties.Resources.bg;
			this.borderPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.borderPanel2.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.borderPanel2.BorderPadding = new System.Windows.Forms.Padding(0);
			this.borderPanel2.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Bottom;
			this.borderPanel2.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
			this.borderPanel2.Controls.Add(this.moneyTypeComboBox);
			this.borderPanel2.Controls.Add(this.label11);
			this.borderPanel2.Controls.Add(this.partnerComboBox);
			this.borderPanel2.Controls.Add(this.label10);
			this.borderPanel2.Controls.Add(this.userGroupsComboBox);
			this.borderPanel2.Controls.Add(this.label9);
			this.borderPanel2.Controls.Add(this.raceTypeComboBox);
			this.borderPanel2.Controls.Add(this.label2);
			this.borderPanel2.Controls.Add(this.toolStrip1);
			this.borderPanel2.Controls.Add(this.label7);
			this.borderPanel2.Controls.Add(this.dateTimePicker2);
			this.borderPanel2.Controls.Add(this.radioButton3);
			this.borderPanel2.Controls.Add(this.radioButton2);
			this.borderPanel2.Controls.Add(this.radioButton1);
			this.borderPanel2.Controls.Add(this.label6);
			this.borderPanel2.Controls.Add(this.dateTimePicker1);
			this.borderPanel2.Controls.Add(this.label1);
			this.borderPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.borderPanel2.InnerColor = System.Drawing.SystemColors.Control;
			this.borderPanel2.Location = new System.Drawing.Point(0, 0);
			this.borderPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.borderPanel2.Name = "borderPanel2";
			this.borderPanel2.Size = new System.Drawing.Size(1138, 53);
			this.borderPanel2.TabIndex = 0;
			this.borderPanel2.UseInnerColor = false;
			// 
			// moneyTypeComboBox
			// 
			this.moneyTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.moneyTypeComboBox.FormattingEnabled = true;
			this.moneyTypeComboBox.Location = new System.Drawing.Point(750, 29);
			this.moneyTypeComboBox.Name = "moneyTypeComboBox";
			this.moneyTypeComboBox.Size = new System.Drawing.Size(136, 21);
			this.moneyTypeComboBox.TabIndex = 15;
			this.moneyTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.moneyTypeComboBox_SelectedIndexChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.BackColor = System.Drawing.Color.Transparent;
			this.label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label11.Location = new System.Drawing.Point(669, 32);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(78, 13);
			this.label11.TabIndex = 14;
			this.label11.Text = "Тип оплаты:";
			// 
			// partnerComboBox
			// 
			this.partnerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.partnerComboBox.FormattingEnabled = true;
			this.partnerComboBox.Location = new System.Drawing.Point(521, 28);
			this.partnerComboBox.Name = "partnerComboBox";
			this.partnerComboBox.Size = new System.Drawing.Size(136, 21);
			this.partnerComboBox.TabIndex = 13;
			this.partnerComboBox.SelectedIndexChanged += new System.EventHandler(this.partnerComboBox_SelectedIndexChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.BackColor = System.Drawing.Color.Transparent;
			this.label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.Location = new System.Drawing.Point(455, 32);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 13);
			this.label10.TabIndex = 12;
			this.label10.Text = "Партнёр:";
			// 
			// userGroupsComboBox
			// 
			this.userGroupsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.userGroupsComboBox.FormattingEnabled = true;
			this.userGroupsComboBox.Location = new System.Drawing.Point(305, 28);
			this.userGroupsComboBox.Name = "userGroupsComboBox";
			this.userGroupsComboBox.Size = new System.Drawing.Size(136, 21);
			this.userGroupsComboBox.TabIndex = 11;
			this.userGroupsComboBox.SelectedIndexChanged += new System.EventHandler(this.userGroupsComboBox_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.Location = new System.Drawing.Point(248, 32);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(51, 13);
			this.label9.TabIndex = 10;
			this.label9.Text = "Группа:";
			// 
			// raceTypeComboBox
			// 
			this.raceTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.raceTypeComboBox.FormattingEnabled = true;
			this.raceTypeComboBox.Location = new System.Drawing.Point(104, 28);
			this.raceTypeComboBox.Name = "raceTypeComboBox";
			this.raceTypeComboBox.Size = new System.Drawing.Size(136, 21);
			this.raceTypeComboBox.TabIndex = 9;
			this.raceTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.raceTypeComboBox_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(4, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Режим заезда:";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip1.BackgroundImage = global::Rentix.Properties.Resources.bg;
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.pageSize_toolStripComboBox,
            this.toolStripButton10,
            this.toolStripButton9,
            this.toolStripComboBox1,
            this.toolStripButton12,
            this.toolStripButton11});
			this.toolStrip1.Location = new System.Drawing.Point(885, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(253, 31);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// pageSize_toolStripComboBox
			// 
			this.pageSize_toolStripComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.pageSize_toolStripComboBox.AutoSize = false;
			this.pageSize_toolStripComboBox.AutoToolTip = true;
			this.pageSize_toolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pageSize_toolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.pageSize_toolStripComboBox.Items.AddRange(new object[] {
            "25",
            "50",
            "100",
            "300"});
			this.pageSize_toolStripComboBox.Margin = new System.Windows.Forms.Padding(10, 0, 1, 0);
			this.pageSize_toolStripComboBox.MergeIndex = 0;
			this.pageSize_toolStripComboBox.Name = "pageSize_toolStripComboBox";
			this.pageSize_toolStripComboBox.Size = new System.Drawing.Size(60, 23);
			this.pageSize_toolStripComboBox.ToolTipText = "Элементов на странице";
			this.pageSize_toolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox2_Click);
			// 
			// toolStripButton10
			// 
			this.toolStripButton10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
			this.toolStripButton10.Name = "toolStripButton10";
			this.toolStripButton10.RightToLeftAutoMirrorImage = true;
			this.toolStripButton10.Size = new System.Drawing.Size(28, 28);
			this.toolStripButton10.Text = "Переместить в конец";
			this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
			// 
			// toolStripButton9
			// 
			this.toolStripButton9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
			this.toolStripButton9.Name = "toolStripButton9";
			this.toolStripButton9.RightToLeftAutoMirrorImage = true;
			this.toolStripButton9.Size = new System.Drawing.Size(28, 28);
			this.toolStripButton9.Text = "Переместить вперед";
			this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
			// 
			// toolStripComboBox1
			// 
			this.toolStripComboBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripComboBox1.AutoSize = false;
			this.toolStripComboBox1.AutoToolTip = true;
			this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.toolStripComboBox1.Items.AddRange(new object[] {
            "25",
            "50",
            "100",
            "Все"});
			this.toolStripComboBox1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.toolStripComboBox1.MergeIndex = 0;
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new System.Drawing.Size(60, 23);
			this.toolStripComboBox1.ToolTipText = "Страница";
			this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_Click);
			// 
			// toolStripButton12
			// 
			this.toolStripButton12.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
			this.toolStripButton12.Name = "toolStripButton12";
			this.toolStripButton12.RightToLeftAutoMirrorImage = true;
			this.toolStripButton12.Size = new System.Drawing.Size(28, 28);
			this.toolStripButton12.Text = "Переместить назад";
			this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
			// 
			// toolStripButton11
			// 
			this.toolStripButton11.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
			this.toolStripButton11.Name = "toolStripButton11";
			this.toolStripButton11.RightToLeftAutoMirrorImage = true;
			this.toolStripButton11.Size = new System.Drawing.Size(28, 28);
			this.toolStripButton11.Text = "Переместить в начало";
			this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.Location = new System.Drawing.Point(191, 9);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(12, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "-";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(206, 5);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(141, 20);
			this.dateTimePicker2.TabIndex = 6;
			this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.BackColor = System.Drawing.Color.Transparent;
			this.radioButton3.Location = new System.Drawing.Point(672, 7);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(95, 17);
			this.radioButton3.TabIndex = 5;
			this.radioButton3.Text = "Оба варианта";
			this.radioButton3.UseVisualStyleBackColor = false;
			this.radioButton3.Visible = false;
			this.radioButton3.Click += new System.EventHandler(this.radioButton3_CheckedChanged);
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.BackColor = System.Drawing.Color.Transparent;
			this.radioButton2.Location = new System.Drawing.Point(573, 7);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(92, 17);
			this.radioButton2.TabIndex = 4;
			this.radioButton2.Text = "Виртуальные";
			this.radioButton2.UseVisualStyleBackColor = false;
			this.radioButton2.Click += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.BackColor = System.Drawing.Color.Transparent;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(491, 7);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(76, 17);
			this.radioButton1.TabIndex = 3;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Реальные";
			this.radioButton1.UseVisualStyleBackColor = false;
			this.radioButton1.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(353, 9);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(131, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Денежные операции";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(48, 5);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(141, 20);
			this.dateTimePicker1.TabIndex = 1;
			this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(4, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Дата ";
			// 
			// borderPanel3
			// 
			this.borderPanel3.BackgroundImage = global::Rentix.Properties.Resources.bg;
			this.borderPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.borderPanel3.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.borderPanel3.BorderPadding = new System.Windows.Forms.Padding(0);
			this.borderPanel3.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Top;
			this.borderPanel3.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
			this.borderPanel3.Controls.Add(this.label8);
			this.borderPanel3.Controls.Add(this.label5);
			this.borderPanel3.Controls.Add(this.label4);
			this.borderPanel3.Controls.Add(this.label3);
			this.borderPanel3.Controls.Add(this.inCash_label2);
			this.borderPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.borderPanel3.InnerColor = System.Drawing.SystemColors.Control;
			this.borderPanel3.Location = new System.Drawing.Point(0, 409);
			this.borderPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.borderPanel3.Name = "borderPanel3";
			this.borderPanel3.Size = new System.Drawing.Size(1138, 32);
			this.borderPanel3.TabIndex = 3;
			this.borderPanel3.UseInnerColor = false;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.ForeColor = System.Drawing.Color.DarkGoldenrod;
			this.label8.Location = new System.Drawing.Point(171, 9);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(141, 13);
			this.label8.TabIndex = 5;
			this.label8.Text = "Сумма операций:  0 грн";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.ForeColor = System.Drawing.Color.DarkBlue;
			this.label5.Location = new System.Drawing.Point(722, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Сальдо:  0 грн";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label4.Location = new System.Drawing.Point(1018, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Кт:  0 грн";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.Color.DarkGreen;
			this.label3.Location = new System.Drawing.Point(885, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Дт:  0 грн";
			// 
			// inCash_label2
			// 
			this.inCash_label2.AutoSize = true;
			this.inCash_label2.BackColor = System.Drawing.Color.Transparent;
			this.inCash_label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.inCash_label2.Location = new System.Drawing.Point(3, 9);
			this.inCash_label2.Name = "inCash_label2";
			this.inCash_label2.Size = new System.Drawing.Size(103, 13);
			this.inCash_label2.TabIndex = 1;
			this.inCash_label2.Text = "В кассе:  100 грн";
			// 
			// cassa_dataGridView1
			// 
			this.cassa_dataGridView1.AllowUserToAddRows = false;
			this.cassa_dataGridView1.AllowUserToDeleteRows = false;
			this.cassa_dataGridView1.AllowUserToResizeColumns = false;
			this.cassa_dataGridView1.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			this.cassa_dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.cassa_dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.cassa_dataGridView1.BackgroundColor = System.Drawing.Color.White;
			this.cassa_dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.cassa_dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.cassa_dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.cassa_dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column6,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
			this.cassa_dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
			this.cassa_dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cassa_dataGridView1.Location = new System.Drawing.Point(0, 53);
			this.cassa_dataGridView1.Name = "cassa_dataGridView1";
			this.cassa_dataGridView1.ReadOnly = true;
			this.cassa_dataGridView1.RowHeadersVisible = false;
			this.cassa_dataGridView1.Size = new System.Drawing.Size(1138, 356);
			this.cassa_dataGridView1.TabIndex = 4;
			this.cassa_dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "ID";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Width = 43;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Операция";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Width = 82;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Пользователь";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			this.Column3.Width = 105;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Сумма";
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			this.Column4.Width = 66;
			// 
			// Column5
			// 
			this.Column5.HeaderText = "Комментарий";
			this.Column5.Name = "Column5";
			this.Column5.ReadOnly = true;
			this.Column5.Width = 102;
			// 
			// Column7
			// 
			this.Column7.HeaderText = "Рейс";
			this.Column7.Name = "Column7";
			this.Column7.ReadOnly = true;
			this.Column7.Width = 57;
			// 
			// Column6
			// 
			this.Column6.HeaderText = "Дата";
			this.Column6.Name = "Column6";
			this.Column6.ReadOnly = true;
			this.Column6.Width = 58;
			// 
			// Column8
			// 
			this.Column8.HeaderText = "Режим заезда";
			this.Column8.Name = "Column8";
			this.Column8.ReadOnly = true;
			this.Column8.Width = 106;
			// 
			// Column9
			// 
			this.Column9.HeaderText = "Партнер";
			this.Column9.Name = "Column9";
			this.Column9.ReadOnly = true;
			this.Column9.Width = 75;
			// 
			// Column10
			// 
			this.Column10.HeaderText = "Терминал";
			this.Column10.Name = "Column10";
			this.Column10.ReadOnly = true;
			this.Column10.Width = 64;
			// 
			// Column11
			// 
			this.Column11.HeaderText = "Группа";
			this.Column11.Name = "Column11";
			this.Column11.ReadOnly = true;
			this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column11.Width = 48;
			// 
			// Column12
			// 
			this.Column12.HeaderText = "Сертификат";
			this.Column12.Name = "Column12";
			this.Column12.ReadOnly = true;
			this.Column12.Width = 93;
			// 
			// CassaJurnal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1138, 481);
			this.Controls.Add(this.cassa_dataGridView1);
			this.Controls.Add(this.borderPanel3);
			this.Controls.Add(this.borderPanel2);
			this.Controls.Add(this.borderPanel1);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.Name = "CassaJurnal";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Журнал кассы";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CassaJurnal_KeyUp);
			this.borderPanel1.ResumeLayout(false);
			this.borderPanel2.ResumeLayout(false);
			this.borderPanel2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.borderPanel3.ResumeLayout(false);
			this.borderPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cassa_dataGridView1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel2;
        private System.Windows.Forms.Label label1;
        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel3;
        private DataGridView cassa_dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label inCash_label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox pageSize_toolStripComboBox;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
		private Label label2;
		private ComboBox raceTypeComboBox;
		private Label label8;
		private ComboBox userGroupsComboBox;
		private Label label9;
		private ComboBox partnerComboBox;
		private Label label10;
		private ComboBox moneyTypeComboBox;
		private Label label11;
		private DataGridViewTextBoxColumn Column1;
		private DataGridViewTextBoxColumn Column2;
		private DataGridViewTextBoxColumn Column3;
		private DataGridViewTextBoxColumn Column4;
		private DataGridViewTextBoxColumn Column5;
		private DataGridViewTextBoxColumn Column7;
		private DataGridViewTextBoxColumn Column6;
		private DataGridViewTextBoxColumn Column8;
		private DataGridViewTextBoxColumn Column9;
		private DataGridViewCheckBoxColumn Column10;
		private DataGridViewTextBoxColumn Column11;
		private DataGridViewTextBoxColumn Column12;
	}
}