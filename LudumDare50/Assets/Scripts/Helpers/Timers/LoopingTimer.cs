using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Basics.Helpers
{
    public class LoopingTimer : Timer
    {
        public bool isLooping = true;

        [SerializeField] private float loopTime;

        [Space(10)]
        public UnityEvent onTimerExpired = new UnityEvent();

        // Resets the timer and restart it.
        public void StartTimer()
        {
            ResetTimer();
            Resume();
        }

        public void StartTimer(float loopTime, bool realTime = false, bool looping = true, bool autoStart = true)
        {
            this.loopTime = loopTime;
            this.realTime = realTime;
            isLooping = looping;

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
            currentTime = loopTime;
        }

        private void OnTimerExpired()
        {
            onTimerExpired?.Invoke();

            if (isLooping)
            {
                StartTimer();
            }
            else
            {
                Pause();
            }
            
        }
    }
}

