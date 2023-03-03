using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CGT.Unity.TimerSys;
using System.Collections;
using TimeSpan = System.TimeSpan;

namespace TimerSysTests
{
    public class TimerSystemMonoBehaviourTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            GameObject newGo = new GameObject();
            timerManager = newGo.AddComponent<TimerSystem>();
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
            timerManager.SetCountdownFor(key, testDuration);
            timerManager.StartTimer(key);
        }

        protected TimerSystem timerManager;

        protected TimerKey key = new TimerKey(1);

        protected double milliseconds = 2000.0;
        protected TimeSpan testDuration;

        [TearDown]
        public virtual void TearDown()
        {
            timerManager.StopTimer(key);
            GameObject.Destroy(timerManager.gameObject);
        }
    }
}
