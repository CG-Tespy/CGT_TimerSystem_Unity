using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
	/// <summary>
	/// For accessing, creating and uniquely identifying timers.
	/// </summary>
	public class TimerKey
	{
		protected System.Object tiedTo;

		public TimerKey(System.Object toTieToThis)
        {
			bool validInput = toTieToThis != null;
			if (!validInput)
				AlertForNullInput();
			
			tiedTo = toTieToThis;
        }

		protected virtual void AlertForNullInput()
        {
			string errorMessage = "Cannot create a TimerKey with a null value to tie it to.";
			throw new System.InvalidOperationException(errorMessage);
        }

		public virtual bool IsValid { get { return tiedTo != null; } }
	}
}