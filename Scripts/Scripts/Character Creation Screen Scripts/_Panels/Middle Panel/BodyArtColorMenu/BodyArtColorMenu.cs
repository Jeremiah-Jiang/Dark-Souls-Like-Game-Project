using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class BodyArtColorMenu : ColorMenu
    {
        protected override void Awake()
        {
            base.Awake();
            _colorName = "_Color_BodyArt";
            _savedColor = rendererList[0].material.GetColor(_colorName);
        }
    }

}

