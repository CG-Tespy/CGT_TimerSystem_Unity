namespace CGT.Unity.TimerSys
{
    public interface IBaseTimerEventArgs
    {
        System.Action<TimerEventArgs> OnStart { get; }
        System.Action<TimerEventArgs> OnStop { get; }
        System.Action<TimerEventArgs> OnReset { get; }
        System.Action<TimerEventArgs> OnRestart { get; }
    }
}