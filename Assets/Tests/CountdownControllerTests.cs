using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using TimeSpan = System.TimeSpan;
using CGT.Unity.TimerSys;

namespace TimerSys.Tests
{
    public class CountdownControllerTests : CountdownTests
    {
        [SetUp]
        public override void SetUp()
        {
            SetUpTimerManager();
            base.SetUp();
        }

        protected virtual void SetUpTimerManager()
        {
            GameObject managerGO = new GameObject("TimerManager");
            timerManager = managerGO.AddComponent<TimerManager>();
        }

        protected TimerManager timerManager;

        protected override void SetUpTestCountdown()
        {
            // Since here we're working with a controller, not the actual Countdown object
            GameObject countdownGO = new GameObject("CountdownTimer");
            testCountdown = countdownGO.AddComponent<CountdownController>();
            base.SetUpTestCountdown();
        }

    }
}