using BrightnessControl.Native;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BrightnessControl.MonitorController
{
    public class PhysicalMonitor : IPhysicalMonitor, IEquatable<PhysicalMonitor>
    {
        private IntPtr _handle;

        public short Brightness { get; set; }

        public PhysicalMonitor(IntPtr handle)
        {
            this._handle = handle;
            this.Brightness = GetBrightness();
        }
        public IntPtr Handle { get => _handle; }

        public short GetBrightness()
        {
            short minimum = 0, current = 0, maximum = 0;

            bool getMonitorBrightnessSuccessful = Calls.GetMonitorBrightness(Handle, ref minimum, ref current, ref maximum);

            //throw if failed
            if (!getMonitorBrightnessSuccessful) throw new Win32Exception(Marshal.GetLastWin32Error());

            return current;
        }

        public void SetBrightness(short value)
        {
            bool setMonitorBrightnessSuccessful = Calls.SetMonitorBrightness(Handle, value);
            if (!setMonitorBrightnessSuccessful) throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public bool Equals(PhysicalMonitor? other)
        {
            if (other == null) return false;
            return this.Handle == other.Handle;
        }
    }
}
