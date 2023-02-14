using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using TimeSpan = System.TimeSpan;
using CGT.Unity.TimerSys;

namespace TimerSys.Tests
{
    public class CountdownComponentTests : CountdownTests
    {
        [SetUp]
        public override void SetUp()
        {
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
            SetUpTimerManager();
            SetUpCountdownComponent();

            testCountdown.SetFor(testDuration);
            testCountdown.OnFinish = OnCountdownFinish;
        }

        protected virtual void SetUpTimerManager()
        {
            GameObject managerGO = new GameObject("TimerManager");
            timerManager = managerGO.AddComponent<TimerManager>();
        }

        protected TimerManager timerManager;

        protected virtual void SetUpCountdownComponent()
        {
            GameObject countdownGO = new GameObject("CountdownTimer");
            countdownGO.AddComponent<CountdownController>();
        }

        protected CountdownController countdownComponent;

        protected override void SetUpTestCountdown()
        {
            // We are not using a Countdown object directly, but rather, working with
            // one using the TimerManager as a middleman. Thus, we leave this func empty.

        }


        [UnityTest]
        public override IEnumerator LastsForIntendedTime()
        {
            countdownComponent.StartTimer();

            // If they end at roughly the same time, then it's a pass
            yield return new WaitForSeconds(testDuration.Seconds);
            testCountdown.Stop();
            Assert.IsTrue(CountdownWithinEndMarginOfError);
        }
    }
}