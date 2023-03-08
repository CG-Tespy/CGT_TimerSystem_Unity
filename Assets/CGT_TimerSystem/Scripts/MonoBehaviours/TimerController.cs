using UnityEngine;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	public abstract class TimerController : MonoBehaviour, ITimer
	{
        protected virtual void Awake()
        {
            EnsureTimerSysIsThere();
            key = new TimerKey(this);
            RegisterInSystem();
        }

        protected virtual void EnsureTimerSysIsThere()
        {
            timerSys = FindObjectOfType<TimerSystem>();
            bool isThere = timerSys != null;
            if (!isThere)
            {
                GameObject holdsTimerSystem = new GameObject("TimerSystem");
                timerSys = holdsTimerSystem.AddComponent<TimerSystem>();
            }
        }

        protected static TimerSystem timerSys;

        protected TimerKey key;

        protected abstract void RegisterInSystem();

        protected virtual void OnEnable()
        {
            LinkToTimerEvents();
        }

        /// <summary>
        /// So that the UnityEvents here fire off in response to the underlying Timer events
        /// doing the same.
        /// </summary>
        protected virtual void LinkToTimerEvents()
        {
            PrepareEventLinker();
            linker.LinkEvents();
        }

        protected virtual void PrepareEventLinker()
        {
            linker = new TimerEventLinker();
            linker.TimerController = this;
        }

        protected TimerEventLinker linker;

        protected virtual void OnDisable()
        {
            linker.UnlinkEvents();
        }

        /// <summary>
        /// For the timer tied to this controller.
        /// </summary>
        public virtual TimerKey Key { get { return key; } }

        public virtual TimeSpan TimeLeft { get { return CurrentTime; } }

        public TimeSpan CurrentTime { get { return timerSys.GetTimerCurrentTime(key); } }

        public virtual bool IsRunning
        {
            get { return timerSys.IsTimerRunning(key); }
        }

        public virtual float TimeScale
        {
            get { return timerSys.GetTimerTimeScale(key); }

            set { timerSys.SetTimerTimeScale(key, value); }
        }

        public virtual void StartUp()
        {
            timerSys.StartTimer(key);
            Events.OnStart.Invoke();
        }

        public virtual void Stop()
        {
            timerSys.StopTimer(key);
            Events.OnStop.Invoke();
        }

        public virtual void Reset()
        {
            timerSys.ResetTimer(key);
            Events.OnReset.Invoke();
        }

        public virtual void Restart()
        {
            timerSys.RestartTimer(key);
            Events.OnRestart.Invoke();
        }

        public virtual void Tick()
        {
            Debug.LogWarning("Should not call Tick from a TimerController. Does a whole lotta nothin'.");
        }

        public abstract ITimerUnityEvents Events { get; }
    }
}