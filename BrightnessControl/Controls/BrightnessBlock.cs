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
            TrySetBrightness();
        }

        private void BrightnessBlock_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                this.Parent.Parent.Hide();
                return;
            }

            TrySetBrightness();
        }

        private void TrySetBrightness()
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
