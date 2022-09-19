using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ConfirmBodyArtChangesButton : ConfirmChangesButton
    {
        protected override void Awake()
        {
            base.Awake();
            _button.onClick.AddListener(ClickedThisButton);

        }

        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _colorMenu.SetSavedColor(_colorMenu.GetCurrentColor());
            _colorMenu.SetColorMenuActive(false);
        }
    }
}

