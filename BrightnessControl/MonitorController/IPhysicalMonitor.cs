namespace BrightnessControl
{
    public interface IPhysicalMonitor
    {
        short GetBrightness();

        void SetBrightness(short value);
    }
}
