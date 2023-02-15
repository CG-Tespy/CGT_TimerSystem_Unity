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
        public virtual uint CountdownCount { get { return countdownManager.TimerCount; } }
        public virtual uint StopwatchCount { get { return stopwatchManager.TimerCount; } }

        protected CountdownManager countdownManager = new CountdownManager();
        protected StopwatchManager stopwatchManager = new StopwatchManager();


        /// <summary>
        /// Starts the Stopwatch with the passed ID
        /// </summary>
        /// <param name="id"></param>
        public virtual void StartStopwatch(uint id)
        {
            stopwatchManager.StartTimer(id);
        }

        public virtual void StopStopwatch(uint id)
        {
            stopwatchManager.StopTimer(id);
        }

        /// <summary>
        /// Starts the Countdown with the passed ID. Does nothing if it's already
        /// running.
        /// </summary>
        /// <param name="id"></param>
        public virtual void StartCountdown(uint id)
        {
            countdownManager.StartTimer(id);
        }

        protected static int millisecondsPerSecond = 1000;

        public virtual void SetCountdownFor(uint id, TimeSpan duration)
        {
            countdownManager.SetCountdownFor(id, duration);
        }

        public virtual void StopCountdown(uint id)
        {
            countdownManager.StopTimer(id);
        }

        public virtual TimeSpan CountdownTimeLeft(uint id)
        {
            return countdownManager.GetCountdownTimeLeft(id);
        }

        public delegate void TimerEventHandler(int timerNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The current time of the Countdown assigned to the passed number.</returns>
        public virtual TimeSpan GetCountdownCurrentTime(uint id)
        {
            return countdownManager.GetCountdownTimeLeft(id);
        }

        public virtual System.Action<TimerEventArgs> GetCountdownFinishEvent(uint id)
        {
            return countdownManager.GetCountdownFinishEvent(id);
        }

        public virtual void ResetCountdown(uint id)
        {
            countdownManager.ResetTimer(id);
        }
    }
}