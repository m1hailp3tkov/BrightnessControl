using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BrightnessControl.Native;
using static BrightnessControl.Native.Calls;
using static BrightnessControl.Native.Constants;

namespace BrightnessControl.MonitorController
{
    public class Win32MonitorController : IMonitorController
    {
        protected List<IPhysicalMonitor> _monitors { get; set; }

        public IReadOnlyList<IPhysicalMonitor> Monitors => _monitors.AsReadOnly();

        public Win32MonitorController()
        {
            Initialize();
        }

        public void Initialize()
        {
            _monitors = new List<IPhysicalMonitor>();

            //Enumerate all monitors
            Attempt(EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, Callback, 0));

            _monitors = _monitors.Distinct().ToList();
        }

        protected virtual bool Callback(IntPtr hMonitor, IntPtr hDC, ref Structures.RECT pRect, int dwData)
        {
            uint numberOfMonitors = 0;
            Structures.PHYSICAL_MONITOR[] physicalMonitors;

            //get number of physical monitors
            Attempt(GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref numberOfMonitors));

            //initialize physical monitor array with specified size
            physicalMonitors = new Structures.PHYSICAL_MONITOR[numberOfMonitors];

            //get physical monitors
            Attempt(GetPhysicalMonitorsFromHMONITOR(hMonitor, numberOfMonitors, physicalMonitors));

            //add to monitor list
            foreach(var physicalMonitor in physicalMonitors) _monitors.Add(new PhysicalMonitor(physicalMonitor.hPhysicalMonitor, _monitors.Count));

            //Continue enumeration
            return true;
        }
    }
}
