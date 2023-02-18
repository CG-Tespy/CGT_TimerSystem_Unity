using TimeSpan = System.TimeSpan;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
    /// <summary>
    /// Timer that implements the Observer Pattern.
    /// </summary>
    public abstract class ObservableTimer : IObservableTimer
    {
        public virtual TimeSpan CurrentTime
        {
            get { return TimeSpan.FromHours(currentTime); }
            protected set { currentTime = value.TotalHours; }
        }

        // Measured in hours to get more mileage out of the double data type
        protected double currentTime;

        public virtual void StartUp()
        {
            if (IsRunning)
                return; 
            // ^So we don't falsely alert event listeners. We apply this approach to some
            // other funcs here as well

            IsRunning = true;
            AlertListenersFor(OnStart);
        }

        public virtual bool IsRunning { get; protected set; }

        protected virtual void AlertListenersFor(OnTimerEvent theEvent)
        {
            TimerEventArgs eventArgs = new TimerEventArgs();
            eventArgs.Timer = this;
            theEvent.Invoke(eventArgs);
        }

        public event OnTimerEvent OnStart = delegate { };

        public virtual void Stop()
        {
            if (!IsRunning)
                return;
            IsRunning = false;
            AlertListenersFor(OnStop);
        }

        public event OnTimerEvent OnStop = delegate { };

        /// <summary>
        /// Sets the timer back to the appropriate time before stopping it from running.
        /// </summary>
        public virtual void Reset()
        {
            currentTime = timeToResetTo;
            IsRunning = false;
            AlertListenersFor(OnReset);
        }

        protected double timeToResetTo; // Also in hours

        public event OnTimerEvent OnReset = delegate { };

        /// <summary>
        /// Resets then starts the timer. Does not trigger the event listeners for those
        /// actions.
        /// </summary>
        public virtual void Restart()
        {
            currentTime = timeToResetTo;
            IsRunning = true;
            AlertListenersFor(OnRestart);
        }

        public event OnTimerEvent OnRestart = delegate { };

        /// <summary>
        /// Called to have this timer register a single frame's worth of time passing,
        /// provided it's running.
        /// </summary>
        public virtual void Tick()
        {
            if (!IsRunning)
                return;

            double scaledSecondsPassed = Time.unscaledDeltaTime * TimeScale;
            //Debug.Log("Unscaled delta time: " + Time.unscaledDeltaTime);
            //Debug.Log("Scaled seconds passed: " + scaledSecondsPassed);
            double scaledHoursPassed = SecondsToHours(scaledSecondsPassed);
            //Debug.Log("Scaled hours passed: " + scaledHoursPassed);
            ApplyTimeElapsed(scaledHoursPassed);
        }

        /// <summary>
        /// The multiplier for the time that each Tick registers as having passed. Gives you
        /// the option to have this timer count time faster or slower than it's
        /// actually going.
        /// </summary>
        public virtual float TimeScale
        {
            get { return timeScale; }
            set { timeScale = value; }
        }

        protected float timeScale = 1f;

        private double SecondsToHours(double seconds)
        {
            double toMinutes = seconds / 60.0;
            double toHours = toMinutes / 60.0;
            return toHours;
        }

        protected abstract void ApplyTimeElapsed(double scaledHoursElapsed);
        
    }
}