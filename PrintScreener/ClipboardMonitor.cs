namespace PrintScreener;

public partial class ClipboardMonitor : Form
{
    public event EventHandler? ClipboardChanged;

    private const int WM_DRAWCLIPBOARD = 0x308;
    private const int WM_CHANGECBCHAIN = 0x30D;

    private IntPtr nextClipboardViewer;

    public ClipboardMonitor()
    {
        nextClipboardViewer = NativeMethods.SetClipboardViewer(Handle);
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_DRAWCLIPBOARD)
        {
            ClipboardChanged?.Invoke(this, EventArgs.Empty);
            NativeMethods.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
        }
        else if (m.Msg == WM_CHANGECBCHAIN)
        {
            if (m.WParam == nextClipboardViewer)
                nextClipboardViewer = m.LParam;
            else
                NativeMethods.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
        }

        base.WndProc(ref m);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        NativeMethods.ChangeClipboardChain(Handle, nextClipboardViewer);
        base.OnFormClosing(e);
    }
}
