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

            SliderForm sliderForm = new SliderForm(monitorController.GetBrightness(), monitorController);

            Application.Run();
        }
    }
}