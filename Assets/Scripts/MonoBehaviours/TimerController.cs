using UnityEngine;
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

        protected abstract void RegisterInSystem();

        protected TimerKey key;

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

        protected static TimerSystem timerSys;

        public virtual void StartUp()
        {
            timerSys.StartTimer(key);
        }

        public virtual void Reset()
        {
            timerSys.ResetTimer(key);
        }

        public virtual void Stop()
        {
            timerSys.StopTimer(key);
        }

        public virtual void Restart()
        {
            timerSys.RestartTimer(key);
        }

        public virtual void Tick()
        {
            Debug.LogWarning("Should not call Tick from a TimerController. Does a whole lotta nothin'.");
        }

    }
}