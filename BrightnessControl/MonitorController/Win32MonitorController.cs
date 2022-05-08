using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BrightnessControl.Native;

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
            bool enumerationSuccessful = Calls.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, Callback, 0);

            //throw exception if failed
            if (!enumerationSuccessful) throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        protected virtual bool Callback(IntPtr hMonitor, IntPtr hDC, ref Structures.Rect pRect, int dwData)
        {
            uint numberOfMonitors = 0;
            Structures.PHYSICAL_MONITOR[] physicalMonitors;

            //get number of physical monitors
            bool getNumberOfMonitorsSuccessful = Calls.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref numberOfMonitors);
            if (!getNumberOfMonitorsSuccessful) throw new Win32Exception(Marshal.GetLastWin32Error());

            //initialize physical monitor array with specified size
            physicalMonitors = new Structures.PHYSICAL_MONITOR[numberOfMonitors];

            //get physical monitors
            bool getPhysicalMonitorsSuccessful = Calls.GetPhysicalMonitorsFromHMONITOR(hMonitor, numberOfMonitors, physicalMonitors);
            if (!getNumberOfMonitorsSuccessful) throw new Win32Exception(Marshal.GetLastWin32Error());

            //add to monitor list
            _monitors.AddRange(physicalMonitors.Select(x => new PhysicalMonitor(x.hPhysicalMonitor)));

            //Continue enumeration
            return true;
        }
    }
}
