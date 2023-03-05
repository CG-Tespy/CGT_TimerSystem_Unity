using UnityEngine;

namespace CGT.Unity.TimerSys
{
    [AddComponentMenu("CGT TimerSys/Stopwatch Controller")]
    public class StopwatchController : TimerController, IStopwatch
	{
        protected override void RegisterInSystem()
        {
            timerSys.RegisterStopwatch(key);
        }

    }
}