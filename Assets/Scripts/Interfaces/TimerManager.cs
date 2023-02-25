using System.Collections;
using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	/// <summary>
	/// Sets up common functionality for the timer managerss
	/// </summary>
	/// <typeparam name="TTimer"></typeparam>
	public abstract class TimerManager<TTimer> where TTimer : IObservableTimer, new()
	{
		public virtual int TimerCount { get { return timers.Keys.Count; } }
		protected Dictionary<TimerKey, TTimer> timers = new Dictionary<TimerKey, TTimer>();

		public virtual void StartTimer(TimerKey key)
        {
			validator.ValidateForStarting(key);

			TTimer inQuestion = GetTimer(key);
			if (inQuestion.IsRunning)
				return;

			inQuestion.StartUp();
        }

		protected TimerKeyValidator validator = new TimerKeyValidator();

		protected virtual TTimer GetTimer(TimerKey key)
        {
			EnsureTimerExists(key);
			return timers[key];
        }

		protected virtual void EnsureTimerExists(TimerKey key)
        {
			bool alreadyExists = timers.ContainsKey(key);
			if (alreadyExists)
				return;

			timers.Add(key, new TTimer());
        }

		public virtual void StopTimer(TimerKey key)
        {
			validator.ValidateForStopping(key);
			TTimer inQuestion = GetTimer(key);
			inQuestion.Stop();
        }

		public virtual void ResetTimer(TimerKey key)
        {
			validator.ValidateForResetting(key);
			TTimer inQuestion = GetTimer(key);
			inQuestion.Reset();
        }

		public virtual void RestartTimer(TimerKey key)
        {
			validator.ValidateForRestarting(key);
			TTimer inQuestion = GetTimer(key);
			inQuestion.Restart();
        }

		public virtual TimeSpan GetTimerCurrentTime(TimerKey key)
        {
			validator.ValidateForGettingCurrentTime(key);
			TTimer inQuestion = GetTimer(key);
			return inQuestion.CurrentTime;
        }

		public virtual void TickTimers()
        {
			foreach (var timerToTick in timers.Values)
            {
				timerToTick.Tick();
            }
        }

		public TimerManager()
        {
			Events = new TimerEvents(this);
        }

		public virtual TimerEvents Events { get; protected set; }
		
		public class TimerEvents
		{
			public TimerEvents(TimerManager<TTimer> manager)
            {
				this.timerManager = manager;
            }

			protected TimerManager<TTimer> timerManager;

			public virtual void ListenForStart(TimerKey key, OnTimerEvent listener)
            {
				TTimer inQuestion = timerManager.GetTimer(key);
				inQuestion.OnStart += listener;
            }

			public virtual void UnlistenForStart(TimerKey key, OnTimerEvent listener)
            {
				TTimer inQuestion = timerManager.GetTimer(key);
				inQuestion.OnStart -= listener;
			}

			public virtual void ListenForStop(TimerKey key, OnTimerEvent listener)
            {
				TTimer inQuestion = timerManager.GetTimer(key);
				inQuestion.OnStop += listener;
			}

			public virtual void UnlistenForStop(TimerKey key, OnTimerEvent listener)
			{
				TTimer inQuestion = timerManager.GetTimer(key);
				inQuestion.OnStop -= listener;
			}

			public virtual void ListenForReset(TimerKey key, OnTimerEvent listener)
            {
				TTimer inQuestion = timerManager.GetTimer(key);
				inQuestion.OnReset += listener;
			}

			public virtual void UnlistenForReset(TimerKey key, OnTimerEvent listener)
			{
				TTimer inQuestion = timerManager.GetTimer(key);
				inQuestion.OnReset -= listener;
			}

			public virtual void ListenForRestart(TimerKey key, OnTimerEvent listener)
			{
				TTimer inQuestion = timerManager.GetTimer(key);
				inQuestion.OnRestart += listener;
			}

			public virtual void UnlistenForRestart(TimerKey key, OnTimerEvent listener)
			{
				TTimer inQuestion = timerManager.GetTimer(key);
				inQuestion.OnRestart -= listener;
			}
		}
	
		public virtual void SetTimerTimeScale(TimerKey key, float newTimeScale)
        {
			TTimer inQuestion = GetTimer(key);
			inQuestion.TimeScale = newTimeScale;
        }
	}
}