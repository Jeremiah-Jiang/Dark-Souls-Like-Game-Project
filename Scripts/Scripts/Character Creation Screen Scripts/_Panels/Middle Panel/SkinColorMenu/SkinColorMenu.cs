using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class SkinColorMenu : ColorMenu
    {
        private string _colorNameTwo = "_Color_Stubble";
        protected override void Awake()
        {
            base.Awake();
            _colorName = "_Color_Skin";
            _savedColor = rendererList[0].material.GetColor(_colorName);
        }

        public override void SetMaterialColor()
        {
            if (_colorName == null)
            {
                Debug.LogError("ColorName is NULL, remember to set name in inherited classes");
                return;
            }
            _currentColor = new Color(_redAmount, _greenAmount, _blueAmount);
            for (int i = 0; i < rendererList.Count; i++)
            {

                rendererList[i].material.SetColor(_colorName, _currentColor);
                rendererList[i].material.SetColor(_colorNameTwo, _currentColor);

            }
        }
    }
}

