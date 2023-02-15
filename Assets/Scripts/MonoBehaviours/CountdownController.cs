using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
	public class CountdownController : MonoBehaviour, ICountdown
	{
		[SerializeField]
        [Tooltip("Which timer this is set to. Two CountdownControllers set to the same number are treated as the same timer.")]
		protected uint timerNumber;

		public uint TimerNumber { get { return timerNumber; } }

        public TimeSpan LastSetFor { get; protected set; }

        public Action<TimerEventArgs> OnFinish
        {
            get { return TimerManager.GetCountdownFinishEvent(timerNumber); }

            set
            {
                var finishEvent = TimerManager.GetCountdownFinishEvent(timerNumber);
                finishEvent = value;
            }
        }

        public virtual TimeSpan TimeLeft { get { return CurrentTime; } }

        public TimeSpan CurrentTime { get { return TimerManager.GetCountdownCurrentTime(timerNumber); }}

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
            TimerManager.StartCountdown(timerNumber);
        }

        public void Reset()
        {
            TimerManager.ResetCountdown(timerNumber);
        }

        public void Stop()
        {
            TimerManager.StopCountdown(timerNumber);
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }
    
        public virtual void SetFor(TimeSpan duration)
        {
            TimerManager.SetCountdownFor(timerNumber, duration);
        }

        protected virtual void Awake()
        {
            TimerManager = FindObjectOfType<MainTimerManager>();
        }
    }
}