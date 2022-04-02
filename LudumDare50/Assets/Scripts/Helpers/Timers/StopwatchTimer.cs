using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Basics.Helpers
{
    public class StopwatchTimer : Timer
    {

        [SerializeField] private List<float> splits = new List<float>();

        public void StartTimer()
        {
            ResetTimer();
            Resume();
        }

        // Only called when timer bool "isRunning" = true
        protected override void TimerTick()
        {
            if (realTime)
            {
                currentTime += Time.unscaledDeltaTime;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }

        protected override void ResetTimer()
        {
            Pause();
            splits.Clear();
            currentTime = 0f;
        }

        public void SetSplit()
        {
            splits.Add(currentTime);
        }

        public float GetLastSplit()
        {
            return splits[splits.Count - 1];
        }

        public List<float> GetSplits()
        {
            return splits;
        }
    }
}

