using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;
using TimeSpan = System.TimeSpan;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace CGT.Unity.TimerSys
{
    public class TimerEventArgs
    {
        public ITimer Timer { get; set; }
    }

    public class CountdownEventArgs
    {
        public virtual Countdown Countdown { get; set; }
        public DateTime WhenLastFinished
        {
            get { return Countdown.WhenLastFinished; }
        }

        public class StopwatchEventArgs
        {
            protected Stopwatch stopwatch;
        }
    }
}