using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System.Diagnostics;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{ 
    [AddComponentMenu("CGT TimerSys/Timer Manager")]
    public class MainTimerManager : MonoBehaviour
    {
        public virtual int CountdownCount { get { return countdownManager.TimerCount; } }
        public virtual int StopwatchCount { get { return stopwatchManager.TimerCount; } }

        protected CountdownManager countdownManager = new CountdownManager();
        protected StopwatchManager stopwatchManager = new StopwatchManager();


        /// <summary>
        /// Starts the Stopwatch with the passed ID
        /// </summary>
        /// <param name="handler"></param>
        public virtual void StartStopwatch(TimerKey handler)
        {
            stopwatchManager.StartTimer(handler);
        }

        public virtual void StopStopwatch(TimerKey handler)
        {
            stopwatchManager.StopTimer(handler);
        }

        /// <summary>
        /// Starts the Countdown with the passed ID. Does nothing if it's already
        /// running.
        /// </summary>
        /// <param name="handler"></param>
        public virtual void StartCountdown(TimerKey handler)
        {
            countdownManager.StartTimer(handler);
        }

        protected static int millisecondsPerSecond = 1000;

        public virtual void SetCountdownFor(TimerKey handler, TimeSpan duration)
        {
            countdownManager.SetCountdownFor(handler, duration);
        }

        public virtual void StopCountdown(TimerKey handler)
        {
            countdownManager.StopTimer(handler);
        }

        public virtual TimeSpan CountdownTimeLeft(TimerKey handler)
        {
            return countdownManager.GetCountdownTimeLeft(handler);
        }

        public delegate void TimerEventHandler(int timerNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The current time of the Countdown assigned to the passed number.</returns>
        public virtual TimeSpan GetCountdownCurrentTime(TimerKey handler)
        {
            return countdownManager.GetCountdownTimeLeft(handler);
        }

        public virtual System.Action<TimerEventArgs> GetCountdownFinishEvent(TimerKey handler)
        {
            return countdownManager.GetCountdownFinishEvent(handler);
        }

        public virtual void ResetCountdown(TimerKey handler)
        {
            countdownManager.ResetTimer(handler);
        }
    }
}