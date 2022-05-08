using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightnessControl
{
    public interface IMonitorController
    {
        short GetBrightness();

        void SetBrightness(short value);
    }
}
