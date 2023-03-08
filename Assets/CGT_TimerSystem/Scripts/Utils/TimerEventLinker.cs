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

		public virtual TimerController TimerController
        {
			get { return timerController; }
			set { timerController = value; }
        }
		public virtual ITimerEvents TimerEvents
        {
			get { return events; }
			set { events = value; }
        }

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
			timerController.Events.OnStart.Invoke();
        }

		protected virtual void OnStop(TimerEventArgs args)
        {
			timerController.Events.OnStop.Invoke();
        }

		protected virtual void OnReset(TimerEventArgs args)
        {
			timerController.Events.OnReset.Invoke();
        }

		protected virtual void OnRestart(TimerEventArgs args)
        {
			timerController.Events.OnRestart.Invoke();
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