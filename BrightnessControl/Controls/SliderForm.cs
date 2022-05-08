using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrightnessControl
{
    public partial class SliderForm : Form
    {
        private IMonitorController monitorController;

        private int brightness;

        public SliderForm(int brightness, IMonitorController monitorController)
        {
            this.monitorController = monitorController;
            this.brightness = brightness;

            // set start position
            this.StartPosition = FormStartPosition.Manual;
            // TODO: CONSTANTS
            this.Location = new Point(1605, 1005);

            InitializeComponent();

            // add right click exit to notifyicon
            // TODO: move to design?
            var exitItem = new ToolStripMenuItem("Exit", null, (sender, e) => Application.Exit());
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add(exitItem);

            //initialize trackbar and label values
            this.trackBar.Value = this.brightness;
            this.label.Text = brightness.ToString();

            //set focus to trackbar
            this.trackBar.Focus();
        }

        // specific events
        private void UpdateBrightness(bool frontEnd = true)
        {
            this.brightness = trackBar.Value;

            if (frontEnd)
            {
                this.label.Text = this.trackBar.Value.ToString();
                return;
            }
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                monitorController.GetBrightness();
                monitorController.SetBrightness((short)brightness);

            }).Start();
        }

        private void ActivateForm()
        {
            //show and focus window
            //this.visible = true;
            this.Show();
            this.Activate();
            this.WindowState = FormWindowState.Normal;
            this.trackBar.Focus();
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


        // update events
        private void trackBar_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateBrightness(false);
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateBrightness(false);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            UpdateBrightness();
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

        private void SliderForm_Deactivate(object sender, EventArgs e)
        {
            this.DeactivateForm();
        }


        // show events
        private void SliderForm_Activated(object sender, EventArgs e)
        {
            this.ActivateForm();
        }

        // notifyicon events
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) this.ActivateForm();
        }
    }
}
