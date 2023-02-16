using TimeSpan = System.TimeSpan;
using System;

namespace CGT.Unity.TimerSys
{
    /// <summary>
    /// A Timer that counts down from a start time. 
    /// </summary>
    public class Countdown : ObservableTimer, ICountdown
    {
        public TimeSpan TimeLeft { get { return CurrentTime; } }

        public event OnTimerEvent OnFinish = delegate { };

        public TimeSpan LastSetFor { get { return TimeSpan.FromHours(timeToResetTo); } }

        public void SetFor(TimeSpan duration)
        {
            timeToResetTo = currentTime = duration.TotalHours;
        }

        protected override void ApplyTimeElapsed(double hoursElapsed)
        {
            currentTime -= hoursElapsed;

            bool HitZero = currentTime <= 0;
            if (HitZero)
            {
                IsRunning = false;
                // Since Countdowns should stop themselves when they hit zero, not keep
                // going further into the negatives
                AlertListenersFor(OnFinish);
            }
        }

    }
}