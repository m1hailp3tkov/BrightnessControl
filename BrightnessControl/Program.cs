using BrightnessControl.MonitorController;

namespace BrightnessControl
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IMonitorController monitorController = new Win32MonitorController();
            ApplicationConfiguration.Initialize();

            //TODO: multiple monitors
            SliderForm sliderForm = new SliderForm(monitorController.Monitors.First().GetBrightness(), monitorController);

            Application.Run();
        }
    }
}