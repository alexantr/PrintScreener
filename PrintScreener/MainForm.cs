using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintScreener
{
    public partial class MainForm : Form
    {
        //private string format;

        private Dictionary<string, string> formatList;

        public MainForm()
        {
            InitializeComponent();

            formatList = new Dictionary<string, string>();
            formatList.Add("jpg", "JPEG (*.jpg)");
            formatList.Add("png", "PNG (*.png)");
            formatList.Add("bmp", "BMP (*.bmp)");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // path from settings
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Path) && Directory.Exists(Properties.Settings.Default.Path))
                textBoxPath.Text = Properties.Settings.Default.Path;
            else
                textBoxPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // format
            int index = 0, selectedIndex = 0;
            comboBoxFormat.Items.Clear();
            foreach (KeyValuePair<string, string> kvp in formatList)
            {
                comboBoxFormat.Items.Add(new ComboBoxItem(kvp.Key, kvp.Value));
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Format) && kvp.Key == Properties.Settings.Default.Format)
                    selectedIndex = index;
                index++;
            }
            comboBoxFormat.SelectedIndex = selectedIndex;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (!string.IsNullOrWhiteSpace(textBoxPath.Text) && Directory.Exists(textBoxPath.Text))
                    dialog.SelectedPath = textBoxPath.Text;
                else
                    dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                dialog.Description = "Select folder";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    textBoxPath.Text = dialog.SelectedPath;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var comboBoxItemFormat = (ComboBoxItem)comboBoxFormat.SelectedItem;

            // save settings
            Properties.Settings.Default.Format = comboBoxItemFormat.Value;
            Properties.Settings.Default.Path = textBoxPath.Text;
            Properties.Settings.Default.Save();
        }
    }
}
