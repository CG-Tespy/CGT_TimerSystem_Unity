namespace CGT.Unity.TimerSys
{
	public class StopwatchController : TimerController, IStopwatch
	{
        protected override void RegisterInSystem()
        {
            timerSys.RegisterStopwatch(key);
        }

    }
}