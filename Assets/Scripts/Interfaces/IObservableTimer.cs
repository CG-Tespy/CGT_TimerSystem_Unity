using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
    public delegate void OnTimerEvent(TimerEventArgs eventArgs);

    public interface IObservableTimer : ITimer
    {
        event OnTimerEvent OnStart;
        event OnTimerEvent OnStop;
        event OnTimerEvent OnReset;
        event OnTimerEvent OnRestart;
    }
}