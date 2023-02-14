using DateTime = System.DateTime;
using TimeSpan = System.TimeSpan;
using BaseTimer = System.Timers.Timer;

namespace CGT.Unity.TimerSys
{
    /// <summary>
    /// A Timer that counts down from a start time. 
    /// </summary>
    public class Countdown : ObservableTimer, ICountdown
    {
        public virtual void SetFor(TimeSpan duration)
        {
            baseTimer.Interval = duration.TotalMilliseconds;
            LastSetFor = TimeLeft = duration;
        }

        protected BaseTimer baseTimer = new BaseTimer();
        public virtual TimeSpan LastSetFor { get; protected set; }

        /// <summary>
        /// Alternate name for CurrentTime
        /// </summary>
        public virtual TimeSpan TimeLeft
        {
            get { return CurrentTime; }
            protected set { CurrentTime = value; }
        }

        /// <summary>
        /// The amount of time left until this Countdown's finished, assuming
        /// this is running at the time of accessing this property.
        /// </summary>
        public override TimeSpan CurrentTime
        {
            get
            {
                UpdateTimeLeft();
                return timeLeft;
            }
            protected set { timeLeft = value; }
        }

        public virtual void UpdateTimeLeft()
        {
            if (this.IsRunning)
                // The time left should only get lower when this is running, after all
                timeLeft = expectedEndTime - DateTime.Now;
        }

        public override bool IsRunning
        {
            get { return baseTimer.Enabled; }
        }

        protected TimeSpan timeLeft;

        public override void StartUp()
        {
            if (this.IsRunning) 
                // ^ We don't want expectedEndTime to get updated if this isn't running, hence
                // why we do this check even though we can call the base Start func here
                return;

            ResetExpectedEndTime();
            // ^At this point, this timer could've been set to start again after
            // being paused. In which case, we need to set expectedEndTime based on
            // how much time was left right as the pausing happened

            base.StartUp();
        }     

        protected virtual void ResetExpectedEndTime()
        {
            this.expectedEndTime = DateTime.Now + timeLeft;
        }

        private DateTime expectedEndTime; // Helps calculate TimeLeft when this is running

        protected override void StartBaseTimer()
        {
            baseTimer.Start();
        }

        /// <summary>
        /// Keeps this from counting down further until started up again.
        /// </summary>
        public override void Stop()
        {
            UpdateTimeLeft();
            base.Stop();
        }

        protected override void StopBaseTimer()
        {
            baseTimer.Stop();
        }

        /// <summary>
        /// Stops this from running and sets this to the time it was last set to.
        /// This does NOT trigger the OnStop event.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            ResetTimeLeft();
        }

        protected virtual void ResetTimeLeft()
        {
            timeLeft = LastSetFor;
        }

        protected override void ResetBaseTimer()
        {
            baseTimer.Stop();
            baseTimer.Interval = LastSetFor.TotalMilliseconds;
        }

        /// <summary>
        /// Resets this and starts it up if it's not already running. 
        /// This does NOT raise the OnStart or OnReset events.
        /// </summary>
        public override void Restart()
        {
            base.Restart();
            ResetTimeLeft();
            ResetExpectedEndTime();
        }
        protected override void RestartBaseTimer()
        {
            baseTimer.Interval = LastSetFor.TotalMilliseconds;
            if (!baseTimer.Enabled)
                baseTimer.Start();
        }

        public Countdown() : base()
        {
            this.baseTimer.Elapsed += this.OnBaseTimerFinished;
            // ^Since the base timer already has its own event for clients to listen
            // for its finishing, we'd best link our custom OnFinish 
            // event to it
        }

        private void OnBaseTimerFinished(object sender, System.Timers.ElapsedEventArgs e)
        {
            WhenLastFinished = DateTime.Now;
            AlertListenersFor(OnFinish);
            if (this.AutoReset)
                this.expectedEndTime = DateTime.Now.AddMilliseconds(baseTimer.Interval);
        }

        public DateTime WhenLastFinished { get; protected set; }

        public virtual System.Action<TimerEventArgs> OnFinish { get; set; } = delegate { };

        public virtual bool AutoReset
        { 
            get { return baseTimer.AutoReset; }
            set { baseTimer.AutoReset = value; }
        }
                
        public virtual void Dispose()
        {
            this.baseTimer.Stop();
            this.baseTimer.Dispose();
        }
    }
}