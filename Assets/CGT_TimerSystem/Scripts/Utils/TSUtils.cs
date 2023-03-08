using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	/// <summary>
	/// Utility class for working with TimeSpans
	/// </summary>
	public static class TSUtils
	{
		public static string TSToString(TSTimeSpan timeSpan, TimeDisplayFormat displayFormat)
        {
			return TSToString(timeSpan.ToNormalTimeSpan(), displayFormat);
        }

		public static string TSToString(TimeSpan timeSpan, TimeDisplayFormat displayFormat)
        {
			string formatToUse = formatOptions[displayFormat];
			return timeSpan.ToString(formatToUse);
        }

		private static Dictionary<TimeDisplayFormat, string> formatOptions =
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