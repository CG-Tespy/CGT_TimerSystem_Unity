using UnityEngine.Events;

namespace CGT.Unity.TimerSys
{
	public interface ITimerUnityEvents
	{
		UnityEvent OnStart { get; }
		UnityEvent OnStop { get; }
		UnityEvent OnReset { get; }
		UnityEvent OnRestart { get; }

	}
}