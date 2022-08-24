using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HairColorMenu : ColorMenu
    {
        protected override void Awake()
        {
            base.Awake();
            _colorName = "_Color_Hair";
            _savedColor = rendererList[0].material.GetColor(_colorName);
        }
    }

}
