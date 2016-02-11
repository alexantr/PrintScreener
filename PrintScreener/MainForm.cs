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

        private List<string> formatList;

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

                if (iData.GetDataPresent(DataFormats.Bitmap) && checkBoxAutoCapture.Checked)
                {
                    Bitmap image = (Bitmap)iData.GetData(DataFormats.Bitmap);
                    SaveImage(image);
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();

            formatList = new List<string>();
            formatList.Add("jpg");
            formatList.Add("png");
            formatList.Add("gif");
            formatList.Add("bmp");
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
            foreach (string oneFormat in formatList)
            {
                comboBoxFormat.Items.Add(oneFormat);
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Format) && oneFormat == Properties.Settings.Default.Format)
                    selectedIndex = index;
                index++;
            }
            comboBoxFormat.SelectedIndex = selectedIndex;

            // hide/show jpg quality
            UpdateQuality();

            // jpg quality
            if (Properties.Settings.Default.JpegQuality >= 1 && Properties.Settings.Default.JpegQuality <= 100)
                numericUpDownQuality.Value = Properties.Settings.Default.JpegQuality;

            // interval
            if (Properties.Settings.Default.Interval > 0)
                numericUpDownInterval.Value = Properties.Settings.Default.Interval;

            // checkboxes
            checkBoxAutoCapture.Checked = Properties.Settings.Default.CaptureButton;
            checkBoxHideWindow.Checked = Properties.Settings.Default.HideWindow;

            // Welcome message
            WriteInLog("PrintScreener is started.", false);

            // Set Clipboard Viewer
            clipboardViewerNext = SetClipboardViewer(this.Handle);

            // Disable stop button
            buttonStop.Enabled = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Removes our from the chain of clipboard viewers when the form closes.
            ChangeClipboardChain(this.Handle, clipboardViewerNext);

            // Save settings
            Properties.Settings.Default.Format = comboBoxFormat.Text;
            Properties.Settings.Default.Name = textBoxName.Text;
            Properties.Settings.Default.Path = textBoxPath.Text;
            Properties.Settings.Default.Interval = Convert.ToInt32(Math.Round(numericUpDownInterval.Value, 0));
            Properties.Settings.Default.CaptureButton = checkBoxAutoCapture.Checked;
            Properties.Settings.Default.HideWindow = checkBoxHideWindow.Checked;
            Properties.Settings.Default.JpegQuality = Convert.ToInt32(Math.Round(numericUpDownQuality.Value, 0));
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

                dialog.Description = "Select output folder";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    textBoxPath.Text = dialog.SelectedPath;
            }
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateQuality();
        }

        private Timer captureTimer;

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (captureTimer != null && captureTimer.Enabled)
                return;

            int interval = Convert.ToInt32(Math.Round(numericUpDownInterval.Value, 0));
            if (interval < 1)
            {
                MessageBox.Show("Wrong interval!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkBoxHideWindow.Checked)
                this.WindowState = FormWindowState.Minimized;

            captureTimer = new Timer(); // System.Windows.Forms.Timer
            captureTimer.Tick += new EventHandler(captureTimer_Tick);
            captureTimer.Interval = interval * 1000;
            captureTimer.Start();

            groupBoxSettings.Enabled = false;
            buttonStop.Enabled = true;

            WriteInLog(string.Format("Auto capturing every {0} sec is started.", interval));
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (captureTimer != null && captureTimer.Enabled)
            {
                captureTimer.Stop();

                groupBoxSettings.Enabled = true;
                buttonStop.Enabled = false;

                WriteInLog("Auto capturing is stopped.");
            }
        }

        private void captureTimer_Tick(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(image as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, image.Size);
            SaveImage(image);
        }

        /// <summary>
        /// Get full path of image
        /// </summary>
        /// <returns>Image Path string</returns>
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

            string format = comboBoxFormat.Text;

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

        /// <summary>
        /// Save Image to disk
        /// </summary>
        /// <param name="image">Bitmap</param>
        private void SaveImage(Bitmap image)
        {
            try
            {
                string outputFilePath = getOutputFilePath();

                if (fileFormat == ImageFormat.Jpeg)
                {
                    long quality = Convert.ToInt64(Math.Round(numericUpDownQuality.Value, 0));

                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                    Encoder myEncoder = Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);

                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                    myEncoderParameters.Param[0] = myEncoderParameter;

                    image.Save(outputFilePath, jpgEncoder, myEncoderParameters);
                }
                else
                {
                    image.Save(outputFilePath, fileFormat);
                }

                WriteInLog(string.Format("\"{0}\" saved.", Path.GetFileName(outputFilePath)));
            }
            catch (Exception ex)
            {
                if (captureTimer != null && captureTimer.Enabled)
                {
                    captureTimer.Stop();

                    groupBoxSettings.Enabled = true;
                    buttonStop.Enabled = false;
                }
                WriteInLog(ex.Message);
            }
        }

        private void WriteInLog(string message, bool newLine = true)
        {
            if (newLine)
                richTextBoxLog.AppendText("\n");
            richTextBoxLog.AppendText(string.Format("[{1}] {0}", message, DateTime.Now.ToString("G")));
            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length; //Set the current caret position at the end
            richTextBoxLog.ScrollToCaret(); //Now scroll it automatically
        }

        private void UpdateQuality()
        {
            if (comboBoxFormat.Text == "jpg")
            {
                labelQuality.Enabled = true;
                numericUpDownQuality.Enabled = true;
            }
            else
            {
                labelQuality.Enabled = false;
                numericUpDownQuality.Enabled = false;
            }
        }

        /// <summary>
        /// How to: Set JPEG Compression Level
        /// https://msdn.microsoft.com/en-us/library/bb882583(v=vs.110).aspx
        /// </summary>
        /// <param name="format">ImageFormat</param>
        /// <returns>ImageCodecInfo</returns>
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }
    }
}
