using BrightnessControl.Native;

namespace BrightnessControl
{
    public class TrackBarWithoutFocus : TrackBar
    {
        private static int MakeParam(int loWord, int hiWord)
        {
            return (hiWord << 16) | (loWord & 0xffff);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Calls.SendMessage(this.Handle, 0x0128, MakeParam(1, 0x1), 0);
        }
    }
}
