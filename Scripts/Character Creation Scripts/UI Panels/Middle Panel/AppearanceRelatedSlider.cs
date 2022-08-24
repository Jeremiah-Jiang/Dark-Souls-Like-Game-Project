using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    [RequireComponent(typeof(Slider))]
    public class AppearanceRelatedSlider : MonoBehaviour
    {
        protected ColorMenu _colorMenu;
        protected HairColorMenu _hairColorMenu;
        [Header("Slider")]
        protected Slider _slider;

        protected virtual void Awake()
        {
            _colorMenu = GetComponentInParent<ColorMenu>();
            _hairColorMenu = GetComponentInParent<HairColorMenu>();
            _slider = GetComponent<Slider>();
        }
        public void UpdateSlider(float sliderValue)
        {
            _slider.value = sliderValue;
        }

        public float GetSliderValue()
        {
            return _slider.value;
        }

        public void SetSliderValue(float value)
        {
            _slider.value = value;
        }
    }
}

