using System.Collections.Generic;
using UnityEngine;
using Fungus;

namespace CGT.Unity.TimerSys.Fungus
{
    [CommandInfo("CGT TimerSys", "TimerAction", "For having timers do things that each type can do.")]
	public class TimerAction : TimerCommand
	{
        [SerializeField]
        protected GeneralActionType actionType;

        public enum GeneralActionType
        {
            start, stop, reset, restart
        }

        protected override void Awake()
        {
            base.Awake();
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
            timerSys.StartTimer(KeyToUse);
        }

        protected virtual void ActionStop()
        {
            timerSys.StopTimer(KeyToUse);
        }

        protected virtual void ActionReset()
        {
            timerSys.ResetTimer(KeyToUse);
        }

        protected virtual void ActionRestart()
        {
            timerSys.RestartTimer(KeyToUse);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            System.Action act = actionFuncs[actionType];
            act();
            Continue();
        }

    }
}