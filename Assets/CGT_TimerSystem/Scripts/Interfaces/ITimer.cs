using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
    /// <summary>
    /// Common interface for both timer types in this system
    /// </summary>
    public interface ITimer : IStartable, IResettable, IStoppable, IRestartable
    {
        TimeSpan CurrentTime { get; }
        bool IsRunning { get; }
        void Tick();

        float TimeScale { get; set; }
    }        
}