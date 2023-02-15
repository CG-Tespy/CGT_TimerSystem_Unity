using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System.Diagnostics;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{ 
    public class TimerManager : MonoBehaviour
    {
        protected IDictionary<int, Countdown> countdowns = new Dictionary<int, Countdown>();
        protected IDictionary<int, Stopwatch> stopwatches = new Dictionary<int, Stopwatch>();

        protected virtual void Awake()
        {
            SetUpInitialTimers();
        }

        protected virtual void SetUpInitialTimers()
        {
            for (int i = 0; i < startingTimerPerTypeCount; i++)
            {
                stopwatches.Add(i, new Stopwatch());
                countdowns.Add(i, new Countdown());
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
            bool itExists = countdowns[recorderNum] != null;
            if (!itExists)
            {
                // We want the amounts of Stopwatches and Countdowns always
                // be the same
                countdowns.Add(recorderNum, new Countdown());
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
        /// <param name="timerNumber"></param>
        public virtual void StartCountdown(int timerNumber)
        {
            Countdown inQuestion = GetCountdown(timerNumber);
            bool alreadyStarted = inQuestion.IsRunning;
            if (alreadyStarted)
                return;

            inQuestion.StartUp();
        }

        protected static int millisecondsPerSecond = 1000;

        protected virtual Countdown GetCountdown(int timerNumber)
        {
            EnsureTimersExistFor(timerNumber);
            return countdowns[timerNumber];
        }

        public virtual void SetCountdownFor(int number, TimeSpan duration)
        {
            Countdown inQuestion = GetCountdown(number);
            inQuestion.SetFor(duration);
        }

        public virtual void StopCountdown(int timerNum)
        {
            Countdown inQuestion = GetCountdown(timerNum);
            inQuestion.Stop();
        }

        public virtual TimeSpan CountdownTimeLeft(int timerNum)
        {
            Countdown inQuestion = GetCountdown(timerNum);
            return inQuestion.CurrentTime;
        }

        public virtual void ListenForCountdownStart(int timerNum)
        {

        }
        public delegate void TimerEventHandler(int timerNumber);
        public virtual void ListenForCountdownEnd(int timerNum, ElapsedEventHandler response)
        {
            Countdown inQuestion = GetCountdown(timerNum);
            //inQuestion.Elapsed += response;
        }

        public virtual void UnlistenForCountdownEnd(int timerNum, ElapsedEventHandler eventHandler)
        {
            Countdown inQuestion = GetCountdown(timerNum);
            //inQuestion.Elapsed -= eventHandler;
        }

        public virtual void ListenForStopwatch(ElapsedEventArgs thing)
        {
            
        }

        /// <summary>
        /// How many Countdowns are currently being kept track of by this manager
        /// </summary>
        public virtual int CountdownCount {  get { return countdowns.Count; } }

        /// <summary>
        /// How many Stopwatches are currently being kept track of by this manager
        /// </summary>
        public virtual int StopwatchCount { get { return stopwatches.Count; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The current time of the Countdown assigned to the passed number.</returns>
        public virtual TimeSpan GetCountdownCurrentTime(int timerNum)
        {
            Countdown inQuestion = GetCountdown(timerNum);
            return inQuestion.CurrentTime;
        }

        public virtual System.Action<TimerEventArgs> GetCountdownFinishEvent(int timerNumber)
        {
            Countdown inQuestion = GetCountdown(timerNumber);
            return inQuestion.OnFinish;
        }

        public virtual void ResetCountdown(int timerNumber)
        {
            Countdown inQuestion = GetCountdown(timerNumber);
            inQuestion.Reset();
        }
    }
}