using System;
using System.Drawing;
using System.IO;
using MoneyBook.Core;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{

    /// <summary>
    /// Представляет форму просмотра и выбора иконок из коллекции пользователя.
    /// </summary>
    public partial class IconsViewer : MoneyBook.WinApp.MForm
    {

        private int _SelectedIconId;

        /// <summary>
        /// Идентификатор выбранной иконки.
        /// </summary>
        public int SelectedIconId
        {
            get
            {
                return _SelectedIconId;
            }
            private set
            {
                _SelectedIconId = value;
                btnDelete.Enabled = btnSelect.Enabled = value > 0;
            }
        }

        public IconsViewer(User user, int selectedIconId)
            : base(user)
        {
            InitializeComponent();
            this.SelectedIconId = selectedIconId;
        }

        private void IconsViewer_Load(object sender, System.EventArgs e)
        {
            this.LoadIcons();

            if (this.listView1.Items.Count <= 0)
            {
                this.SelectedIconId = 0;
            }

            if (this.listView1.Items.Count > 0 && this.SelectedIconId == 0)
            {
                this.SelectedIconId = Convert.ToInt32(this.listView1.Items[0].ImageKey);
            }

            if (this.SelectedIconId > 0)
            {
                int index = this.listView1.Items.IndexOfKey(this.SelectedIconId.ToString());

                if (index == -1)
                {
                    this.SelectedIconId = 0;
                }
                else
                {
                    this.listView1.FocusedItem = this.listView1.Items[index];
                    this.listView1.FocusedItem.Selected = true;
                }
            }
        }

        private void LoadIcons()
        {
            this.listView1.Items.Clear();
            this.imageList1.Images.Clear();

            var icons = this.User.GetIcons();

            foreach (var icon in icons)
            {
                using (var m = new MemoryStream(icon.Data))
                {
                    string key = icon.Id.ToString();

                    try
                    {
                        this.imageList1.Images.Add(key, Image.FromStream(m));                       
                    }
                    catch
                    {
                        this.imageList1.Images.Add(key, Properties.Resources.cross);
                    }

                    this.listView1.Items.Add(icon.Id.ToString(), null, key);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.listView1.FocusedItem != null)
            {
                this.SelectedIconId = Convert.ToInt32(this.listView1.FocusedItem.ImageKey);

                pictureBox1.Image = this.imageList1.Images[this.SelectedIconId.ToString()];
            }
            else
            {
                this.SelectedIconId = 0;
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void listView1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.listView1.FocusedItem != null)
            {
                // this.SelectedIconId = Convert.ToInt32(this.listView1.FocusedItem.ImageKey);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы изображений (*.jpg, *.jpeg, *.jpe, *.png) | *.jpg; *.jpeg; *.jpe; *.png";
            fileDialog.Title = "Выберите файл изображения";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] buffer = null;
                using (FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                }
                this.User.AddIcon(buffer);
            }
            this.LoadIcons();
        }
    }
}