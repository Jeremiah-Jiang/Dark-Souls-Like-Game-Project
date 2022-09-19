using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    public class ChooseNameButton : CharacterMenuButton
    {
        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterMenu.SetChooseNameInputFieldActive(true);
            _characterMenu.SelectChooseNameInputField();
            _characterMenu.SetLastButtonSelected(this);
            gameObject.SetActive(false);
        }

        public void SetChooseNameButtonText(string text)
        {
            _buttonText.text = text;
        }
    }
}

