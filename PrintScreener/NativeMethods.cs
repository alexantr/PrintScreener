using System.Runtime.InteropServices;

namespace PrintScreener;

internal partial class NativeMethods
{
    [LibraryImport("user32.dll")]
    internal static partial IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ReleaseCapture();

    [LibraryImport("user32.dll", EntryPoint = "SendMessageW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    [LibraryImport("msvcrt.dll")]
    internal static partial int memcmp(IntPtr b1, IntPtr b2, long count);
}
