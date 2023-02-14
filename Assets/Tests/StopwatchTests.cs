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
    public class StopwatchTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
        }
        protected double milliseconds = 2000;
        protected TimeSpan testDuration;

        protected Stopwatch testStopwatch = new Stopwatch();

        [UnityTest]
        public IEnumerator StopsAtRightTime()
        {
            testStopwatch.StartUp();

            // If they end at roughly the same time, then it's a pass
            yield return new WaitForSeconds(testDuration.Seconds);
            testStopwatch.Stop();
            Assert.IsTrue(StopwatchWithinEndMarginOfError);
        }

        protected virtual bool StopwatchWithinEndMarginOfError
        {
            get {  return Math.Abs(testDuration.TotalMilliseconds - 
                testStopwatch.CurrentTime.TotalMilliseconds) <=
                marginOfError;
            }
        }

        protected float marginOfError = 15; // milliseconds

        [UnityTest]
        public virtual IEnumerator ResetsToRightTime()
        {
            testStopwatch.StartUp();

            yield return new WaitForSeconds(testDuration.Seconds / 1.25f);

            testStopwatch.Reset();
            Assert.IsTrue(AtStartDuration);
        }

        protected virtual bool AtStartDuration
        {
            get { return testStopwatch.CurrentTime.Equals(startDuration); }
        }

        protected TimeSpan startDuration;

        [UnityTest]
        public virtual IEnumerator RestartStartCountingBeforeregularStart()
        {
            testStopwatch.Restart();
            yield return new WaitForSeconds(testDuration.Seconds / 2);
            Assert.IsTrue(HasNonZeroTimeMeasured);
        }

        protected virtual bool HasNonZeroTimeMeasured
        {
            get { return testStopwatch.CurrentTime.TotalMilliseconds > startDuration.TotalMilliseconds; }
        }

        [UnityTest]
        public virtual IEnumerator RestartResetsTimeAfterRegularStart()
        {
            testStopwatch.StartUp();
            yield return new WaitForSeconds(testDuration.Seconds);

            testStopwatch.Restart();
            Assert.IsTrue(WithinStartDurationMarginOfError);
        }

        protected virtual bool WithinStartDurationMarginOfError
        {
            get { return testStopwatch.CurrentTime.TotalMilliseconds <= startMarginOfError; }
        }

        protected float startMarginOfError = 30;

        [TearDown]
        public virtual void TearDown()
        {
            testStopwatch.Stop();
            testStopwatch.Reset();
        }
    }
}
