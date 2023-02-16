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

		public virtual void ListenForCountdownFinish(TimerKey key, OnTimerEvent listener)
        {
			Countdown countdown = GetTimer(key);
			countdown.OnFinish += listener;
        }

		
	}
}