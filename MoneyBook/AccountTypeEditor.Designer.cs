namespace MoneyBook.WinApp
{
    partial class AccountTypeEditor
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.AcceptButton = new System.Windows.Forms.Button();
      this.CancelButton = new System.Windows.Forms.Button();
      this.AccountTypeName = new System.Windows.Forms.TextBox();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.btnSelectIcon = new System.Windows.Forms.Button();
      this.picIcon = new System.Windows.Forms.PictureBox();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.08108F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.91892F));
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.AccountTypeName, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(444, 92);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Left;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(58, 35);
      this.label1.TabIndex = 10;
      this.label1.Text = "Картинка:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Left;
      this.label2.Location = new System.Drawing.Point(3, 35);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(86, 26);
      this.label2.TabIndex = 11;
      this.label2.Text = "Наименование:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Controls.Add(this.AcceptButton, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.CancelButton, 1, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(140, 64);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(301, 33);
      this.tableLayoutPanel2.TabIndex = 12;
      // 
      // AcceptButton
      // 
      this.AcceptButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.AcceptButton.Enabled = false;
      this.AcceptButton.Location = new System.Drawing.Point(72, 3);
      this.AcceptButton.Name = "AcceptButton";
      this.AcceptButton.Size = new System.Drawing.Size(75, 27);
      this.AcceptButton.TabIndex = 3;
      this.AcceptButton.Text = "Сохранить";
      this.AcceptButton.UseVisualStyleBackColor = true;
      this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
      // 
      // CancelButton
      // 
      this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelButton.Dock = System.Windows.Forms.DockStyle.Left;
      this.CancelButton.Location = new System.Drawing.Point(153, 3);
      this.CancelButton.Name = "CancelButton";
      this.CancelButton.Size = new System.Drawing.Size(75, 27);
      this.CancelButton.TabIndex = 4;
      this.CancelButton.Text = "Отмена";
      this.CancelButton.UseVisualStyleBackColor = true;
      // 
      // AccountTypeName
      // 
      this.AccountTypeName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.AccountTypeName.Location = new System.Drawing.Point(140, 38);
      this.AccountTypeName.MaxLength = 50;
      this.AccountTypeName.Name = "AccountTypeName";
      this.AccountTypeName.Size = new System.Drawing.Size(301, 20);
      this.AccountTypeName.TabIndex = 2;
      this.AccountTypeName.TextChanged += new System.EventHandler(this.AccountTypeName_TextChanged);
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.picIcon);
      this.flowLayoutPanel1.Controls.Add(this.btnSelectIcon);
      this.flowLayoutPanel1.Location = new System.Drawing.Point(140, 3);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 29);
      this.flowLayoutPanel1.TabIndex = 13;
      // 
      // btnSelectIcon
      // 
      this.btnSelectIcon.Location = new System.Drawing.Point(33, 3);
      this.btnSelectIcon.Name = "btnSelectIcon";
      this.btnSelectIcon.Size = new System.Drawing.Size(75, 23);
      this.btnSelectIcon.TabIndex = 14;
      this.btnSelectIcon.Text = "Выбрать";
      this.btnSelectIcon.UseVisualStyleBackColor = true;
      this.btnSelectIcon.Click += new System.EventHandler(this.btnSelectIcon_Click);
      // 
      // picIcon
      // 
      this.picIcon.Location = new System.Drawing.Point(3, 3);
      this.picIcon.Name = "picIcon";
      this.picIcon.Size = new System.Drawing.Size(24, 24);
      this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.picIcon.TabIndex = 15;
      this.picIcon.TabStop = false;
      // 
      // AccountTypeEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(444, 92);
      this.Controls.Add(this.tableLayoutPanel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AccountTypeEditor";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Добавить";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.flowLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TextBox AccountTypeName;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.PictureBox picIcon;
    private System.Windows.Forms.Button btnSelectIcon;
  }
}