using System.Collections.Generic;
using UnityEngine;
using Fungus;
using FetchArgs = CGT.Unity.TimerSys.Fungus.KeyFetcher.FetchArgs;

namespace CGT.Unity.TimerSys.Fungus
{
    [CommandInfo("CGT TimerSys", "TimerAction", "For having timers do things that each type can do.")]
	public class TimerAction : TimerCommand
	{
        [SerializeField]
        protected GeneralActionType actionType;

        [Header("When not working with a Timer Controller")]
        [SerializeField]
        protected TimerType timerType;

        public enum GeneralActionType
        {
            start, stop, reset, restart
        }

        protected override void Awake()
        {
            base.Awake();
            RegisterActionFuncs();
        }

        protected virtual void RegisterActionFuncs()
        {
            actionFuncs[GeneralActionType.start] = ActionStart;
            actionFuncs[GeneralActionType.stop] = ActionStop;
            actionFuncs[GeneralActionType.reset] = ActionReset;
            actionFuncs[GeneralActionType.restart] = ActionRestart;
        }

        protected Dictionary<GeneralActionType, System.Action> actionFuncs = new Dictionary<GeneralActionType, System.Action>();
        
        protected virtual void ActionStart()
        {
            timerSys.StartTimer(keyToUse);
        }

        protected virtual void ActionStop()
        {
            timerSys.StopTimer(keyToUse);
        }

        protected virtual void ActionReset()
        {
            timerSys.ResetTimer(keyToUse);
        }

        protected virtual void ActionRestart()
        {
            timerSys.RestartTimer(keyToUse);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            System.Action act = actionFuncs[actionType];
            act();
            Continue();
        }

        protected override KeyFetcher.FetchArgs PrepareFetchArgs()
        {
            FetchArgs args = base.PrepareFetchArgs();
            args.timerType = timerType;

            return args;
        }

    }
}