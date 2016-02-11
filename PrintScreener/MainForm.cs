using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PrintScreener
{
    public partial class MainForm : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        private const int WM_DRAWCLIPBOARD = 0x0308; // WM_DRAWCLIPBOARD message
        private IntPtr clipboardViewerNext; // Our variable that will hold the value to identify the next window in the clipboard viewer chain

        ImageFormat fileFormat = ImageFormat.Png;

        private Dictionary<string, string> formatList;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m); // Process the message 

            if (m.Msg == WM_DRAWCLIPBOARD)
            {
                IDataObject iData = Clipboard.GetDataObject(); // Clipboard's data

                //if (iData.GetDataPresent(DataFormats.Text))
                //{
                //    string text = (string)iData.GetData(DataFormats.Text);
                //}

                // TODO: use checkbox to enable/disable autocapture of "PrintScreen" button
                if (iData.GetDataPresent(DataFormats.Bitmap))
                {
                    Bitmap image = (Bitmap)iData.GetData(DataFormats.Bitmap);

                    try
                    {
                        string outputFilePath = getOutputFilePath();

                        image.Save(outputFilePath, fileFormat);
                        WriteInLog(string.Format("\"{0}\" saved.", Path.GetFileName(outputFilePath)));
                    }
                    catch (Exception ex)
                    {
                        if (shotTimer != null && shotTimer.Enabled)
                        {
                            shotTimer.Stop();
                            numericUpDownInterval.Enabled = true;
                            buttonStartStop.Text = "Start";
                            WriteInLog("Task is stopped");
                        }
                        WriteInLog(ex.Message);
                    }
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();

            formatList = new Dictionary<string, string>();
            formatList.Add("jpg", "JPEG (*.jpg)");
            formatList.Add("png", "PNG (*.png)");
            formatList.Add("gif", "GIF (*.gif)");
            formatList.Add("bmp", "BMP (*.bmp)");
        }

        char[] invalidChars = Path.GetInvalidPathChars();

        private void MainForm_Load(object sender, EventArgs e)
        {
            // output path from settings
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Path) && Directory.Exists(Properties.Settings.Default.Path))
                textBoxPath.Text = Properties.Settings.Default.Path;
            else
                textBoxPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // file name
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Name) && !invalidChars.Any(Properties.Settings.Default.Name.Contains))
                textBoxName.Text = Properties.Settings.Default.Name;
            else
                textBoxName.Text = "Screenshot %date% %time%";

            // file format
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

            // interval
            if (Properties.Settings.Default.Interval > 0)
                numericUpDownInterval.Value = Properties.Settings.Default.Interval;

            // clipboard
            clipboardViewerNext = SetClipboardViewer(this.Handle);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChangeClipboardChain(this.Handle, clipboardViewerNext); // Removes our from the chain of clipboard viewers when the form closes.

            var comboBoxItemFormat = (ComboBoxItem)comboBoxFormat.SelectedItem;

            // save settings
            Properties.Settings.Default.Format = comboBoxItemFormat.Value;
            Properties.Settings.Default.Name = textBoxName.Text;
            Properties.Settings.Default.Path = textBoxPath.Text;
            Properties.Settings.Default.Interval = Convert.ToInt32(Math.Round(numericUpDownInterval.Value, 0));
            Properties.Settings.Default.Save();
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

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxPath.Text) && Directory.Exists(textBoxPath.Text))
                Process.Start(textBoxPath.Text);
            else
                MessageBox.Show("Path not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Get full path of image
        /// </summary>
        /// <returns>Image Path</returns>
        private string getOutputFilePath()
        {
            string outputPath = textBoxPath.Text;
            string fileName = textBoxName.Text;

            if (string.IsNullOrWhiteSpace(outputPath) || invalidChars.Any(outputPath.Contains))
                throw new Exception("Wrong output folder path!");

            if (string.IsNullOrWhiteSpace(fileName) || invalidChars.Any(fileName.Contains))
                throw new Exception("Wrong file name!");

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            var comboBoxItemFormat = (ComboBoxItem)comboBoxFormat.SelectedItem;
            string format = comboBoxItemFormat.Value;

            switch (format)
            {
                case "png":
                    fileFormat = ImageFormat.Png;
                    break;
                case "gif":
                    fileFormat = ImageFormat.Gif;
                    break;
                case "bmp":
                    fileFormat = ImageFormat.Bmp;
                    break;
                default:
                    fileFormat = ImageFormat.Jpeg;
                    break;
            }

            fileName = fileName.Replace("%date%", DateTime.Now.ToString("yyyy-MM-dd")).Replace("%time%", DateTime.Now.ToString("HH-mm-ss"));

            string path = Path.Combine(textBoxPath.Text, fileName + "." + format);

            int count = 2;
            while (File.Exists(path))
            {
                path = Path.Combine(textBoxPath.Text, fileName + " (" + count + ")." + format);
                count++;
            }

            return path;
        }

        private void GetSreenshot()
        {
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            try
            {
                string outputFilePath = getOutputFilePath();

                printscreen.Save(outputFilePath, fileFormat);
                WriteInLog(string.Format("\"{0}\" saved.", Path.GetFileName(outputFilePath)));
            }
            catch (Exception ex)
            {
                if (shotTimer != null && shotTimer.Enabled)
                {
                    shotTimer.Stop();
                    numericUpDownInterval.Enabled = true;
                    buttonStartStop.Text = "Start";
                    WriteInLog("Task is stopped");
                }
                WriteInLog(ex.Message);
            }
        }

        private Timer shotTimer;

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (shotTimer != null && shotTimer.Enabled)
            {
                shotTimer.Stop();
                numericUpDownInterval.Enabled = true;
                (sender as Button).Text = "Start";
                WriteInLog("Task is stopped");
                return;
            }

            int interval = Convert.ToInt32(Math.Round(numericUpDownInterval.Value, 0));

            shotTimer = new Timer(); // System.Windows.Forms.Timer
            shotTimer.Tick += new EventHandler(shotTimer_Tick);
            shotTimer.Interval = interval * 1000; // in miliseconds
            shotTimer.Start();

            numericUpDownInterval.Enabled = false;
            (sender as Button).Text = "Stop";

            WriteInLog(string.Format("Task is started every {0} second(s)", interval));

            //var timer = new System.Threading.Timer((ev) => GetSreenshot(), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
        }

        private void shotTimer_Tick(object sender, EventArgs e)
        {
            GetSreenshot();
        }

        private void WriteInLog(string message)
        {
            richTextBoxLog.AppendText(string.Format("[{1}] {0}\n", message, DateTime.Now.ToString("G")));
            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length; //Set the current caret position at the end
            richTextBoxLog.ScrollToCaret(); //Now scroll it automatically
        }
    }
}
