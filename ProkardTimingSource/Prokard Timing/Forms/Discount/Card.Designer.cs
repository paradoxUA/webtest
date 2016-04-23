using System.Windows.Forms;
namespace Prokard_Timing
{
    partial class Discount_Card
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cardsList_dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.owner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seller = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salePlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsBlocked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addCard_Button1 = new System.Windows.Forms.ToolStripButton();
            this.blockCard_Button2 = new System.Windows.Forms.ToolStripButton();
            this.addCartsRange_toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.editCard_Button3 = new System.Windows.Forms.ToolStripButton();
            this.cardDetails_Button4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.filter_TextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.applyFilter_Button5 = new System.Windows.Forms.ToolStripButton();
            this.clearFilter_Button6 = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.addDiscountcardType_toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.deleteDiscountCardType_toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.cardTypies_dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardsList_dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardTypies_dataGridView1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(844, 518);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cardsList_dataGridView1);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(836, 492);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Скидочные карты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cardsList_dataGridView1
            // 
            this.cardsList_dataGridView1.AllowUserToAddRows = false;
            this.cardsList_dataGridView1.AllowUserToDeleteRows = false;
            this.cardsList_dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cardsList_dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.cardsList_dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardsList_dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.number,
            this.discount,
            this.owner,
            this.createDate,
            this.seller,
            this.salePlace,
            this.referent,
            this.IsBlocked});
            this.cardsList_dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardsList_dataGridView1.Location = new System.Drawing.Point(3, 28);
            this.cardsList_dataGridView1.MultiSelect = false;
            this.cardsList_dataGridView1.Name = "cardsList_dataGridView1";
            this.cardsList_dataGridView1.ReadOnly = true;
            this.cardsList_dataGridView1.RowHeadersVisible = false;
            this.cardsList_dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cardsList_dataGridView1.Size = new System.Drawing.Size(830, 461);
            this.cardsList_dataGridView1.TabIndex = 2;
            this.cardsList_dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cardsList_dataGridView1_MouseDoubleClick);
            // 
            // id
            // 
            this.id.HeaderText = "#";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // number
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.number.DefaultCellStyle = dataGridViewCellStyle1;
            this.number.HeaderText = "Номер карты";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            // 
            // discount
            // 
            this.discount.HeaderText = "% скидки";
            this.discount.Name = "discount";
            this.discount.ReadOnly = true;
            // 
            // owner
            // 
            this.owner.HeaderText = "Владелец";
            this.owner.Name = "owner";
            this.owner.ReadOnly = true;
            // 
            // createDate
            // 
            this.createDate.HeaderText = "Дата выдачи";
            this.createDate.Name = "createDate";
            this.createDate.ReadOnly = true;
            // 
            // seller
            // 
            this.seller.HeaderText = "Кем выдана";
            this.seller.Name = "seller";
            this.seller.ReadOnly = true;
            // 
            // salePlace
            // 
            this.salePlace.HeaderText = "Где выдана";
            this.salePlace.Name = "salePlace";
            this.salePlace.ReadOnly = true;
            this.salePlace.Visible = false;
            // 
            // referent
            // 
            this.referent.HeaderText = "Референт";
            this.referent.Name = "referent";
            this.referent.ReadOnly = true;
            this.referent.Visible = false;
            // 
            // IsBlocked
            // 
            this.IsBlocked.HeaderText = "Заблокирована";
            this.IsBlocked.Name = "IsBlocked";
            this.IsBlocked.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCard_Button1,
            this.blockCard_Button2,
            this.addCartsRange_toolStripButton3,
            this.editCard_Button3,
            this.cardDetails_Button4,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.filter_TextBox1,
            this.toolStripComboBox1,
            this.applyFilter_Button5,
            this.clearFilter_Button6});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(830, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addCard_Button1
            // 
            this.addCard_Button1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addCard_Button1.Image = global::Prokard_Timing.Properties.Resources.add;
            this.addCard_Button1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addCard_Button1.Name = "addCard_Button1";
            this.addCard_Button1.Size = new System.Drawing.Size(23, 22);
            this.addCard_Button1.Text = "Добавить карту";
            this.addCard_Button1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // blockCard_Button2
            // 
            this.blockCard_Button2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.blockCard_Button2.Image = global::Prokard_Timing.Properties.Resources.edit_remove;
            this.blockCard_Button2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.blockCard_Button2.Name = "blockCard_Button2";
            this.blockCard_Button2.Size = new System.Drawing.Size(23, 22);
            this.blockCard_Button2.Text = "Удалить карту";
            this.blockCard_Button2.Click += new System.EventHandler(this.blockCard_Button2_Click);
            // 
            // addCartsRange_toolStripButton3
            // 
            this.addCartsRange_toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addCartsRange_toolStripButton3.Image = global::Prokard_Timing.Properties.Resources.gnome_window_manager;
            this.addCartsRange_toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addCartsRange_toolStripButton3.Name = "addCartsRange_toolStripButton3";
            this.addCartsRange_toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.addCartsRange_toolStripButton3.Text = "Добавить диапазон карт";
            this.addCartsRange_toolStripButton3.Click += new System.EventHandler(this.addCartsRange_toolStripButton3_Click);
            // 
            // editCard_Button3
            // 
            this.editCard_Button3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editCard_Button3.Image = global::Prokard_Timing.Properties.Resources.edit__1_;
            this.editCard_Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editCard_Button3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editCard_Button3.Name = "editCard_Button3";
            this.editCard_Button3.Size = new System.Drawing.Size(23, 22);
            this.editCard_Button3.Text = "Редактировать карту";
            this.editCard_Button3.Click += new System.EventHandler(this.editCard_Button3_Click);
            // 
            // cardDetails_Button4
            // 
            this.cardDetails_Button4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cardDetails_Button4.Image = global::Prokard_Timing.Properties.Resources.getinfo;
            this.cardDetails_Button4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cardDetails_Button4.Name = "cardDetails_Button4";
            this.cardDetails_Button4.Size = new System.Drawing.Size(23, 22);
            this.cardDetails_Button4.Text = "toolStripButton4";
            this.cardDetails_Button4.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(49, 22);
            this.toolStripLabel1.Text = "Фильтр:";
            // 
            // filter_TextBox1
            // 
            this.filter_TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filter_TextBox1.Name = "filter_TextBox1";
            this.filter_TextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.Visible = false;
            // 
            // applyFilter_Button5
            // 
            this.applyFilter_Button5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.applyFilter_Button5.Image = global::Prokard_Timing.Properties.Resources.apply;
            this.applyFilter_Button5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.applyFilter_Button5.Name = "applyFilter_Button5";
            this.applyFilter_Button5.Size = new System.Drawing.Size(23, 22);
            this.applyFilter_Button5.Text = "Apply Filter";
            this.applyFilter_Button5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // clearFilter_Button6
            // 
            this.clearFilter_Button6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearFilter_Button6.Image = global::Prokard_Timing.Properties.Resources.delete;
            this.clearFilter_Button6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearFilter_Button6.Name = "clearFilter_Button6";
            this.clearFilter_Button6.Size = new System.Drawing.Size(23, 22);
            this.clearFilter_Button6.Text = "Reset Filter";
            this.clearFilter_Button6.Click += new System.EventHandler(this.clearFilter_Button6_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.toolStrip3);
            this.tabPage3.Controls.Add(this.cardTypies_dataGridView1);
            this.tabPage3.Controls.Add(this.toolStrip2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(836, 492);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Типы карт (справочник)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDiscountcardType_toolStripButton3,
            this.deleteDiscountCardType_toolStripButton4});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(830, 25);
            this.toolStrip3.TabIndex = 2;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // addDiscountcardType_toolStripButton3
            // 
            this.addDiscountcardType_toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addDiscountcardType_toolStripButton3.Image = global::Prokard_Timing.Properties.Resources.add;
            this.addDiscountcardType_toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addDiscountcardType_toolStripButton3.Name = "addDiscountcardType_toolStripButton3";
            this.addDiscountcardType_toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.addDiscountcardType_toolStripButton3.Text = "Добавить тип карт";
            this.addDiscountcardType_toolStripButton3.Click += new System.EventHandler(this.addDiscountcardType_toolStripButton3_Click);
            // 
            // deleteDiscountCardType_toolStripButton4
            // 
            this.deleteDiscountCardType_toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteDiscountCardType_toolStripButton4.Image = global::Prokard_Timing.Properties.Resources.delete;
            this.deleteDiscountCardType_toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteDiscountCardType_toolStripButton4.Name = "deleteDiscountCardType_toolStripButton4";
            this.deleteDiscountCardType_toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.deleteDiscountCardType_toolStripButton4.Text = "Удалить тип карт";
            this.deleteDiscountCardType_toolStripButton4.Click += new System.EventHandler(this.deleteDiscountCardType_toolStripButton4_Click);
            // 
            // cardTypies_dataGridView1
            // 
            this.cardTypies_dataGridView1.AllowUserToAddRows = false;
            this.cardTypies_dataGridView1.AllowUserToDeleteRows = false;
            this.cardTypies_dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardTypies_dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardTypies_dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.cardTypies_dataGridView1.Location = new System.Drawing.Point(0, 29);
            this.cardTypies_dataGridView1.MultiSelect = false;
            this.cardTypies_dataGridView1.Name = "cardTypies_dataGridView1";
            this.cardTypies_dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cardTypies_dataGridView1.Size = new System.Drawing.Size(840, 463);
            this.cardTypies_dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "id";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "% скидки";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Удалён";
            this.Column3.Name = "Column3";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(830, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Prokard_Timing.Properties.Resources.add;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "add_toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Prokard_Timing.Properties.Resources.delete;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "delete_toolStripButton2";
            // 
            // Discount_Card
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 518);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "Discount_Card";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Скидочные карты";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Discount_Card_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardsList_dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardTypies_dataGridView1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DataGridView cardsList_dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addCard_Button1;
        private System.Windows.Forms.ToolStripButton blockCard_Button2;
        private System.Windows.Forms.ToolStripButton editCard_Button3;
        private System.Windows.Forms.ToolStripButton cardDetails_Button4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox filter_TextBox1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton applyFilter_Button5;
        private System.Windows.Forms.ToolStripButton clearFilter_Button6;
        private TabPage tabPage3;
        private DataGridView cardTypies_dataGridView1;
        private ToolStrip toolStrip2;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStrip toolStrip3;
        private ToolStripButton addDiscountcardType_toolStripButton3;
        private ToolStripButton deleteDiscountCardType_toolStripButton4;
        private ToolStripButton addCartsRange_toolStripButton3;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn number;
        private DataGridViewTextBoxColumn discount;
        private DataGridViewTextBoxColumn owner;
        private DataGridViewTextBoxColumn createDate;
        private DataGridViewTextBoxColumn seller;
        private DataGridViewTextBoxColumn salePlace;
        private DataGridViewTextBoxColumn referent;
        private DataGridViewTextBoxColumn IsBlocked;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
    }
}