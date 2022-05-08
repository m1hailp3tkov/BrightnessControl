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
        protected List<IntPtr> _monitorHandles = new();
        protected List<IntPtr> _physicalMonitorHandles = new();

        public Win32MonitorController()
        {
            InitializeHandles();
        }

        protected virtual void InitializeHandles()
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
            //add monitor handle to list
            _monitorHandles.Add(hMonitor);

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

        private short GetPhysicalMonitorBrightness(IntPtr hPhysicalMonitor, bool retry = true)
        {
            short minimum = 0, current = 0, maximum = 0;

            bool getMonitorBrightnessSuccessful = Calls.GetMonitorBrightness(hPhysicalMonitor, ref minimum, ref current, ref maximum);

            //throw if failed
            if (!getMonitorBrightnessSuccessful) throw new Win32Exception($"Could not get brightness for physical monitor with handle {hPhysicalMonitor}");

            return current;
        }

        #region IMonitorController methods
        public short GetBrightness()
        {
            List<short> brightnessLevels = new();

            for (int i = 0; i < _physicalMonitorHandles.Count;)
            {
                try
                {
                    short brightness = GetPhysicalMonitorBrightness(_physicalMonitorHandles[i++]);
                    brightnessLevels.Add(brightness);
                }
                catch (Win32Exception exception)
                {
                    //reinitialize and retry (reset i)
                    brightnessLevels = new();
                    InitializeHandles();
                    i = 0;
                }
            }

            //TODO: return list
            return brightnessLevels.First();
        }

        public void SetBrightness(short value)
        {
            _physicalMonitorHandles.ForEach(x =>
            {
                bool setMonitorBrightnessSuccessful = Calls.SetMonitorBrightness(x, value);
                if (!setMonitorBrightnessSuccessful) throw new Win32Exception($"Failed to set brightness on monitor with handle {x}");
            });
        }
        #endregion
    }
}
