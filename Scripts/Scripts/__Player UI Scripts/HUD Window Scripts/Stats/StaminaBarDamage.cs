using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class StaminaBarDamage : MonoBehaviour
    {
        private const float DAMAGED_STAMINA_DEPLETION_TIMER_MAX = 0.5f;
        [SerializeField]
        private float depletionSpeed = 1;
        [SerializeField]
        private Slider damageSlider;
        [SerializeField]
        private StaminaBar staminaBar;
        private RectTransform sliderRectTransform;
        [SerializeField]
        private float damagedStaminaDepletionTimer = DAMAGED_STAMINA_DEPLETION_TIMER_MAX;

        private void Awake()
        {
            damageSlider = GetComponent<Slider>();
            staminaBar = GetComponentInParent<StaminaBar>();
            sliderRectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            damagedStaminaDepletionTimer -= Time.deltaTime;
            if (staminaBar.GetStaminaBarValue() >= damageSlider.value)
            {
                damageSlider.value = staminaBar.GetStaminaBarValue();
                ResetStaminaBarDepletionTimer();

            }
            else
            {
                if (damagedStaminaDepletionTimer <= 0)
                {
                    damageSlider.value -= depletionSpeed * Time.deltaTime;
                    if (damageSlider.value <= staminaBar.GetStaminaBarValue())
                    {
                        damageSlider.value = staminaBar.GetStaminaBarValue();
                    }
                }
            }
        }

        public void SetStaminaBarDamageCurrentAndMaxValue(float currentValue, float maxValue)
        {
            SetStaminaBarDamageCurrentValue(currentValue);
            SetStaminaBarDamageMaxValue(maxValue);
        }

        public float GetStaminaBarDamageCurrentValue()
        {
            return damageSlider.value;
        }

        public void SetStaminaBarDamageCurrentValue(float currentValue)
        {
            damageSlider.value = currentValue;
        }

        public void SetStaminaBarDamageMaxValue(float maxValue)
        {
            damageSlider.maxValue = maxValue;
        }

        public void SetStaminaBarDamageDepletionSpeed(float speed)
        {
            depletionSpeed = speed;
        }

        public void ResetStaminaBarDepletionTimer()
        {
            damagedStaminaDepletionTimer = DAMAGED_STAMINA_DEPLETION_TIMER_MAX;
        }

    }
}

