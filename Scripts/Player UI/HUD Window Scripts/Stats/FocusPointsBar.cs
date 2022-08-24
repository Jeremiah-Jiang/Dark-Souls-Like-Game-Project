using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class FocusPointsBar : MonoBehaviour
    {
        private Slider slider;
        [SerializeField]
        private StatBarFrameUI _statBarFrameUI;
        [SerializeField]
        private FocusPointsBarDamage _focusPointsBarDamage;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            _statBarFrameUI = GetComponentInParent<StatBarFrameUI>();
            _focusPointsBarDamage = GetComponentInChildren<FocusPointsBarDamage>();
            if (slider == null)
            {
                Debug.LogError("Slider is NULL");
            }
        }
        public void SetMaxFocusPointsOnSlider(float maxFocusPoints)
        {
            slider.maxValue = maxFocusPoints;
            slider.value = maxFocusPoints;
            _statBarFrameUI.SetFrameRectTransformSizeDelta(slider.value + 50);
            _focusPointsBarDamage.SetFocusPointsBarDamageCurrentAndMaxValue(slider.value, slider.maxValue);
        }
        public void SetCurrentFocusPointsOnSlider(float currentFocusPoints)
        {
            if (currentFocusPoints <= 0)
            {
                currentFocusPoints = 0;
            }
            slider.value = currentFocusPoints;
            _focusPointsBarDamage.ResetFocusPointsBarDepletionTimer();
            _focusPointsBarDamage.SetFocusPointsBarDamageDepletionSpeed(DetermineConstantDepletionSpeed());
        }

        public void SetCurrentFocusPointsOnSliderNoDamageTaken(float currentFocusPoints)
        {
            if (currentFocusPoints <= 0)
            {
                currentFocusPoints = 0;
            }
            slider.value = currentFocusPoints;
            _focusPointsBarDamage.SetFocusPointsBarDamageCurrentValue(slider.value);
        }

        public float GetDifferenceInCurrentFocusPointsAndFocusPointsBarDamage()
        {
            return _focusPointsBarDamage.GetFocusPointsBarDamageCurrentValue() - slider.value;
        }

        public float DetermineConstantDepletionSpeed()
        {
            return GetDifferenceInCurrentFocusPointsAndFocusPointsBarDamage() * 4f;
        }

        public float GetFocusPointsBarValue()
        {
            return slider.value;
        }

        public float GetMaxFocusPointsBarValue()
        {
            return slider.maxValue;
        }
    }
}

