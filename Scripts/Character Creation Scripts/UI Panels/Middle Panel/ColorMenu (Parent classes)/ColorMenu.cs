using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ColorMenu : MonoBehaviour
    {
        [Header("Color Values")]
        [SerializeField]
        protected float _redAmount;
        [SerializeField]
        protected float _greenAmount;
        [SerializeField]
        protected float _blueAmount;

        protected Color _savedColor;
        protected Color _currentColor;
        protected string _colorName;
        [SerializeField]
        protected ColorButtonParent _colorButtonParent;
        [SerializeField]
        protected ColorSliderParent _colorSliderParent;

        [SerializeField]
        protected List<SkinnedMeshRenderer> rendererList = new List<SkinnedMeshRenderer>();

        protected virtual void Awake()
        {
            _colorButtonParent = GetComponentInChildren<ColorButtonParent>(true);
            _colorSliderParent = GetComponentInChildren<ColorSliderParent>(true);
        }

        public void SetColorFromSliderValues()
        {
            _redAmount = _colorSliderParent.GetRedColorSliderValue();
            _greenAmount = _colorSliderParent.GetGreenColorSliderValue();
            _blueAmount = _colorSliderParent.GetBlueColorSliderValue();
            SetMaterialColor();
        }

        public void SetCurrentColor(float newRedAmount, float newGreenAmount, float newBlueAmount)
        {
            _redAmount = newRedAmount;
            _greenAmount = newGreenAmount;
            _blueAmount = newBlueAmount;
        }

        public void SetSliderValues(float redAmount, float greenAmount, float blueAmount)
        {
            _colorSliderParent.SetRedColorSliderValue(redAmount);
            _colorSliderParent.SetGreenColorSliderValue(greenAmount);
            _colorSliderParent.SetBlueColorSliderValue(blueAmount);
        }

        public virtual void SetMaterialColor()
        {
            if(_colorName == null)
            {
                Debug.LogError("ColorName is NULL, remember to set name in inherited classes");
                return;
            }
            _currentColor = new Color(_redAmount, _greenAmount, _blueAmount);
            for (int i = 0; i < rendererList.Count; i++)
            {
                rendererList[i].material.SetColor(_colorName, _currentColor);
            }
        }

        public void SetColorMenuActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SelectFirstButton()
        {
            _colorButtonParent.SelectFirstButton();
        }

        public Color GetCurrentColor()
        {
            return _currentColor;
        }

        public void SetSavedColor(Color saveColor)
        {
            _savedColor = saveColor;
        }

        public Color GetSavedColor()
        {
            return _savedColor;
        }

        public void ResetCurrentColor()
        {
            _redAmount = _savedColor.r;
            _greenAmount = _savedColor.g;
            _blueAmount = _savedColor.b;
            SetMaterialColor();
        }
    }
}

