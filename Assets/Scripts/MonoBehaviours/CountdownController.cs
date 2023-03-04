using System;

namespace CGT.Unity.TimerSys
{
	public class CountdownController : TimerController, ICountdown
	{
        protected override void RegisterInSystem()
        {
            timerSys.RegisterCountdown(key);
        }

        public virtual TimeSpan LastSetFor
        {
            get { return timerSys.GetCountdownTimeLastSetFor(key); }
        }
    }
}