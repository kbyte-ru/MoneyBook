namespace MoneyBook.WinApp
{
    partial class Login
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
      this.label1 = new System.Windows.Forms.Label();
      this.UserNameComboBox = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.PasswordTextBox = new System.Windows.Forms.TextBox();
      this.ShowPasswordCheckBox = new System.Windows.Forms.CheckBox();
      this.CreateButton = new System.Windows.Forms.Button();
      this.LogInButton = new System.Windows.Forms.Button();
      this.CancelButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(119, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(32, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Имя:";
      // 
      // UserNameComboBox
      // 
      this.UserNameComboBox.FormattingEnabled = true;
      this.UserNameComboBox.Location = new System.Drawing.Point(173, 12);
      this.UserNameComboBox.Name = "UserNameComboBox";
      this.UserNameComboBox.Size = new System.Drawing.Size(179, 21);
      this.UserNameComboBox.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(119, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Пароль:";
      // 
      // PasswordTextBox
      // 
      this.PasswordTextBox.Location = new System.Drawing.Point(173, 42);
      this.PasswordTextBox.Name = "PasswordTextBox";
      this.PasswordTextBox.PasswordChar = '*';
      this.PasswordTextBox.Size = new System.Drawing.Size(179, 20);
      this.PasswordTextBox.TabIndex = 3;
      // 
      // ShowPasswordCheckBox
      // 
      this.ShowPasswordCheckBox.AutoSize = true;
      this.ShowPasswordCheckBox.Location = new System.Drawing.Point(173, 68);
      this.ShowPasswordCheckBox.Name = "ShowPasswordCheckBox";
      this.ShowPasswordCheckBox.Size = new System.Drawing.Size(179, 17);
      this.ShowPasswordCheckBox.TabIndex = 4;
      this.ShowPasswordCheckBox.Text = "отображать пароль при вводе";
      this.ShowPasswordCheckBox.UseVisualStyleBackColor = true;
      this.ShowPasswordCheckBox.CheckedChanged += new System.EventHandler(this.ShowPasswordCheckBox_CheckedChanged);
      // 
      // CreateButton
      // 
      this.CreateButton.Location = new System.Drawing.Point(12, 91);
      this.CreateButton.Name = "CreateButton";
      this.CreateButton.Size = new System.Drawing.Size(75, 23);
      this.CreateButton.TabIndex = 5;
      this.CreateButton.Text = "Создать";
      this.CreateButton.UseVisualStyleBackColor = true;
      this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
      // 
      // LogInButton
      // 
      this.LogInButton.Location = new System.Drawing.Point(173, 91);
      this.LogInButton.Name = "LogInButton";
      this.LogInButton.Size = new System.Drawing.Size(75, 23);
      this.LogInButton.TabIndex = 6;
      this.LogInButton.Text = "Войти";
      this.LogInButton.UseVisualStyleBackColor = true;
      this.LogInButton.Click += new System.EventHandler(this.LogInButton_Click);
      // 
      // CancelButton
      // 
      this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelButton.Location = new System.Drawing.Point(277, 91);
      this.CancelButton.Name = "CancelButton";
      this.CancelButton.Size = new System.Drawing.Size(75, 23);
      this.CancelButton.TabIndex = 7;
      this.CancelButton.Text = "Отмена";
      this.CancelButton.UseVisualStyleBackColor = true;
      this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
      // 
      // Login
      // 
      this.AcceptButton = this.LogInButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(367, 136);
      this.Controls.Add(this.CancelButton);
      this.Controls.Add(this.LogInButton);
      this.Controls.Add(this.CreateButton);
      this.Controls.Add(this.ShowPasswordCheckBox);
      this.Controls.Add(this.PasswordTextBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.UserNameComboBox);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.Name = "Login";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Вход в систему";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
      this.Load += new System.EventHandler(this.Login_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox UserNameComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.CheckBox ShowPasswordCheckBox;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button LogInButton;
        private System.Windows.Forms.Button CancelButton;
    }
}