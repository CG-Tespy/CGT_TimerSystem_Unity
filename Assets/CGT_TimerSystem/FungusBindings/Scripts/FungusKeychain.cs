using System.Collections.Generic;
using UnityEngine;

namespace CGT.Unity.TimerSys.Fungus
{
	/// <summary>
	/// Holds the Timer Keys that the Fungus bindings will use.
	/// </summary>
	public class FungusKeychain : MonoBehaviour
	{
		protected Dictionary<int, TimerKey> stopwatchKeys = new Dictionary<int, TimerKey>();
		protected Dictionary<int, TimerKey> countdownKeys = new Dictionary<int, TimerKey>();

		protected virtual void Awake()
        {
			DontDestroyOnLoad(this.gameObject);
			InitializeKeys();
        }

		protected virtual void InitializeKeys()
        {
			for (int i = 0; i < defaultKeyCount; i++)
            {
				stopwatchKeys.Add(i, new TimerKey(i));
				countdownKeys.Add(i, new TimerKey(i));
            }
        }

		protected int defaultKeyCount = 255;

		public virtual void RegisterStopwatchKeyFor(int num)
        {
			bool alreadyThere = stopwatchKeys.ContainsKey(num);

			if (alreadyThere)
				return;

			stopwatchKeys.Add(num, new TimerKey(num));
        }

		public virtual void RegisterCountdownKeyFor(int num)
		{
			bool alreadyThere = countdownKeys.ContainsKey(num);

			if (alreadyThere)
				return;

			countdownKeys.Add(num, new TimerKey(num));
		}

		public virtual TimerKey GetStopwatchKeyFor(int num)
        {
			bool isRegistered = stopwatchKeys.ContainsKey(num);

			if (!isRegistered)
				return null;

			return stopwatchKeys[num];
        }

		public virtual TimerKey GetCountdownKeyFor(int num)
		{
			bool isRegistered = countdownKeys.ContainsKey(num);

			if (!isRegistered)
				return null;

			return countdownKeys[num];
		}

	}
}