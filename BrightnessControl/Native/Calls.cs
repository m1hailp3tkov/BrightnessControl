using System.ComponentModel;
using System.Runtime.InteropServices;
using static BrightnessControl.Native.Structures;

namespace BrightnessControl.Native
{
    public static class Calls
    {
        #region user32.dll
        [DllImport("user32.dll", EntryPoint = "MonitorFromWindow", SetLastError = true)]
        public static extern IntPtr MonitorFromWindow([In] IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll", EntryPoint = "EnumDisplayMonitors", SetLastError = true)]
        public static extern bool EnumDisplayMonitors(IntPtr hDC, IntPtr lpRect, EnumDisplayMonitorsCallback callback, int dwData);

        [DllImport("User32.dll", EntryPoint = "EnumDisplayDevices", SetLastError = true)]
        public static extern bool EnumDisplayDevices(string lpDevice, int iDevNum, ref Structures.DISPLAY_DEVICE lpDisplayDevice, int dwFlags);

        [DllImport("User32.dll", EntryPoint = "GetMonitorInfo", SetLastError = true)]
        public static extern bool GetMonitorInfo(IntPtr hmonitor, [In, Out] ref Structures.MONITORINFOEX info);

        [DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        #endregion



        #region dxva2.dll
        [DllImport("dxva2.dll", EntryPoint = "GetNumberOfPhysicalMonitorsFromHMONITOR", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, ref uint pdwNumberOfPhysicalMonitors);


        [DllImport("dxva2.dll", EntryPoint = "GetPhysicalMonitorsFromHMONITOR", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, uint dwPhysicalMonitorArraySize, [Out] Structures.PHYSICAL_MONITOR[] pPhysicalMonitorArray);


        [DllImport("dxva2.dll", EntryPoint = "DestroyPhysicalMonitors", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyPhysicalMonitors(uint dwPhysicalMonitorArraySize, [Out] Native.Structures.PHYSICAL_MONITOR[] pPhysicalMonitorArray);


        [DllImport("dxva2.dll", EntryPoint = "GetMonitorTechnologyType", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorTechnologyType(IntPtr hMonitor, ref Flags.MC_DISPLAY_TECHNOLOGY_TYPE pdtyDisplayTechnologyType);


        [DllImport("dxva2.dll", EntryPoint = "GetMonitorCapabilities", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorCapabilities(IntPtr hMonitor, ref uint pdwMonitorCapabilities, ref uint pdwSupportedColorTemperatures);


        [DllImport("dxva2.dll", EntryPoint = "SetMonitorBrightness", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetMonitorBrightness(IntPtr hMonitor, uint brightness);


        [DllImport("dxva2.dll", EntryPoint = "GetMonitorBrightness", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorBrightness(IntPtr hMonitor, ref uint pdwMinimumBrightness, ref uint pdwCurrentBrightness, ref uint pdwMaximumBrightness);
        #endregion

        #region Delegates
        public delegate bool EnumDisplayMonitorsCallback(IntPtr hMonitor, IntPtr hDC, ref Structures.RECT pRect, int dwData);

        public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Structures.RECT lprcMonitor, IntPtr dwData);
        #endregion

        #region Helpers
        public static void Attempt(bool methodCallResult)
        {
            if (!methodCallResult) throw new Win32Exception(Marshal.GetLastWin32Error());
            return;
        }
        #endregion
    }
}
