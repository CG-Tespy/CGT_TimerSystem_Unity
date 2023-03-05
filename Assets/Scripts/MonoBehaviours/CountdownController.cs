using System;

namespace CGT.Unity.TimerSys
{
	public class CountdownController : TimerController, ICountdown
	{
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