using NUnit.Framework;
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
            timerManager = newGo.AddComponent<MainTimerManager>();
            testDuration = TimeSpan.FromMilliseconds(milliseconds);
            timerManager.SetCountdownFor(key, testDuration);
        }

        protected MainTimerManager timerManager;

        protected TimerKey key = new TimerKey(1);

        protected double milliseconds = 2000.0;
        protected TimeSpan testDuration;

        [UnityTest]
        public virtual IEnumerator TimerLastsForIntendedTime()
        {
            
            timerManager.StartCountdown(key);

            // If they end at roughly the same time, then it's a pass
            yield return new WaitForSeconds(testDuration.Seconds);
            // The countdown should stop itself, so we won't stop it manually here

            Assert.IsTrue(TimeElapsedWithinMarginOfError);
        }

        protected virtual bool TimeElapsedWithinMarginOfError
        {
            get
            {
                TimeSpan lastSetFor = timerManager.GetCountdownTimeLastSetFor(key);
                TimeSpan currentTime = timerManager.GetCountdownCurrentTime(key);
                Debug.Log("Milliseconds left after stopping: " + currentTime.TotalMilliseconds);
                TimeSpan timeElapsed = lastSetFor - currentTime;
                Debug.Log("Total time elapsed in milliseconds: " + timeElapsed.TotalMilliseconds);
                bool atLeastEnoughTimePassed = timeElapsed.TotalMilliseconds >= testDuration.TotalMilliseconds;
                double extraTimePassed = System.Math.Abs(testDuration.TotalMilliseconds - timeElapsed.TotalMilliseconds);
                
                bool withinMarginOfError = atLeastEnoughTimePassed && extraTimePassed <= endMarginOfError;
                return withinMarginOfError;
            }
        }

        protected float endMarginOfError = 300;
        // Timers aren't always so precise, so we need margins of error like this.
        // In particular, this is for when the countdown's supposed to be over


        [TearDown]
        public virtual void TearDown()
        {
            GameObject.Destroy(timerManager.gameObject);
        }
    }
}
