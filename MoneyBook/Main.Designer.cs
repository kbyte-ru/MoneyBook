﻿namespace MoneyBook.WinApp
{
  partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabExpenses = new System.Windows.Forms.TabPage();
            this.Expenses = new MoneyBook.WinApp.MoneyHistory();
            this.tabIncomes = new System.Windows.Forms.TabPage();
            this.Incomes = new MoneyBook.WinApp.MoneyHistory();
            this.tabAccounts = new System.Windows.Forms.TabPage();
            this.pnlAccounts = new System.Windows.Forms.Panel();
            this.Accounts = new MoneyBook.WinApp.MDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountsStatus = new System.Windows.Forms.StatusStrip();
            this.AccountsTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.AccountsTotalAmount = new System.Windows.Forms.ToolStripStatusLabel();
            this.mToolStrip1 = new MoneyBook.WinApp.MToolStrip();
            this.btnAccountNew = new System.Windows.Forms.ToolStripButton();
            this.btnAccountDelete = new System.Windows.Forms.ToolStripButton();
            this.btnAccountEdit = new System.Windows.Forms.ToolStripButton();
            this.tabDictionaries = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.toolAccoutType = new System.Windows.Forms.ToolStrip();
            this.btnAccountTypeAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.dgvInfo = new MoneyBook.WinApp.MDataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1.SuspendLayout();
            this.tabExpenses.SuspendLayout();
            this.tabIncomes.SuspendLayout();
            this.tabAccounts.SuspendLayout();
            this.pnlAccounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Accounts)).BeginInit();
            this.AccountsStatus.SuspendLayout();
            this.mToolStrip1.SuspendLayout();
            this.tabDictionaries.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.toolAccoutType.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabExpenses);
            this.tabControl1.Controls.Add(this.tabIncomes);
            this.tabControl1.Controls.Add(this.tabAccounts);
            this.tabControl1.Controls.Add(this.tabDictionaries);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Controls.Add(this.tabAbout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.iconList;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 462);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabExpenses
            // 
            this.tabExpenses.Controls.Add(this.Expenses);
            this.tabExpenses.ImageIndex = 0;
            this.tabExpenses.Location = new System.Drawing.Point(4, 23);
            this.tabExpenses.Name = "tabExpenses";
            this.tabExpenses.Padding = new System.Windows.Forms.Padding(3);
            this.tabExpenses.Size = new System.Drawing.Size(676, 435);
            this.tabExpenses.TabIndex = 0;
            this.tabExpenses.Text = "Расходы";
            this.tabExpenses.UseVisualStyleBackColor = true;
            // 
            // Expenses
            // 
            this.Expenses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Expenses.DetailsSize = 175;
            this.Expenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Expenses.ItemsType = MoneyBook.Core.EntryType.Expense;
            this.Expenses.Location = new System.Drawing.Point(3, 3);
            this.Expenses.Name = "Expenses";
            this.Expenses.ShowDetails = false;
            this.Expenses.Size = new System.Drawing.Size(670, 429);
            this.Expenses.TabIndex = 0;
            this.Expenses.User = null;
            this.Expenses.DetailsVisibleChanged += new System.EventHandler(this.MoneyHistory_DetailsVisibleChanged);
            this.Expenses.DetailsSizeChanged += new System.EventHandler(this.MoneyHistory_DetailsSizeChanged);
            // 
            // tabIncomes
            // 
            this.tabIncomes.Controls.Add(this.Incomes);
            this.tabIncomes.ImageIndex = 1;
            this.tabIncomes.Location = new System.Drawing.Point(4, 23);
            this.tabIncomes.Name = "tabIncomes";
            this.tabIncomes.Padding = new System.Windows.Forms.Padding(3);
            this.tabIncomes.Size = new System.Drawing.Size(676, 435);
            this.tabIncomes.TabIndex = 1;
            this.tabIncomes.Text = "Доходы";
            this.tabIncomes.UseVisualStyleBackColor = true;
            // 
            // Incomes
            // 
            this.Incomes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Incomes.DetailsSize = 175;
            this.Incomes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Incomes.ItemsType = MoneyBook.Core.EntryType.Income;
            this.Incomes.Location = new System.Drawing.Point(3, 3);
            this.Incomes.Name = "Incomes";
            this.Incomes.ShowDetails = false;
            this.Incomes.Size = new System.Drawing.Size(670, 429);
            this.Incomes.TabIndex = 0;
            this.Incomes.User = null;
            this.Incomes.DetailsVisibleChanged += new System.EventHandler(this.MoneyHistory_DetailsVisibleChanged);
            this.Incomes.DetailsSizeChanged += new System.EventHandler(this.MoneyHistory_DetailsSizeChanged);
            // 
            // tabAccounts
            // 
            this.tabAccounts.Controls.Add(this.pnlAccounts);
            this.tabAccounts.ImageIndex = 2;
            this.tabAccounts.Location = new System.Drawing.Point(4, 23);
            this.tabAccounts.Name = "tabAccounts";
            this.tabAccounts.Size = new System.Drawing.Size(676, 435);
            this.tabAccounts.TabIndex = 2;
            this.tabAccounts.Text = "Счета";
            this.tabAccounts.UseVisualStyleBackColor = true;
            // 
            // pnlAccounts
            // 
            this.pnlAccounts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAccounts.Controls.Add(this.Accounts);
            this.pnlAccounts.Controls.Add(this.AccountsStatus);
            this.pnlAccounts.Controls.Add(this.mToolStrip1);
            this.pnlAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAccounts.Location = new System.Drawing.Point(0, 0);
            this.pnlAccounts.Name = "pnlAccounts";
            this.pnlAccounts.Size = new System.Drawing.Size(676, 435);
            this.pnlAccounts.TabIndex = 14;
            // 
            // Accounts
            // 
            this.Accounts.AllowUserToAddRows = false;
            this.Accounts.AllowUserToDeleteRows = false;
            this.Accounts.AllowUserToResizeRows = false;
            this.Accounts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Accounts.BackgroundColor = System.Drawing.SystemColors.Window;
            this.Accounts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Accounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Accounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.Accounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Accounts.EnableHeadersVisualStyles = false;
            this.Accounts.Location = new System.Drawing.Point(0, 25);
            this.Accounts.MultiSelect = false;
            this.Accounts.Name = "Accounts";
            this.Accounts.ReadOnly = true;
            this.Accounts.RowHeadersWidth = 12;
            this.Accounts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Accounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Accounts.Size = new System.Drawing.Size(672, 378);
            this.Accounts.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.MinimumWidth = 18;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 18;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Наименование";
            this.Column2.MinimumWidth = 20;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Тип";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Валюта";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Остаток";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Расходов";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Доходов";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // AccountsStatus
            // 
            this.AccountsStatus.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.AccountsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AccountsTitle,
            this.AccountsTotalAmount});
            this.AccountsStatus.Location = new System.Drawing.Point(0, 403);
            this.AccountsStatus.Name = "AccountsStatus";
            this.AccountsStatus.Size = new System.Drawing.Size(672, 28);
            this.AccountsStatus.SizingGrip = false;
            this.AccountsStatus.TabIndex = 13;
            this.AccountsStatus.Text = "StatusStrip1";
            // 
            // AccountsTitle
            // 
            this.AccountsTitle.Name = "AccountsTitle";
            this.AccountsTitle.Padding = new System.Windows.Forms.Padding(4);
            this.AccountsTitle.Size = new System.Drawing.Size(222, 23);
            this.AccountsTitle.Text = "Общий остаток средств на 14.06.2015:";
            // 
            // AccountsTotalAmount
            // 
            this.AccountsTotalAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.AccountsTotalAmount.Name = "AccountsTotalAmount";
            this.AccountsTotalAmount.Size = new System.Drawing.Size(12, 23);
            this.AccountsTotalAmount.Text = "-";
            // 
            // mToolStrip1
            // 
            this.mToolStrip1.CanOverflow = false;
            this.mToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAccountNew,
            this.btnAccountDelete,
            this.btnAccountEdit});
            this.mToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.mToolStrip1.Name = "mToolStrip1";
            this.mToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mToolStrip1.ShowItemToolTips = false;
            this.mToolStrip1.Size = new System.Drawing.Size(672, 25);
            this.mToolStrip1.TabIndex = 1;
            this.mToolStrip1.Text = "mToolStrip1";
            // 
            // btnAccountNew
            // 
            this.btnAccountNew.Image = global::MoneyBook.WinApp.Properties.Resources.plus;
            this.btnAccountNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountNew.Name = "btnAccountNew";
            this.btnAccountNew.Size = new System.Drawing.Size(92, 22);
            this.btnAccountNew.Text = "Новый счёт";
            // 
            // btnAccountDelete
            // 
            this.btnAccountDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAccountDelete.Image = global::MoneyBook.WinApp.Properties.Resources.cross;
            this.btnAccountDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountDelete.Name = "btnAccountDelete";
            this.btnAccountDelete.Size = new System.Drawing.Size(71, 22);
            this.btnAccountDelete.Text = "Удалить";
            // 
            // btnAccountEdit
            // 
            this.btnAccountEdit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAccountEdit.Image = global::MoneyBook.WinApp.Properties.Resources.application_form_edit;
            this.btnAccountEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountEdit.Name = "btnAccountEdit";
            this.btnAccountEdit.Size = new System.Drawing.Size(107, 22);
            this.btnAccountEdit.Text = "Редактировать";
            // 
            // tabDictionaries
            // 
            this.tabDictionaries.Controls.Add(this.tableLayoutPanel1);
            this.tabDictionaries.ImageIndex = 3;
            this.tabDictionaries.Location = new System.Drawing.Point(4, 23);
            this.tabDictionaries.Name = "tabDictionaries";
            this.tabDictionaries.Size = new System.Drawing.Size(676, 435);
            this.tabDictionaries.TabIndex = 3;
            this.tabDictionaries.Text = "Справочники";
            this.tabDictionaries.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox6, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(676, 435);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.toolStrip5);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(332, 139);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Статьи расходов";
            // 
            // toolStrip5
            // 
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton16,
            this.toolStripButton17,
            this.toolStripButton18});
            this.toolStrip5.Location = new System.Drawing.Point(3, 16);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip5.Size = new System.Drawing.Size(326, 25);
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.Image = global::MoneyBook.WinApp.Properties.Resources.plus;
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton16.Text = "Добавить";
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.Image = global::MoneyBook.WinApp.Properties.Resources.application_form_edit;
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton17.Text = "Редактировать";
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.Image = global::MoneyBook.WinApp.Properties.Resources.cross;
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton18.Text = "Удалить";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(341, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 139);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Статьи доходов";
            // 
            // toolStrip6
            // 
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripButton15});
            this.toolStrip6.Location = new System.Drawing.Point(3, 16);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip6.Size = new System.Drawing.Size(326, 25);
            this.toolStrip6.TabIndex = 0;
            this.toolStrip6.Text = "toolStrip6";
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.Image = global::MoneyBook.WinApp.Properties.Resources.plus;
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton13.Text = "Добавить";
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.Image = global::MoneyBook.WinApp.Properties.Resources.application_form_edit;
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton14.Text = "Редактировать";
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.Image = global::MoneyBook.WinApp.Properties.Resources.cross;
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton15.Text = "Удалить";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.toolStrip3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 139);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Категории расходов";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton9});
            this.toolStrip3.Location = new System.Drawing.Point(3, 16);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(326, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = global::MoneyBook.WinApp.Properties.Resources.plus;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton7.Text = "Добавить";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Image = global::MoneyBook.WinApp.Properties.Resources.application_form_edit;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton8.Text = "Редактировать";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.Image = global::MoneyBook.WinApp.Properties.Resources.cross;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton9.Text = "Удалить";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.toolStrip4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(341, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 139);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Категории доходов";
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton10,
            this.toolStripButton11,
            this.toolStripButton12});
            this.toolStrip4.Location = new System.Drawing.Point(3, 16);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip4.Size = new System.Drawing.Size(326, 25);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Image = global::MoneyBook.WinApp.Properties.Resources.plus;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton10.Text = "Добавить";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.Image = global::MoneyBook.WinApp.Properties.Resources.application_form_edit;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton11.Text = "Редактировать";
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.Image = global::MoneyBook.WinApp.Properties.Resources.cross;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton12.Text = "Удалить";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.toolAccoutType);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 293);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(332, 139);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Типы счетов";
            // 
            // toolAccoutType
            // 
            this.toolAccoutType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAccountTypeAdd,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolAccoutType.Location = new System.Drawing.Point(3, 16);
            this.toolAccoutType.Name = "toolAccoutType";
            this.toolAccoutType.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolAccoutType.Size = new System.Drawing.Size(326, 25);
            this.toolAccoutType.TabIndex = 0;
            this.toolAccoutType.Text = "toolStrip1";
            // 
            // btnAccountTypeAdd
            // 
            this.btnAccountTypeAdd.Image = global::MoneyBook.WinApp.Properties.Resources.plus;
            this.btnAccountTypeAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountTypeAdd.Name = "btnAccountTypeAdd";
            this.btnAccountTypeAdd.Size = new System.Drawing.Size(79, 22);
            this.btnAccountTypeAdd.Text = "Добавить";
            this.btnAccountTypeAdd.Click += new System.EventHandler(this.btnAccountTypeAdd_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::MoneyBook.WinApp.Properties.Resources.application_form_edit;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton2.Text = "Редактировать";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::MoneyBook.WinApp.Properties.Resources.cross;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton3.Text = "Удалить";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.toolStrip2);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(341, 293);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(332, 139);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Валюта";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(326, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::MoneyBook.WinApp.Properties.Resources.plus;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton4.Text = "Добавить";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = global::MoneyBook.WinApp.Properties.Resources.application_form_edit;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton5.Text = "Редактировать";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = global::MoneyBook.WinApp.Properties.Resources.cross;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton6.Text = "Удалить";
            // 
            // tabSettings
            // 
            this.tabSettings.ImageIndex = 4;
            this.tabSettings.Location = new System.Drawing.Point(4, 23);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(676, 435);
            this.tabSettings.TabIndex = 4;
            this.tabSettings.Text = "Параметры";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.dgvInfo);
            this.tabInfo.ImageIndex = 5;
            this.tabInfo.Location = new System.Drawing.Point(4, 23);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Size = new System.Drawing.Size(676, 435);
            this.tabInfo.TabIndex = 6;
            this.tabInfo.Text = "Info";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // dgvInfo
            // 
            this.dgvInfo.AllowUserToAddRows = false;
            this.dgvInfo.AllowUserToDeleteRows = false;
            this.dgvInfo.AllowUserToResizeRows = false;
            this.dgvInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvInfo.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column5,
            this.Column4});
            this.dgvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInfo.EnableHeadersVisualStyles = false;
            this.dgvInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvInfo.MultiSelect = false;
            this.dgvInfo.Name = "dgvInfo";
            this.dgvInfo.ReadOnly = true;
            this.dgvInfo.RowHeadersWidth = 12;
            this.dgvInfo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInfo.Size = new System.Drawing.Size(676, 435);
            this.dgvInfo.TabIndex = 0;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Id";
            this.Column3.HeaderText = "Id";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 40;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Name";
            this.Column5.HeaderText = "Name";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 175;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Value";
            this.Column4.HeaderText = "Value";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 275;
            // 
            // tabAbout
            // 
            this.tabAbout.ImageIndex = 6;
            this.tabAbout.Location = new System.Drawing.Point(4, 23);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(676, 435);
            this.tabAbout.TabIndex = 5;
            this.tabAbout.Text = "О программе...";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "coins_delete.png");
            this.iconList.Images.SetKeyName(1, "coins_add.png");
            this.iconList.Images.SetKeyName(2, "entity.png");
            this.iconList.Images.SetKeyName(3, "category.png");
            this.iconList.Images.SetKeyName(4, "gear_in.png");
            this.iconList.Images.SetKeyName(5, "application_terminal.png");
            this.iconList.Images.SetKeyName(6, "information_frame.png");
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabExpenses.ResumeLayout(false);
            this.tabIncomes.ResumeLayout(false);
            this.tabAccounts.ResumeLayout(false);
            this.pnlAccounts.ResumeLayout(false);
            this.pnlAccounts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Accounts)).EndInit();
            this.AccountsStatus.ResumeLayout(false);
            this.AccountsStatus.PerformLayout();
            this.mToolStrip1.ResumeLayout(false);
            this.mToolStrip1.PerformLayout();
            this.tabDictionaries.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.toolAccoutType.ResumeLayout(false);
            this.toolAccoutType.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabExpenses;
    private System.Windows.Forms.TabPage tabIncomes;
    private System.Windows.Forms.TabPage tabAccounts;
    private System.Windows.Forms.TabPage tabDictionaries;
    private System.Windows.Forms.TabPage tabSettings;
    private System.Windows.Forms.TabPage tabInfo;
    private System.Windows.Forms.TabPage tabAbout;
    private MoneyHistory Expenses;
    private MoneyHistory Incomes;
    private MDataGridView Accounts;
    private MDataGridView dgvInfo;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.ImageList iconList;
    private MToolStrip mToolStrip1;
    private System.Windows.Forms.ToolStripButton btnAccountNew;
    private System.Windows.Forms.ToolStripButton btnAccountDelete;
    private System.Windows.Forms.ToolStripButton btnAccountEdit;
    internal System.Windows.Forms.StatusStrip AccountsStatus;
    internal System.Windows.Forms.ToolStripStatusLabel AccountsTitle;
    internal System.Windows.Forms.ToolStripStatusLabel AccountsTotalAmount;
    private System.Windows.Forms.Panel pnlAccounts;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.ToolStrip toolAccoutType;
    private System.Windows.Forms.ToolStripButton btnAccountTypeAdd;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripButton toolStripButton5;
    private System.Windows.Forms.ToolStripButton toolStripButton6;
    private System.Windows.Forms.ToolStrip toolStrip5;
    private System.Windows.Forms.ToolStrip toolStrip6;
    private System.Windows.Forms.ToolStrip toolStrip3;
    private System.Windows.Forms.ToolStrip toolStrip4;
    private System.Windows.Forms.ToolStripButton toolStripButton16;
    private System.Windows.Forms.ToolStripButton toolStripButton17;
    private System.Windows.Forms.ToolStripButton toolStripButton18;
    private System.Windows.Forms.ToolStripButton toolStripButton13;
    private System.Windows.Forms.ToolStripButton toolStripButton14;
    private System.Windows.Forms.ToolStripButton toolStripButton15;
    private System.Windows.Forms.ToolStripButton toolStripButton7;
    private System.Windows.Forms.ToolStripButton toolStripButton8;
    private System.Windows.Forms.ToolStripButton toolStripButton9;
    private System.Windows.Forms.ToolStripButton toolStripButton10;
    private System.Windows.Forms.ToolStripButton toolStripButton11;
    private System.Windows.Forms.ToolStripButton toolStripButton12;

  }
}