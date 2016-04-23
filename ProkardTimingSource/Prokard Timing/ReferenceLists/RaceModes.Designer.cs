using System.Windows.Forms;
namespace Prokard_Timing
{
    partial class RaceModes
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
            this.raceModes_dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addRaceMode_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.editRaceMode_toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.deleteRaceMode_toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.borderPanel3 = new Microsoft.TeamFoundation.Client.BorderPanel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isDeleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.raceModes_dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.borderPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // raceModes_dataGridView1
            // 
            this.raceModes_dataGridView1.AllowUserToAddRows = false;
            this.raceModes_dataGridView1.AllowUserToDeleteRows = false;
            this.raceModes_dataGridView1.AllowUserToResizeColumns = false;
            this.raceModes_dataGridView1.AllowUserToResizeRows = false;
            this.raceModes_dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.raceModes_dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.raceModes_dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.raceModes_dataGridView1.ColumnHeadersHeight = 20;
            this.raceModes_dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.raceModes_dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.isDeleted});
            this.raceModes_dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.raceModes_dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.raceModes_dataGridView1.MultiSelect = false;
            this.raceModes_dataGridView1.Name = "raceModes_dataGridView1";
            this.raceModes_dataGridView1.ReadOnly = true;
            this.raceModes_dataGridView1.RowHeadersVisible = false;
            this.raceModes_dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.raceModes_dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.raceModes_dataGridView1.ShowCellErrors = false;
            this.raceModes_dataGridView1.ShowCellToolTips = false;
            this.raceModes_dataGridView1.ShowEditingIcon = false;
            this.raceModes_dataGridView1.ShowRowErrors = false;
            this.raceModes_dataGridView1.Size = new System.Drawing.Size(480, 257);
            this.raceModes_dataGridView1.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRaceMode_toolStripButton,
            this.editRaceMode_toolStripButton3,
            this.deleteRaceMode_toolStripButton2,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(480, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addRaceMode_toolStripButton
            // 
            this.addRaceMode_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addRaceMode_toolStripButton.Image = global::Prokard_Timing.Properties.Resources.edit_add;
            this.addRaceMode_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addRaceMode_toolStripButton.Name = "addRaceMode_toolStripButton";
            this.addRaceMode_toolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addRaceMode_toolStripButton.Text = "Добавить трек";
            this.addRaceMode_toolStripButton.ToolTipText = "Добавить режим заезда";
            this.addRaceMode_toolStripButton.Click += new System.EventHandler(this.addRaceMode_toolStripButton_Click);
            // 
            // editRaceMode_toolStripButton3
            // 
            this.editRaceMode_toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editRaceMode_toolStripButton3.Image = global::Prokard_Timing.Properties.Resources.edit__1_;
            this.editRaceMode_toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editRaceMode_toolStripButton3.Name = "editRaceMode_toolStripButton3";
            this.editRaceMode_toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.editRaceMode_toolStripButton3.Text = "Изменить";
            this.editRaceMode_toolStripButton3.Click += new System.EventHandler(this.editRaceMode_toolStripButton3_Click);
            // 
            // deleteRaceMode_toolStripButton2
            // 
            this.deleteRaceMode_toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteRaceMode_toolStripButton2.Image = global::Prokard_Timing.Properties.Resources.edit_remove;
            this.deleteRaceMode_toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteRaceMode_toolStripButton2.Name = "deleteRaceMode_toolStripButton2";
            this.deleteRaceMode_toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.deleteRaceMode_toolStripButton2.Text = "Удалить трек";
            this.deleteRaceMode_toolStripButton2.ToolTipText = "Удалить";
            this.deleteRaceMode_toolStripButton2.Click += new System.EventHandler(this.deleteRaceMode_toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // borderPanel3
            // 
            this.borderPanel3.BackgroundImage = global::Prokard_Timing.Properties.Resources.bg;
            this.borderPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderPanel3.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.borderPanel3.BorderPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel3.BorderSides = Microsoft.TeamFoundation.Client.BorderPanel.Sides.Top;
            this.borderPanel3.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.borderPanel3.Controls.Add(this.CloseButton);
            this.borderPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderPanel3.InnerColor = System.Drawing.SystemColors.Control;
            this.borderPanel3.Location = new System.Drawing.Point(0, 282);
            this.borderPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.borderPanel3.Name = "borderPanel3";
            this.borderPanel3.Size = new System.Drawing.Size(480, 40);
            this.borderPanel3.TabIndex = 3;
            this.borderPanel3.UseInnerColor = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(327, 5);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(150, 30);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Закрыть";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Название";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 140;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Длительность, минут";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 190;
            // 
            // isDeleted
            // 
            this.isDeleted.HeaderText = "Удалён";
            this.isDeleted.Name = "isDeleted";
            this.isDeleted.ReadOnly = true;
            // 
            // RaceModes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 322);
            this.Controls.Add(this.raceModes_dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.borderPanel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RaceModes";
            this.ShowIcon = false;
            this.Text = "Режимы заезда";
            ((System.ComponentModel.ISupportInitialize)(this.raceModes_dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.borderPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.TeamFoundation.Client.BorderPanel borderPanel3;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addRaceMode_toolStripButton;
        private System.Windows.Forms.ToolStripButton editRaceMode_toolStripButton3;
        private System.Windows.Forms.ToolStripButton deleteRaceMode_toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DataGridView raceModes_dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn isDeleted;
    }
}