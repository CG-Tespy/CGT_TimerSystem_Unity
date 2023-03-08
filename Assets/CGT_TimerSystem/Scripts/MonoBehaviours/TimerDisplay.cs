using UnityEngine;
using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	/// <summary>
	/// For displaying a TimerController's text.
	/// </summary>
	public abstract class TimerDisplay : MonoBehaviour, ITimerDisplay
	{
		[SerializeField]
		protected TimerController toDisplayFor;

		[SerializeField]
		protected TimeDisplayFormat displayFormat = TimeDisplayFormat.hoursMinutesSeconds;

		[SerializeField]
		protected bool displayOnStart = true;

		protected virtual void Start()
		{
			IsDisplayingTime = displayOnStart;
		}

		protected virtual void Update()
		{
			if (IsDisplayingTime)
				DisplayTime();
		}

		public virtual bool IsDisplayingTime { get; set; }

		public abstract void DisplayTime();
		public abstract void StopDisplayingTime();

		protected virtual string TextToDisplay 
		{ get { return TSUtils.TSToString(TimeToDisplay, displayFormat); } }

		protected virtual TimeSpan TimeToDisplay { get { return toDisplayFor.CurrentTime; } }

	}
}