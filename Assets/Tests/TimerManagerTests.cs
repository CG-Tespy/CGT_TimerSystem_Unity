using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CGT.Unity.TimerSys;

namespace TimerSys.Tests
{
    public class TimerManagerTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            GameObject newGo = new GameObject();
            timerManager = newGo.AddComponent<TimerManager>();
        }

        protected TimerManager timerManager;

        [Test]
        public virtual void StartsWithRightCountdownAmounts()
        {
            bool success = timerManager.CountdownCount == startingTimerPerTypeCount;
            Assert.IsTrue(success);
        }

        protected int startingTimerPerTypeCount = 10;

        [Test]
        public virtual void StartsWithRightStopwatchAmounts()
        {
            bool success = timerManager.StopwatchCount == startingTimerPerTypeCount;
            Assert.IsTrue(success);
        }

        [TearDown]
        public virtual void TearDown()
        {
            GameObject.Destroy(timerManager.gameObject);
        }
    }
}
