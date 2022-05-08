using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using BrightnessControl.Native;

namespace BrightnessControl
{
    class NativeMonitorController : IMonitorController
    {
        public static List<IntPtr> monitors = new List<IntPtr>();

        public NativeMonitorController()
        {
            SetupMonitors();
        }

        public static uint monCount = 0;

        public void SetupMonitors()
        {
            monitors.Clear();

            Calls.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, Callback, 0);
        }
        private static bool Callback(IntPtr hMonitor, IntPtr hDC, ref Native.Structures.Rect prect, int d)
        {
            //monitors.Add(hMonitor);
            int lastWin32Error;
            uint pdwNumberOfPhysicalMonitors = 0;
            bool numberOfPhysicalMonitorsFromHmonitor = Calls.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref pdwNumberOfPhysicalMonitors);
            lastWin32Error = Marshal.GetLastWin32Error();

            var pPhysicalMonitorArray = new Native.Structures.PHYSICAL_MONITOR[pdwNumberOfPhysicalMonitors];
            bool physicalMonitorsFromHmonitor = Calls.GetPhysicalMonitorsFromHMONITOR(hMonitor, pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);
            lastWin32Error = Marshal.GetLastWin32Error();

            monitors.Add(pPhysicalMonitorArray[0].hPhysicalMonitor);

            Debug.WriteLine($"Handle: 0x{hMonitor:X}, Num: {pdwNumberOfPhysicalMonitors}, Physical: {pPhysicalMonitorArray[0].hPhysicalMonitor}");

            GetMonitorCapabilities((int)monCount);
            return ++monCount > 0;
        }

        private static void GetMonitorCapabilities(int monitorNumber)
        {
            uint pdwMonitorCapabilities = 0u;
            uint pdwSupportedColorTemperatures = 0u;
            var monitorCapabilities = Calls.GetMonitorCapabilities(monitors[monitorNumber], ref pdwMonitorCapabilities, ref pdwSupportedColorTemperatures);
            Debug.WriteLine(pdwMonitorCapabilities);
            Debug.WriteLine(pdwSupportedColorTemperatures);
            int lastWin32Error = Marshal.GetLastWin32Error();
            Native.Structures.MC_DISPLAY_TECHNOLOGY_TYPE type = Native.Structures.MC_DISPLAY_TECHNOLOGY_TYPE.MC_SHADOW_MASK_CATHODE_RAY_TUBE;
            var monitorTechnologyType = Calls.GetMonitorTechnologyType(monitors[monitorNumber], ref type);
            Debug.WriteLine(type);
            lastWin32Error = Marshal.GetLastWin32Error();
        }

        public bool SetBrightness(short brightness, int monitorNumber)
        {
            var brightnessWasSet = Calls.SetMonitorBrightness(monitors[monitorNumber], brightness);
            if (brightnessWasSet)
                Debug.WriteLine("Brightness set to " + (short)brightness);
            int lastWin32Error = Marshal.GetLastWin32Error();
            return brightnessWasSet;
        }

        //public BrightnessInfo GetBrightnessCapabilities(int monitorNumber)
        //{
        //    short current = -1, minimum = -1, maximum = -1;
        //    bool getBrightness = NativeCalls.GetMonitorBrightness(monitors[monitorNumber], ref minimum, ref current, ref maximum);
        //    int lastWin32Error = Marshal.GetLastWin32Error();
        //    return new BrightnessInfo { minimum = minimum, maximum = maximum, current = current };
        //}

        //public void DestroyMonitors(uint pdwNumberOfPhysicalMonitors, NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray)
        //{
        //    var destroyPhysicalMonitors = NativeCalls.DestroyPhysicalMonitors(pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);
        //    int lastWin32Error = Marshal.GetLastWin32Error();
        //}

        public uint GetMonitors()
        {
            return monCount;
        }

        public short GetBrightness()
        {
            short current = -1, minimum = -1, maximum = -1;
            bool getBrightness = Calls.GetMonitorBrightness(monitors.First(), ref minimum, ref current, ref maximum);
            Debug.WriteLine(monitors.First());
            int lastWin32Error = Marshal.GetLastWin32Error();

            return current;
        }

        public void SetBrightness(short value)
        {
            var brightnessWasSet = Calls.SetMonitorBrightness(monitors.First(), (short)value);

            if (brightnessWasSet) Debug.WriteLine("Brightness set to " + value);
            else
            {
                SetupMonitors();
                SetBrightness((short)value);
            }
            int lastWin32Error = Marshal.GetLastWin32Error();

            Debug.WriteLine(lastWin32Error);
        }
    }
}
