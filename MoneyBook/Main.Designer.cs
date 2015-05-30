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
      this.tabIncomes = new System.Windows.Forms.TabPage();
      this.tabAccounts = new System.Windows.Forms.TabPage();
      this.tabDictionaries = new System.Windows.Forms.TabPage();
      this.tabSettings = new System.Windows.Forms.TabPage();
      this.tabInfo = new System.Windows.Forms.TabPage();
      this.tabAbout = new System.Windows.Forms.TabPage();
      this.Expenses = new MoneyBook.WinApp.MoneyHistory();
      this.Incomes = new MoneyBook.WinApp.MoneyHistory();
      this.tabControl1.SuspendLayout();
      this.tabExpenses.SuspendLayout();
      this.tabIncomes.SuspendLayout();
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
      this.tabControl1.Size = new System.Drawing.Size(639, 380);
      this.tabControl1.TabIndex = 0;
      // 
      // tabExpenses
      // 
      this.tabExpenses.Controls.Add(this.Expenses);
      this.tabExpenses.Location = new System.Drawing.Point(4, 22);
      this.tabExpenses.Name = "tabExpenses";
      this.tabExpenses.Padding = new System.Windows.Forms.Padding(3);
      this.tabExpenses.Size = new System.Drawing.Size(631, 354);
      this.tabExpenses.TabIndex = 0;
      this.tabExpenses.Text = "Расходы";
      this.tabExpenses.UseVisualStyleBackColor = true;
      // 
      // tabIncomes
      // 
      this.tabIncomes.Controls.Add(this.Incomes);
      this.tabIncomes.Location = new System.Drawing.Point(4, 22);
      this.tabIncomes.Name = "tabIncomes";
      this.tabIncomes.Padding = new System.Windows.Forms.Padding(3);
      this.tabIncomes.Size = new System.Drawing.Size(631, 354);
      this.tabIncomes.TabIndex = 1;
      this.tabIncomes.Text = "Доходы";
      this.tabIncomes.UseVisualStyleBackColor = true;
      // 
      // tabAccounts
      // 
      this.tabAccounts.Location = new System.Drawing.Point(4, 22);
      this.tabAccounts.Name = "tabAccounts";
      this.tabAccounts.Size = new System.Drawing.Size(631, 354);
      this.tabAccounts.TabIndex = 2;
      this.tabAccounts.Text = "Счета";
      this.tabAccounts.UseVisualStyleBackColor = true;
      // 
      // tabDictionaries
      // 
      this.tabDictionaries.Location = new System.Drawing.Point(4, 22);
      this.tabDictionaries.Name = "tabDictionaries";
      this.tabDictionaries.Size = new System.Drawing.Size(631, 354);
      this.tabDictionaries.TabIndex = 3;
      this.tabDictionaries.Text = "Справочники";
      this.tabDictionaries.UseVisualStyleBackColor = true;
      // 
      // tabSettings
      // 
      this.tabSettings.Location = new System.Drawing.Point(4, 22);
      this.tabSettings.Name = "tabSettings";
      this.tabSettings.Size = new System.Drawing.Size(631, 354);
      this.tabSettings.TabIndex = 4;
      this.tabSettings.Text = "Параметры";
      this.tabSettings.UseVisualStyleBackColor = true;
      // 
      // tabInfo
      // 
      this.tabInfo.Location = new System.Drawing.Point(4, 22);
      this.tabInfo.Name = "tabInfo";
      this.tabInfo.Size = new System.Drawing.Size(631, 354);
      this.tabInfo.TabIndex = 6;
      this.tabInfo.Text = "Info";
      this.tabInfo.UseVisualStyleBackColor = true;
      // 
      // tabAbout
      // 
      this.tabAbout.Location = new System.Drawing.Point(4, 22);
      this.tabAbout.Name = "tabAbout";
      this.tabAbout.Size = new System.Drawing.Size(631, 354);
      this.tabAbout.TabIndex = 5;
      this.tabAbout.Text = "О программе...";
      this.tabAbout.UseVisualStyleBackColor = true;
      // 
      // Expenses
      // 
      this.Expenses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.Expenses.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Expenses.Location = new System.Drawing.Point(3, 3);
      this.Expenses.Name = "Expenses";
      this.Expenses.Size = new System.Drawing.Size(625, 348);
      this.Expenses.TabIndex = 0;
      // 
      // Incomes
      // 
      this.Incomes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.Incomes.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Incomes.Location = new System.Drawing.Point(3, 3);
      this.Incomes.Name = "Incomes";
      this.Incomes.Size = new System.Drawing.Size(625, 348);
      this.Incomes.TabIndex = 0;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(639, 380);
      this.Controls.Add(this.tabControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "Main";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Main";
      this.Load += new System.EventHandler(this.Main_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabExpenses.ResumeLayout(false);
      this.tabIncomes.ResumeLayout(false);
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

  }
}