using System;
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
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            this.label.Text = trackBar.Value.ToString();
            try
            {
                _monitor.Brightness = (short)trackBar.Value;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
