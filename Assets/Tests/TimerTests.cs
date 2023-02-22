using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using TimeSpan = System.TimeSpan;
using CGT.Unity.TimerSys;

namespace TimerSys.Tests
{
    public abstract class TimerTests
    {

        [SetUp]
        public virtual void SetUp()
        {
            GameObject newGo = new GameObject();
            timerManager = newGo.AddComponent<MainTimerManager>();
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
        }

        protected MainTimerManager timerManager;

        protected TimerKey key = new TimerKey(1);

        protected double milliseconds = 2000.0;
        protected TimeSpan testDuration;

        [TearDown]
        public virtual void TearDown()
        {
            timerManager.StopCountdown(key);
            GameObject.Destroy(timerManager.gameObject);
        }

        [Test]
        public abstract void TriggersOnStartListeners();

        [UnityTest]
        public abstract IEnumerator TriggersOnStopListeners();

        [UnityTest]
        public abstract IEnumerator TriggersOnResetListeners();

        [UnityTest]
        public abstract IEnumerator TriggersOnRestartListeners();

        
    }
}