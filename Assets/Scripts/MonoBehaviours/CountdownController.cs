using System;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
	public class CountdownController : TimerController, ICountdown
	{
        [Header("Starting Time")]
        [SerializeField]
        protected int milliseconds = 100;

        [SerializeField]
        protected int seconds = 10, minutes, hours, days;

        protected override void Awake()
        {
            base.Awake();
            TimeSpan startingTime = new TimeSpan(days, hours, minutes, seconds, milliseconds);
            SetFor(startingTime);
        }

        protected override void RegisterInSystem()
        {
            timerSys.RegisterCountdown(key);
        }

        public virtual void SetFor(TimeSpan duration)
        {
            timerSys.SetCountdownFor(key, duration);
        }

        public virtual TimeSpan LastSetFor
        {
            get { return timerSys.GetCountdownTimeLastSetFor(key); }
        }
    }
}