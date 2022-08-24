using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class PoisonBuildUpBar : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            if (slider == null)
            {
                Debug.LogError("Slider is NULL");
            }
            slider.maxValue = 100;
            slider.value = 0;
        }

        private void Start()
        {
            gameObject.SetActive(false); //this must be here otherwise we wont be able to get component
        }
        public void SetPoisonBuildUpAmount(int currentPoisonBuildUp)
        {
            slider.value = currentPoisonBuildUp;
        }

    }
}


