namespace BrightnessControl
{
    public interface IMonitorController
    {
        IReadOnlyList<IPhysicalMonitor> Monitors { get;}

        void Initialize();
    }
}
