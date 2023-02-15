using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		protected TimerHandlerValidator validator = new TimerHandlerValidator();

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
			inQuestion.Reset();
        }

	}
}