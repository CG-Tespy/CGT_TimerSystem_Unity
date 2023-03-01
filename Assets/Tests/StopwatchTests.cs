using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TimeSpan = System.TimeSpan;
using Debug = UnityEngine.Debug;
using CGT.Unity.TimerSys;
using Math = System.Math;

namespace TimerSysTests
{
    public class StopwatchTests : TimerTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            ListenForEvents();
            timerSystem.StartStopwatch(key);
        }

        protected virtual void ListenForEvents()
        {
            var stopwatchEvents = timerSystem.SWEvents;
            stopwatchEvents.ListenForStart(key, OnStopwatchStart);
            stopwatchEvents.ListenForStop(key, OnStopwatchStop);
            stopwatchEvents.ListenForReset(key, OnStopwatchReset);
            stopwatchEvents.ListenForRestart(key, OnStopwatchRestart);
        }

        protected virtual bool StopwatchWithinEndMarginOfError
        {
            get 
            {
                return Math.Abs(testDuration.TotalMilliseconds - 
                CurrentTime.TotalMilliseconds) <=
                marginOfError;
            }
        }

        protected override TimeSpan CurrentTime { get { return timerSystem.GetStopwatchCurrentTime(key); } }

        protected float marginOfError = 15; // milliseconds

        [UnityTest]
        public virtual IEnumerator ResetsToRightTime()
        {
            yield return new WaitForSeconds(testDuration.Seconds / 1.25f);

            timerSystem.ResetStopwatch(key);
            Assert.IsTrue(AtStartDuration);
        }

        protected virtual bool AtStartDuration
        {
            get { return CurrentTime.Equals(startDuration); }
        }

        protected TimeSpan startDuration = new TimeSpan(0, 0, 0);

        [UnityTest]
        public virtual IEnumerator RestartCausesCountingWithoutRegularStart()
        {
            timerSystem.RestartStopwatch(key);
            yield return new WaitForSeconds(testDuration.Seconds / 2);
            Assert.IsTrue(HasNonZeroTimeMeasured);
        }

        protected virtual bool HasNonZeroTimeMeasured
        {
            get { return CurrentTime.TotalMilliseconds > startDuration.TotalMilliseconds; }
        }

        [UnityTest]
        public virtual IEnumerator RestartResetsTime()
        {
            yield return new WaitForSeconds(testDuration.Seconds / 2);
            timerSystem.RestartStopwatch(key);
            Assert.IsTrue(AtStartDuration);
        }

        protected virtual bool WithinStartDurationMarginOfError
        {
            get { return CurrentTime.TotalMilliseconds <= startMarginOfError; }
        }

        protected float startMarginOfError = 30;

        [Test]
        public override void TriggersOnStartListeners()
        {
            bool success = onStartListenerTriggered;
            Assert.IsTrue(success);
        }

        protected bool onStartListenerTriggered;

        protected virtual void OnStopwatchStart(TimerEventArgs args)
        {
            onStartListenerTriggered = true;
        }

        [UnityTest]
        public override IEnumerator TriggersOnStopListeners()
        {
            yield return null;
            timerSystem.StopStopwatch(key);
            bool success = onStopListenerTriggered;
            Assert.IsTrue(success);
        }

        protected bool onStopListenerTriggered;

        protected virtual void OnStopwatchStop(TimerEventArgs args)
        {
            onStopListenerTriggered = true;
        }

        [UnityTest]
        public override IEnumerator TriggersOnResetListeners()
        {
            yield return null;
            timerSystem.ResetStopwatch(key);
            bool success = onResetListenerTriggered;
            Assert.IsTrue(success);
        }

        protected bool onResetListenerTriggered;

        protected virtual void OnStopwatchReset(TimerEventArgs args)
        {
            onResetListenerTriggered = true;
        }

        [UnityTest]
        public override IEnumerator TriggersOnRestartListeners()
        {
            yield return null;
            timerSystem.RestartStopwatch(key);
            bool success = onRestartListenerTriggered;
            Assert.IsTrue(success);
        }

        protected bool onRestartListenerTriggered;

        protected virtual void OnStopwatchRestart(TimerEventArgs args)
        {
            onRestartListenerTriggered = true;
        }

        [TearDown]
        public override void TearDown()
        {
            timerSystem.StopStopwatch(key);
            timerSystem.ResetStopwatch(key);
            UnlistenForEvents();
            onStopListenerTriggered = onResetListenerTriggered =
                onRestartListenerTriggered = onStartListenerTriggered = false;
            base.TearDown();
        }

        protected virtual void UnlistenForEvents()
        {
            var stopwatchEvents = timerSystem.SWEvents;
            stopwatchEvents.UnlistenForStart(key, OnStopwatchStart);
            stopwatchEvents.UnlistenForStop(key, OnStopwatchStop);
            stopwatchEvents.UnlistenForReset(key, OnStopwatchReset);
            stopwatchEvents.UnlistenForRestart(key, OnStopwatchRestart);
        }

        [UnityTest]
        public override IEnumerator TicksRightBasedOnRaisedTimeScale()
        {
            timerSystem.ResetStopwatch(key);
            timerSystem.SetStopwatchTimeScale(key, raisedTimeScale);
            timerSystem.StartStopwatch(key);

            // We're expecting the countdown to work faster than normal, 
            // meaning it should finish its job sooner than usual
            float lessThanNormal = (testDuration.Seconds / raisedTimeScale);
            lessThanNormal += alteredTSMarginOfError * 0.001f;
            yield return new WaitForSeconds(lessThanNormal);

            float excessTime = Mathf.Abs(CurrentTime.Seconds - testDuration.Seconds);
            bool success = excessTime <= alteredTSMarginOfError * 0.001f;
            Assert.IsTrue(success);
        }

        protected float raisedTimeScale = 1.50f;
        protected float alteredTSMarginOfError = 25; // in ms


        [UnityTest]
        public override IEnumerator TicksRightBasedOnReducedTimeScale()
        {
            timerSystem.ResetCountdown(key);
            timerSystem.SetStopwatchTimeScale(key, reducedTimeScale);
            timerSystem.StartStopwatch(key);

            // We're expecting the stop to work _slower_ than normal, 
            // meaning it should _take longer_ reach the test duration
            float moreThanNormal = (testDuration.Seconds / reducedTimeScale);
            moreThanNormal += alteredTSMarginOfError * 0.001f;
            yield return new WaitForSeconds(moreThanNormal);

            float excessTime = Mathf.Abs(CurrentTime.Seconds - testDuration.Seconds);
            bool success = excessTime <= alteredTSMarginOfError * 0.001f;
            Assert.IsTrue(success);
        }

        protected float reducedTimeScale = 0.64f;
    }
}
