using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class DiscardEyeChangesButton : DiscardChangesButton
    {
        protected override void Awake()
        {
            base.Awake();
            _button.onClick.AddListener(ClickedThisButton);

        }

        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _colorMenu.ResetCurrentColor();
            _colorMenu.SetColorMenuActive(false);
        }
    }
}

