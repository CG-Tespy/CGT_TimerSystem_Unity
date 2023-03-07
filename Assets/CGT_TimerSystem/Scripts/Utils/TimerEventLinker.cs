using UnityEngine;

namespace CGT.Unity.TimerSys
{
	/// <summary>
	/// For linking the TimerController's UnityEvents to their corresponding Timer events
	/// </summary>
	public class TimerEventLinker
	{
		public TimerEventLinker()
        {
			timerSys = GameObject.FindObjectOfType<TimerSystem>();
		}

		public TimerController TimerController { get; set; }
		public ITimerEvents TimerEvents { get; set; }

		protected TimerController timerController;
		protected ITimerEvents events;
		protected TimerSystem timerSys;

		public virtual void LinkEvents()
        {
			events.ListenForStart(Key, OnStart);
			events.ListenForStop(Key, OnStop);
			events.ListenForReset(Key, OnReset);
			events.ListenForRestart(Key, OnRestart);
        }

		protected virtual TimerKey Key { get { return timerController.Key; } }

		protected virtual void OnStart(TimerEventArgs args)
        {
			timerController.OnStart.Invoke();
        }

		protected virtual void OnStop(TimerEventArgs args)
        {
			timerController.OnStop.Invoke();
        }

		protected virtual void OnReset(TimerEventArgs args)
        {
			timerController.OnReset.Invoke();
        }

		protected virtual void OnRestart(TimerEventArgs args)
        {
			timerController.OnRestart.Invoke();
        }

		public virtual void UnlinkEvents()
        {
			events.UnlistenForStart(Key, OnStart);
			events.UnlistenForStop(Key, OnStop);
			events.UnlistenForReset(Key, OnReset);
			events.UnlistenForRestart(Key, OnRestart);
		}

	}
}