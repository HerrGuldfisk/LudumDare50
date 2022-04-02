using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics.Camera
{
    public class CameraShake : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField] float time;
        [ReadOnly]
        [SerializeField] float amplitude;

        public Shake shakeType;

        Coroutine isRunning = null;
        Vector3 originalPosition;

        public enum Shake
        {
            NONE,
            SIMPLE,
            CIRCLE
        }

        public void ShakeCamera(float time, float amplitude)
        {
            switch (shakeType)
            {
                case Shake.SIMPLE:
                    SimpleShake(time, amplitude);
                    break;

                case Shake.CIRCLE:
                    CircleShake(time, amplitude);
                    break;

                default:
                    break;
            }
        }

        public void ShakeCamera(float time, float amplitude, Shake shakeType)
        {
            this.shakeType = shakeType;
            ShakeCamera(time, amplitude);
        }

        /// <summary>
        /// Shakes the camera on the X axis.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="amplitude"></param>
        private void SimpleShake(float time, float amplitude)
        {
            originalPosition = transform.position;
            if(isRunning == null)
            {
                isRunning = StartCoroutine(CSimpleShake(time, amplitude));
            }
        }

        private IEnumerator CSimpleShake(float time, float amplitude)
        {
            float startTime = Time.time;

            while(startTime + time > Time.time)
            {
                transform.position = originalPosition + new Vector3(Random.Range(-amplitude, amplitude), 0, 0) * (1 - (Time.time - startTime) / time);

                int frames = Random.Range(1, 5);

                for(int i = 0; i < frames; i++)
                {
                    yield return null;
                }
            }

            transform.position = originalPosition;
            isRunning = null;
        }

        /// <summary>
        /// Shakes the camera in 2 dimensions (X and Y).
        /// </summary>
        /// <param name="time"></param>
        /// <param name="amplitude"></param>
        private void CircleShake(float time, float amplitude)
        {
            originalPosition = transform.position;
            if (isRunning == null)
            {
                isRunning = StartCoroutine(CCircleShake(time, amplitude));
            }
        }

        private IEnumerator CCircleShake(float time, float amplitude)
        {
            float startTime = Time.time;

            while (startTime + time > Time.time)
            {
                Vector2 randomPointInCirlce = Random.insideUnitCircle * amplitude;

                transform.position = originalPosition + new Vector3(randomPointInCirlce.x, randomPointInCirlce.y, 0) * (1 - (Time.time - startTime) / time);

                int frames = Random.Range(1, 5);

                for (int i = 0; i < frames; i++)
                {
                    yield return null;
                }
            }

            transform.position = originalPosition;
            isRunning = null;
        }
    }
}

