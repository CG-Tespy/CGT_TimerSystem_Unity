using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using TimeSpan = System.TimeSpan;
using CGT.Unity.TimerSys;

namespace TimerSysTests
{
    public class CountdownControllerTests : TimerTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            SetUpCountdownController();
            
            controller.SetFor(testDuration);
            controller.StartUp();
            key = controller.Key; // To make sure we're testing the timer the controller is tied to
        }

        protected override void RegisterTimerInSystem()
        {
            // The timer controller should get its thing registered, so we dont need to do it here
        }

        protected virtual void SetUpCountdownController()
        {
            GameObject controllerGO = new GameObject("CountdownController");
            controller = controllerGO.AddComponent<CountdownController>();
        }

        protected CountdownController controller;

        [UnityTest]
        public virtual IEnumerator CountsDownForIntendedTime()
        {
            // If they end at roughly the same time, then it's a pass
            yield return new WaitForSeconds(testDuration.Seconds);
            // The countdown should stop itself, so we won't stop it manually here

            Assert.IsTrue(TimeElapsedWithinMarginOfError);
        }

        protected virtual bool TimeElapsedWithinMarginOfError
        {
            get
            {
                TimeSpan lastSetFor = controller.LastSetFor;
                TimeSpan timeElapsed = lastSetFor - CurrentTime;
                bool atLeastEnoughTimePassed = timeElapsed.TotalMilliseconds >= testDuration.TotalMilliseconds;
                double extraTimePassed = System.Math.Abs(testDuration.TotalMilliseconds - timeElapsed.TotalMilliseconds);

                bool withinMarginOfError = atLeastEnoughTimePassed && extraTimePassed <= endMarginOfError;
                return withinMarginOfError;
            }
        }

        protected float endMarginOfError = 300;


        [UnityTest]
        public virtual IEnumerator ResetsProperly()
        {
            yield return new WaitForSeconds(testDuration.Seconds);

            //controller.Reset();
            timerSystem.ResetTimer(key);
            bool success = CountdownAtTestDuration;
            Assert.IsTrue(success);
        }

        protected virtual bool CountdownAtTestDuration { get { return CurrentTime.Equals(testDuration); } }

        [UnityTest]
        public virtual IEnumerator RestartsCorrectlyMidRun()
        {
            yield return new WaitForSeconds(testDuration.Seconds / 2);

            controller.Restart();
            Assert.IsTrue(CountdownWithinBeginMarginOfError);
        }

        protected virtual bool CountdownWithinBeginMarginOfError
        {
            get
            {
                TimeSpan timeLeft = CurrentTime;
                double excessTime = System.Math.Abs(testDuration.TotalMilliseconds - timeLeft.TotalMilliseconds);
                bool result = excessTime <= beginMarginOfError;
                return result;
            }
        }

        protected float beginMarginOfError = 30;

        [Test]
        public virtual void RecordsTimeLastSetCorrectly()
        {
            TimeSpan fiftyFiveSeconds = new TimeSpan(0, 0, 55);
            controller.SetFor(fiftyFiveSeconds);
            TimeSpan fromCountdown = CurrentTime;
            bool firstSetSuccess = fromCountdown.Equals(fiftyFiveSeconds);

            TimeSpan twoMinutesTwelveSeconds = new TimeSpan(0, 2, 12);
            controller.SetFor(twoMinutesTwelveSeconds);
            fromCountdown = CurrentTime;
            bool secondSetSuccess = fromCountdown.Equals(twoMinutesTwelveSeconds);

            bool everythingGood = firstSetSuccess && secondSetSuccess;
            Assert.IsTrue(everythingGood);

        }

        [Test]
        [Ignore("Controller not listenable")]
        public override void TriggersOnStartListeners()
        {
            throw new System.NotImplementedException();
        }

        [UnityTest]
        [Ignore("Controller not listenable")]
        public override IEnumerator TriggersOnStopListeners()
        {
            throw new System.NotImplementedException();
        }

        [UnityTest]
        [Ignore("Controller not listenable")]
        public override IEnumerator TriggersOnResetListeners()
        {
            throw new System.NotImplementedException();
        }

        [UnityTest]
        [Ignore("Controller not listenable")]
        public override IEnumerator TriggersOnRestartListeners()
        {
            throw new System.NotImplementedException();
        }

        [UnityTest]
        [Ignore("Tick-checks are for the underlying timer, not the controller")]
        public override IEnumerator TicksRightBasedOnRaisedTimeScale()
        {
            throw new System.NotImplementedException();
        }

        [UnityTest]
        [Ignore("Tick-checks are for the underlying timer, not the controller")]
        public override IEnumerator TicksRightBasedOnReducedTimeScale()
        {
            throw new System.NotImplementedException();
        }

        

        protected override TimeSpan CurrentTime
        {
            get { return controller.CurrentTime; }
        }

        [TearDown]
        public override void TearDown()
        {
            controller.Stop();
            base.TearDown();
            GameObject.Destroy(controller.gameObject);
        }

    }
}