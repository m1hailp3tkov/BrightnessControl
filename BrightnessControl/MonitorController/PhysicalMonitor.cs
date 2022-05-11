using BrightnessControl.Native;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BrightnessControl.MonitorController
{
    public class PhysicalMonitor : IPhysicalMonitor, IEquatable<PhysicalMonitor>
    {
        private uint _capabilitiesFlags = 0;
        private uint _supportedColorTemperatures = 0;
        private short _minBrightness = 0, _maxBrightness = 0, _currentBrightness = 0;

        private IntPtr _handle;
        private Structures.DISPLAY_DEVICE _device;

        public int DeviceNumber { get; private set; }

        public short Brightness
        {
            get => _currentBrightness;
            set => SetBrightness(value);
        }

        public PhysicalMonitor(IntPtr handle, int monitorNumber)
        {
            this._handle = handle;
            this.DeviceNumber = monitorNumber;
            this._device = new Structures.DISPLAY_DEVICE();
            _device.cb = Marshal.SizeOf(_device);

            Calls.EnumDisplayDevices(_device.DeviceName, DeviceNumber, ref _device, 0);

            bool getMonitorCapabilitiesSuccess = Calls.GetMonitorCapabilities(handle, ref _capabilitiesFlags, ref _supportedColorTemperatures);
            if (!getMonitorCapabilitiesSuccess) throw new Win32Exception(Marshal.GetLastWin32Error());

            this.GetBrightness();
        }


        public IntPtr Handle { get => _handle; }

        public void GetBrightness()
        {
            bool getMonitorBrightnessSuccessful = Calls.GetMonitorBrightness(Handle, ref _minBrightness, ref _currentBrightness, ref _maxBrightness);

            //throw if failed
            if (!getMonitorBrightnessSuccessful) throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public void SetBrightness(short value)
        {
            if (value < _minBrightness || value > _maxBrightness)
                throw new ArgumentOutOfRangeException(nameof(value), $"Brightness param is out of acceptable range for monitor [{_minBrightness} ~ {_maxBrightness}]");

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
