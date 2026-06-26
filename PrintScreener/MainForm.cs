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

    private const string appName = "PrintScreener";
    private const string dateFormat = "yyyy-MM-dd";
    private const string timeFormat = "HH-mm-ss";
    private const int maxInterval = 3600;

    private const string defaultName = "Screenshot %date% %time%";
    private const int defaultInterval = 10;
    private const int defaultQuality = 80;
    private readonly string defaultPath;

    private readonly char[] invalidChars;

    private readonly Dictionary<string, ImageFormat> imageFormats = new()
    {
        { "jpg", ImageFormat.Jpeg },
        { "png", ImageFormat.Png },
        { "gif", ImageFormat.Gif },
        { "bmp", ImageFormat.Bmp },
    };

    private readonly Timer captureTimer;

    private int numCounter = 1;

    private bool fullScreen = true;
    private Rectangle area = new(0, 0, 100, 100);

    private Bitmap? prevImage;

    public MainForm()
    {
        InitializeComponent();

        defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
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
            if (captureTimer.Enabled && checkBoxHideWindow.Checked)
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
        if (IsPathOk(settings.Path) && Directory.Exists(settings.Path))
            textBoxPath.Text = settings.Path;
        else
            textBoxPath.Text = defaultPath;

        // file name
        if (IsPathOk(settings.Name))
            textBoxName.Text = settings.Name;
        else
            textBoxName.Text = defaultName;

        // file format
        Utility.FillComboBox(comboBoxType, [.. imageFormats.Keys], settings.Type);

        // jpg quality
        if (settings.JpegQuality >= 1 && settings.JpegQuality <= 100)
            numericQuality.Value = settings.JpegQuality;

        // hide/show jpg quality
        ToggleQualityInput();

        // interval
        if (settings.Interval >= 1 && settings.Interval <= maxInterval)
            numericInterval.Value = settings.Interval;

        // checkboxes
        checkBoxHideWindow.Checked = settings.HideWindow;
        checkBoxMonitorClipboard.Checked = settings.MonitorClipboard;

        // area buttons and text
        ToggleCurrentArea();

        // Monitor Clipboard
        clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;

        // Welcome message
        WriteToLog($"{appName} started at {DateTime.Now:G}.");
    }

    private void ClipboardMonitor_ClipboardChanged(object? sender, EventArgs e)
    {
        if (!checkBoxMonitorClipboard.Checked)
            return;
        var image = Bitmaps.GetBitmapFromClipboard();
        if (image != null)
            SaveImage(image);
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Save settings
        Properties.Settings.Default.Path = textBoxPath.Text;
        Properties.Settings.Default.Name = textBoxName.Text;
        Properties.Settings.Default.Type = comboBoxType.Text;
        Properties.Settings.Default.Interval = (int)numericInterval.Value;
        Properties.Settings.Default.MonitorClipboard = checkBoxMonitorClipboard.Checked;
        Properties.Settings.Default.HideWindow = checkBoxHideWindow.Checked;
        Properties.Settings.Default.JpegQuality = (int)numericQuality.Value;
        Properties.Settings.Default.Save();
    }

    private void SelectAreaBtn_Click(object sender, EventArgs e)
    {
        Hide();
        if (selectAreaForm == null || selectAreaForm.IsDisposed)
            selectAreaForm = new(this);
        selectAreaForm.Show();
    }

    private void ResetAreaBtn_Click(object sender, EventArgs e)
    {
        fullScreen = true;
        ToggleCurrentArea();
        buttonSelectArea.Focus();
    }

    private void BrowseBtn_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new();
        if (IsPathOk(textBoxPath.Text) && Directory.Exists(textBoxPath.Text))
            dialog.SelectedPath = textBoxPath.Text;
        else
            dialog.SelectedPath = defaultPath;

        dialog.Description = "Select output folder";
        dialog.UseDescriptionForTitle = true;
        dialog.ShowNewFolderButton = true;

        if (dialog.ShowDialog() == DialogResult.OK && IsPathOk(dialog.SelectedPath))
            textBoxPath.Text = dialog.SelectedPath;
    }

    private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ToggleQualityInput();
    }

    private void StartBtn_Click(object sender, EventArgs e)
    {
        if (captureTimer.Enabled)
            return;

        int interval = (int)numericInterval.Value;
        if (interval < 1 || interval > maxInterval)
        {
            MessageBox.Show("Wrong interval!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (checkBoxHideWindow.Checked)
            WindowState = FormWindowState.Minimized;

        StartCapturing(interval);
    }

    private void StopBtn_Click(object sender, EventArgs e)
    {
        StopCapturing();
    }

    private async void ShotBtn_Click(object sender, EventArgs e)
    {
        Opacity = 0;
        await Task.Delay(200);
        CaptureScreen();
        Opacity = 1;
    }

    private void OpenFolderBtn_Click(object sender, EventArgs e)
    {
        if (IsPathOk(textBoxPath.Text) && Directory.Exists(textBoxPath.Text))
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

    private void LogText_TextChanged(object sender, EventArgs e)
    {
        NativeMethods.SendMessage(richTextBoxLog.Handle, WM_VSCROLL, SB_BOTTOM, IntPtr.Zero);
    }

    private void ResetOptionsMenuItem_Click(object sender, EventArgs e)
    {
        ResetOptions();
        fullScreen = true;
        ToggleCurrentArea();
    }

    private void ResetCounterMenuItem_Click(object sender, EventArgs e)
    {
        numCounter = 1;
    }

    private void SaveLogMenuItem_Click(object sender, EventArgs e)
    {
        using SaveFileDialog dialog = new();

        dialog.OverwritePrompt = true;
        dialog.ValidateNames = true;
        dialog.Filter = "Text files|*.txt";

        if (IsPathOk(textBoxPath.Text) && Directory.Exists(textBoxPath.Text))
            dialog.InitialDirectory = textBoxPath.Text;

        string date = DateTime.Now.ToString(dateFormat);
        dialog.FileName = $"{appName} log {date}.txt";

        if (dialog.ShowDialog() == DialogResult.OK && IsPathOk(dialog.FileName))
        {
            try
            {
                File.WriteAllText(dialog.FileName, richTextBoxLog.Text);
                string fileName = Path.GetFileName(dialog.FileName);
                WriteToLog($"\"{fileName}\" is saved.");
            }
            catch (Exception ex)
            {
                WriteToLog(ex.Message);
            }
        }
    }

    private void ClearLogMenuItem_Click(object sender, EventArgs e)
    {
        richTextBoxLog.Text = "";
    }

    #endregion

    private void CaptureScreen()
    {
        var image = fullScreen ? Bitmaps.GetBitmapFromScreen() : Bitmaps.GetBitmapFromScreen(area);
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
        if (!force && Bitmaps.CompareBitmapsMemCmp(prevImage, image))
            return;

        try
        {
            string outputFilePath = GetOutputFilePath();
            string fileName = Path.GetFileName(outputFilePath);

            ImageFormat format;
            if (imageFormats.TryGetValue(comboBoxType.Text, out ImageFormat? value))
                format = value;
            else
                format = imageFormats.First().Value;

            if (Bitmaps.SaveBitmap(image, outputFilePath, format, (long)numericQuality.Value))
            {
                prevImage = image;
                WriteToLog($"\"{fileName}\" is saved.");
            }
            else
                WriteToLog($"\"{fileName}\" isn't saved!");
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

        if (!IsPathOk(outputPath))
            throw new Exception("Wrong output folder!");

        if (!IsPathOk(fileName))
            throw new Exception("Wrong file name!");

        string type = comboBoxType.Text;
        if (!imageFormats.ContainsKey(type))
            type = imageFormats.First().Key;

        fileName = fileName
            .Replace("%date%", DateTime.Now.ToString(dateFormat))
            .Replace("%time%", DateTime.Now.ToString(timeFormat));
        if (fileName.Contains("%num%"))
        {
            fileName = fileName.Replace("%num%", numCounter.ToString());
            numCounter++;
        }

        // combine parts and find actual directory name if fileName has slashes
        string fullPath = Path.Combine(outputPath, fileName + "." + type);
        outputPath = Path.GetDirectoryName(fullPath) ?? "";
        if (IsPathOk(outputPath) && !Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        return Utility.GetUniqueFullPath(outputPath, fileName, type);
    }

    private void StartCapturing(int interval)
    {
        captureTimer.Interval = interval * 1000;
        captureTimer.Start();

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
        ToggleControls();
    }

    private void WriteToLog(string message)
    {
        if (richTextBoxLog.TextLength > 0)
            richTextBoxLog.AppendText("\n");
        richTextBoxLog.AppendText($"[{DateTime.Now:T}] {message}");
    }

    private void ToggleQualityInput()
    {
        bool isJpeg = comboBoxType.Text == "jpg";
        labelQuality.Enabled = isJpeg;
        numericQuality.Enabled = isJpeg;
    }

    private void ToggleControls()
    {
        bool isRunning = captureTimer.Enabled;
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

    private void ResetOptions()
    {
        textBoxPath.Text = defaultPath;
        textBoxName.Text = defaultName;
        comboBoxType.SelectedIndex = 0;
        numericQuality.Value = defaultQuality;
        numericInterval.Value = defaultInterval;
    }

    private bool IsPathOk(string path)
    {
        return !string.IsNullOrWhiteSpace(path) && path.IndexOfAny(invalidChars) == -1;
    }
}
