using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics.Helpers
{
    public abstract class Timer : MonoBehaviour
    {
        [Header("Timer Settings")]
        [Tooltip("If the timer is running.")]
        [SerializeField] protected bool isRunning;

        [Tooltip("If true the timer works with real time instead of unity time.")]
        [SerializeField] protected bool realTime;

        [Header("Timer Data")]
        [ReadOnly]
        [Tooltip("The current time of the timer.")]
        [SerializeField] protected float currentTime;

        // Public getter for the current time of the timer.
        [HideInInspector] public float CurrentTime { get { return currentTime; } }

        // Update loop run in all timers.
        protected void Update()
        {
            // If not running exit the update.
            if (!isRunning) { return; }

            // If running delegate to each timers "TimerTick"
            TimerTick();
        }

        // What the timer does each update if it is running.
        protected abstract void TimerTick();

        // Starting the timer.
        public void Resume()
        {
            isRunning = true;
        }

        public void Pause()
        {
            isRunning = false;
        }

        public void ToggleTimer()
        {
            isRunning = !isRunning;
        }

        protected abstract void ResetTimer();

        protected void DestroyTimer(TimerDestroy toDestroy)
        {
            switch (toDestroy)
            {
                case TimerDestroy.Self:
                    Destroy(this);
                    break;

                case TimerDestroy.GameObject:
                    Destroy(gameObject);
                    break;

                default:
                    break;
            }
        }

        public enum TimerDestroy
        {
            Nothing,
            Self,
            GameObject
        }
    }
}

