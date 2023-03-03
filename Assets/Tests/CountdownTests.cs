using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CGT.Unity.TimerSys;
using System.Diagnostics;
using System.Timers;
using TimeSpan = System.TimeSpan;
using Debug = UnityEngine.Debug;
using CDEventManager = CGT.Unity.TimerSys.CountdownManager.CountdownEvents;
using BuiltInMath = System.Math;

namespace TimerSysTests
{
    public class CountdownTests : TimerTests
    {
        public override void SetUp()
        {
            base.SetUp();

            timerSystem.SetCountdownFor(key, testDuration);

            CountdownEvents.ListenForStart(key, OnCountdownStart);
            CountdownEvents.ListenForEnd(key, OnCountdownEnd);
            CountdownEvents.ListenForStop(key, OnCountdownStop);
            CountdownEvents.ListenForReset(key, OnCountdownReset);
            CountdownEvents.ListenForRestart(key, OnCountdownRestart);
            timerSystem.StartTimer(key);
        }

        protected override void RegisterTimerInSystem()
        {
            timerSystem.RegisterCountdown(key);
        }

        protected virtual CDEventManager CountdownEvents { get { return timerSystem.CDEvents; } }

        [UnityTest]
        public virtual IEnumerator CountsDownForIntendedTime()
        {
            // If they end at roughly the same time, then it's a pass
            yield return new WaitForSeconds(testDuration.Seconds);
            // The countdown should stop itself, so we won't stop it manually here

            Assert.IsTrue(TimeElapsedWithinMarginOfError);
        }

        protected virtual bool TimeElapsedWithinMarginOfError
        {
            get
            {
                TimeSpan lastSetFor = timerSystem.GetCountdownTimeLastSetFor(key);
                TimeSpan timeElapsed = lastSetFor - CurrentTime;
                bool atLeastEnoughTimePassed = timeElapsed.TotalMilliseconds >= testDuration.TotalMilliseconds;
                double extraTimePassed = System.Math.Abs(testDuration.TotalMilliseconds - timeElapsed.TotalMilliseconds);

                bool withinMarginOfError = atLeastEnoughTimePassed && extraTimePassed <= endMarginOfError;
                return withinMarginOfError;
            }
        }

        protected override TimeSpan CurrentTime { get { return timerSystem.GetTimerCurrentTime(key); } }

        protected float endMarginOfError = 300;
        // Timers aren't always so precise, so we need margins of error like this.
        // In particular, this is for when the countdown's supposed to be over

        [UnityTest]
        public virtual IEnumerator ResetsProperly()
        {
            yield return new WaitForSeconds(testDuration.Seconds);

            timerSystem.ResetTimer(key);
            Assert.IsTrue(CountdownAtTestDuration);
        }

        protected virtual bool CountdownAtTestDuration { get { return CurrentTime.Equals(testDuration); } }

        [UnityTest]
        public virtual IEnumerator RestartsCorrectlyMidRun()
        {
            yield return new WaitForSeconds(testDuration.Seconds / 2);

            timerSystem.RestartTimer(key);
            Assert.IsTrue(CountdownWithinBeginMarginOfError);
        }

        protected virtual bool CountdownWithinBeginMarginOfError
        {
            get
            {
                TimeSpan timeLeft = CurrentTime;
                return testDuration.TotalMilliseconds - timeLeft.TotalMilliseconds <= beginMarginOfError;
            }
        }

        protected float beginMarginOfError = 30;

        [Test]
        public virtual void RecordsTimeLastSetCorrectly()
        {
            TimeSpan fiftyFiveSeconds = new TimeSpan(0, 0, 55);
            timerSystem.SetCountdownFor(key, fiftyFiveSeconds);
            TimeSpan fromCountdown = CurrentTime;
            bool firstSetSuccess = fromCountdown.Equals(fiftyFiveSeconds);

            TimeSpan twoMinutesTwelveSeconds = new TimeSpan(0, 2, 12);
            timerSystem.SetCountdownFor(key, twoMinutesTwelveSeconds);
            fromCountdown = CurrentTime;
            bool secondSetSuccess = fromCountdown.Equals(twoMinutesTwelveSeconds);

            bool everythingGood = firstSetSuccess && secondSetSuccess;
            Assert.IsTrue(everythingGood);

        }

