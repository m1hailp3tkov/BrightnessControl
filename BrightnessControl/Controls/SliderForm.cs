using BrightnessControl.Controls;
using BrightnessControl.Native;
using BrightnessControl.Helpers;
using static BrightnessControl.Native.Structures;
using static BrightnessControl.Native.Calls;
using System.Runtime.InteropServices;

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
            GetWindowRect(GetDesktopWindow(), ref scBounds);

            //int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            //int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;

            int x = scBounds.right - this.Width;
            int y = scBounds.bottom - this.Height - 30;

            return new Point(x, y);
        }

        private void SetUpForm(bool hide = true)
        {
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
        }

        private void SetUpContextMenu()
        {
            var refreshItem = new ToolStripMenuItem("Refresh", null, (sender, e) => {
                Icon icon = notifyIcon.Icon;
                this.notifyIcon.Icon = Properties.Resources.IconRefresh;
                this.notifyIcon.Text = "Refreshing...";
                SetUpForm(hide: false);
                this.notifyIcon.Icon = icon;
                this.notifyIcon.Text = "Brightness";
            });
            var exitItem = new ToolStripMenuItem("Exit", null, (sender, e) => Application.Exit());

            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add(exitItem);
            notifyIcon.ContextMenuStrip.Items.Add(refreshItem);
        }

        private void ActivateForm()
        {
            this.Show();
            this.Activate();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void DeactivateForm(object sender)
        {
            this.Visible = false;
            this.Hide();
        }

        private void SliderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // hide events
        private void trackBar_KeyDown(object sender, KeyEventArgs e)
        {
            // enter to confirm -> deactivate
            if (e.KeyCode == Keys.Enter)
            {
                this.DeactivateForm(sender);
            }
        }

        // notifyicon events
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //TODO: Intercept lost focus event somehow
            if (e.Button != MouseButtons.Left) return;

            if (Visible) { DeactivateForm(sender); return; }

            if (!Visible) ActivateForm();
        }

        private void SliderForm_LostFocus(object sender, EventArgs e)
        {
            this.DeactivateForm(sender);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if(m.Msg == Constants.WM_DISPLAYCHANGE)
            {
                SetUpForm();
            }
        }
    }
}
