using BrightnessControl.Native;
using System.Runtime.InteropServices;

namespace BrightnessControl.MonitorController
{
    public class PhysicalMonitor : IPhysicalMonitor, IEquatable<PhysicalMonitor>
    {
        private uint _supportedColorTemperatures = 0;
        private uint _minBrightness = 0, _maxBrightness = 0, _currentBrightness = 0;

        private IntPtr _handle;
        private DISPLAY_DEVICE _device;

        public IntPtr Handle => _handle;
        public int DeviceNumber { get; private set; }
        public bool HasBrightnessCapability { get; private set; } = false;

        public uint Brightness
        {
            get => _currentBrightness;
            set
            {
                if (value < _minBrightness || value > _maxBrightness)
                    throw new ArgumentOutOfRangeException(nameof(value),
                        $"Brightness param is out of acceptable range for monitor {_device.DeviceName} [{_minBrightness} ~ {_maxBrightness}]");

                Calls.Attempt(Calls.SetMonitorBrightness(Handle, value));
            }
        }

        public bool Equals(PhysicalMonitor? other)
        {
            if (other == null) return false;
            return Handle == other.Handle;
        }

        public PhysicalMonitor(IntPtr handle, int deviceNumber)
        {
            _handle = handle;
            DeviceNumber = deviceNumber;
            _device = new DISPLAY_DEVICE();
            _device.cb = Marshal.SizeOf(_device);

            // get device information
            Calls.Attempt(Calls.EnumDisplayDevices(_device.DeviceName, DeviceNumber, ref _device, 0));

            HasBrightnessCapability = Calls.GetMonitorBrightness(Handle, ref _minBrightness, ref _currentBrightness, ref _maxBrightness);
        }
    }
}
