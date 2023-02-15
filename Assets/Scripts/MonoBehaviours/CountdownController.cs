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
		protected int timerNumber;

		public int TimerNumber { get { return timerNumber; } }

        public TimeSpan LastSetFor { get; protected set; }

        public Action<TimerEventArgs> OnFinish
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public TimeSpan CurrentTime
        {
            get { return TimerManager.GetCountdownCurrentTime(timerNumber); }
        }

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

        protected static TimerManager TimerManager;

        public void StartUp()
        {
            TimerManager.StartCountdown(timerNumber);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            TimerManager.StopCountdown(timerNumber);
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }
    
        protected virtual void Awake()
        {
            TimerManager = FindObjectOfType<TimerManager>();
        }
    }
}