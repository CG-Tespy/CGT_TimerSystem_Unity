using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	public class CountdownManager : TimerManager<Countdown>
	{
		public virtual void SetCountdownFor(uint id, double durationInSeconds)
		{
			TimeSpan durationAsSpan = TimeSpan.FromSeconds(durationInSeconds);
			SetCountdownFor(id, durationAsSpan);
		}

		public virtual void SetCountdownFor(uint id, TimeSpan duration)
		{
			Countdown inQuestion = GetTimer(id);
			inQuestion.SetFor(duration);
		}

		public virtual TimeSpan GetCountdownTimeLeft(uint id)
        {
			Countdown inQuestion = GetTimer(id);
			return inQuestion.TimeLeft;
        }

		public virtual System.Action<TimerEventArgs> GetCountdownFinishEvent(uint id)
        {
			Countdown inQuestion = GetTimer(id);
			return inQuestion.OnFinish;
        }
	}
}