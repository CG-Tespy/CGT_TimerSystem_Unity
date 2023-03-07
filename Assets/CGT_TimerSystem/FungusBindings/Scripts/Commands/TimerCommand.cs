﻿using UnityEngine;
using Fungus;
using FetchArgs = CGT.Unity.TimerSys.Fungus.KeyFetcher.FetchArgs;

namespace CGT.Unity.TimerSys.Fungus
{
	public abstract class TimerCommand : Command
	{
		[SerializeField]
		[VariableProperty(typeof(ObjectVariable))]
		protected ObjectVariable hasTimerController;

		[SerializeField]
		[Tooltip("For accessing the timer tied to this particular number")]
		protected IntegerData timerKeyNum; 
		// ^If there is no timer controller assigned, this will instead be used to access the timer

		protected virtual void Awake()
        {
			EnsureTimerSystemIsThere();
			DecideKeyToUse();
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

		protected virtual void DecideKeyToUse()
        {
			keyFetcher = new KeyFetcher();
			FetchArgs args = PrepareFetchArgs();
			keyToUse = keyFetcher.Fetch(args);
        }

		protected KeyFetcher keyFetcher;

		protected virtual FetchArgs PrepareFetchArgs()
        {
			FetchArgs args = new FetchArgs();
			args.hasTimerController = hasTimerController;
			args.keyNum = timerKeyNum;
			args.timerType = TimerType.nullType;

			return args;
		}

		protected TimerKey keyToUse;


    }
}