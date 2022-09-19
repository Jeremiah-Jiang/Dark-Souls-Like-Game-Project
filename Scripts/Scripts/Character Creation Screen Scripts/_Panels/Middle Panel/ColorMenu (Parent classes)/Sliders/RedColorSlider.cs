using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class RedColorSlider : AppearanceRelatedSlider
    {
        protected override void Awake()
        {
            base.Awake();
            _slider.onValueChanged.AddListener(SetCurrentColorAndMaterial);
        }

        private void SetCurrentColorAndMaterial(float value)
        {
            _colorMenu.SetColorFromSliderValues();
        }
    }
}
