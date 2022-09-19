 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class SelectedSliderOnEnable : MonoBehaviour
    {
        public Slider statSlider;

        private void OnEnable()
        {
            statSlider.Select();
            statSlider.OnSelect(null);
        }
    }
}

