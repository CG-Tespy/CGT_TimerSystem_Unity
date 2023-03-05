using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
    public interface ICountdown : ITimer
    {
        void SetFor(TimeSpan duration);
        TimeSpan TimeLeft { get; }
        TimeSpan LastSetFor { get; } // For resetting
        
    }
}