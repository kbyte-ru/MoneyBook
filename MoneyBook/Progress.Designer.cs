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
      this.ProgressBar1 = new MoneyBook.WinApp.MProgressBar();
      this.SuspendLayout();
      // 
      // ProgressBar1
      // 
      this.ProgressBar1.ActionName = "";
      this.ProgressBar1.AllowCancel = false;
      this.ProgressBar1.CancelCallback = null;
      this.ProgressBar1.DetailedInfo = "";
      this.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ProgressBar1.Location = new System.Drawing.Point(0, 0);
      this.ProgressBar1.Name = "ProgressBar1";
      this.ProgressBar1.ProgressMaximum = 100;
      this.ProgressBar1.ProgressMinimum = 0;
      this.ProgressBar1.ProgressValue = 0;
      this.ProgressBar1.Size = new System.Drawing.Size(375, 182);
      this.ProgressBar1.TabIndex = 0;
      // 
      // Progress
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(375, 182);
      this.ControlBox = false;
      this.Controls.Add(this.ProgressBar1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Progress";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Процесс идет...";
      this.ResumeLayout(false);

    }

    #endregion

    private MProgressBar ProgressBar1;

  }
}