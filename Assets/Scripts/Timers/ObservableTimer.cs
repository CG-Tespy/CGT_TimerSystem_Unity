using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
    /// <summary>
    /// Timer that both implements the Observer Pattern and wraps another type
    /// of timer.
    /// </summary>
    public abstract class ObservableTimer : IObservableTimer
    {
        public abstract TimeSpan CurrentTime { get; protected set; }

        public virtual void Start()
        {
            if (IsRunning)
                return;
            StartBaseTimer();
            AlertListenersFor(OnStart);
        }

        public abstract bool IsRunning { get; }

        protected abstract void StartBaseTimer();

        protected virtual void AlertListenersFor(System.Action<TimerEventArgs> theEvent)
        {
            TimerEventArgs eventArgs = new TimerEventArgs();
            eventArgs.Timer = this;
            theEvent.Invoke(eventArgs);
        }

        public Action<TimerEventArgs> OnStart { get; set; } = delegate { };

        public virtual void Stop()
        {
            if (!IsRunning)
                return;
            StopBaseTimer();
            AlertListenersFor(OnStop);
        }

        protected abstract void StopBaseTimer();

        public Action<TimerEventArgs> OnStop { get; set; } = delegate { };

        public virtual void Reset()
        {
            StopBaseTimer();
            ResetBaseTimer();
            AlertListenersFor(OnReset);
        }

        protected abstract void ResetBaseTimer();

        public Action<TimerEventArgs> OnReset { get; set; } = delegate { };

        public virtual void Restart()
        {
            RestartBaseTimer();
            AlertListenersFor(OnRestart);
        }

        protected abstract void RestartBaseTimer();

        public Action<TimerEventArgs> OnRestart { get; set; } = delegate { };

    }
}