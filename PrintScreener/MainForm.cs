using System.Diagnostics;
using System.Drawing.Imaging;
using Timer = System.Windows.Forms.Timer;

namespace PrintScreener;

public partial class MainForm : Form
{
    private readonly ClipboardMonitor clipboardMonitor;
    private SelectArea? selectAreaForm;

    private const int WM_VSCROLL = 0x115;
    private const int SB_BOTTOM = 7;

    private const string defaultName = "Screenshot %date% %time%";

    private readonly char[] invalidChars;

    private ImageFormat imageFormat = ImageFormat.Jpeg; // first in formatList

    private readonly List<string> formatList = ["jpg", "png", "gif", "bmp"];

    private bool isRunning = false;

    private readonly Timer captureTimer;

    private int numCounter = 1;

    private bool fullScreen = true;
    private Rectangle area = new(0, 0, 0, 0);

    private Bitmap? prevImage;

    public MainForm()
    {
        InitializeComponent();

        invalidChars = Path.GetInvalidPathChars();

        clipboardMonitor = new();

        captureTimer = new();
        captureTimer.Tick += (o, args) => CaptureScreen();
    }

    protected override void WndProc(ref Message m)
    {
        const int WM_SYSCOMMAND = 0x112;
        const int SC_RESTORE = 0xF120;

        if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
        {
            if (isRunning && checkBoxHideWindow.Checked)
            {
                StopCapturing();
            }
        }

        base.WndProc(ref m);
    }

    public void SetArea(int x, int y, int w, int h)
    {
        fullScreen = false;
        area.X = x;
        area.Y = y;
        area.Width = w;
        area.Height = h;
        ToggleCurrentArea();
    }

    #region Events

    private void MainForm_Load(object sender, EventArgs e)
    {
        var settings = Properties.Settings.Default;

        // output path from settings
        if (!string.IsNullOrWhiteSpace(settings.Path) && Directory.Exists(settings.Path))
            textBoxPath.Text = settings.Path;
        else
            textBoxPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        // file name
        if (!string.IsNullOrWhiteSpace(settings.Name) && settings.Name.IndexOfAny(invalidChars) == -1)
            textBoxName.Text = settings.Name;
        else
            textBoxName.Text = defaultName;

        // file format
        Utility.FillComboBox(comboBoxFormat, formatList, settings.Format);

        // jpg quality
        if (settings.JpegQuality >= 1 && settings.JpegQuality <= 100)
            numericQuality.Value = settings.JpegQuality;

        // hide/show jpg quality
        ToggleQualityInput();

        // interval
        if (settings.Interval >= 1 && settings.Interval <= 3600)
            numericInterval.Value = settings.Interval;

        // checkboxes
        checkBoxMonitorClipboard.Checked = settings.MonitorClipboard;
        checkBoxHideWindow.Checked = settings.HideWindow;

        // area buttons and text
        ToggleCurrentArea();

        // Monitor Clipboard
        clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;

        // Welcome message
        WriteToLog("PrintScreener is started.", false);
    }

    private void ClipboardMonitor_ClipboardChanged(object? sender, EventArgs e)
    {
        if (!checkBoxMonitorClipboard.Checked)
            return;
        var image = Utility.GetBitmapFromClipboard();
        if (image != null)
            SaveImage(image);
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Save settings
        Properties.Settings.Default.Path = textBoxPath.Text;
        Properties.Settings.Default.Format = comboBoxFormat.Text;
        Properties.Settings.Default.Name = textBoxName.Text;
        Properties.Settings.Default.Interval = Convert.ToInt32(Math.Round(numericInterval.Value, 0));
        Properties.Settings.Default.MonitorClipboard = checkBoxMonitorClipboard.Checked;
        Properties.Settings.Default.HideWindow = checkBoxHideWindow.Checked;
        Properties.Settings.Default.JpegQuality = Convert.ToInt32(Math.Round(numericQuality.Value, 0));
        Properties.Settings.Default.Save();
    }

    private void SelectAreaBtnClick(object sender, EventArgs e)
    {
        Hide();
        if (selectAreaForm == null || selectAreaForm.IsDisposed)
            selectAreaForm = new(this);
        selectAreaForm.Show();
    }

    private void ResetAreaBtnClick(object sender, EventArgs e)
    {
        fullScreen = true;
        ToggleCurrentArea();
        buttonSelectArea.Focus();
    }

    private void BrowseBtnClick(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new();
        if (!string.IsNullOrWhiteSpace(textBoxPath.Text) && Directory.Exists(textBoxPath.Text))
            dialog.SelectedPath = textBoxPath.Text;
        else
            dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        dialog.Description = "Select output folder";
        dialog.UseDescriptionForTitle = true;
        dialog.ShowNewFolderButton = true;

        if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            textBoxPath.Text = dialog.SelectedPath;
    }

    private void FormatIndexChanged(object sender, EventArgs e)
    {
        ToggleQualityInput();
    }

