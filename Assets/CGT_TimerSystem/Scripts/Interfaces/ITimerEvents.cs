namespace CGT.Unity.TimerSys
{
	/// <summary>
	/// For classes that handle listening to and unlistening to events.
	/// </summary>
	public interface ITimerEvents
	{
		void ListenForStart(TimerKey key, OnTimerEvent listener);
		void ListenForStop(TimerKey key, OnTimerEvent listener);
		void ListenForReset(TimerKey key, OnTimerEvent listener);
		void ListenForRestart(TimerKey key, OnTimerEvent listener);

		void UnlistenForStart(TimerKey key, OnTimerEvent listener);
		void UnlistenForStop(TimerKey key, OnTimerEvent listener);
		void UnlistenForReset(TimerKey key, OnTimerEvent listener);
		void UnlistenForRestart(TimerKey key, OnTimerEvent listener);

	}
}