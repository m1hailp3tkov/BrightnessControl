using System.Runtime.InteropServices;

namespace BrightnessControl.Native
{
    public class Win32Exception : Exception
    {

        public Win32Exception()
            : base(Marshal.GetLastWin32Error().ToString()) { }
        public Win32Exception(string? message)
            : base($"{Marshal.GetLastWin32Error()}: {message}") { }
    }
}
