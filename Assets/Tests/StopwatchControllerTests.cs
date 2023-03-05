using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using CGT.Unity.TimerSys;
using TimeSpan = System.TimeSpan;

namespace TimerSysTests
{
    public class StopwatchControllerTests : TimerTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            SetUpStopwatchController();

            controller.StartUp();
            key = controller.Key; // To make sure we're testing the timer the controller is tied to
        }

        protected override void RegisterTimerInSystem()
        {
            // The timer controller should get its thing registered, so we dont need to do it here
        }

        protected virtual void SetUpStopwatchController()
        {
            GameObject controllerGO = new GameObject("StopwatchController");
            controller = controllerGO.AddComponent<StopwatchController>();
        }

        protected StopwatchController controller;

        [UnityTest]
        public virtual IEnumerator RecordsIntendedTime()
        {
            // If they end at roughly the same time, then it's a pass
            yield return new WaitForSeconds(testDuration.Seconds);
            // The countdown should stop itself, so we won't stop it manually here

            Assert.IsTrue(TimeRecordedWithinMarginOfError);
        }

        protected virtual bool TimeRecordedWithinMarginOfError
        {
            get
            {
                double extraTimePassed = System.Math.Abs(CurrentTime.Milliseconds - testDuration.Milliseconds);

                bool withinMarginOfError = extraTimePassed <= endMarginOfError;
                return withinMarginOfError;
            }
        }

        protected float endMarginOfError = 25; // ms

        [UnityTest]
        public virtual IEnumerator ResetsProperly()
        {
            yield return new WaitForSeconds(testDuration.Seconds);
            controller.Reset();
            bool success = StopwatchAtZero;
            Assert.IsTrue(success);
        }

        protected virtual bool StopwatchAtZero { get { return CurrentTime.Equals(TimeSpan.FromTicks(0)); } }

        [UnityTest]
        public virtual IEnumerator RestartsCorrectlyMidRun()
        {
            yield return new WaitForSeconds(testDuration.Seconds / 2);

            controller.Restart();
            Assert.IsTrue(StopwatchAtZero);
        }

        protected float beginMarginOfError = 30;

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