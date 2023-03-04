using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using TimeSpan = System.TimeSpan;
using CGT.Unity.TimerSys;

namespace TimerSysTests
{
    public abstract class TimerTests
    {

        [SetUp]
        public virtual void SetUp()
        {
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
            GameObject newGo = new GameObject("TimerSystem");
            timerSystem = newGo.AddComponent<TimerSystem>();
            RegisterTimerInSystem();
        }

        protected abstract void RegisterTimerInSystem();

        protected TimerSystem timerSystem;

        protected TimerKey key = new TimerKey(1);

        protected double milliseconds = 2000.0;
        protected TimeSpan testDuration;

        [TearDown]
        public virtual void TearDown()
        {
            timerSystem.StopTimer(key);
            GameObject.Destroy(timerSystem.gameObject);
        }

        [Test]
        public abstract void TriggersOnStartListeners();

        [UnityTest]
        public abstract IEnumerator TriggersOnStopListeners();

        [UnityTest]
        public abstract IEnumerator TriggersOnResetListeners();

        [UnityTest]
        public abstract IEnumerator TriggersOnRestartListeners();

        [UnityTest]
        public abstract IEnumerator TicksRightBasedOnRaisedTimeScale();

        [UnityTest]
        public abstract IEnumerator TicksRightBasedOnReducedTimeScale();

        protected abstract TimeSpan CurrentTime { get; }
    }
}