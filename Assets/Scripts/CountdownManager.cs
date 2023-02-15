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

		public virtual TimeSpan GetCountdownTimeLeft(TimerKey handler)
        {
			Countdown inQuestion = GetTimer(handler);
			return inQuestion.TimeLeft;
        }

		public virtual System.Action<TimerEventArgs> GetCountdownFinishEvent(TimerKey handler)
        {
			Countdown inQuestion = GetTimer(handler);
			return inQuestion.OnFinish;
        }
	}
}