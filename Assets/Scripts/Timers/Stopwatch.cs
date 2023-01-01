using System;
using BaseStopwatch = System.Diagnostics.Stopwatch;

namespace CGT.Unity.TimerSys
{
    public class Stopwatch : ObservableTimer
    {
        public override TimeSpan CurrentTime
        {
            get { return baseStopwatch.Elapsed; }
            protected set
            {
                throw new System.InvalidOperationException("Can't rig this Stopwatch's time through CurrentTime");
            }
        }

        protected BaseStopwatch baseStopwatch = new BaseStopwatch();

        public override bool IsRunning
        {
            get { return baseStopwatch.IsRunning; }
        }

        protected override void StartBaseTimer()
        {
            baseStopwatch.Start();
        }

        protected override void StopBaseTimer()
        {
            baseStopwatch.Stop();
        }

        /// <summary>
        /// Sets its time to zero while stopping this from measuring how much time's elapsed.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
        }

        protected override void ResetBaseTimer()
        {
            baseStopwatch.Reset();
        }

        /// <summary>
        /// Has this start measuring again from zero.
        /// </summary>
        public override void Restart()
        {
            base.Restart();
        }

        protected override void RestartBaseTimer()
        {
            baseStopwatch.Restart();
        }
    }
}