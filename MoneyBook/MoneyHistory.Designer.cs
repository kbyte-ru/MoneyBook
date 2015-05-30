namespace MoneyBook.WinApp
{
  partial class MoneyHistory
  {
    /// <summary> 
    /// Требуется переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором компонентов

    /// <summary> 
    /// Обязательный метод для поддержки конструктора - не изменяйте 
    /// содержимое данного метода при помощи редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoneyHistory));
      this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
      this.StatusTitle = new System.Windows.Forms.ToolStripStatusLabel();
      this.TotalItems = new System.Windows.Forms.ToolStripStatusLabel();
      this.TotalAmountTitle = new System.Windows.Forms.ToolStripStatusLabel();
      this.TotalAmount = new System.Windows.Forms.ToolStripStatusLabel();
      this.DateFrom = new System.Windows.Forms.DateTimePicker();
      this.DateTo = new System.Windows.Forms.DateTimePicker();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.DataGridView1 = new MoneyBook.WinApp.MDataGridView();
      this.ItemIcon = new System.Windows.Forms.DataGridViewImageColumn();
      this.ItemCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ItemSubcategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ItemTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ItemAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ItemDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ItemAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ItemCurrency = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ToolStrip3 = new MoneyBook.WinApp.MToolStrip();
      this.btnAdd = new System.Windows.Forms.ToolStripButton();
      this.btnReport = new System.Windows.Forms.ToolStripButton();
      this.btnDelete = new System.Windows.Forms.ToolStripButton();
      this.btnEdit = new System.Windows.Forms.ToolStripButton();
      this.ToolStrip2 = new MoneyBook.WinApp.MToolStrip();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
      this.AmountFrom = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
      this.AmountTo = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnFilter = new System.Windows.Forms.ToolStripButton();
      this.ToolStrip1 = new MoneyBook.WinApp.MToolStrip();
      this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.Accounts = new System.Windows.Forms.ToolStripComboBox();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.MoneyItems = new System.Windows.Forms.ToolStripComboBox();
      this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripLabel13 = new System.Windows.Forms.ToolStripLabel();
      this.Categories = new System.Windows.Forms.ToolStripComboBox();
      this.StatusStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
      this.ToolStrip3.SuspendLayout();
      this.ToolStrip2.SuspendLayout();
      this.ToolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // StatusStrip1
      // 
      this.StatusStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.StatusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
      this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusTitle,
            this.TotalItems,
            this.TotalAmountTitle,
            this.TotalAmount});
      this.StatusStrip1.Location = new System.Drawing.Point(0, 320);
      this.StatusStrip1.Name = "StatusStrip1";
      this.StatusStrip1.Size = new System.Drawing.Size(659, 22);
      this.StatusStrip1.SizingGrip = false;
      this.StatusStrip1.TabIndex = 9;
      this.StatusStrip1.Text = "StatusStrip1";
      // 
      // StatusTitle
      // 
      this.StatusTitle.Name = "StatusTitle";
      this.StatusTitle.Size = new System.Drawing.Size(161, 17);
      this.StatusTitle.Text = "Доходы с 01.06 по 30.06.2015";
      // 
      // TotalItems
      // 
      this.TotalItems.Name = "TotalItems";
      this.TotalItems.Size = new System.Drawing.Size(48, 17);
      this.TotalItems.Text = "Всего: 0";
      // 
      // TotalAmountTitle
      // 
      this.TotalAmountTitle.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
      this.TotalAmountTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.TotalAmountTitle.Name = "TotalAmountTitle";
      this.TotalAmountTitle.Size = new System.Drawing.Size(50, 17);
      this.TotalAmountTitle.Text = "Сумма: ";
      // 
      // TotalAmount
      // 
      this.TotalAmount.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
      this.TotalAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.TotalAmount.Name = "TotalAmount";
      this.TotalAmount.Size = new System.Drawing.Size(12, 17);
      this.TotalAmount.Text = "-";
      // 
      // DateFrom
      // 
      this.DateFrom.CustomFormat = "dd.MM.yyyy";
      this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.DateFrom.Location = new System.Drawing.Point(444, 28);
      this.DateFrom.Name = "DateFrom";
      this.DateFrom.Size = new System.Drawing.Size(103, 20);
      this.DateFrom.TabIndex = 12;
      // 
      // DateTo
      // 
      this.DateTo.CustomFormat = "dd.MM.yyyy";
      this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.DateTo.Location = new System.Drawing.Point(553, 28);
      this.DateTo.Name = "DateTo";
      this.DateTo.Size = new System.Drawing.Size(103, 20);
      this.DateTo.TabIndex = 13;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "Column1";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      // 
      // DataGridView1
      // 
      this.DataGridView1.AllowUserToAddRows = false;
      this.DataGridView1.AllowUserToDeleteRows = false;
      this.DataGridView1.AllowUserToResizeRows = false;
      this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
      this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemIcon,
            this.ItemCategory,
            this.ItemSubcategory,
            this.ItemTitle,
            this.ItemAccount,
            this.ItemDate,
            this.ItemAmount,
            this.ItemCurrency});
      this.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DataGridView1.EnableHeadersVisualStyles = false;
      this.DataGridView1.Location = new System.Drawing.Point(0, 75);
      this.DataGridView1.MultiSelect = false;
      this.DataGridView1.Name = "DataGridView1";
      this.DataGridView1.ReadOnly = true;
      this.DataGridView1.RowHeadersWidth = 12;
      this.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.DataGridView1.Size = new System.Drawing.Size(659, 245);
      this.DataGridView1.TabIndex = 15;
      // 
      // ItemIcon
      // 
      this.ItemIcon.HeaderText = "";
      this.ItemIcon.MinimumWidth = 18;
      this.ItemIcon.Name = "ItemIcon";
      this.ItemIcon.ReadOnly = true;
      this.ItemIcon.Width = 18;
      // 
      // ItemCategory
      // 
      this.ItemCategory.HeaderText = "Статья";
      this.ItemCategory.Name = "ItemCategory";
      this.ItemCategory.ReadOnly = true;
      this.ItemCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.ItemCategory.Width = 90;
      // 
      // ItemSubcategory
      // 
      this.ItemSubcategory.HeaderText = "Категория";
      this.ItemSubcategory.Name = "ItemSubcategory";
      this.ItemSubcategory.ReadOnly = true;
      this.ItemSubcategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.ItemSubcategory.Width = 90;
      // 
      // ItemTitle
      // 
      this.ItemTitle.HeaderText = "Наименование";
      this.ItemTitle.MinimumWidth = 50;
      this.ItemTitle.Name = "ItemTitle";
      this.ItemTitle.ReadOnly = true;
      this.ItemTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.ItemTitle.Width = 120;
      // 
      // ItemAccount
      // 
      this.ItemAccount.HeaderText = "Счет";
      this.ItemAccount.MinimumWidth = 25;
      this.ItemAccount.Name = "ItemAccount";
      this.ItemAccount.ReadOnly = true;
      this.ItemAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // ItemDate
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.Format = "dd.MM.yyyy";
      this.ItemDate.DefaultCellStyle = dataGridViewCellStyle1;
      this.ItemDate.HeaderText = "Дата";
      this.ItemDate.MinimumWidth = 25;
      this.ItemDate.Name = "ItemDate";
      this.ItemDate.ReadOnly = true;
      this.ItemDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.ItemDate.Width = 75;
      // 
      // ItemAmount
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle2.Format = "##,###,##0.00";
      this.ItemAmount.DefaultCellStyle = dataGridViewCellStyle2;
      this.ItemAmount.HeaderText = "Сумма";
      this.ItemAmount.MinimumWidth = 15;
      this.ItemAmount.Name = "ItemAmount";
      this.ItemAmount.ReadOnly = true;
      this.ItemAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.ItemAmount.Width = 60;
      // 
      // ItemCurrency
      // 
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.ItemCurrency.DefaultCellStyle = dataGridViewCellStyle3;
      this.ItemCurrency.HeaderText = "Ед.";
      this.ItemCurrency.MinimumWidth = 15;
      this.ItemCurrency.Name = "ItemCurrency";
      this.ItemCurrency.ReadOnly = true;
      this.ItemCurrency.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.ItemCurrency.Width = 40;
      // 
      // ToolStrip3
      // 
      this.ToolStrip3.CanOverflow = false;
      this.ToolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.ToolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnReport,
            this.btnDelete,
            this.btnEdit});
      this.ToolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.ToolStrip3.Location = new System.Drawing.Point(0, 50);
      this.ToolStrip3.Name = "ToolStrip3";
      this.ToolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.ToolStrip3.Size = new System.Drawing.Size(659, 25);
      this.ToolStrip3.TabIndex = 14;
      this.ToolStrip3.Text = "mToolStrip1";
      // 
      // btnAdd
      // 
      this.btnAdd.Image = global::MoneyBook.WinApp.Properties.Resources.plus;
      this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(79, 22);
      this.btnAdd.Text = "Добавить";
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // btnReport
      // 
      this.btnReport.Image = global::MoneyBook.WinApp.Properties.Resources.chart_bar;
      this.btnReport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnReport.Name = "btnReport";
      this.btnReport.Size = new System.Drawing.Size(59, 22);
      this.btnReport.Text = "Отчёт";
      this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.btnDelete.Image = global::MoneyBook.WinApp.Properties.Resources.cross;
      this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(71, 22);
      this.btnDelete.Text = "Удалить";
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnEdit
      // 
      this.btnEdit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.btnEdit.Image = global::MoneyBook.WinApp.Properties.Resources.application_form_edit;
      this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(107, 22);
      this.btnEdit.Text = "Редактировать";
      this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
      // 
      // ToolStrip2
      // 
      this.ToolStrip2.CanOverflow = false;
      this.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.ToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripLabel4,
            this.toolStripSeparator1,
            this.toolStripLabel5,
            this.AmountFrom,
            this.toolStripLabel6,
            this.AmountTo,
            this.toolStripSeparator4,
            this.btnFilter});
      this.ToolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.ToolStrip2.Location = new System.Drawing.Point(0, 25);
      this.ToolStrip2.Name = "ToolStrip2";
      this.ToolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.ToolStrip2.Size = new System.Drawing.Size(659, 25);
      this.ToolStrip2.TabIndex = 11;
      this.ToolStrip2.Text = "mToolStrip1";
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(61, 22);
      this.toolStripLabel3.Text = "Период с:";
      // 
      // toolStripLabel4
      // 
      this.toolStripLabel4.Name = "toolStripLabel4";
      this.toolStripLabel4.Size = new System.Drawing.Size(24, 22);
      this.toolStripLabel4.Text = "по:";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel5
      // 
      this.toolStripLabel5.Name = "toolStripLabel5";
      this.toolStripLabel5.Size = new System.Drawing.Size(63, 22);
      this.toolStripLabel5.Text = "Сумма от:";
      // 
      // AmountFrom
      // 
      this.AmountFrom.Name = "AmountFrom";
      this.AmountFrom.Size = new System.Drawing.Size(60, 25);
      // 
      // toolStripLabel6
      // 
      this.toolStripLabel6.Name = "toolStripLabel6";
      this.toolStripLabel6.Size = new System.Drawing.Size(23, 22);
      this.toolStripLabel6.Text = "до:";
      // 
      // AmountTo
      // 
      this.AmountTo.Name = "AmountTo";
      this.AmountTo.Size = new System.Drawing.Size(60, 25);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // btnFilter
      // 
      this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
      this.btnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFilter.Name = "btnFilter";
      this.btnFilter.Size = new System.Drawing.Size(77, 22);
      this.btnFilter.Text = "Показать";
      this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
      // 
      // ToolStrip1
      // 
      this.ToolStrip1.CanOverflow = false;
      this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel1,
            this.Accounts,
            this.ToolStripSeparator2,
            this.ToolStripLabel2,
            this.MoneyItems,
            this.ToolStripSeparator3,
            this.ToolStripLabel13,
            this.Categories});
      this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
      this.ToolStrip1.Name = "ToolStrip1";
      this.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.ToolStrip1.Size = new System.Drawing.Size(659, 25);
      this.ToolStrip1.TabIndex = 10;
      this.ToolStrip1.Text = "ToolStrip1";
      // 
      // ToolStripLabel1
      // 
      this.ToolStripLabel1.Name = "ToolStripLabel1";
      this.ToolStripLabel1.Size = new System.Drawing.Size(36, 22);
      this.ToolStripLabel1.Text = "Счёт:";
      // 
      // Accounts
      // 
      this.Accounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.Accounts.Items.AddRange(new object[] {
            "<Все>"});
      this.Accounts.Name = "Accounts";
      this.Accounts.Size = new System.Drawing.Size(150, 25);
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // ToolStripLabel2
      // 
      this.ToolStripLabel2.Name = "ToolStripLabel2";
      this.ToolStripLabel2.Size = new System.Drawing.Size(46, 22);
      this.ToolStripLabel2.Text = "Статья:";
      // 
      // MoneyItems
      // 
      this.MoneyItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.MoneyItems.Items.AddRange(new object[] {
            "<Все>"});
      this.MoneyItems.Name = "MoneyItems";
      this.MoneyItems.Size = new System.Drawing.Size(150, 25);
      // 
      // ToolStripSeparator3
      // 
      this.ToolStripSeparator3.Name = "ToolStripSeparator3";
      this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // ToolStripLabel13
      // 
      this.ToolStripLabel13.Name = "ToolStripLabel13";
      this.ToolStripLabel13.Size = new System.Drawing.Size(66, 22);
      this.ToolStripLabel13.Text = "Категория:";
      // 
      // Categories
      // 
      this.Categories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.Categories.Items.AddRange(new object[] {
            "<Все>"});
      this.Categories.Name = "Categories";
      this.Categories.Size = new System.Drawing.Size(150, 25);
      // 
      // MoneyHistory
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.Controls.Add(this.DataGridView1);
      this.Controls.Add(this.ToolStrip3);
      this.Controls.Add(this.DateTo);
      this.Controls.Add(this.DateFrom);
      this.Controls.Add(this.ToolStrip2);
      this.Controls.Add(this.ToolStrip1);
      this.Controls.Add(this.StatusStrip1);
      this.Name = "MoneyHistory";
      this.Size = new System.Drawing.Size(659, 342);
      this.Load += new System.EventHandler(this.MoneyHistory_Load);
      this.StatusStrip1.ResumeLayout(false);
      this.StatusStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
      this.ToolStrip3.ResumeLayout(false);
      this.ToolStrip3.PerformLayout();
      this.ToolStrip2.ResumeLayout(false);
      this.ToolStrip2.PerformLayout();
      this.ToolStrip1.ResumeLayout(false);
      this.ToolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion


    internal System.Windows.Forms.StatusStrip StatusStrip1;
    internal System.Windows.Forms.ToolStripStatusLabel StatusTitle;
    internal System.Windows.Forms.ToolStripStatusLabel TotalItems;
    internal System.Windows.Forms.ToolStripStatusLabel TotalAmountTitle;
    internal System.Windows.Forms.ToolStripStatusLabel TotalAmount;
    internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
    internal System.Windows.Forms.ToolStripComboBox Accounts;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
    internal System.Windows.Forms.ToolStripComboBox MoneyItems;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
    internal System.Windows.Forms.ToolStripLabel ToolStripLabel13;
    internal System.Windows.Forms.ToolStripComboBox Categories;
    internal MToolStrip ToolStrip1;
    private MToolStrip ToolStrip2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripLabel toolStripLabel4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel5;
    internal System.Windows.Forms.ToolStripTextBox AmountFrom;
    private System.Windows.Forms.ToolStripLabel toolStripLabel6;
    internal System.Windows.Forms.ToolStripTextBox AmountTo;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton btnFilter;
    internal System.Windows.Forms.DateTimePicker DateFrom;
    internal System.Windows.Forms.DateTimePicker DateTo;
    private MToolStrip ToolStrip3;
    private System.Windows.Forms.ToolStripButton btnAdd;
    private System.Windows.Forms.ToolStripButton btnReport;
    private System.Windows.Forms.ToolStripButton btnDelete;
    private System.Windows.Forms.ToolStripButton btnEdit;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private MDataGridView DataGridView1;
    private System.Windows.Forms.DataGridViewImageColumn ItemIcon;
    private System.Windows.Forms.DataGridViewTextBoxColumn ItemCategory;
    private System.Windows.Forms.DataGridViewTextBoxColumn ItemSubcategory;
    private System.Windows.Forms.DataGridViewTextBoxColumn ItemTitle;
    private System.Windows.Forms.DataGridViewTextBoxColumn ItemAccount;
    private System.Windows.Forms.DataGridViewTextBoxColumn ItemDate;
    private System.Windows.Forms.DataGridViewTextBoxColumn ItemAmount;
    private System.Windows.Forms.DataGridViewTextBoxColumn ItemCurrency;
  }
}
