﻿using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CGT.Unity.TimerSys;
using System.Collections;
using TimeSpan = System.TimeSpan;

namespace TimerSys.Tests
{
    public class TimerManagerTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            GameObject newGo = new GameObject();
            timerManager = newGo.AddComponent<TimerSystem>();
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
            timerManager.SetCountdownFor(key, testDuration);
            timerManager.StartCountdown(key);
        }

        protected TimerSystem timerManager;

        protected TimerKey key = new TimerKey(1);

        protected double milliseconds = 2000.0;
        protected TimeSpan testDuration;

        

        [TearDown]
        public virtual void TearDown()
        {
            timerManager.StopCountdown(key);
            GameObject.Destroy(timerManager.gameObject);
        }
    }
}
