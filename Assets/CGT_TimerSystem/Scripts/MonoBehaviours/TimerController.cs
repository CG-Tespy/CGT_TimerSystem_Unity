using UnityEngine;
using UnityEngine.Events;
using TimeSpan = System.TimeSpan;

namespace CGT.Unity.TimerSys
{
	public abstract class TimerController : MonoBehaviour, ITimer
	{
        protected virtual void Awake()
        {
            EnsureTimerSysIsThere();
            timerSys = FindObjectOfType<TimerSystem>();
            key = new TimerKey(this);
            RegisterInSystem();
        }

        protected virtual void EnsureTimerSysIsThere()
        {
            bool isThere = FindObjectOfType<TimerSystem>() != null;
            if (!isThere)
            {
                GameObject holdsTimerSystem = new GameObject("TimerSystem");
                holdsTimerSystem.AddComponent<TimerSystem>();
            }
        }

        protected static TimerSystem timerSys;

        protected TimerKey key;

        protected abstract void RegisterInSystem();

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
            OnStart.Invoke();
        }

        public virtual void Stop()
        {
            timerSys.StopTimer(key);
            OnStop.Invoke();
        }

        public virtual void Reset()
        {
            timerSys.ResetTimer(key);
            OnReset.Invoke();
        }

        public virtual void Restart()
        {
            timerSys.RestartTimer(key);
            OnRestart.Invoke();
        }

        public virtual void Tick()
        {
            Debug.LogWarning("Should not call Tick from a TimerController. Does a whole lotta nothin'.");
        }

        [SerializeField]
        protected UnityEvent OnStart = new UnityEvent(), OnStop = new UnityEvent(),
            OnReset = new UnityEvent(), OnRestart = new UnityEvent();

    }
}