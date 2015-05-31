namespace MoneyBook.WinApp
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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabExpenses = new System.Windows.Forms.TabPage();
      this.Expenses = new MoneyBook.WinApp.MoneyHistory();
      this.tabIncomes = new System.Windows.Forms.TabPage();
      this.Incomes = new MoneyBook.WinApp.MoneyHistory();
      this.tabAccounts = new System.Windows.Forms.TabPage();
      this.mDataGridView1 = new MoneyBook.WinApp.MDataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tabDictionaries = new System.Windows.Forms.TabPage();
      this.label1 = new System.Windows.Forms.Label();
      this.tabSettings = new System.Windows.Forms.TabPage();
      this.tabInfo = new System.Windows.Forms.TabPage();
      this.dgvInfo = new MoneyBook.WinApp.MDataGridView();
      this.tabAbout = new System.Windows.Forms.TabPage();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tabControl1.SuspendLayout();
      this.tabExpenses.SuspendLayout();
      this.tabIncomes.SuspendLayout();
      this.tabAccounts.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.mDataGridView1)).BeginInit();
      this.tabDictionaries.SuspendLayout();
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
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(670, 403);
      this.tabControl1.TabIndex = 0;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      // 
      // tabExpenses
      // 
      this.tabExpenses.Controls.Add(this.Expenses);
      this.tabExpenses.Location = new System.Drawing.Point(4, 22);
      this.tabExpenses.Name = "tabExpenses";
      this.tabExpenses.Padding = new System.Windows.Forms.Padding(3);
      this.tabExpenses.Size = new System.Drawing.Size(662, 377);
      this.tabExpenses.TabIndex = 0;
      this.tabExpenses.Text = "Расходы";
      this.tabExpenses.UseVisualStyleBackColor = true;
      // 
      // Expenses
      // 
      this.Expenses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.Expenses.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Expenses.ItemsType = MoneyBook.Core.EntryType.Expense;
      this.Expenses.Location = new System.Drawing.Point(3, 3);
      this.Expenses.Name = "Expenses";
      this.Expenses.Size = new System.Drawing.Size(656, 371);
      this.Expenses.TabIndex = 0;
      this.Expenses.User = null;
      // 
      // tabIncomes
      // 
      this.tabIncomes.Controls.Add(this.Incomes);
      this.tabIncomes.Location = new System.Drawing.Point(4, 22);
      this.tabIncomes.Name = "tabIncomes";
      this.tabIncomes.Padding = new System.Windows.Forms.Padding(3);
      this.tabIncomes.Size = new System.Drawing.Size(662, 377);
      this.tabIncomes.TabIndex = 1;
      this.tabIncomes.Text = "Доходы";
      this.tabIncomes.UseVisualStyleBackColor = true;
      // 
      // Incomes
      // 
      this.Incomes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.Incomes.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Incomes.ItemsType = MoneyBook.Core.EntryType.Income;
      this.Incomes.Location = new System.Drawing.Point(3, 3);
      this.Incomes.Name = "Incomes";
      this.Incomes.Size = new System.Drawing.Size(656, 371);
      this.Incomes.TabIndex = 0;
      this.Incomes.User = null;
      // 
      // tabAccounts
      // 
      this.tabAccounts.Controls.Add(this.mDataGridView1);
      this.tabAccounts.Location = new System.Drawing.Point(4, 22);
      this.tabAccounts.Name = "tabAccounts";
      this.tabAccounts.Size = new System.Drawing.Size(662, 377);
      this.tabAccounts.TabIndex = 2;
      this.tabAccounts.Text = "Счета";
      this.tabAccounts.UseVisualStyleBackColor = true;
      // 
      // mDataGridView1
      // 
      this.mDataGridView1.AllowUserToAddRows = false;
      this.mDataGridView1.AllowUserToDeleteRows = false;
      this.mDataGridView1.AllowUserToResizeRows = false;
      this.mDataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.mDataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
      this.mDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.mDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.mDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
      this.mDataGridView1.EnableHeadersVisualStyles = false;
      this.mDataGridView1.Location = new System.Drawing.Point(8, 20);
      this.mDataGridView1.MultiSelect = false;
      this.mDataGridView1.Name = "mDataGridView1";
      this.mDataGridView1.ReadOnly = true;
      this.mDataGridView1.RowHeadersWidth = 12;
      this.mDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.mDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.mDataGridView1.Size = new System.Drawing.Size(240, 150);
      this.mDataGridView1.TabIndex = 0;
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
      // tabDictionaries
      // 
      this.tabDictionaries.Controls.Add(this.label1);
      this.tabDictionaries.Location = new System.Drawing.Point(4, 22);
      this.tabDictionaries.Name = "tabDictionaries";
      this.tabDictionaries.Size = new System.Drawing.Size(662, 377);
      this.tabDictionaries.TabIndex = 3;
      this.tabDictionaries.Text = "Справочники";
      this.tabDictionaries.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 96F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(32, 93);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(408, 147);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      // 
      // tabSettings
      // 
      this.tabSettings.Location = new System.Drawing.Point(4, 22);
      this.tabSettings.Name = "tabSettings";
      this.tabSettings.Size = new System.Drawing.Size(662, 377);
      this.tabSettings.TabIndex = 4;
      this.tabSettings.Text = "Параметры";
      this.tabSettings.UseVisualStyleBackColor = true;
      // 
      // tabInfo
      // 
      this.tabInfo.Controls.Add(this.dgvInfo);
      this.tabInfo.Location = new System.Drawing.Point(4, 22);
      this.tabInfo.Name = "tabInfo";
      this.tabInfo.Size = new System.Drawing.Size(662, 377);
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
      this.dgvInfo.EnableHeadersVisualStyles = false;
      this.dgvInfo.Location = new System.Drawing.Point(8, 15);
      this.dgvInfo.MultiSelect = false;
      this.dgvInfo.Name = "dgvInfo";
      this.dgvInfo.ReadOnly = true;
      this.dgvInfo.RowHeadersWidth = 12;
      this.dgvInfo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvInfo.Size = new System.Drawing.Size(345, 296);
      this.dgvInfo.TabIndex = 0;
      // 
      // tabAbout
      // 
      this.tabAbout.Location = new System.Drawing.Point(4, 22);
      this.tabAbout.Name = "tabAbout";
      this.tabAbout.Size = new System.Drawing.Size(662, 377);
      this.tabAbout.TabIndex = 5;
      this.tabAbout.Text = "О программе...";
      this.tabAbout.UseVisualStyleBackColor = true;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "Id";
      this.Column3.HeaderText = "Id";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      // 
      // Column5
      // 
      this.Column5.DataPropertyName = "Name";
      this.Column5.HeaderText = "Name";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      // 
      // Column4
      // 
      this.Column4.DataPropertyName = "Value";
      this.Column4.HeaderText = "Value";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(670, 403);
      this.Controls.Add(this.tabControl1);
      this.DoubleBuffered = true;
      this.Name = "Main";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Main";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
      this.Load += new System.EventHandler(this.Main_Load);
      this.Shown += new System.EventHandler(this.Main_Shown);
      this.tabControl1.ResumeLayout(false);
      this.tabExpenses.ResumeLayout(false);
      this.tabIncomes.ResumeLayout(false);
      this.tabAccounts.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.mDataGridView1)).EndInit();
      this.tabDictionaries.ResumeLayout(false);
      this.tabDictionaries.PerformLayout();
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
    private MDataGridView mDataGridView1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.Label label1;
    private MDataGridView dgvInfo;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;

  }
}