using System.Runtime.InteropServices;

namespace BrightnessControl.Native
{
    public class Win32Exception : Exception
    {
        public Win32Exception()
            : base() { }
        public Win32Exception(string? message)
            : base(message) { }
        public int Win32ErrorCode { get; private set; } = Marshal.GetLastWin32Error();
    }
}
