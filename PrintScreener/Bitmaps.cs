using System.Drawing.Imaging;

namespace PrintScreener;

internal static class Bitmaps
{
    internal static bool SaveBitmap(Bitmap image, string outputFilePath, ImageFormat format, long quality)
    {
        // How to: Set JPEG Compression Level
        // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-set-jpeg-compression-level

        ImageCodecInfo? encoder;
        if (format == ImageFormat.Jpeg && (encoder = GetEncoder(format)) != null)
        {
            EncoderParameters encoderParameters = new(1);
            encoderParameters.Param[0] = new(Encoder.Quality, quality);

            image.Save(outputFilePath, encoder, encoderParameters);
        }
        else
            image.Save(outputFilePath, format);

        return File.Exists(outputFilePath);
    }

    /// <summary>
    /// Bitmap from clipboard
    /// </summary>
    /// <returns>Bitmap or null</returns>
    internal static Bitmap? GetBitmapFromClipboard()
    {
        if (Clipboard.ContainsImage())
        {
            IDataObject? iData = Clipboard.GetDataObject();

            if (iData != null && iData.GetDataPresent(DataFormats.Bitmap))
            {
                iData.TryGetData(DataFormats.Bitmap, out Bitmap? image);
                return image;
            }
        }
        return null;
    }

    internal static Bitmap? GetBitmapFromScreen()
    {
        if (Screen.PrimaryScreen == null)
            return null;

        Rectangle bounds = Screen.PrimaryScreen.Bounds;

        Bitmap image = new(bounds.Width, bounds.Height, PixelFormat.Format24bppRgb);

        using Graphics graphics = Graphics.FromImage(image);
        graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size, CopyPixelOperation.SourceCopy);

        return image;
    }

    internal static Bitmap GetBitmapFromScreen(Rectangle area)
    {
        Bitmap image = new(area.Width, area.Height, PixelFormat.Format24bppRgb);

        using Graphics graphics = Graphics.FromImage(image);
        graphics.CopyFromScreen(area.Location, Point.Empty, area.Size, CopyPixelOperation.SourceCopy);

        return image;
    }

    /// <summary>
    /// Get Encoder
    /// </summary>
    /// <param name="format">ImageFormat</param>
    /// <returns>ImageCodecInfo</returns>
    internal static ImageCodecInfo? GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
                return codec;
        }
        return null;
    }

    /// <summary>
    /// Compare two bitmaps to determine whether they are identical
    /// https://stackoverflow.com/questions/2031217/
    /// </summary>
    /// <param name="b1">Bitmap</param>
    /// <param name="b2">Bitmap</param>
    /// <returns>True if two bitmaps are identical</returns>
    internal static bool CompareBitmapsMemCmp(Bitmap? b1, Bitmap? b2)
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

            return NativeMethods.memcmp(bd1scan0, bd2scan0, len) == 0;
        }
        finally
        {
            b1.UnlockBits(bd1);
            b2.UnlockBits(bd2);
        }
    }
}
