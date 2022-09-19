using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class BrazierDecoration : MonoBehaviour
    {
        public Light lightSource;
        private float initialIntensity = 2.0f;
        private float[] smoothing = new float[10];

        private void Awake()
        {
            lightSource = GetComponentInChildren<Light>();
            for(int i = 0; i < smoothing.Length; i++)
            {
                smoothing[i] = 0.0f;
            }
        }

        private void Update()
        {
            float sum = 0.0f;

            for(int i = 1; i < smoothing.Length; i++)
            {
                smoothing[i - 1] = smoothing[i];
                sum += smoothing[i - 1];
            }

            smoothing[smoothing.Length - 1] = Random.Range(2.0f, 3.5f);
            sum += smoothing[smoothing.Length - 1];

            lightSource.intensity = sum/smoothing.Length;
        }
    }
}

