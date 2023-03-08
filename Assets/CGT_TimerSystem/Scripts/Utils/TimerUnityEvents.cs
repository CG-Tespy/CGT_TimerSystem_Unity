using UnityEngine;
using UnityEngine.Events;

namespace CGT.Unity.TimerSys
{
	[System.Serializable]
	public class TimerUnityEvents : ITimerUnityEvents
	{
		[SerializeField]
		protected UnityEvent onStart = new UnityEvent(),
			onStop = new UnityEvent(),
			onReset = new UnityEvent(),
			onRestart = new UnityEvent();

		public virtual UnityEvent OnStart { get { return onStart; } }
		public virtual UnityEvent OnStop { get { return onStop; } }
		public virtual UnityEvent OnReset { get { return onReset; } }
		public virtual UnityEvent OnRestart { get { return onRestart; } }
	}
}