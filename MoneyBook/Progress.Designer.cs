namespace MoneyBook.WinApp
{
  partial class Progress
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
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.lblAction = new System.Windows.Forms.Label();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.lblDetails = new System.Windows.Forms.Label();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // progressBar1
      // 
      this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.progressBar1.Location = new System.Drawing.Point(21, 59);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(331, 24);
      this.progressBar1.TabIndex = 2;
      // 
      // lblAction
      // 
      this.lblAction.AutoSize = true;
      this.lblAction.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblAction.Location = new System.Drawing.Point(21, 43);
      this.lblAction.Name = "lblAction";
      this.lblAction.Size = new System.Drawing.Size(331, 13);
      this.lblAction.TabIndex = 1;
      this.lblAction.Text = "Выполняю какую-то операцию";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
      this.tableLayoutPanel1.Controls.Add(this.progressBar1, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.lblAction, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.lblDetails, 1, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(375, 143);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // lblDetails
      // 
      this.lblDetails.AutoSize = true;
      this.lblDetails.Location = new System.Drawing.Point(21, 86);
      this.lblDetails.Name = "lblDetails";
      this.lblDetails.Size = new System.Drawing.Size(329, 26);
      this.lblDetails.TabIndex = 3;
      this.lblDetails.Text = "Конкретно сейчас делаю конкретно вот такую вот непонятную, но очень конкретную за" +
    "дачу";
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
      this.flowLayoutPanel1.Controls.Add(this.btnCancel);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 143);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
      this.flowLayoutPanel1.Size = new System.Drawing.Size(375, 39);
      this.flowLayoutPanel1.TabIndex = 1;
      this.flowLayoutPanel1.Visible = false;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(297, 7);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // Progress
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(375, 182);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.flowLayoutPanel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Progress";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Процесс идет...";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.flowLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label lblAction;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label lblDetails;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button btnCancel;
  }
}