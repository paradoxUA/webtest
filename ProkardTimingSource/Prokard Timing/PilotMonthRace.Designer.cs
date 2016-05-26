using System.Windows.Forms;
namespace Rentix
{
    partial class PilotMonthRace
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
            this.labelSmooth2 = new Rentix.LabelSmooth();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.borderPanel3 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelSmooth1 = new Rentix.LabelSmooth();
            this.dataGridView1 = new DataGridView();
            this.MemberID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.borderPanel1.SuspendLayout();
            this.borderPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.borderPanel1.Controls.Add(this.labelSmooth2);
            this.borderPanel1.Controls.Add(this.button2);
            this.borderPanel1.Controls.Add(this.button1);
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderPanel1.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel1.Location = new System.Drawing.Point(0, 381);
            this.borderPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(673, 40);
            this.borderPanel1.TabIndex = 0;
            this.borderPanel1.UseInnerColor = false;
            // 
            // labelSmooth2
            // 
            this.labelSmooth2.AutoSize = true;
            this.labelSmooth2.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth2.Location = new System.Drawing.Point(3, 13);
            this.labelSmooth2.Name = "labelSmooth2";
            this.labelSmooth2.Size = new System.Drawing.Size(79, 14);
            this.labelSmooth2.TabIndex = 1;
            this.labelSmooth2.Text = "Участников";
            this.labelSmooth2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(362, 5);
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
            this.button1.Location = new System.Drawing.Point(518, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // borderPanel3
            // 
            this.borderPanel3.BackgroundImage = global::Rentix.Properties.Resources.bg;
            this.borderPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel3.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel3.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel3.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Bottom;
            this.borderPanel3.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel3.Controls.Add(this.comboBox1);
            this.borderPanel3.Controls.Add(this.labelSmooth1);
            this.borderPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.borderPanel3.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel3.Location = new System.Drawing.Point(0, 0);
            this.borderPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel3.Name = "borderPanel3";
            this.borderPanel3.Size = new System.Drawing.Size(673, 40);
            this.borderPanel3.TabIndex = 2;
            this.borderPanel3.UseInnerColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(51, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(214, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelSmooth1
            // 
            this.labelSmooth1.AutoSize = true;
            this.labelSmooth1.BackColor = System.Drawing.Color.Transparent;
            this.labelSmooth1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSmooth1.Location = new System.Drawing.Point(4, 12);
            this.labelSmooth1.Name = "labelSmooth1";
            this.labelSmooth1.Size = new System.Drawing.Size(41, 14);
            this.labelSmooth1.TabIndex = 0;
            this.labelSmooth1.Text = "Даты";
            this.labelSmooth1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MemberID,
            this.Number,
            this.FullName,
            this.email,
            this.tel,
            this.Time,
            this.Track,
            this.Column1,
            this.Column2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(673, 341);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // MemberID
            // 
            this.MemberID.HeaderText = "ID";
            this.MemberID.Name = "MemberID";
            this.MemberID.ReadOnly = true;
            this.MemberID.Visible = false;
            // 
            // Number
            // 
            this.Number.HeaderText = "№";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Width = 40;
            // 
            // FullName
            // 
            this.FullName.HeaderText = "ФИО";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Width = 150;
            // 
            // email
            // 
            this.email.HeaderText = "email";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            this.email.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.email.Visible = false;
            // 
            // tel
            // 
            this.tel.HeaderText = "Телефон";
            this.tel.Name = "tel";
            this.tel.ReadOnly = true;
            this.tel.Visible = false;
            // 
            // Time
            // 
            this.Time.HeaderText = "Время (c)";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 60;
            // 
            // Track
            // 
            this.Track.HeaderText = "Трасса";
            this.Track.Name = "Track";
            this.Track.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Дата";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nickname";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // PilotMonthRace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 421);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.borderPanel3);
            this.Controls.Add(this.borderPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PilotMonthRace";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Участники месячной гонки";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PilotMonthRace_KeyDown);
            this.borderPanel1.ResumeLayout(false);
            this.borderPanel1.PerformLayout();
            this.borderPanel3.ResumeLayout(false);
            this.borderPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel1;
        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel3;
        private DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private LabelSmooth labelSmooth1;
        private LabelSmooth labelSmooth2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}