using BrightnessControl.Controls;
using BrightnessControl.Native;
using System.ComponentModel;
using BrightnessControl.Helpers;

namespace BrightnessControl
{
    public partial class SliderForm : Form
    {
        private IMonitorController monitorController;

        private Point GetLocation()
        {
            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            return new Point(x, y);
        }

        public SliderForm(IMonitorController monitorController)
        {
            this.monitorController = monitorController;

            InitializeComponent();

            this.Height = monitorController.Monitors.Count == 0
                ? ApplicationConstants.TRACKBAR_CONTAINER_HEIGHT
                : ApplicationConstants.TRACKBAR_CONTAINER_HEIGHT * monitorController.Monitors.Count;
            this.Width = ApplicationConstants.WINDOW_WIDTH;

            this.Location = GetLocation();

            foreach (var monitor in monitorController.Monitors)
            {
                this.flowLayoutPanel
                    .Controls.Add(new BrightnessBlock(monitor));
            }

            // add right click exit to notifyicon
            // TODO: move to design?
            var exitItem = new ToolStripMenuItem("Exit", null, (sender, e) => Application.Exit());
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add(exitItem);
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
    }
}
