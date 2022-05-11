using BrightnessControl.Native;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static BrightnessControl.Native.Calls;
using static BrightnessControl.Native.Constants;
using static BrightnessControl.Native.Flags;
using static BrightnessControl.Native.Structures;

namespace BrightnessControl.MonitorController
{
    public class PhysicalMonitor : IPhysicalMonitor, IEquatable<PhysicalMonitor>
    {
        private uint _capabilitiesFlags = 0;
        private uint _supportedColorTemperatures = 0;
        private short _minBrightness = 0, _maxBrightness = 0, _currentBrightness = 0;

        private IntPtr _handle;
        private DISPLAY_DEVICE _device;

        public IntPtr Handle => _handle;
        public int DeviceNumber { get; private set; }
        public bool HasBrightnessCapability { get; private set; }

        public short Brightness
        {
            get => _currentBrightness;
            set
            {
                if (value < _minBrightness || value > _maxBrightness)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Brightness param is out of acceptable range for monitor [{_minBrightness} ~ {_maxBrightness}]");

                Attempt(SetMonitorBrightness(Handle, value));
            }
        }

        public bool Equals(PhysicalMonitor? other)
        {
            if (other == null) return false;
            return this.Handle == other.Handle;
        }

        public PhysicalMonitor(IntPtr handle, int deviceNumber)
        {
            this._handle = handle;
            this.DeviceNumber = deviceNumber;
            this._device = new DISPLAY_DEVICE();
            _device.cb = Marshal.SizeOf(_device);

            // get device information
            Attempt(EnumDisplayDevices(_device.DeviceName, DeviceNumber, ref _device, 0));

            Attempt(GetMonitorCapabilities(handle, ref _capabilitiesFlags, ref _supportedColorTemperatures));

            // check if the monitor has the capability to modify brightness
            HasBrightnessCapability = ((MonitorCapabilities)_capabilitiesFlags).HasFlag(MonitorCapabilities.MC_CAPS_BRIGHTNESS);

            if (HasBrightnessCapability) Attempt(GetMonitorBrightness(Handle, ref _minBrightness, ref _currentBrightness, ref _maxBrightness));
        }
    }
}
