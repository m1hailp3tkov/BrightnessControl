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
            SliderForm sliderForm = new SliderForm(monitorController);

            Application.Run();
        }
    }
}

//TODO: Incorporate this
/*
 * The operating system asynchronously destroyed the monitor which corresponds to 
 * this handle because the operating system's state changed. This error typically 
 * occurs because the monitor PDO associated with this handle was removed, the monitor PDO 
 * associated with this handle was stopped, or a display mode change occurred. 
 * A display mode change occurs when windows sends a WM_DISPLAYCHANGE windows message to applications.
 * */