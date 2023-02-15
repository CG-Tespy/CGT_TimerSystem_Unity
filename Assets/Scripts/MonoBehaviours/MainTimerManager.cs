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
        protected IDictionary<int, Stopwatch> stopwatches = new Dictionary<int, Stopwatch>();

        protected CountdownManager countdownManager = new CountdownManager();

        protected virtual void Awake()
        {
            SetUpInitialTimers();
        }

        protected virtual void SetUpInitialTimers()
        {
            for (int i = 0; i < startingTimerPerTypeCount; i++)
            {
                stopwatches.Add(i, new Stopwatch());
            }
        }

        protected int startingTimerPerTypeCount = 10;

        /// <summary>
        /// Starts the Stopwatch with the passed ID
        /// </summary>
        /// <param name="timerNumber"></param>
        public virtual void StartStopwatch(int timerNumber)
        {
            Stopwatch inQuestion = GetStopwatch(timerNumber);
            bool alreadyStarted = inQuestion.IsRunning;
            if (alreadyStarted)
                return;

            inQuestion.StartUp();
        }

        /// <summary>
        /// Returns the Stopwatch with the passed ID
        /// </summary>
        /// <param name="timerNumber"></param>
        protected virtual Stopwatch GetStopwatch(int timerNumber)
        {
            EnsureTimersExistFor(timerNumber);
            return stopwatches[timerNumber];
        }

        protected virtual void EnsureTimersExistFor(int recorderNum)
        {
            bool itExists = stopwatches[recorderNum] != null;
            if (!itExists)
            {
                // We want the amounts of Stopwatches and Countdowns always
                // be the same
                stopwatches.Add(recorderNum, new Stopwatch());
            }
        }

        public virtual void StopStopwatch(int timerNum)
        {
            Stopwatch inQuestion = GetStopwatch(timerNum);
            inQuestion.Stop();
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


        public virtual void ListenForStopwatch(ElapsedEventArgs thing)
        {
            
        }



        /// <summary>
        /// How many Stopwatches are currently being kept track of by this manager
        /// </summary>
        public virtual int StopwatchCount { get { return stopwatches.Count; } }

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