using BrightnessControl.Controls;
using BrightnessControl.Native;
using BrightnessControl.Helpers;

namespace BrightnessControl
{
    public partial class SliderForm : Form
    {
        private IMonitorController monitorController;
        public SliderForm(IMonitorController monitorController)
        {
            this.monitorController = monitorController;

            monitorController.Initialize();

            InitializeComponent();

            SetUpForm();
            
            SetUpContextMenu();
        }

        private Point GetLocation()
        {
            RECT scBounds = new RECT();
            Calls.Attempt(Calls.GetWindowRect(Calls.GetDesktopWindow(), ref scBounds));

            int x = scBounds.right - this.Width;
            int y = scBounds.bottom - this.Height - 30;

            return new Point(x, y);
        }

        private void SetUpForm(bool hide = true)
        {
            Icon icon = notifyIcon.Icon;

            this.notifyIcon.Icon = Properties.Resources.IconRefresh;
            this.notifyIcon.Text = "Detecting monitors...";
            
            monitorController.Initialize();

            this.Height = ApplicationConstants.TRACKBAR_CONTAINER_HEIGHT * monitorController.Monitors.Count;
            this.Width = ApplicationConstants.WINDOW_WIDTH;

            this.Location = GetLocation();

            if(hide) this.Visible = false;

            this.flowLayoutPanel.Controls.Clear();

            foreach (var monitor in monitorController.Monitors)
            {
                this.flowLayoutPanel
                    .Controls.Add(new BrightnessBlock(monitor));
            }

            this.notifyIcon.Icon = icon;
            this.notifyIcon.Text = "Brightness";
        }

        private void SetUpContextMenu()
        {
            var refreshItem = new ToolStripMenuItem("Rescan monitors", null, (sender, e) => SetUpForm());

            var exitItem = new ToolStripMenuItem("Exit", null, (sender, e) => Application.Exit());

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
