using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys.Fungus
{
    [CommandInfo("CGT TimerSys/Unity UI", "Display Timer",
        "Displays the state of a timer on a Unity UI text field.")]
	public class DisplayTimerUnityUI : TimerCommand
	{
        [SerializeField]
        protected TimerType timerType;

        [SerializeField]
        protected Text textField;

		[SerializeField]
		protected TimeDisplayFormat displayFormat = TimeDisplayFormat.hoursMinutesSeconds;

        public override void OnEnter()
        {
            base.OnEnter();
            TimeSpan currentTime = timerSys.GetTimerCurrentTime(keyToUse);
            string toDisplay = TSUtils.TSToString(currentTime, displayFormat);
            textField.text = toDisplay;
            Continue();
        }

        protected override KeyFetcher.FetchArgs PrepareFetchArgs()
        {
            KeyFetcher.FetchArgs args = base.PrepareFetchArgs();
            args.timerType = timerType;
            return args;
        }

    }
}