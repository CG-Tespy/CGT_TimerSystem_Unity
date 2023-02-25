namespace CGT.Unity.TimerSys
{
    public class TimerEventArgs
    {
        public ITimer Timer { get; set; }
    }

    public class CountdownEventArgs
    {
        public virtual Countdown Countdown { get; set; }
    }

    public class StopwatchEventArgs
    {
        public virtual Stopwatch Stopwatch { get; set; }
    }
}