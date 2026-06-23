using System.Runtime.InteropServices;

namespace PrintScreener;

internal partial class ClipboardMonitor : Form
{
    public event EventHandler? ClipboardChanged;

    private const int WM_DRAWCLIPBOARD = 0x0308;
    private const int WM_CHANGECBCHAIN = 0x030D;

    [LibraryImport("user32.dll")]
    internal static partial IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

    [LibraryImport("user32.dll", EntryPoint = "SendMessageW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

    private IntPtr nextClipboardViewer;

    public ClipboardMonitor()
    {
        nextClipboardViewer = SetClipboardViewer(Handle);
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case WM_DRAWCLIPBOARD:
                ClipboardChanged?.Invoke(this, EventArgs.Empty);
                SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                break;
            case WM_CHANGECBCHAIN:
                if (m.WParam == nextClipboardViewer)
                    nextClipboardViewer = m.LParam;
                else
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                break;
        }

        base.WndProc(ref m);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        ChangeClipboardChain(Handle, nextClipboardViewer);
        base.OnFormClosing(e);
    }
}
