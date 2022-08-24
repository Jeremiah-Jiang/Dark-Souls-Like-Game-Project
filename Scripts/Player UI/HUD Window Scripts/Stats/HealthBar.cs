using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HealthBar : MonoBehaviour
    {

        private Slider slider;
        [SerializeField]
        private StatBarFrameUI _statBarFrameUI;
        [SerializeField]
        private HealthBarDamage _healthBarDamage;

        private void Awake()
        {
            _statBarFrameUI = GetComponentInParent<StatBarFrameUI>();
            slider = GetComponent<Slider>();
            _healthBarDamage = GetComponentInChildren<HealthBarDamage>();
            if (slider == null)
            {
                Debug.LogError("Slider is NULL");
            }
        }

        public void SetMaxHealthOnSlider(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
            _statBarFrameUI.SetFrameRectTransformSizeDelta(slider.maxValue + 50);
            _healthBarDamage.SetHealthBarDamageCurrentAndMaxValue(slider.value, slider.maxValue);
        }
        public void SetCurrentHealthOnSlider(int currentHealth)
        {
            if(currentHealth <= 0)
            {
                currentHealth = 0;
            }
            slider.value = currentHealth;
            _healthBarDamage.ResetHealthBarDepletionTimer();
            _healthBarDamage.SetHealthBarDamageDepletionSpeed(DetermineConstantDepletionSpeed());
        }

        public void SetCurrentHealthOnSliderNoDamageTaken(int currentHealth)
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
            slider.value = currentHealth;
            _healthBarDamage.SetHealthBarDamageCurrentValue(slider.value);
        }

        public float GetDifferenceInCurrentHealthAndHealthBarDamage()
        {
            return _healthBarDamage.GetHealthBarDamageCurrentValue() - slider.value;
        }

        public float DetermineConstantDepletionSpeed()
        {
            return GetDifferenceInCurrentHealthAndHealthBarDamage() * 4f;
        }

        public float GetHealthBarValue()
        {
            return slider.value;
        }

        public float GetMaxHealthBarValue()
        {
            return slider.maxValue;
        }
    }
}

