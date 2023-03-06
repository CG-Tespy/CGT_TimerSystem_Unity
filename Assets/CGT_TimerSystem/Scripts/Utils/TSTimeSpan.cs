using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	[System.Serializable]
	public struct TSTimeSpan
	{

		public int milliseconds, seconds, minutes, hours, days;

		public static TSTimeSpan From(TimeSpan regularTimeSpan)
        {
			TSTimeSpan result;
			result.milliseconds = regularTimeSpan.Milliseconds;
			result.seconds = regularTimeSpan.Seconds;
			result.minutes = regularTimeSpan.Minutes;
			result.hours = regularTimeSpan.Hours;
			result.days = regularTimeSpan.Days;

			return result;
        }

		public TimeSpan ToNormalTimeSpan()
        {
			TimeSpan result = new TimeSpan(this.days, this.hours,
				this.minutes, this.seconds,
				this.milliseconds);
			return result;
        }

		public TSTimeSpan(int days = 0, int hours = 0,
			int minutes = 0, int seconds = 0,
			int milliseconds = 0)
        {
			this.milliseconds = milliseconds;
			this.seconds = seconds;
			this.minutes = minutes;
			this.hours = hours;
			this.days = days;
        }
	}
}