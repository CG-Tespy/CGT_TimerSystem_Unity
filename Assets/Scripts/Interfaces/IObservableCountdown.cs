namespace CGT.Unity.TimerSys
{
	public interface IObservableCountdown : ICountdown, IObservableTimer
	{
		event OnTimerEvent OnEnd;
		// ^Not all timer types really finish in the usual sense, hence why
		// this isn't in IBaseTimerEventArgs
	}
}