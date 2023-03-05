using UnityEngine;
using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	/// <summary>
	/// For displaying a TimerController's text.
	/// </summary>
	public abstract class TimerText : MonoBehaviour, ITimerText
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

		public virtual bool IsDisplayingTime { get; set; }

		public abstract void DisplayTime();
		public abstract void StopDisplayingTime();

		protected virtual string TextToDisplay { get { return TimeToDisplay.ToString(FormatToDisplayIn); } }

		protected virtual TimeSpan TimeToDisplay { get { return toDisplayFor.CurrentTime; } }

		protected virtual string FormatToDisplayIn { get { return formatOptions[displayFormat]; } }


		protected static Dictionary<TimeDisplayFormat, string> formatOptions = 
			new Dictionary<TimeDisplayFormat, string>()
			{
				{ TimeDisplayFormat.seconds, "ss" },
				{ TimeDisplayFormat.minutes, "mm" },
				{ TimeDisplayFormat.hours, "hh" },
				{ TimeDisplayFormat.days, "dd" },

				{ TimeDisplayFormat.minutesSeconds, "mm':'ss" },
				{ TimeDisplayFormat.hoursMinutes, "hh':'mm" },

				{ TimeDisplayFormat.hoursMinutesSeconds, "hh':'mm':'ss" },
				{ TimeDisplayFormat.daysHoursMinutesSeconds, "dd':'hh':'mm':'ss" }

			};

	}
}