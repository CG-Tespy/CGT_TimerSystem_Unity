﻿using UnityEngine;
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
            timerSystem = newGo.AddComponent<TimerSystem>();
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
        }

        protected TimerSystem timerSystem;

        protected TimerKey key = new TimerKey(1);

        protected double milliseconds = 2000.0;
        protected TimeSpan testDuration;

        [TearDown]
        public virtual void TearDown()
        {
            timerSystem.StopCountdown(key);
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
        
    }
}