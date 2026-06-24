namespace PrintScreener;

public partial class SelectArea : Form
{
    private readonly MainForm mainForm;

    private const int WM_NCHITTEST = 0x84;
    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HTCAPTION = 2;

    private const int
        HTLEFT = 10,
        HTRIGHT = 11,
        HTTOP = 12,
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17;

    private const int wh = 10;

    private Rectangle RectTop => new(0, 0, ClientSize.Width, wh);
    private Rectangle RectLeft => new(0, 0, wh, ClientSize.Height);
    private Rectangle RectBottom => new(0, ClientSize.Height - wh, ClientSize.Width, wh);
    private Rectangle RectRight => new(ClientSize.Width - wh, 0, wh, ClientSize.Height);
    private Rectangle RectTopLeft => new(0, 0, wh, wh);
    private Rectangle RectTopRight => new(ClientSize.Width - wh, 0, wh, wh);
    private Rectangle RectBottomLeft => new(0, ClientSize.Height - wh, wh, wh);
    private Rectangle RectBottomRight => new(ClientSize.Width - wh, ClientSize.Height - wh, wh, wh);

    public SelectArea(MainForm parent)
    {
        InitializeComponent();

        panelDrag.Cursor = Cursors.SizeAll;
        buttonSelect.Cursor = Cursors.Default;

        SetStyle(ControlStyles.ResizeRedraw, true); // avoid visual artifacts

        mainForm = parent;
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == WM_NCHITTEST)
        {
            var cursor = PointToClient(Cursor.Position);

            if (RectTopLeft.Contains(cursor)) m.Result = HTTOPLEFT;
            else if (RectTopRight.Contains(cursor)) m.Result = HTTOPRIGHT;
            else if (RectBottomLeft.Contains(cursor)) m.Result = HTBOTTOMLEFT;
            else if (RectBottomRight.Contains(cursor)) m.Result = HTBOTTOMRIGHT;

            else if (RectTop.Contains(cursor)) m.Result = HTTOP;
            else if (RectLeft.Contains(cursor)) m.Result = HTLEFT;
            else if (RectRight.Contains(cursor)) m.Result = HTRIGHT;
            else if (RectBottom.Contains(cursor)) m.Result = HTBOTTOM;
        }
    }

    private void DragControlsMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, IntPtr.Zero);
        }
    }

    private void SelectArea_FormClosing(object sender, FormClosingEventArgs e)
    {
        mainForm.SetArea(Location.X, Location.Y, Width, Height);
        mainForm.Show();
    }

    private void SelectBtnClick(object sender, EventArgs e)
    {
        Hide();
        mainForm.SetArea(Location.X, Location.Y, Width, Height);
        mainForm.Show();
    }

    private void SelectArea_Load(object sender, EventArgs e)
    {
        ShowCurrentArea();
    }

    private void SelectArea_Move(object sender, EventArgs e)
    {
        ShowCurrentArea();
    }

    private void SelectArea_Resize(object sender, EventArgs e)
    {
        ShowCurrentArea();
    }

    private void ShowCurrentArea()
    {
        labelSize.Text = $"{Location.X}, {Location.Y}, {Width}, {Height}";
    }
}
