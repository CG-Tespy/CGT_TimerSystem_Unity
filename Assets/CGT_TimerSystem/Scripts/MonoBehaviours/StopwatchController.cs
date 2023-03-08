using UnityEngine;

namespace CGT.Unity.TimerSys
{
    [AddComponentMenu("CGT TimerSys/Stopwatch Controller")]
    public class StopwatchController : TimerController, IStopwatch
	{
        [SerializeField]
        protected TimerUnityEvents events = new TimerUnityEvents();

        public override ITimerUnityEvents Events { get { return events; } }

        protected override void RegisterInSystem()
        {
            timerSys.RegisterStopwatch(key);
        }

        protected override void PrepareEventLinker()
        {
            base.PrepareEventLinker();
            linker.TimerEvents = timerSys.SWEvents;
        }

    }
}