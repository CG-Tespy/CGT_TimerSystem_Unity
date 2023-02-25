using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	public class CountdownManager : TimerManager<Countdown>
	{
		public virtual void SetCountdownFor(TimerKey handler, double durationInSeconds)
		{
			TimeSpan durationAsSpan = TimeSpan.FromSeconds(durationInSeconds);
			SetCountdownFor(handler, durationAsSpan);
		}

		public virtual void SetCountdownFor(TimerKey handler, TimeSpan duration)
		{
			Countdown inQuestion = GetTimer(handler);
			inQuestion.SetFor(duration);
		}

		public virtual TimeSpan GetCountdownTimeLeft(TimerKey key)
        {
			Countdown inQuestion = GetTimer(key);
			return inQuestion.TimeLeft;
        }

		public virtual TimeSpan GetCountdownTimeLastSetFor(TimerKey key)
        {
			Countdown inQuestion = GetTimer(key);
			return inQuestion.LastSetFor;
        }

		public CountdownManager()
        {
			Events = new CountdownEventManager(this);
        }

		new public CountdownEventManager Events { get; protected set; }

		public class CountdownEventManager : EventManager<Countdown>
        {
			public CountdownEventManager(CountdownManager manager) : base(manager)
			{
				this.manager = manager;
			}

			protected CountdownManager manager; 
			// Since I need this to be able to treat Countdown objects as Countdowns, which
			// would then let me access their OnEnd events. Just using them as TTimers wouldn't
			// let us do that

			public virtual void ListenForEnd(TimerKey key, OnTimerEvent listener)
            {
				Countdown inQuestion = manager.GetTimer(key);
				inQuestion.OnEnd += listener;
            }

			public virtual void UnlistenForEnd(TimerKey key, OnTimerEvent listener)
            {
				Countdown inQuestion = manager.GetTimer(key);
				inQuestion.OnEnd -= listener;
            }

			
        }

	}
}