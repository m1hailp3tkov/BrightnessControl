using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightnessControl
{
    public class CMMController : IMonitorController
    {
        private const string CMM_PATH = "cmm";
        private const string CMM_GET_BRIGHTNESS_ARG = "/GetValue Primary 10";
        private const string CMM_SET_BRIGHTNESS_ARG = "/SetValue Primary 10";

        private short Execute(string args)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = CMM_PATH,
                    Arguments = args
                };

                // Call WaitForExit and then the using statement will close.
                using (Process cmm = Process.Start(processStartInfo))
                {
                    cmm.WaitForExit();
                    // Read/Get function returns are captured via %errorlevel% (ExitCode)
                    // * Version 1.16 -- CustomizeMyMonitor readme
                    return (short)cmm.ExitCode;
                }
            }
            catch (Exception ex)
            {
                // Show exception message
                MessageBox.Show(ex.Message);
                
                return -1;
            }
        }

        public short GetBrightness()
        {
            return Execute(CMM_GET_BRIGHTNESS_ARG);
        }

        public void SetBrightness(short value)
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException("Brightness value is out of range (0 - 100)");
            }

            Execute($"{CMM_SET_BRIGHTNESS_ARG} {value}");
        }
    }
}
