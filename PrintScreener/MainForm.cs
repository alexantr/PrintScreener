using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace PrintScreener;

public partial class MainForm : Form
{
    private readonly ClipboardMonitor clipboardMonitor;

    private readonly char[] invalidChars;

    private ImageFormat imageFormat = ImageFormat.Jpeg; // first in formatList

    private readonly List<string> formatList = ["jpg", "png", "gif", "bmp"];

    private Timer? captureTimer;

    private int numCounter = 1;

    private Bitmap? prevImage;

    [LibraryImport("msvcrt.dll")]
    internal static partial int memcmp(IntPtr b1, IntPtr b2, long count);

    public MainForm()
    {
        InitializeComponent();

        invalidChars = Path.GetInvalidPathChars();

        clipboardMonitor = new();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        // output path from settings
        if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Path) && Directory.Exists(Properties.Settings.Default.Path))
            textBoxPath.Text = Properties.Settings.Default.Path;
        else
            textBoxPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

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
        ToggleQualityInput();

        // jpg quality
        if (Properties.Settings.Default.JpegQuality >= 1 && Properties.Settings.Default.JpegQuality <= 100)
            numericQuality.Value = Properties.Settings.Default.JpegQuality;

        // interval
        if (Properties.Settings.Default.Interval > 0 && Properties.Settings.Default.Interval <= 3600)
            numericInterval.Value = Properties.Settings.Default.Interval;

        // checkboxes
        checkBoxMonitorClipboard.Checked = Properties.Settings.Default.MonitorClipboard;
        checkBoxHideWindow.Checked = Properties.Settings.Default.HideWindow;

        // Welcome message
        WriteInLog("PrintScreener is started.", false);

        // Monitor Clipboard
        clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
    }

    private void ClipboardMonitor_ClipboardChanged(object? sender, EventArgs e)
    {
        if (!checkBoxMonitorClipboard.Checked)
            return;
        if (Clipboard.ContainsImage())
        {
            IDataObject? iData = Clipboard.GetDataObject();

            if (iData != null && iData.GetDataPresent(DataFormats.Bitmap))
            {
                iData.TryGetData(DataFormats.Bitmap, out Bitmap? image);
                SaveImage(image);
            }
        }
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

    private void BrowseBtnClick(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new();
        if (!string.IsNullOrWhiteSpace(textBoxPath.Text) && Directory.Exists(textBoxPath.Text))
            dialog.SelectedPath = textBoxPath.Text;
        else
            dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        dialog.Description = "Select output folder";
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
        if (captureTimer != null && captureTimer.Enabled)
            return;

        int interval = Convert.ToInt32(Math.Round(numericInterval.Value, 0));
        if (interval < 1 || interval > 3600)
        {
            MessageBox.Show("Wrong interval!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (checkBoxHideWindow.Checked)
            WindowState = FormWindowState.Minimized;

        if (captureTimer == null)
        {
            captureTimer = new();
            captureTimer.Tick += CaptureTimer_Tick;
        }
        captureTimer.Interval = interval * 1000;
        captureTimer.Start();

        WriteInLog(string.Format("Start taking screenshots every {0} sec.", interval));

        ToggleControls(true);
    }

    private void CaptureTimer_Tick(object? sender, EventArgs e)
    {
        Rectangle bounds = Screen.PrimaryScreen.Bounds;
        Bitmap image = new(bounds.Width, bounds.Height);
        using Graphics graphics = Graphics.FromImage(image);
        graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
        SaveImage(image);
    }

    private void StopBtnClick(object sender, EventArgs e)
    {
        if (captureTimer != null && captureTimer.Enabled)
        {
            captureTimer.Stop();
            WriteInLog("Stop taking screenshots.");
        }
        ToggleControls(false);
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
        // manually scroll to bottom cause AppendText doesn't do it if it doesn't have focus
        ClipboardMonitor.SendMessage(richTextBoxLog.Handle, 0x115, 7, IntPtr.Zero); // 0x115: WM_VSCROLL, 7: SB_BOTTOM
    }

    /// <summary>
    /// Get image full path
    /// </summary>
    /// <returns>Image path</returns>
    private string GetOutputFilePath()
    {
        string outputPath = textBoxPath.Text;
        string fileName = textBoxName.Text;

        if (string.IsNullOrWhiteSpace(outputPath) || invalidChars.Any(outputPath.Contains))
            throw new Exception("Wrong output folder!");

        if (string.IsNullOrWhiteSpace(fileName) || invalidChars.Any(fileName.Contains))
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

    /// <summary>
    /// Save Image to disk
    /// </summary>
    /// <param name="image">Bitmap</param>
    private void SaveImage(Bitmap? image)
    {
        if (image == null)
            return;

        if (CompareBitmapsMemCmp(prevImage, image))
            return;

        try
        {
            string outputFilePath = GetOutputFilePath();

            if (imageFormat == ImageFormat.Jpeg)
            {
                long quality = Convert.ToInt64(numericQuality.Value);

                ImageCodecInfo? jpgEncoder = GetEncoder(imageFormat);
                if (jpgEncoder != null)
                {
                    EncoderParameters encoderParameters = new(1);
                    encoderParameters.Param[0] = new(Encoder.Quality, quality);

                    image.Save(outputFilePath, jpgEncoder, encoderParameters);
                }
                else
                {
                    image.Save(outputFilePath, imageFormat);
                }
            }
            else
            {
                image.Save(outputFilePath, imageFormat);
            }

            prevImage = image;

            WriteInLog(string.Format("\"{0}\" saved.", Path.GetFileName(outputFilePath)));
        }
        catch (Exception ex)
        {
            if (captureTimer != null && captureTimer.Enabled)
            {
                captureTimer.Stop();
                ToggleControls(false);
            }
            WriteInLog(ex.Message);
        }
    }

    private void WriteInLog(string message, bool newLine = true)
    {
        if (newLine)
            richTextBoxLog.AppendText("\n");
        richTextBoxLog.AppendText(string.Format("[{1}] {0}", message, DateTime.Now.ToString("G")));
    }

    /// <summary>
    /// Set JPEG Compression Level
    /// https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-set-jpeg-compression-level
    /// </summary>
    /// <param name="format">ImageFormat</param>
    /// <returns>ImageCodecInfo</returns>
    private static ImageCodecInfo? GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
                return codec;
        }
        return null;
    }

    private void ToggleQualityInput()
    {
        bool isJpeg = comboBoxFormat.Text == "jpg";
        labelQuality.Enabled = isJpeg;
        numericQuality.Enabled = isJpeg;
    }

    private void ToggleControls(bool isRunning)
    {
        groupBoxOptions.Enabled = !isRunning;
        buttonStart.Enabled = !isRunning;
        buttonStop.Enabled = isRunning;
    }

    /// <summary>
    /// Compare two bitmaps to determine whether they are identical
    /// https://stackoverflow.com/questions/2031217/
    /// </summary>
    /// <param name="b1">Bitmap</param>
    /// <param name="b2">Bitmap</param>
    /// <returns>True if two bitmaps are identical</returns>
    private static bool CompareBitmapsMemCmp(Bitmap? b1, Bitmap? b2)
    {
        if (b1 == null || b2 == null)
            return false;
        if (b1.Size != b2.Size)
            return false;

        var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        try
        {
            IntPtr bd1scan0 = bd1.Scan0;
            IntPtr bd2scan0 = bd2.Scan0;

            int stride = bd1.Stride;
            int len = stride * b1.Height;

            return memcmp(bd1scan0, bd2scan0, len) == 0;
        }
        finally
        {
            b1.UnlockBits(bd1);
            b2.UnlockBits(bd2);
        }
    }
}
