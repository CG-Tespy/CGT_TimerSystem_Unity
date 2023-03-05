using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	public interface ITimerManager
	{
		void StartTimer(TimerKey key);
		void StopTimer(TimerKey key);
		void ResetTimer(TimerKey key);
		void RestartTimer(TimerKey key);

		float GetTimerTimeScale(TimerKey key);
		void SetTimerTimeScale(TimerKey key, float newScale);
		bool IsTimerRunning(TimerKey key);

		TimeSpan GetTimerCurrentTime(TimerKey key);
	}
}