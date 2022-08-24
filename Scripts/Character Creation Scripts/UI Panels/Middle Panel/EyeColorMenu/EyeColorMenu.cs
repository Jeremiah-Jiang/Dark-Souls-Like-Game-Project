using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class EyeColorMenu : ColorMenu
    {
        protected override void Awake()
        {
            base.Awake();
            _colorName = "_Color_Eyes";
            _savedColor = rendererList[0].material.GetColor(_colorName);
        }
    }

}
