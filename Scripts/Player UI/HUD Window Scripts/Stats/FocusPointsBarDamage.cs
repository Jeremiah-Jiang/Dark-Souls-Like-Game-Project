using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class FocusPointsBarDamage : MonoBehaviour
    {
        private const float DAMAGED_FP_DEPLETION_TIMER_MAX = 2.0f;
        [SerializeField]
        private float depletionSpeed = 1;
        [SerializeField]
        private Slider damageSlider;
        [SerializeField]
        private FocusPointsBar focusPointsBar;
        private RectTransform sliderRectTransform;
        [SerializeField]
        private float damagedFPDepletionTimer = DAMAGED_FP_DEPLETION_TIMER_MAX;

        private void Awake()
        {
            damageSlider = GetComponent<Slider>();
            focusPointsBar = GetComponentInParent<FocusPointsBar>();
            sliderRectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            damagedFPDepletionTimer -= Time.deltaTime;
            if (focusPointsBar.GetFocusPointsBarValue() >= damageSlider.value)
            {
                damageSlider.value = focusPointsBar.GetFocusPointsBarValue();
                ResetFocusPointsBarDepletionTimer();
            }
            else
            {
                if (damagedFPDepletionTimer <= 0)
                {
                    damageSlider.value -= depletionSpeed * Time.deltaTime;
                    if (damageSlider.value <= focusPointsBar.GetFocusPointsBarValue())
                    {
                        damageSlider.value = focusPointsBar.GetFocusPointsBarValue();
                    }
                }
            }
        }

        public void SetFocusPointsBarDamageCurrentAndMaxValue(float currentValue, float maxValue)
        {
            SetFocusPointsBarDamageCurrentValue(currentValue);
            SetFocusPointsBarDamageMaxValue(maxValue);
        }

        public float GetFocusPointsBarDamageCurrentValue()
        {
            return damageSlider.value;
        }

        public void SetFocusPointsBarDamageCurrentValue(float currentValue)
        {
            damageSlider.value = currentValue;
        }

        public void SetFocusPointsBarDamageMaxValue(float maxValue)
        {
            damageSlider.maxValue = maxValue;
        }

        public void SetFocusPointsBarDamageDepletionSpeed(float speed)
        {
            depletionSpeed = speed;
        }

        public void ResetFocusPointsBarDepletionTimer()
        {
            damagedFPDepletionTimer = DAMAGED_FP_DEPLETION_TIMER_MAX;
        }

    }
}