        [Test]
        public override void TriggersOnStartListeners()
        {
            bool success = countdownStartTriggered;
            Assert.IsTrue(success);
        }

        protected virtual void OnCountdownStart(TimerEventArgs args)
        {
            countdownStartTriggered = true;
        }

        bool countdownStartTriggered = false;

        [UnityTest]
        public virtual  IEnumerator TriggersOnEndListeners()
        {
            yield return new WaitForSeconds(testDuration.Seconds + 0.1f);
            bool success = countdownFinishTriggered;
            Assert.IsTrue(success);

        }

        protected virtual void OnCountdownEnd(TimerEventArgs args)
        {
            countdownFinishTriggered = true;
        }

        protected bool countdownFinishTriggered = false;

        [UnityTest]
        public override IEnumerator TriggersOnStopListeners()
        {
            yield return null;
            timerSystem.StopTimer(key);
            yield return null;
            bool success = countdownStopTriggered;
            Assert.IsTrue(success);
        }

        protected bool countdownStopTriggered = false;

        protected virtual void OnCountdownStop(TimerEventArgs args)
        {
            countdownStopTriggered = true;
        }

        [UnityTest]
        public override IEnumerator TriggersOnResetListeners()
        {
            yield return null;
            timerSystem.ResetTimer(key);
            yield return null;
            bool success = countdownResetTriggered;
            Assert.IsTrue(success);
        }

        protected virtual void OnCountdownReset(TimerEventArgs args)
        {
            countdownResetTriggered = true;
        }

        protected bool countdownResetTriggered = false;

        [UnityTest]
        public override IEnumerator TriggersOnRestartListeners()
        {
            yield return null;
            timerSystem.RestartTimer(key);
            yield return null;
            bool success = countdownRestartTriggered;
            Assert.IsTrue(success);
        }

        protected bool countdownRestartTriggered = false;

        protected virtual void OnCountdownRestart(TimerEventArgs args)
        {
            countdownRestartTriggered = true;
        }

        [UnityTest]
        public override IEnumerator TicksRightBasedOnRaisedTimeScale()
        {
            timerSystem.ResetTimer(key);
            timerSystem.SetTimerTimeScale(key, raisedTimeScale);
            timerSystem.StartTimer(key);

            // We're expecting the countdown to work faster than normal, 
            // meaning it should finish its job sooner than usual
            float lessThanNormal = (testDuration.Seconds / raisedTimeScale);
            lessThanNormal *= alteredTSMarginOfError;
            yield return new WaitForSeconds(lessThanNormal);

            bool success = countdownFinishTriggered;
            Assert.IsTrue(success);
        }

        protected float raisedTimeScale = 1.50f;
        protected float alteredTSMarginOfError = 1.01f; 
        // So we wait ever so slightly more than the expected time, what with how imprecise
        // timers can be


        [UnityTest]
        public override IEnumerator TicksRightBasedOnReducedTimeScale()
        {
            timerSystem.ResetTimer(key);
            timerSystem.SetTimerTimeScale(key, reducedTimeScale);
            timerSystem.StartTimer(key);

            // We're expecting the countdown to work _slower_ than normal, 
            // meaning it should finish its job _later_ than usual
            float moreThanNormal = (testDuration.Seconds / reducedTimeScale);
            moreThanNormal *= alteredTSMarginOfError;
            yield return new WaitForSeconds(moreThanNormal);

            bool success = countdownFinishTriggered;
            Assert.IsTrue(success);
        }

        protected float reducedTimeScale = 0.64f;

        [TearDown]
        public override void TearDown()
        {
            countdownFinishTriggered = countdownStartTriggered =
                countdownStopTriggered = countdownResetTriggered = 
                countdownRestartTriggered = false;
            CountdownEvents.UnlistenForEnd(key, OnCountdownEnd);
            CountdownEvents.UnlistenForStart(key, OnCountdownStart);
            CountdownEvents.UnlistenForStop(key, OnCountdownStop);
            CountdownEvents.UnlistenForReset(key, OnCountdownReset);
            CountdownEvents.UnlistenForRestart(key, OnCountdownRestart);
            base.TearDown();
            
        }
    }
}
