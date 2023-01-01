using System.Collections;
using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
    public interface ICountdown : IObservableTimer
    {
        TimeSpan LastSetFor { get; } // For resetting
        System.Action<TimerEventArgs> OnFinish { get; set; }
        // ^Not all timer types really finish in the usual sense, hence why
        // this isn't in IBaseTimerEventArgs
    }
}