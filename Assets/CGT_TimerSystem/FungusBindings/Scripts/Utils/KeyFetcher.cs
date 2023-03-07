using Fungus;
using UnityEngine;

namespace CGT.Unity.TimerSys.Fungus
{
	public class KeyFetcher
	{
		protected virtual void EnsureKeychainIsThere()
        {
			keychain = GameObject.FindObjectOfType<FungusKeychain>();

			bool isAvailable = keychain != null;

			if (!isAvailable)
			{
				GameObject systemKeyGO = new GameObject("FungusTimerKeys");
				keychain = systemKeyGO.AddComponent<FungusKeychain>();
			}
		}

		protected FungusKeychain keychain;

		public virtual TimerKey Fetch(FetchArgs args)
        {
			if (IsValid(args.hasTimerController))
            {
				GameObject controllerGO = args.hasTimerController.Value;
				TimerController controller = controllerGO.GetComponent<TimerController>();
				return controller.Key;
            }
			else
            {
				return FetchKeyByNum(args);
            }
        }

		public class FetchArgs
		{
			public GameObjectVariable hasTimerController;
			public int keyNum;
			public TimerType timerType;
		}

		protected virtual bool IsValid(GameObjectVariable hasTimerController)
        {
			bool isNothing = hasTimerController == null;
			if (isNothing)
				return false;

			GameObject controllerGO = hasTimerController.Value;
			TimerController controller = controllerGO.GetComponent<TimerController>();
			bool hasActualController = controller != null;

			return hasActualController;
        }

		protected virtual TimerKey FetchKeyByNum(FetchArgs args)
        {
			if (args.timerType == TimerType.countdown)
			{
				keychain.RegisterCountdownKeyFor(args.keyNum);
				return keychain.GetCountdownKeyFor(args.keyNum);
			}
			else if (args.timerType == TimerType.stopwatch)
			{
				keychain.RegisterStopwatchKeyFor(args.keyNum);
				return keychain.GetStopwatchKeyFor(args.keyNum);
			}
			else
				return null;
		}

	}
}