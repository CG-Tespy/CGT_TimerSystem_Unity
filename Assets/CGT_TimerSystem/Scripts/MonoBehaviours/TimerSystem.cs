using UnityEngine;
using TimeSpan = System.TimeSpan;
using CountdownEvents = CGT.Unity.TimerSys.CountdownManager.CountdownEvents;
using TimerEvents = CGT.Unity.TimerSys.StopwatchManager.TimerEvents;

namespace CGT.Unity.TimerSys
{ 
    [AddComponentMenu("")]
    public class TimerSystem : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public virtual int CountdownCount { get { return countdownManager.TimerCount; } }
        protected CountdownManager countdownManager = new CountdownManager();

        public virtual int StopwatchCount { get { return stopwatchManager.TimerCount; } }
        protected StopwatchManager stopwatchManager = new StopwatchManager();

        public virtual CountdownEvents CDEvents { get { return countdownManager.Events; } }

        public virtual TimerEvents SWEvents { get { return stopwatchManager.Events; } }

        public virtual void RegisterStopwatch(TimerKey key)
        {
            if (stopwatchManager.HasTimerWith(key))
                return;

            if (countdownManager.HasTimerWith(key))
            {
                string errorMessage = "Cannot register Stopwatch with a key that is already tied to a Countdown.";
                Debug.LogError(errorMessage);
                return;
            }

            stopwatchManager.RegisterTimerWith(key);
        }

        public virtual void RegisterCountdown(TimerKey key)
        {
            if (countdownManager.HasTimerWith(key))
                return;

            if (stopwatchManager.HasTimerWith(key))
            {
                string errorMessage = "Cannot register Stopwatch with a key that is already tied to a Countdown.";
                Debug.LogError(errorMessage);
                return;
            }

            countdownManager.RegisterTimerWith(key);
        }

        public virtual void StartTimer(TimerKey key)
        {
            Validate(key);
            ITimerManager managerForTheJob = TimerManagerWith(key);
            managerForTheJob.StartTimer(key);
        }

        protected virtual ITimerManager TimerManagerWith(TimerKey key)
        {
            if (countdownManager.HasTimerWith(key))
                return countdownManager;
            else if (stopwatchManager.HasTimerWith(key))
                return stopwatchManager;
            else
                return null;
        }

        protected virtual void Validate(TimerKey key)
        {
            bool registered = countdownManager.HasTimerWith(key) || stopwatchManager.HasTimerWith(key);  
            if (!registered)
            {
                string errorMessage = "Cannot work with a timer key not registered with either a Stopwatch or a Countdown.";
                throw new System.ArgumentException(errorMessage);
            }
        }

        public virtual void StopTimer(TimerKey key)
        {
            Validate(key);
            ITimerManager managerForTheJob = TimerManagerWith(key);
            managerForTheJob.StopTimer(key);
        }

        public virtual void ResetTimer(TimerKey key)
        {
            Validate(key);
            ITimerManager managerForTheJob = TimerManagerWith(key);
            managerForTheJob.ResetTimer(key);
        }

        public virtual void RestartTimer(TimerKey key)
        {
            Validate(key);
            ITimerManager managerForTheJob = TimerManagerWith(key);
            managerForTheJob.RestartTimer(key);
        }

        public virtual TimeSpan GetTimerCurrentTime(TimerKey key)
        {
            Validate(key);
            ITimerManager managerForTheJob = TimerManagerWith(key);
            return managerForTheJob.GetTimerCurrentTime(key);
        }

        public virtual float GetTimerTimeScale(TimerKey key)
        {
            Validate(key);
            ITimerManager managerForTheJob = TimerManagerWith(key);
            return managerForTheJob.GetTimerTimeScale(key);
        }

        public virtual void SetTimerTimeScale(TimerKey key, float newScale)
        {
            Validate(key);
            ITimerManager managerForTheJob = TimerManagerWith(key);
            managerForTheJob.SetTimerTimeScale(key, newScale);
        }

        public virtual bool IsTimerRunning(TimerKey key)
        {
            Validate(key);
            ITimerManager managerForTheJob = TimerManagerWith(key);
            return managerForTheJob.IsTimerRunning(key);
        }

        protected static int millisecondsPerSecond = 1000;

        public virtual void SetCountdownFor(TimerKey key, TimeSpan duration)
        {
            ValidateCountdown(key);
            countdownManager.SetCountdownFor(key, duration);
        }

        protected virtual void ValidateCountdown(TimerKey key)
        {
            bool isValid = countdownManager.HasTimerWith(key);
            if (!isValid)
            {
                string errorMessage = "Can't use a Countdown function with a key not tied to a Countdown";
                throw new System.ArgumentException(errorMessage);
            }
        }

        public delegate void TimerEventHandler(TimerKey timerNumber);

        public virtual TimeSpan GetCountdownTimeLastSetFor(TimerKey key)
        {
            ValidateCountdown(key);
            return countdownManager.GetCountdownTimeLastSetFor(key);
        }

        protected virtual void Update()
        {
            countdownManager.TickTimers();
            stopwatchManager.TickTimers();
        }
    }
}