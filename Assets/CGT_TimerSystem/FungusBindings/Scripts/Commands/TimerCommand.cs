using UnityEngine;
using Fungus;

namespace CGT.Unity.TimerSys.Fungus
{
	public abstract class TimerCommand : Command
	{
		[SerializeField]
		[VariableProperty(typeof(ObjectVariable))]
		protected ObjectVariable timerController;

		[Header("For when there's no TimerController")]
		[SerializeField]
		protected TimerType timerType;

		public enum TimerType
		{
			stopwatch, countdown
		}

		[SerializeField]
		[Tooltip("For accessing the timer tied to this particular number")]
		protected IntegerData timerKeyNum; 
		// ^If there is no timer controller assigned, this will instead be used to access the timer

		protected virtual void Awake()
        {
			EnsureTimerSystemIsThere();
			EnsureKeychainIsThere();
			EnsureKeyForNumIsThere();
        }

		protected virtual void EnsureTimerSystemIsThere()
		{
			timerSys = FindObjectOfType<TimerSystem>();
			bool itIsThere = timerSys != null;

			if (!itIsThere)
			{
				GameObject holdsSystem = new GameObject("TimerSystem");
				timerSys = holdsSystem.AddComponent<TimerSystem>();
			}
		}

		protected TimerSystem timerSys;

		protected virtual void EnsureKeychainIsThere()
        {
			keychain = FindObjectOfType<FungusKeychain>();

			bool isAvailable = keychain != null;

			if (!isAvailable)
            {
				GameObject systemKeyGO = new GameObject("FungusTimerKeys");
				keychain = systemKeyGO.AddComponent<FungusKeychain>();
            }
        }

		protected static FungusKeychain keychain;

		protected virtual void EnsureKeyForNumIsThere()
        {
			if (timerType == TimerType.countdown)
				keychain.RegisterCountdownKeyFor(timerKeyNum);
			else
				keychain.RegisterStopwatchKeyFor(timerKeyNum);
        }

		protected virtual TimerKey KeyToUse
        {
			get
			{
				if (TimerControllerIsValid)
					return TimerControllerKey;

				else
					return NumKey;
			}
        }

		protected virtual bool TimerControllerIsValid
        {
			get
			{
				bool objectVarSet = timerController != null;

				if (!objectVarSet)
					return false;

				bool hasActualTimerController = (timerController.Value as TimerController) != null;

				return hasActualTimerController;
			}
        }

		protected virtual TimerKey TimerControllerKey
        {
			get
            {
				// We assume that the controller is valid here
				TimerController actualController = (TimerController)timerController.Value;
				return actualController.Key;
            }
        }

		protected virtual TimerKey NumKey
        {
			get
            {
				if (timerType == TimerType.countdown)
					return keychain.GetCountdownKeyFor(timerKeyNum);
				else
					return keychain.GetStopwatchKeyFor(timerKeyNum);
            }
        }
		
    }
}