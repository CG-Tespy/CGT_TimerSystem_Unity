using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
	// Set up this way for more informative error messages on validation
	public class TimerHandlerValidator
	{
		public void ValidateForStarting(TimerKey key)
        {
			if (key == null || !key.IsValid)
            {
				throw new System.ArgumentException(forInvalidTimerStarting);
            }
        }

		protected static string forInvalidTimerStarting = "Cannot start a timer with an invalid key.";
	
		public virtual void ValidateForStopping(TimerKey key)
        {
			if (key == null || !key.IsValid)
			{
				throw new System.ArgumentException(forInvalidTimerStopping);
			}
        }

		protected static string forInvalidTimerStopping = "Cannot stop a timer with an invalid key.";

		public virtual void ValidateForResetting(TimerKey key)
		{
			if (key == null || !key.IsValid)
			{
				throw new System.ArgumentException(forInvalidTimerResetting);
			}
		}

		protected static string forInvalidTimerResetting = "Cannot reset a timer with an invalid key.";

		public virtual void ValidateForRestarting(TimerKey key)
		{
			if (key == null || !key.IsValid)
			{
				throw new System.ArgumentException(forInvalidTimerRestarting);
			}
		}

		protected static string forInvalidTimerRestarting = "Cannot restart a timer with an invalid key.";

	}
}