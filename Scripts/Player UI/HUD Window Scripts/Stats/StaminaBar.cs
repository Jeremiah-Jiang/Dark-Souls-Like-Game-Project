using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class StaminaBar : MonoBehaviour
    {
        private Slider slider;
        [SerializeField]
        private StatBarFrameUI _statBarFrameUI;
        [SerializeField]
        private StaminaBarDamage _staminaBarDamage;
        private void Awake()
        {
            _statBarFrameUI = GetComponentInParent<StatBarFrameUI>();
            slider = GetComponent<Slider>();
            _staminaBarDamage = GetComponentInChildren<StaminaBarDamage>();
            if (slider == null)
            {
                Debug.LogError("Slider is NULL");
            }
        }
        public void SetMaxStaminaOnSlider(float maxStamina)
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
            _statBarFrameUI.SetFrameRectTransformSizeDelta(slider.maxValue + 50);
            _staminaBarDamage.SetStaminaBarDamageCurrentAndMaxValue(slider.value, slider.maxValue);
        }
        public void SetCurrentStaminaOnSlider(float currentStamina)
        {
            if(currentStamina <= 0)
            {
                currentStamina = 0;
            }
            slider.value = currentStamina;
            _staminaBarDamage.ResetStaminaBarDepletionTimer();
            _staminaBarDamage.SetStaminaBarDamageDepletionSpeed(DetermineConstantDepletionSpeed());
        }

        public void SetCurrentStaminaOnSliderNoDamageTaken(float currentStamina)
        {
            if (currentStamina <= 0)
            {
                currentStamina = 0;
            }
            slider.value = currentStamina;
            _staminaBarDamage.SetStaminaBarDamageCurrentValue(slider.value);
        }

        public float GetDifferenceInCurrentStaminaAndStaminaBarDamage()
        {
            return _staminaBarDamage.GetStaminaBarDamageCurrentValue() - slider.value;
        }

        public float DetermineConstantDepletionSpeed()
        {
            return GetDifferenceInCurrentStaminaAndStaminaBarDamage() * 4f;
        }
        public float GetStaminaBarValue()
        {
            return slider.value;
        }
    }
}

