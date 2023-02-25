using UnityEngine;
using TimeSpan = System.TimeSpan;
using CountdownEvents = CGT.Unity.TimerSys.CountdownManager.CountdownEvents;
using TimerEvents = CGT.Unity.TimerSys.StopwatchManager.TimerEvents;

namespace CGT.Unity.TimerSys
{ 
    [AddComponentMenu("CGT TimerSys/Timer Manager")]
    public class TimerSystem : MonoBehaviour
    {
        public virtual int CountdownCount { get { return countdownManager.TimerCount; } }
        protected CountdownManager countdownManager = new CountdownManager();

        public virtual int StopwatchCount { get { return stopwatchManager.TimerCount; } }
        protected StopwatchManager stopwatchManager = new StopwatchManager();

        public virtual CountdownEvents CDEvents { get { return countdownManager.Events; } }

        public virtual TimerEvents SWEvents { get { return stopwatchManager.Events; } }

        /// <summary>
        /// Starts the Stopwatch with the passed ID
        /// </summary>
        /// <param name="key"></param>
        public virtual void StartStopwatch(TimerKey key)
        {
            stopwatchManager.StartTimer(key);
        }

        public virtual void StopStopwatch(TimerKey key)
        {
            stopwatchManager.StopTimer(key);
        }

        /// <summary>
        /// Starts the Countdown with the passed ID. Does nothing if it's already
        /// running.
        /// </summary>
        /// <param name="key"></param>
        public virtual void StartCountdown(TimerKey key)
        {
            countdownManager.StartTimer(key);
        }

        protected static int millisecondsPerSecond = 1000;

        public virtual void SetCountdownFor(TimerKey key, TimeSpan duration)
        {
            countdownManager.SetCountdownFor(key, duration);
        }

        public virtual void StopCountdown(TimerKey key)
        {
            countdownManager.StopTimer(key);
        }

        public virtual TimeSpan CountdownTimeLeft(TimerKey key)
        {
            return countdownManager.GetCountdownTimeLeft(key);
        }

        public delegate void TimerEventHandler(TimerKey timerNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The current time of the Countdown assigned to the passed number.</returns>
        public virtual TimeSpan GetCountdownCurrentTime(TimerKey key)
        {
            return countdownManager.GetCountdownTimeLeft(key);
        }

        public virtual void ResetCountdown(TimerKey key)
        {
            countdownManager.ResetTimer(key);
        }

        public virtual void RestartCountdown(TimerKey key)
        {
            countdownManager.RestartTimer(key);
        }

        public virtual TimeSpan GetCountdownTimeLastSetFor(TimerKey key)
        {
            return countdownManager.GetCountdownTimeLastSetFor(key);
        }

        public virtual TimeSpan GetStopwatchCurrentTime(TimerKey key)
        {
            return stopwatchManager.GetTimerCurrentTime(key);
        }

        public virtual void ResetStopwatch(TimerKey key)
        {
            stopwatchManager.ResetTimer(key);
        }

        public virtual void RestartStopwatch(TimerKey key)
        {
            stopwatchManager.RestartTimer(key);
        }

        protected virtual void Update()
        {
            countdownManager.TickTimers();
            stopwatchManager.TickTimers();
        }

        public virtual void SetCountdownTimeScale(TimerKey key, float newTimeScale)
        {
            countdownManager.SetTimerTimeScale(key, newTimeScale);
        }

        public virtual void SetStopwatchTimeScale(TimerKey key, float newTimeScale)
        {
            stopwatchManager.SetTimerTimeScale(key, newTimeScale);
        }

    }
}