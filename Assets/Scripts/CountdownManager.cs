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

		public virtual void ListenForStart(TimerKey key, OnTimerEvent listener)
        {
			Countdown inQuestion = GetTimer(key);
			inQuestion.OnStart += listener;
        }

		public virtual void UnlistenForStart(TimerKey key, OnTimerEvent listener)
        {
			Countdown inQuestion = GetTimer(key);
			inQuestion.OnStart -= listener;
        }

		public virtual void ListenForEnd(TimerKey key, OnTimerEvent listener)
        {
			Countdown inQuestion = GetTimer(key);
			inQuestion.OnEnd += listener;
        }

		public virtual void UnlistenForEnd(TimerKey key, OnTimerEvent listener)
        {
			Countdown inQuestion = GetTimer(key);
			inQuestion.OnEnd -= listener;
		}

	}
}