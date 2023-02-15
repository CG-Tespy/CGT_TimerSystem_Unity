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
		public virtual uint TimerCount {  get { return (uint)timers.Keys.Count; } }
		protected Dictionary<uint, TTimer> timers = new Dictionary<uint, TTimer>();

		public TimerManager()
        {
			PrepareInitialTimers();
        }

		protected virtual void PrepareInitialTimers()
        {
			uint howManyToPrepare = 10;
			for (uint i = 0; i < howManyToPrepare; i++)
            {
				timers.Add(i, new TTimer());
            }
        }

		public virtual void StartTimer(uint id)
        {
			TTimer inQuestion = GetTimer(id);
			if (inQuestion.IsRunning)
				return;

			inQuestion.StartUp();
        }

		protected virtual TTimer GetTimer(uint id)
        {
			EnsureTimerExists(id);
			return timers[id];
        }

		protected virtual void EnsureTimerExists(uint id)
        {
			bool alreadyExists = timers.ContainsKey(id);
			if (alreadyExists)
				return;

			timers.Add(id, new TTimer());
        }

		public virtual void StopTimer(uint id)
        {
			TTimer inQuestion = GetTimer(id);
			inQuestion.Stop();
        }

		public virtual void ResetTimer(uint id)
        {
			TTimer inQuestion = GetTimer(id);
			inQuestion.Reset();
        }

		public virtual void RestartTimer(uint id)
        {
			TTimer inQuestion = GetTimer(id);
			inQuestion.Reset();
        }

		
	}
}