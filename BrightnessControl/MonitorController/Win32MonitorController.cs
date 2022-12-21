using BrightnessControl.Native;

namespace BrightnessControl.MonitorController
{
    public class Win32MonitorController : IMonitorController
    {
        protected List<IPhysicalMonitor> _monitors { get; set; }

        public IReadOnlyList<IPhysicalMonitor> Monitors => _monitors.AsReadOnly();

        public Win32MonitorController()
        {
            _monitors = new List<IPhysicalMonitor>();
            Initialize();
        }

        public void Initialize()
        {
            _monitors.Clear();

            //Enumerate all monitors
            Calls.Attempt(Calls.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, Callback, 0));

            _monitors = _monitors.Distinct()
                .Reverse()
                .ToList();
        }

        protected virtual bool Callback(IntPtr hMonitor, IntPtr hDC, ref RECT pRect, int dwData)
        {
            uint numberOfMonitors = 0;
            PHYSICAL_MONITOR[] physicalMonitors;

            //get number of physical monitors
            Calls.Attempt(Calls.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref numberOfMonitors));

            //initialize physical monitor array with specified size
            physicalMonitors = new PHYSICAL_MONITOR[numberOfMonitors];

            //get physical monitors
            Calls.Attempt(Calls.GetPhysicalMonitorsFromHMONITOR(hMonitor, numberOfMonitors, physicalMonitors));

            foreach(var physicalMonitor in physicalMonitors)
            {
                // create physical monitor object
                var monitor = new PhysicalMonitor(physicalMonitor.hPhysicalMonitor, _monitors.Count);

                // add to list only if supports brightness
                if(monitor.HasBrightnessCapability) _monitors.Add(monitor);
            }
            

            //Continue enumeration
            return true;
        }
    }
}
