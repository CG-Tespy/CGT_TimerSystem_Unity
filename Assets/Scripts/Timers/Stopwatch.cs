using System;

namespace CGT.Unity.TimerSys
{
    public class Stopwatch : ObservableTimer, IObservableStopwatch
    {
        public Stopwatch()
        {
            timeToResetTo = 0; // Since stopwatches always reset to 0, unlike Countdowns
        }

        protected override void ApplyTimeElapsed(double hoursElapsed)
        {
            currentTime += hoursElapsed;
        }
    }
}