using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using CGT.Unity.TimerSys;
using FetchArgs = CGT.Unity.TimerSys.Fungus.KeyFetcher.FetchArgs;

namespace CGT.Unity.TimerSys.Fungus
{
	public class SetCountdownFor : TimerCommand
	{
		[SerializeField]
		protected TSTimeSpan duration;

        public override void OnEnter()
        {
            base.OnEnter();
            timerSys.SetCountdownFor(keyToUse, duration.ToNormalTimeSpan());
            Continue();
        }

        protected override FetchArgs PrepareFetchArgs()
        {
            FetchArgs args = base.PrepareFetchArgs();
            args.timerType = TimerType.countdown;
            return args;
        }

    }
}