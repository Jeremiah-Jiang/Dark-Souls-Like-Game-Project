using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class ColorSliderParent : MonoBehaviour
    {
        [SerializeField]
        protected ColorMenu _colorMenu;
        [SerializeField]
        protected RedColorSlider _redSlider;
        [SerializeField]
        protected GreenColorSlider _greenSlider;
        [SerializeField]
        protected BlueColorSlider _blueSlider;

        private void Awake()
        {
            _colorMenu = GetComponentInParent<ColorMenu>();
            _redSlider = GetComponentInChildren<RedColorSlider>();
            _greenSlider = GetComponentInChildren<GreenColorSlider>();
            _blueSlider = GetComponentInChildren<BlueColorSlider>();
        }

        public RedColorSlider GetRedColorSlider()
        {
            return _redSlider;
        }

        public float GetRedColorSliderValue()
        {
            return _redSlider.GetSliderValue();
        }

        public void SetRedColorSliderValue(float value)
        {
            _redSlider.SetSliderValue(value);
        }

        public GreenColorSlider GetGreenColorSlider()
        {
            return _greenSlider;
        }

        public float GetGreenColorSliderValue()
        {
            return _greenSlider.GetSliderValue();
        }
        public void SetGreenColorSliderValue(float value)
        {
            _greenSlider.SetSliderValue(value);
        }

        public BlueColorSlider GetBlueColorSlider()
        {
            return _blueSlider;
        }

        public float GetBlueColorSliderValue()
        {
            return _blueSlider.GetSliderValue();
        }

        public void SetBlueColorSliderValue(float value)
        {
            _blueSlider.SetSliderValue(value);
        }
    }
}

