using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Basics.Helpers
{
    public class CountdownTimer : Timer
    {
        [SerializeField] private float startTime;

        [Tooltip("What the timer will destroy upon expiring.")]
        [SerializeField] TimerDestroy destroyOnExpired = TimerDestroy.Nothing;

        [Space(10)]
        public UnityEvent onTimerExpired = new UnityEvent();

        // Resets the timer and restart it.
        public void StartTimer()
        {
            ResetTimer();
            Resume();
        }

        public void StartTimer(float startTime, TimerDestroy destroyAction = TimerDestroy.Self, bool realTime = false, bool autoStart = true)
        {
            this.startTime = startTime;
            destroyOnExpired = destroyAction;
            this.realTime = realTime;

            ResetTimer();

            if (autoStart)
            {
                Resume();
            }
        }

        // Run each update if the timer "isRunning".
        protected override void TimerTick()
        {
            // If the timer is below 0 ignore rest.
            if (currentTime <= 0) { return; }

            if (realTime)
            {
                currentTime -= Time.unscaledDeltaTime;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }

            if (currentTime <= 0)
            {
                OnTimerExpired();
            }
        }

        protected override void ResetTimer()
        {
            Pause();
            currentTime = startTime;
        }

        private void OnTimerExpired()
        {
            onTimerExpired?.Invoke();

            DestroyTimer(destroyOnExpired);
        }
    }
}

