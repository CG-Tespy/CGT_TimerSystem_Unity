using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	public class TimeSpanConverter
	{

		public virtual string Convert(TSTimeSpan timeSpan, TimeDisplayFormat displayFormat)
        {
			return Convert(timeSpan.ToNormalTimeSpan(), displayFormat);
        }

		public virtual string Convert(TimeSpan timeSpan, TimeDisplayFormat displayFormat)
        {
			string formatString = formatOptions[displayFormat];
			return timeSpan.ToString();
        }

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