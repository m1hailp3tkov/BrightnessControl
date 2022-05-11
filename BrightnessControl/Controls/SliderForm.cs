using BrightnessControl.Controls;
using BrightnessControl.Native;
using System.ComponentModel;

namespace BrightnessControl
{
    public partial class SliderForm : Form
    {
        private const int TRACKBAR_CONTAINER_HEIGHT = 45;
        private const int WINDOW_WIDTH = 320;

        private IMonitorController monitorController;
        private Screen screen;

        public Point GetLocation()
        {
            return new Point(100, 100);
        }

        public void OnLoad(int monitorCount)
        {
            this.Height = monitorCount == 0 ? TRACKBAR_CONTAINER_HEIGHT : TRACKBAR_CONTAINER_HEIGHT * monitorCount;
            this.Width = WINDOW_WIDTH;
        }

        public SliderForm(IMonitorController monitorController)
        {
            this.monitorController = monitorController;
            
            InitializeComponent();
            OnLoad(monitorController.Monitors.Count);

            foreach(var monitor in monitorController.Monitors)
            {
                this.flowLayoutPanel.Controls.Add(new BrightnessBlock(monitor));
            }

            // add right click exit to notifyicon
            // TODO: move to design?
            var exitItem = new ToolStripMenuItem("Exit", null, (sender, e) => Application.Exit());
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add(exitItem);
        }

        private void ActivateForm()
        {
            //show and focus window
            //this.visible = true;
            this.Show();
            this.Activate();
            this.WindowState = FormWindowState.Normal;
            //this.trackBar.Focus();
            this.BringToFront();
        }

        private void DeactivateForm()
        {
            //this.visible = false;
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
                this.DeactivateForm();
            }
        }

        // notifyicon events
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) this.ActivateForm();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