    private void StartBtnClick(object sender, EventArgs e)
    {
        if (captureTimer.Enabled)
            return;

        int interval = Convert.ToInt32(Math.Round(numericInterval.Value, 0));
        if (interval < 1 || interval > 3600)
        {
            MessageBox.Show("Wrong interval!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (checkBoxHideWindow.Checked)
            WindowState = FormWindowState.Minimized;

        StartCapturing(interval);
    }

    private void StopBtnClick(object sender, EventArgs e)
    {
        StopCapturing();
    }

    private async void ShotBtnClick(object sender, EventArgs e)
    {
        Opacity = 0;
        await Task.Delay(200);
        CaptureScreen();
        Opacity = 1;
    }

    private void OpenFolderBtnClick(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(textBoxPath.Text) && Directory.Exists(textBoxPath.Text))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = textBoxPath.Text,
                UseShellExecute = true,
                Verb = "open"
            });
        }
        else
            MessageBox.Show("Folder not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void LogTextChanged(object sender, EventArgs e)
    {
        NativeMethods.SendMessage(richTextBoxLog.Handle, WM_VSCROLL, SB_BOTTOM, IntPtr.Zero);
    }

    #endregion

    private void CaptureScreen()
    {
        var image = fullScreen ? Utility.GetBitmapFromScreen() : Utility.GetBitmapFromScreen(area);
        if (image != null)
            SaveImage(image, true);
    }

    /// <summary>
    /// Save Image to disk
    /// </summary>
    /// <param name="image">Bitmap</param>
    /// <param name="force">Force saving (do not compare images)</param>
    private void SaveImage(Bitmap image, bool force = false)
    {
        if (!force && Utility.CompareBitmapsMemCmp(prevImage, image))
            return;

        try
        {
            string outputFilePath = GetOutputFilePath();
            string fileName = Path.GetFileName(outputFilePath);

            if (imageFormat == ImageFormat.Jpeg)
            {
                long quality = Convert.ToInt64(numericQuality.Value);

                ImageCodecInfo? jpgEncoder = Utility.GetEncoder(imageFormat);
                if (jpgEncoder != null)
                {
                    EncoderParameters encoderParameters = new(1);
                    encoderParameters.Param[0] = new(Encoder.Quality, quality);

                    image.Save(outputFilePath, jpgEncoder, encoderParameters);
                }
                else
                    image.Save(outputFilePath, imageFormat);
            }
            else
                image.Save(outputFilePath, imageFormat);

            prevImage = image;

            if (File.Exists(outputFilePath))
                WriteToLog($"\"{fileName}\" saved.");
            else
                WriteToLog($"\"{fileName}\" not saved!");
        }
        catch (Exception ex)
        {
            WriteToLog(ex.Message);
            StopCapturing();
        }
    }

    private string GetOutputFilePath()
    {
        string outputPath = textBoxPath.Text;
        string fileName = textBoxName.Text;

        if (string.IsNullOrWhiteSpace(outputPath) || outputPath.IndexOfAny(invalidChars) >= 0)
            throw new Exception("Wrong output folder!");

        if (string.IsNullOrWhiteSpace(fileName) || fileName.IndexOfAny(invalidChars) >= 0)
            throw new Exception("Wrong file name!");

        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        string format = comboBoxFormat.Text;
        if (formatList.IndexOf(format) == -1)
            format = "jpg";

        imageFormat = format switch
        {
            "png" => ImageFormat.Png,
            "gif" => ImageFormat.Gif,
            "bmp" => ImageFormat.Bmp,
            _ => ImageFormat.Jpeg,
        };
        fileName = fileName
            .Replace("%date%", DateTime.Now.ToString("yyyy-MM-dd"))
            .Replace("%time%", DateTime.Now.ToString("HH-mm-ss"));
        if (fileName.Contains("%num%"))
        {
            fileName = fileName.Replace("%num%", numCounter.ToString());
            numCounter++;
        }

        string path = Path.Combine(textBoxPath.Text, fileName + "." + format);

        int count = 2;
        while (File.Exists(path))
        {
            path = Path.Combine(textBoxPath.Text, fileName + " (" + count + ")." + format);
            count++;
        }

        return path;
    }

    private void StartCapturing(int interval)
    {
        captureTimer.Interval = interval * 1000;
        captureTimer.Start();

        isRunning = true;
        ToggleControls();

        WriteToLog($"Start capturing every {interval} sec.");
    }

    private void StopCapturing()
    {
        if (captureTimer.Enabled)
        {
            captureTimer.Stop();
            WriteToLog("Stop capturing.");
        }
        isRunning = false;
        ToggleControls();
    }

    private void WriteToLog(string message, bool newLine = true)
    {
        if (newLine)
            richTextBoxLog.AppendText("\n");
        richTextBoxLog.AppendText(string.Format("[{1}] {0}", message, DateTime.Now.ToString("G")));
    }

    private void ToggleQualityInput()
    {
        bool isJpeg = comboBoxFormat.Text == "jpg";
        labelQuality.Enabled = isJpeg;
        numericQuality.Enabled = isJpeg;
    }

    private void ToggleControls()
    {
        groupBoxOptions.Enabled = !isRunning;
        checkBoxHideWindow.Enabled = !isRunning;
        buttonStart.Enabled = !isRunning;
        buttonStop.Enabled = isRunning;
    }

    private void ToggleCurrentArea()
    {
        buttonResetArea.Enabled = !fullScreen;
        if (fullScreen)
            textBoxArea.Text = "Full screen";
        else
            textBoxArea.Text = $"{area.X}, {area.Y}, {area.Width}, {area.Height}";
    }
}
