using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TimeSpan = System.TimeSpan;
using Debug = UnityEngine.Debug;
using CGT.Unity.TimerSys;
using Math = System.Math;

namespace TimerSys.Tests
{
    public class StopwatchTests : TimerTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            timerManager.StartStopwatch(key);
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

        protected virtual TimeSpan CurrentTime { get { return timerManager.GetStopwatchCurrentTime(key); } }

        protected float marginOfError = 15; // milliseconds

        [UnityTest]
        public virtual IEnumerator ResetsToRightTime()
        {
            yield return new WaitForSeconds(testDuration.Seconds / 1.25f);

            timerManager.ResetStopwatch(key);
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
            timerManager.RestartStopwatch(key);
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
            yield return new WaitForSeconds(testDuration.Seconds);

            timerManager.RestartStopwatch(key);
            Assert.IsTrue(WithinStartDurationMarginOfError);
        }

        protected virtual bool WithinStartDurationMarginOfError
        {
            get { return CurrentTime.TotalMilliseconds <= startMarginOfError; }
        }

        protected float startMarginOfError = 30;

        [TearDown]
        public override void TearDown()
        {
            timerManager.StopStopwatch(key);
            timerManager.ResetStopwatch(key);
            base.TearDown();
        }

        [Test]
        [Ignore("")]
        public override void TriggersOnStartListeners()
        {
            throw new System.NotImplementedException();
        }

        [UnityTest]
        [Ignore("")]
        public override IEnumerator TriggersOnStopListeners()
        {
            throw new System.NotImplementedException();
        }

        [UnityTest]
        [Ignore("")]
        public override IEnumerator TriggersOnResetListeners()
        {
            throw new System.NotImplementedException();
        }

        [UnityTest]
        [Ignore("")]
        public override IEnumerator TriggersOnRestartListeners()
        {
            throw new System.NotImplementedException();
        }
    }
}
