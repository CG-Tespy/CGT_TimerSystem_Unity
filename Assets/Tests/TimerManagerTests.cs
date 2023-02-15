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
            timerManager = newGo.AddComponent<MainTimerManager>();
        }

        protected MainTimerManager timerManager;


        [TearDown]
        public virtual void TearDown()
        {
            GameObject.Destroy(timerManager.gameObject);
        }
    }
}
