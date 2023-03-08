using System;
using UnityEngine;
using UnityEngine.Events;

namespace CGT.Unity.TimerSys
{
    [AddComponentMenu("CGT TimerSys/Countdown Controller")]
    public class CountdownController : TimerController, ICountdown
	{
        [SerializeField]
        protected CountdownUnityEvents events = new CountdownUnityEvents();

        public override ITimerUnityEvents Events { get { return events; } }
        public virtual UnityEvent OnEnd { get { return events.OnEnd; } }


        [SerializeField]
        protected TSTimeSpan startingTime = new TSTimeSpan(0, 0, 0, 10, 100);

        protected override void Awake()
        {
            base.Awake();
            SetFor(startingTime.ToNormalTimeSpan());
            ListenForTimerEnd();
        }

        protected virtual void ListenForTimerEnd()
        {
            timerSys.CDEvents.ListenForEnd(key, OnTimerEnd);
        }

        protected virtual void OnTimerEnd(TimerEventArgs args)
        {
            events.OnEnd.Invoke();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnlistenForTimerEnd();
        }

        protected virtual void UnlistenForTimerEnd()
        {
            timerSys.CDEvents.UnlistenForEnd(key, OnTimerEnd);
        }

        protected virtual void OnDestroy()
        {
            UnlistenForTimerEnd();
        }

        protected override void RegisterInSystem()
        {
            timerSys.RegisterCountdown(key);
        }

        public virtual void SetFor(TimeSpan duration)
        {
            timerSys.SetCountdownFor(key, duration);
        }

        public virtual TimeSpan LastSetFor
        {
            get { return timerSys.GetCountdownTimeLastSetFor(key); }
        }

        protected override void PrepareEventLinker()
        {
            base.PrepareEventLinker();
            linker.TimerEvents = timerSys.CDEvents;
        }
    }
}