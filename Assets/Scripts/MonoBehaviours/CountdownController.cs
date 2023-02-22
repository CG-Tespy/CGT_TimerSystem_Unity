using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
	public class CountdownController : MonoBehaviour, ICountdown
	{
        event OnTimerEvent ICountdown.OnEnd
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event OnTimerEvent IObservableTimer.OnStart
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event OnTimerEvent IObservableTimer.OnStop
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event OnTimerEvent IObservableTimer.OnReset
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event OnTimerEvent IObservableTimer.OnRestart
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

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

        public virtual TimeSpan TimeLeft { get { return CurrentTime; } }

        public TimeSpan CurrentTime { get { return TimerManager.GetCountdownCurrentTime(timerKey); }}

        public bool IsRunning
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

        public void Tick()
        {
            Debug.LogWarning("Should not call Tick from a CountdownController");
        }
    }
}