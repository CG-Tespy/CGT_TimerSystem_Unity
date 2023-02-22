using System.Collections;
using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
    public interface ICountdown : IObservableTimer
    {
        void SetFor(TimeSpan duration);
        TimeSpan TimeLeft { get; }
        TimeSpan LastSetFor { get; } // For resetting
        event OnTimerEvent OnEnd;
        // ^Not all timer types really finish in the usual sense, hence why
        // this isn't in IBaseTimerEventArgs
    }
}