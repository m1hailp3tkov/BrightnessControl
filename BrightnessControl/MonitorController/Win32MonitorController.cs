﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightnessControl.Native;

namespace BrightnessControl.MonitorController
{
    public class Win32MonitorController : IMonitorController
    {
        protected List<IntPtr> _monitorHandles;
        protected List<IntPtr> _physicalMonitorHandles;

        public Win32MonitorController()
        {
            // initialize fields
            _monitorHandles = new();
            _physicalMonitorHandles = new();

            //Enumerate all monitors
            bool enumerationSuccessful = Calls.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, Callback, 0);

            //throw exception if failed
            if (!enumerationSuccessful) throw new Win32Exception("Failed to enumerate display monitors");
        }

        protected virtual bool Callback(IntPtr hMonitor, IntPtr hDC, ref Structures.Rect pRect, int dwData)
        {
            _monitorHandles.Add(hMonitor);

            //TODO: add physical handles here?
            uint numberOfMonitors = 0;
            Structures.PHYSICAL_MONITOR[] physicalMonitors;

            //get physical monitors
            bool getNumberOfMonitorsSuccessful = Calls.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref numberOfMonitors);
            if (!getNumberOfMonitorsSuccessful) throw new Win32Exception("Failed to get number of physical monitors");

            //initialize physical monitor array with specified size
            physicalMonitors = new Structures.PHYSICAL_MONITOR[numberOfMonitors];

            bool getPhysicalMonitorsSuccessful = Calls.GetPhysicalMonitorsFromHMONITOR(hMonitor, numberOfMonitors, physicalMonitors);
            if (!getNumberOfMonitorsSuccessful) throw new Win32Exception($"Failed to get physical monitor from handle {hMonitor}");

            //add physical monitors handles to handle list
            _physicalMonitorHandles.AddRange(physicalMonitors.Select(x => x.hPhysicalMonitor));

            //Continue enumeration
            return true;
        }

        #region debug

        private void DebugFN()
        {

            foreach (var monitorHandle in _monitorHandles)
            {
                uint numberOfMonitors = 0;
                Structures.PHYSICAL_MONITOR[] physicalMonitors;

                //get physical monitors
                Calls.GetNumberOfPhysicalMonitorsFromHMONITOR(monitorHandle, ref numberOfMonitors);
                physicalMonitors = new Structures.PHYSICAL_MONITOR[numberOfMonitors];
                //
                Calls.GetPhysicalMonitorsFromHMONITOR(monitorHandle, numberOfMonitors, physicalMonitors);

                foreach (var physicalMonitor in physicalMonitors)
                {
                    uint capabilities = 0u, colortemps = 0u;
                    Calls.GetMonitorCapabilities(physicalMonitor.hPhysicalMonitor, ref capabilities, ref colortemps);

                    //debugging
                    continue;
                }

            }
        }
        #endregion

        public short GetBrightness()
        {
            return 50;
        }

        public void SetBrightness(short value)
        {
            _physicalMonitorHandles.ForEach(x =>
            {
                Calls.SetMonitorBrightness(x, value);
            });
        }
    }
}
