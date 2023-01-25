using BrightnessControl.Controls;
using BrightnessControl.Native;
using BrightnessControl.Helpers;

namespace BrightnessControl
{
    public partial class SliderForm : Form
    {
        private IMonitorController _monitorController;

        public SliderForm(IMonitorController monitorController)
        {
            _monitorController = monitorController;

            InitializeComponent();

            SetUpForm();
            SetUpContextMenu();
        }

        private Point GetLocation()
        {
            RECT scBounds = new RECT();
            Calls.Attempt(Calls.GetWindowRect(Calls.GetDesktopWindow(), ref scBounds));

            int x = scBounds.right - Width;
            int y = scBounds.bottom - Height - 30;

            return new Point(x, y);
        }

        private void SetUpLayout()
        {
            Visible = false;
            Height = ApplicationConstants.TRACKBAR_CONTAINER_HEIGHT * _monitorController.Monitors.Count;
            Width = ApplicationConstants.WINDOW_WIDTH;
            Location = GetLocation();

            flowLayoutPanel.Controls.Clear();
            foreach (var monitor in _monitorController.Monitors)
            {
                flowLayoutPanel.Controls
                    .Add(new BrightnessBlock(monitor));
            }
        }

        private void SetUpForm()
        {
            notifyIcon.Icon = Properties.Resources.IconLoader;
            notifyIcon.Text = "Detecting monitors...";
            
            _monitorController.Initialize();
            SetUpLayout();

            notifyIcon.Icon = Properties.Resources.Icon;
            notifyIcon.Text = "Brightness";
        }

        private void SetUpContextMenu()
        {
            var refreshItem = new ToolStripMenuItem("Rescan monitors", Properties.Resources.IconRefresh.ToBitmap(), (sender, e) => SetUpForm());
            var exitItem = new ToolStripMenuItem("Exit", Properties.Resources.IconExit, (sender, e) => Application.Exit());

            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add(refreshItem);
            notifyIcon.ContextMenuStrip.Items.Add(exitItem);
        }

        private void ActivateForm()
        {
            WindowState = FormWindowState.Normal;
            Show();
            Activate();
            BringToFront();
        }

        private void DeactivateForm()
        {
            Hide();
        }

        private void SliderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // hide events
        private void trackBar_KeyDown(object sender, KeyEventArgs e)
        {
            // enter to confirm -> deactivate
            if (e.KeyCode == Keys.Enter) DeactivateForm();
        }

        // notifyicon events
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (Visible) DeactivateForm();
            else ActivateForm();
        }

        private void SliderForm_LostFocus(object sender, EventArgs e)
        {
            DeactivateForm();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if(m.Msg == (uint)WM.DISPLAYCHANGE) SetUpForm();
        }
    }
}
