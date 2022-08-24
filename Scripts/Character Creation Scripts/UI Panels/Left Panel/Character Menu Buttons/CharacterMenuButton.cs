using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    [RequireComponent(typeof(Button))]
    public class CharacterMenuButton : MonoBehaviour
    {
        protected CharacterMenu _characterMenu;
        protected Button _button;
        protected Text _buttonText;

        protected virtual void Awake()
        {
            _characterMenu = GetComponentInParent<CharacterMenu>();
            _button = GetComponent<Button>();
            _buttonText = GetComponentInChildren<Text>();
            _button.onClick.AddListener(ClickedThisButton);

        }

        protected virtual void ClickedThisButton()
        {
            _characterMenu.SetAllCharacterMenuButtonsInteractable(false);
        }

        public void SetButtonInteractable(bool value)
        {
            _button.interactable = value;
        }

        public void SelectThisButton()
        {
            _button.Select();
        }

        public void SetButtonActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

