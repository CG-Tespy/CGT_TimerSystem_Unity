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

namespace TimerSys.Tests
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
            timerSystem.StartCountdown(key);
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
                TimeSpan currentTime = timerSystem.GetCountdownCurrentTime(key);
                TimeSpan timeElapsed = lastSetFor - currentTime;
                bool atLeastEnoughTimePassed = timeElapsed.TotalMilliseconds >= testDuration.TotalMilliseconds;
                double extraTimePassed = System.Math.Abs(testDuration.TotalMilliseconds - timeElapsed.TotalMilliseconds);

                bool withinMarginOfError = atLeastEnoughTimePassed && extraTimePassed <= endMarginOfError;
                return withinMarginOfError;
            }
        }

        protected float endMarginOfError = 300;
        // Timers aren't always so precise, so we need margins of error like this.
        // In particular, this is for when the countdown's supposed to be over

        [UnityTest]
        public virtual IEnumerator ResetsProperly()
        {
            yield return new WaitForSeconds(testDuration.Seconds);

            timerSystem.ResetCountdown(key);
            Assert.IsTrue(CountdownAtTestDuration);
        }

        protected virtual bool CountdownAtTestDuration
        {
            get
            {
                TimeSpan currentTime = timerSystem.GetCountdownCurrentTime(key);
                return currentTime.Equals(testDuration);
            }
        }

        [UnityTest]
        public virtual IEnumerator RestartsCorrectlyMidRun()
        {
            yield return new WaitForSeconds(testDuration.Seconds / 2);

            timerSystem.RestartCountdown(key);
            Assert.IsTrue(CountdownWithinBeginMarginOfError);
        }

        protected virtual bool CountdownWithinBeginMarginOfError
        {
            get
            {
                TimeSpan timeLeft = timerSystem.GetCountdownCurrentTime(key);
                return testDuration.TotalMilliseconds - timeLeft.TotalMilliseconds <= beginMarginOfError;
            }
        }

        protected float beginMarginOfError = 30;

        [Test]
        public virtual void RecordsTimeLastSetCorrectly()
        {
            TimeSpan fiftyFiveSeconds = new TimeSpan(0, 0, 55);
            timerSystem.SetCountdownFor(key, fiftyFiveSeconds);
            TimeSpan fromCountdown = timerSystem.GetCountdownCurrentTime(key);
            bool firstSetSuccess = fromCountdown.Equals(fiftyFiveSeconds);

            TimeSpan twoMinutesTwelveSeconds = new TimeSpan(0, 2, 12);
            timerSystem.SetCountdownFor(key, twoMinutesTwelveSeconds);
            fromCountdown = timerSystem.GetCountdownCurrentTime(key);
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
            timerSystem.StopCountdown(key);
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
            timerSystem.ResetCountdown(key);
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
            timerSystem.RestartCountdown(key);
            yield return null;
            bool success = countdownRestartTriggered;
            Assert.IsTrue(success);
        }

        protected bool countdownRestartTriggered = false;

        protected virtual void OnCountdownRestart(TimerEventArgs args)
        {
            countdownRestartTriggered = true;
        }

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
