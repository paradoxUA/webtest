using System.Windows.Forms;
namespace Prokard_Timing
{
    partial class AllPilots
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllPilots));
            this.borderPanel2 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.pilotsList_dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.borderPanel3 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addPilot_toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.editPilot_toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.deletePilot_toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pilotDetails_toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.ballanceCorrection_toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.prepareCertificate_toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.pilotsStat_toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.exportPilots_toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pageSize_toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.lastPage_toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.nextPage_toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.pageNumber_toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.previousPage_toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.firstPage_toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.groupFilter_toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.filter_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.applyFilter_button = new System.Windows.Forms.ToolStripButton();
            this.borderPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pilotsList_dataGridView1)).BeginInit();
            this.borderPanel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // borderPanel2
            // 
            this.borderPanel2.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel2.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel2.BorderSides = ((Microsoft.TeamFoundation.Client.BorderPanel.Sides)((((Microsoft.TeamFoundation.Client.BorderPanel.Sides.Top | Microsoft.TeamFoundation.Client.BorderPanel.Sides.Bottom) 
            | Microsoft.TeamFoundation.Client.BorderPanel.Sides.Left) 
            | Microsoft.TeamFoundation.Client.BorderPanel.Sides.Right)));
            this.borderPanel2.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel2.Controls.Add(this.pilotsList_dataGridView1);
            this.borderPanel2.Controls.Add(this.borderPanel3);
            this.borderPanel2.Controls.Add(this.toolStrip1);
            this.borderPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel2.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel2.Location = new System.Drawing.Point(0, 0);
            this.borderPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel2.Name = "borderPanel2";
            this.borderPanel2.Size = new System.Drawing.Size(1006, 429);
            this.borderPanel2.TabIndex = 1;
            this.borderPanel2.UseInnerColor = false;
            // 
            // pilotsList_dataGridView1
            // 
            this.pilotsList_dataGridView1.AllowUserToAddRows = false;
            this.pilotsList_dataGridView1.AllowUserToDeleteRows = false;
            this.pilotsList_dataGridView1.AllowUserToOrderColumns = true;
            this.pilotsList_dataGridView1.AllowUserToResizeColumns = false;
            this.pilotsList_dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pilotsList_dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.pilotsList_dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.pilotsList_dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pilotsList_dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.pilotsList_dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.pilotsList_dataGridView1.ColumnHeadersHeight = 30;
            this.pilotsList_dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.pilotsList_dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column4,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column5,
            this.Column6,
            this.Column11,
            this.Column12,
            this.Column13});
            this.pilotsList_dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pilotsList_dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.pilotsList_dataGridView1.Location = new System.Drawing.Point(0, 31);
            this.pilotsList_dataGridView1.MultiSelect = false;
            this.pilotsList_dataGridView1.Name = "pilotsList_dataGridView1";
            this.pilotsList_dataGridView1.ReadOnly = true;
            this.pilotsList_dataGridView1.RowHeadersVisible = false;
            this.pilotsList_dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pilotsList_dataGridView1.ShowCellErrors = false;
            this.pilotsList_dataGridView1.ShowCellToolTips = false;
            this.pilotsList_dataGridView1.ShowEditingIcon = false;
            this.pilotsList_dataGridView1.ShowRowErrors = false;
            this.pilotsList_dataGridView1.Size = new System.Drawing.Size(1006, 358);
            this.pilotsList_dataGridView1.TabIndex = 4;
            this.pilotsList_dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.pilotsList_dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 50F;
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Имя";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 90;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Фамилия";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 89;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Никнейм";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 89;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 30F;
            this.Column4.HeaderText = "Пол";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 30;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "e-mail";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 89;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Тел.";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 90;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Группа";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 89;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Дата рождения";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 89;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Создан";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 90;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Баланс";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 89;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "bann";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Visible = false;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Номер карты";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // borderPanel3
            // 
            this.borderPanel3.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.borderPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel3.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel3.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel3.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Top;
            this.borderPanel3.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel3.Controls.Add(this.button2);
            this.borderPanel3.Controls.Add(this.button1);
            this.borderPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderPanel3.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel3.Location = new System.Drawing.Point(0, 389);
            this.borderPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel3.Name = "borderPanel3";
            this.borderPanel3.Size = new System.Drawing.Size(1006, 40);
            this.borderPanel3.TabIndex = 1;
            this.borderPanel3.UseInnerColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(697, 5);
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
            this.button1.Location = new System.Drawing.Point(853, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPilot_toolStripButton1,
            this.editPilot_toolStripButton5,
            this.deletePilot_toolStripButton2,
            this.toolStripSeparator1,
            this.pilotDetails_toolStripButton3,
            this.ballanceCorrection_toolStripButton4,
            this.prepareCertificate_toolStripButton8,
            this.pilotsStat_toolStripButton6,
            this.exportPilots_toolStripButton1,
            this.pageSize_toolStripComboBox2,
            this.lastPage_toolStripButton10,
            this.nextPage_toolStripButton9,
            this.pageNumber_toolStripComboBox,
            this.previousPage_toolStripButton12,
            this.firstPage_toolStripButton11,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.groupFilter_toolStripComboBox,
            this.toolStripLabel2,
            this.filter_TextBox,
            this.applyFilter_button});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1006, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addPilot_toolStripButton1
            // 
            this.addPilot_toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addPilot_toolStripButton1.Image = global::Prokard_Timing.Properties.Resources.edit_add;
            this.addPilot_toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addPilot_toolStripButton1.Name = "addPilot_toolStripButton1";
            this.addPilot_toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.addPilot_toolStripButton1.Text = "Создать карточку пилота";
            this.addPilot_toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // editPilot_toolStripButton5
            // 
            this.editPilot_toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editPilot_toolStripButton5.Image = global::Prokard_Timing.Properties.Resources.edit__1_;
            this.editPilot_toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editPilot_toolStripButton5.Name = "editPilot_toolStripButton5";
            this.editPilot_toolStripButton5.Size = new System.Drawing.Size(28, 28);
            this.editPilot_toolStripButton5.Text = "Изменить данные";
            this.editPilot_toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // deletePilot_toolStripButton2
            // 
            this.deletePilot_toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deletePilot_toolStripButton2.Enabled = false;
            this.deletePilot_toolStripButton2.Image = global::Prokard_Timing.Properties.Resources.edit_remove;
            this.deletePilot_toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deletePilot_toolStripButton2.Name = "deletePilot_toolStripButton2";
            this.deletePilot_toolStripButton2.Size = new System.Drawing.Size(28, 28);
            this.deletePilot_toolStripButton2.Text = "Стереть карточку";
            this.deletePilot_toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // pilotDetails_toolStripButton3
            // 
            this.pilotDetails_toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pilotDetails_toolStripButton3.Image = global::Prokard_Timing.Properties.Resources.messagebox_info;
            this.pilotDetails_toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pilotDetails_toolStripButton3.Name = "pilotDetails_toolStripButton3";
            this.pilotDetails_toolStripButton3.Size = new System.Drawing.Size(28, 28);
            this.pilotDetails_toolStripButton3.Text = "Детальнее";
            this.pilotDetails_toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // ballanceCorrection_toolStripButton4
            // 
            this.ballanceCorrection_toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ballanceCorrection_toolStripButton4.Image = global::Prokard_Timing.Properties.Resources.coins;
            this.ballanceCorrection_toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ballanceCorrection_toolStripButton4.Name = "ballanceCorrection_toolStripButton4";
            this.ballanceCorrection_toolStripButton4.Size = new System.Drawing.Size(28, 28);
            this.ballanceCorrection_toolStripButton4.Text = "Коррекция Баланса";
            this.ballanceCorrection_toolStripButton4.ToolTipText = "Коррекция баланса";
            this.ballanceCorrection_toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // prepareCertificate_toolStripButton8
            // 
            this.prepareCertificate_toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.prepareCertificate_toolStripButton8.Image = global::Prokard_Timing.Properties.Resources.layout;
            this.prepareCertificate_toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prepareCertificate_toolStripButton8.Name = "prepareCertificate_toolStripButton8";
            this.prepareCertificate_toolStripButton8.Size = new System.Drawing.Size(28, 28);
            this.prepareCertificate_toolStripButton8.Text = "Выдать сертификат";
            this.prepareCertificate_toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // pilotsStat_toolStripButton6
            // 
            this.pilotsStat_toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pilotsStat_toolStripButton6.Image = global::Prokard_Timing.Properties.Resources.chart_bar;
            this.pilotsStat_toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pilotsStat_toolStripButton6.Name = "pilotsStat_toolStripButton6";
            this.pilotsStat_toolStripButton6.Size = new System.Drawing.Size(28, 28);
            this.pilotsStat_toolStripButton6.Text = "Общая статистика по пилотам";
            this.pilotsStat_toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // exportPilots_toolStripButton1
            // 
            this.exportPilots_toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportPilots_toolStripButton1.Image = global::Prokard_Timing.Properties.Resources.edit_group;
            this.exportPilots_toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportPilots_toolStripButton1.Name = "exportPilots_toolStripButton1";
            this.exportPilots_toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.exportPilots_toolStripButton1.Text = "Экспорт списка пилотов";
            this.exportPilots_toolStripButton1.Visible = false;
            this.exportPilots_toolStripButton1.Click += new System.EventHandler(this.exportPilots_toolStripButton1_Click);
            // 
            // pageSize_toolStripComboBox2
            // 
            this.pageSize_toolStripComboBox2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pageSize_toolStripComboBox2.AutoSize = false;
            this.pageSize_toolStripComboBox2.AutoToolTip = true;
            this.pageSize_toolStripComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pageSize_toolStripComboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.pageSize_toolStripComboBox2.Items.AddRange(new object[] {
            "25",
            "50",
            "100"});
            this.pageSize_toolStripComboBox2.Margin = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.pageSize_toolStripComboBox2.MergeIndex = 0;
            this.pageSize_toolStripComboBox2.Name = "pageSize_toolStripComboBox2";
            this.pageSize_toolStripComboBox2.Size = new System.Drawing.Size(47, 21);
            this.pageSize_toolStripComboBox2.ToolTipText = "Элементов на странице";
            this.pageSize_toolStripComboBox2.SelectedIndexChanged += new System.EventHandler(this.pageSizeComboboxIndexChanged);
            // 
            // lastPage_toolStripButton10
            // 
            this.lastPage_toolStripButton10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lastPage_toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.lastPage_toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("lastPage_toolStripButton10.Image")));
            this.lastPage_toolStripButton10.Name = "lastPage_toolStripButton10";
            this.lastPage_toolStripButton10.RightToLeftAutoMirrorImage = true;
            this.lastPage_toolStripButton10.Size = new System.Drawing.Size(28, 28);
            this.lastPage_toolStripButton10.Text = "Переместить в конец";
            this.lastPage_toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // nextPage_toolStripButton9
            // 
            this.nextPage_toolStripButton9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.nextPage_toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextPage_toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("nextPage_toolStripButton9.Image")));
            this.nextPage_toolStripButton9.Name = "nextPage_toolStripButton9";
            this.nextPage_toolStripButton9.RightToLeftAutoMirrorImage = true;
            this.nextPage_toolStripButton9.Size = new System.Drawing.Size(28, 28);
            this.nextPage_toolStripButton9.Text = "Переместить вперед";
            this.nextPage_toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // pageNumber_toolStripComboBox
            // 
            this.pageNumber_toolStripComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pageNumber_toolStripComboBox.AutoSize = false;
            this.pageNumber_toolStripComboBox.AutoToolTip = true;
            this.pageNumber_toolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pageNumber_toolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.pageNumber_toolStripComboBox.Items.AddRange(new object[] {
            "25",
            "50",
            "100",
            "Все"});
            this.pageNumber_toolStripComboBox.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.pageNumber_toolStripComboBox.MergeIndex = 0;
            this.pageNumber_toolStripComboBox.Name = "pageNumber_toolStripComboBox";
            this.pageNumber_toolStripComboBox.Size = new System.Drawing.Size(60, 21);
            this.pageNumber_toolStripComboBox.ToolTipText = "Страница";
            this.pageNumber_toolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // previousPage_toolStripButton12
            // 
            this.previousPage_toolStripButton12.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.previousPage_toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previousPage_toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("previousPage_toolStripButton12.Image")));
            this.previousPage_toolStripButton12.Name = "previousPage_toolStripButton12";
            this.previousPage_toolStripButton12.RightToLeftAutoMirrorImage = true;
            this.previousPage_toolStripButton12.Size = new System.Drawing.Size(28, 28);
            this.previousPage_toolStripButton12.Text = "Переместить назад";
            this.previousPage_toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // firstPage_toolStripButton11
            // 
            this.firstPage_toolStripButton11.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.firstPage_toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.firstPage_toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("firstPage_toolStripButton11.Image")));
            this.firstPage_toolStripButton11.Name = "firstPage_toolStripButton11";
            this.firstPage_toolStripButton11.RightToLeftAutoMirrorImage = true;
            this.firstPage_toolStripButton11.Size = new System.Drawing.Size(28, 28);
            this.firstPage_toolStripButton11.Text = "Переместить в начало";
            this.firstPage_toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(15, 1, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 28);
            this.toolStripLabel1.Text = "Группа";
            // 
            // groupFilter_toolStripComboBox
            // 
            this.groupFilter_toolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupFilter_toolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.groupFilter_toolStripComboBox.Name = "groupFilter_toolStripComboBox";
            this.groupFilter_toolStripComboBox.Size = new System.Drawing.Size(100, 31);
            this.groupFilter_toolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox3_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(48, 28);
            this.toolStripLabel2.Text = " Фильтр";
            // 
            // filter_TextBox
            // 
            this.filter_TextBox.AutoSize = false;
            this.filter_TextBox.Name = "filter_TextBox";
            this.filter_TextBox.Size = new System.Drawing.Size(100, 25);
            this.filter_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            this.filter_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyUp);
            // 
            // applyFilter_button
            // 
            this.applyFilter_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.applyFilter_button.Image = global::Prokard_Timing.Properties.Resources.apply;
            this.applyFilter_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.applyFilter_button.Name = "applyFilter_button";
            this.applyFilter_button.Size = new System.Drawing.Size(28, 28);
            this.applyFilter_button.Text = "Применить";
            this.applyFilter_button.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // AllPilots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1006, 429);
            this.Controls.Add(this.borderPanel2);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "AllPilots";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Все пилоты";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllPilots_KeyUp);
            this.borderPanel2.ResumeLayout(false);
            this.borderPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pilotsList_dataGridView1)).EndInit();
            this.borderPanel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel2;
        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton deletePilot_toolStripButton2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton pilotDetails_toolStripButton3;
        private System.Windows.Forms.ToolStripButton ballanceCorrection_toolStripButton4;
        private System.Windows.Forms.ToolStripButton editPilot_toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton pilotsStat_toolStripButton6;
        private System.Windows.Forms.ToolStripComboBox pageSize_toolStripComboBox2;
        private System.Windows.Forms.ToolStripButton addPilot_toolStripButton1;
        private System.Windows.Forms.ToolStripButton lastPage_toolStripButton10;
        private System.Windows.Forms.ToolStripButton nextPage_toolStripButton9;
        private System.Windows.Forms.ToolStripComboBox pageNumber_toolStripComboBox;
        private System.Windows.Forms.ToolStripButton previousPage_toolStripButton12;
        private System.Windows.Forms.ToolStripButton firstPage_toolStripButton11;
        private DataGridView pilotsList_dataGridView1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox groupFilter_toolStripComboBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox filter_TextBox;
        private System.Windows.Forms.ToolStripButton applyFilter_button;
        private System.Windows.Forms.ToolStripButton prepareCertificate_toolStripButton8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private ToolStripButton exportPilots_toolStripButton1;
    }
}