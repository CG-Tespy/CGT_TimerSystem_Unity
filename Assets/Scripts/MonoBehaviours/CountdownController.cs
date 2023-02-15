using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
	public class CountdownController : MonoBehaviour, ICountdown
	{
		protected virtual void Awake()
        {
            TimerManager = FindObjectOfType<MainTimerManager>();
            timerKey = new TimerKey(this);
        }

        protected TimerKey timerKey;

        /// <summary>
        /// For the timer tied to this controller.
        /// </summary>
        public virtual TimerKey TimerKey { get { return timerKey; } }
       
        public TimeSpan LastSetFor { get; protected set; }

        public Action<TimerEventArgs> OnFinish
        {
            get { return TimerManager.GetCountdownFinishEvent(timerKey); }

            set
            {
                var finishEvent = TimerManager.GetCountdownFinishEvent(timerKey);
                finishEvent = value;
            }
        }
        
        public virtual TimeSpan TimeLeft { get { return CurrentTime; } }

        public TimeSpan CurrentTime { get { return TimerManager.GetCountdownCurrentTime(timerKey); }}

        public bool IsRunning
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Action<TimerEventArgs> OnStart
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Action<TimerEventArgs> OnStop
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Action<TimerEventArgs> OnReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Action<TimerEventArgs> OnRestart
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected static MainTimerManager TimerManager;

        public void StartUp()
        {
            TimerManager.StartCountdown(timerKey);
        }

        public void Reset()
        {
            TimerManager.ResetCountdown(timerKey);
        }

        public void Stop()
        {
            TimerManager.StopCountdown(timerKey);
        }

        public void Restart()
        {
            TimerManager.RestartCountdown(timerKey);
        }
    
        public virtual void SetFor(TimeSpan duration)
        {
            TimerManager.SetCountdownFor(timerKey, duration);
        }

    }
}