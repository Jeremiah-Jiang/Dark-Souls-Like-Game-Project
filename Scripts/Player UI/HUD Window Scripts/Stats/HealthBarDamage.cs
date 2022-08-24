using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HealthBarDamage : MonoBehaviour
    {
        private const float DAMAGED_HEALTH_DEPLETION_TIMER_MAX = 2.0f;
        [SerializeField]
        private float depletionSpeed = 1;
        [SerializeField]
        private Slider damageSlider;
        [SerializeField]
        private HealthBar healthBar;
        private RectTransform sliderRectTransform;
        [SerializeField]
        private float damagedHealthDepletionTimer = DAMAGED_HEALTH_DEPLETION_TIMER_MAX;

        private void Awake()
        {
            damageSlider = GetComponent<Slider>();
            healthBar = GetComponentInParent<HealthBar>();
            sliderRectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            damagedHealthDepletionTimer -= Time.deltaTime;
            if (healthBar.GetHealthBarValue() >= damageSlider.value)
            {
                damageSlider.value = healthBar.GetHealthBarValue();
                ResetHealthBarDepletionTimer();
            }
            else
            {
                if (damagedHealthDepletionTimer <= 0)
                {
                    damageSlider.value -= depletionSpeed * Time.deltaTime;
                    if (damageSlider.value <= healthBar.GetHealthBarValue())
                    {
                        damageSlider.value = healthBar.GetHealthBarValue();
                    }
                }
            }
        }

        public void SetHealthBarDamageCurrentAndMaxValue(float currentValue, float maxValue)
        {
            SetHealthBarDamageCurrentValue(currentValue);
            SetHealthBarDamageMaxValue(maxValue);
        }

        public float GetHealthBarDamageCurrentValue()
        {
            return damageSlider.value;
        }

        public void SetHealthBarDamageCurrentValue(float currentValue)
        {
            damageSlider.value = currentValue;
        }

        public void SetHealthBarDamageMaxValue(float maxValue)
        {
            damageSlider.maxValue = maxValue;
        }

        public void SetHealthBarDamageDepletionSpeed(float speed)
        {
            depletionSpeed = speed;
        }

        public void ResetHealthBarDepletionTimer()
        {
            damagedHealthDepletionTimer = DAMAGED_HEALTH_DEPLETION_TIMER_MAX;
        }

    }
}

