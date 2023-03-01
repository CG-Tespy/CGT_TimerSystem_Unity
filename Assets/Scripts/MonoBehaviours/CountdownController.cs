using System;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
	public class CountdownController : MonoBehaviour, ICountdown
	{

        protected virtual void Awake()
        {
            TimerSys = FindObjectOfType<TimerSystem>();
            key = new TimerKey(this);
        }

        protected TimerKey key;

        /// <summary>
        /// For the timer tied to this controller.
        /// </summary>
        public virtual TimerKey Key { get { return key; } }
        
        public TimeSpan LastSetFor
        {
            get { return TimerSys.GetCountdownTimeLastSetFor(key); }
        }

        public virtual TimeSpan TimeLeft { get { return CurrentTime; } }

        public TimeSpan CurrentTime { get { return TimerSys.GetCountdownCurrentTime(key); }}

        public virtual bool IsRunning
        {
            get { return TimerSys.IsCountdownRunning(key); }
        }

        public virtual float TimeScale
        {
            get { return TimerSys.GetCountdownTimeScale(key); }

            set { TimerSys.SetCountdownTimeScale(key, value); }
        }

        protected static TimerSystem TimerSys;

        public void StartUp()
        {
            TimerSys.StartCountdown(key);
        }

        public void Reset()
        {
            TimerSys.ResetCountdown(key);
        }

        public void Stop()
        {
            TimerSys.StopCountdown(key);
        }

        public void Restart()
        {
            TimerSys.RestartCountdown(key);
        }
    
        public virtual void SetFor(TimeSpan duration)
        {
            TimerSys.SetCountdownFor(key, duration);
        }

        public void Tick()
        {
            Debug.LogWarning("Should not call Tick from a CountdownController. Does a whole lotta nothin'.");
        }
    }
}