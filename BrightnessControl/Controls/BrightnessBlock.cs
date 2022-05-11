﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrightnessControl.Controls
{
    public partial class BrightnessBlock : UserControl
    {
        private IPhysicalMonitor _monitor;

        public BrightnessBlock(IPhysicalMonitor monitor)
        {
            _monitor = monitor;

            InitializeComponent();

            this.label.Text = monitor.Brightness.ToString();
            this.trackBar.Value = (int)monitor.Brightness;
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            this.label.Text = trackBar.Value.ToString();
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                _monitor.Brightness = (uint)trackBar.Value;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}