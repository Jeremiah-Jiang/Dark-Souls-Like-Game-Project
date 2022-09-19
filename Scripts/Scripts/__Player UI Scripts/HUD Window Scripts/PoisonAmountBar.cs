using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class PoisonAmountBar : MonoBehaviour
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
            slider.value = 100;
        }

        private void Start()
        {
            gameObject.SetActive(false); //this must be here otherwise we wont be able to get component
        }
        public void SetPoisonAmountOnSlider(int poisonAmount)
        {
            slider.value = poisonAmount;
        }

    }
}


