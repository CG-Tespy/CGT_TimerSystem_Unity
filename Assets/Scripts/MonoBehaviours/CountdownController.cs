using System;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
	public class CountdownController : MonoBehaviour, ICountdown
	{

        protected virtual void Awake()
        {
            TimerSys = FindObjectOfType<TimerSystem>();
            timerKey = new TimerKey(this);
        }

        protected TimerKey timerKey;

        /// <summary>
        /// For the timer tied to this controller.
        /// </summary>
        public virtual TimerKey TimerKey { get { return timerKey; } }
        
        public TimeSpan LastSetFor { get; protected set; }

        public virtual TimeSpan TimeLeft { get { return CurrentTime; } }

        public TimeSpan CurrentTime { get { return TimerSys.GetCountdownCurrentTime(timerKey); }}

        public virtual bool IsRunning
        {
            get { return TimerSys.IsCountdownRunning(timerKey); }
        }

        public virtual float TimeScale
        {
            get { return TimerSys.GetCountdownTimeScale(timerKey); }

            set { TimerSys.SetCountdownTimeScale(timerKey, value); }
        }

        protected static TimerSystem TimerSys;

        public void StartUp()
        {
            TimerSys.StartCountdown(timerKey);
        }

        public void Reset()
        {
            TimerSys.ResetCountdown(timerKey);
        }

        public void Stop()
        {
            TimerSys.StopCountdown(timerKey);
        }

        public void Restart()
        {
            TimerSys.RestartCountdown(timerKey);
        }
    
        public virtual void SetFor(TimeSpan duration)
        {
            TimerSys.SetCountdownFor(timerKey, duration);
        }

        public void Tick()
        {
            Debug.LogWarning("Should not call Tick from a CountdownController. Does a whole lotta nothin'.");
        }
    }
}