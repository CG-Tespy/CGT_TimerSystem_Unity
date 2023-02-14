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
    public class CountdownTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
            SetUpTestCountdown();
        }
        protected double milliseconds = 2000;
        protected TimeSpan testDuration;

        protected virtual void SetUpTestCountdown()
        {
            testCountdown.SetFor(testDuration);
            testCountdown.OnFinish = OnCountdownFinish;
        }

        protected Countdown testCountdown = new Countdown();

        [UnityTest]
        public virtual IEnumerator LastsForIntendedTime()
        {
            testCountdown.StartTimer();

            // If they end at roughly the same time, then it's a pass
            yield return new WaitForSeconds(testDuration.Seconds);
            testCountdown.Stop();
            Assert.IsTrue(CountdownWithinEndMarginOfError);
        }

        protected virtual bool CountdownWithinEndMarginOfError
        {
            get
            {
                return System.Math.Abs(testCountdown.CurrentTime.TotalMilliseconds) <=
                endMarginOfError; 
            }
        }

        protected float endMarginOfError = 300; 
        // Hardware timers aren't always so precise, so we need margins of error like this.
        // In particular, this is for when the countdown's supposed to be over

        [UnityTest]
        public virtual IEnumerator StopsAtRightTime()
        {
            TimeSpan halfTestDuration = TimeSpan.FromTicks(testDuration.Ticks / 2);
            testCountdown.StartTimer();

            yield return new WaitForSeconds(halfTestDuration.Seconds);
            testCountdown.Stop();

            double theDiff = testCountdown.TimeLeft.TotalMilliseconds - halfTestDuration.TotalMilliseconds;
            theDiff = System.Math.Abs(theDiff);

            bool success = theDiff <= endMarginOfError;
            Assert.IsTrue(success);
        }

        [UnityTest]
        public virtual IEnumerator ResetsToRightTime()
        {
            testCountdown.StartTimer();

            yield return new WaitForSeconds(testDuration.Seconds);

            testCountdown.ResetTimer();
            Assert.IsTrue(CountdownAtTestDuration);
        }

        protected virtual bool CountdownAtTestDuration
        {
            get { return testCountdown.CurrentTime.Equals(testDuration); }
        }

        protected virtual void OnCountdownFinish(TimerEventArgs args)
        {

        }

        [UnityTest]
        public virtual IEnumerator SetsTimeCorrectlyOnMidRunRestart()
        {
            testCountdown.StartTimer();
            yield return new WaitForSeconds(testDuration.Seconds / 2);

            testCountdown.RestartTimer();
            Assert.IsTrue(CountdownWithinBeginMarginOfError);
        }

        protected virtual bool CountdownWithinBeginMarginOfError
        {
            get { return testDuration.TotalMilliseconds - 
                    testCountdown.TimeLeft.TotalMilliseconds 
                    <= beginMarginOfError; }
        }

        protected float beginMarginOfError = 30;

        [Test]
        public virtual void SetsTimeCorrectlyOnPreRunRestart()
        {
            testCountdown.RestartTimer();
            Assert.IsTrue(CountdownWithinBeginMarginOfError);
        }

        [TearDown]
        public virtual void TearDown()
        {
            testCountdown.Stop();
            testCountdown.ResetTimer();
            testCountdown.OnFinish -= OnCountdownFinish;
        }

    }
}
