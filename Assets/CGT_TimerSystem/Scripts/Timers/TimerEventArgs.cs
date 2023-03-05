using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
    public class TimerEventArgs
    {
        public TimerEventArgs(ITimer timer)
        {
            this.Timer = timer;
        }

        protected ITimer Timer { get; set; }

        public virtual TimeSpan CurrentTime {  get { return Timer.CurrentTime; } }
        public virtual float TimeScale { get { return Timer.TimeScale; } }
    }

    public class CountdownEventArgs : TimerEventArgs
    {
        public CountdownEventArgs(Countdown timer) : base(timer) { }
        public virtual TimeSpan LastSetFor { get { return (Timer as Countdown).LastSetFor; } }
    }

    public class StopwatchEventArgs : TimerEventArgs
    {
        public StopwatchEventArgs(Stopwatch timer) : base(timer) { }
    }
}