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

namespace TimerSys.Tests
{
    public class CountdownTests : TimerTests
    {
        public override void SetUp()
        {
            base.SetUp();

            timerManager.SetCountdownFor(key, testDuration);
            timerManager.ListenForCountdownStart(key, OnCountdownStart);
            timerManager.ListenForCountdownEnd(key, OnCountdownEnd);
            timerManager.ListenForCountdownStop(key, OnCountdownStop);
            timerManager.ListenForCountdownReset(key, OnCountdownReset);
            timerManager.ListenForCountdownRestart(key, OnCountdownRestart);
            timerManager.StartCountdown(key);
        }


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
                TimeSpan lastSetFor = timerManager.GetCountdownTimeLastSetFor(key);
                TimeSpan currentTime = timerManager.GetCountdownCurrentTime(key);
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

            timerManager.ResetCountdown(key);
            Assert.IsTrue(CountdownAtTestDuration);
        }

        protected virtual bool CountdownAtTestDuration
        {
            get
            {
                TimeSpan currentTime = timerManager.GetCountdownCurrentTime(key);
                return currentTime.Equals(testDuration);
            }
        }

        [UnityTest]
        public virtual IEnumerator RestartsCorrectlyMidRun()
        {
            yield return new WaitForSeconds(testDuration.Seconds / 2);

            timerManager.RestartCountdown(key);
            Assert.IsTrue(CountdownWithinBeginMarginOfError);
        }

        protected virtual bool CountdownWithinBeginMarginOfError
        {
            get
            {
                TimeSpan timeLeft = timerManager.GetCountdownCurrentTime(key);
                return testDuration.TotalMilliseconds - timeLeft.TotalMilliseconds <= beginMarginOfError;
            }
        }

        protected float beginMarginOfError = 30;

        [Test]
        public virtual void RecordsTimeLastSetCorrectly()
        {
            TimeSpan fiftyFiveSeconds = new TimeSpan(0, 0, 55);
            timerManager.SetCountdownFor(key, fiftyFiveSeconds);
            TimeSpan fromCountdown = timerManager.GetCountdownCurrentTime(key);
            bool firstSetSuccess = fromCountdown.Equals(fiftyFiveSeconds);

            TimeSpan twoMinutesTwelveSeconds = new TimeSpan(0, 2, 12);
            timerManager.SetCountdownFor(key, twoMinutesTwelveSeconds);
            fromCountdown = timerManager.GetCountdownCurrentTime(key);
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
            timerManager.StopCountdown(key);
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
            timerManager.ResetCountdown(key);
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
            timerManager.RestartCountdown(key);
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
            timerManager.UnlistenForCountdownEnd(key, OnCountdownEnd);
            timerManager.UnlistenForCountdownStart(key, OnCountdownStart);
            timerManager.UnlistenForCountdownStop(key, OnCountdownStop);
            timerManager.UnlistenForCountdownReset(key, OnCountdownReset);
            timerManager.UnlistenForCountdownRestart(key, OnCountdownRestart);
            base.TearDown();
            
        }
    }
}
