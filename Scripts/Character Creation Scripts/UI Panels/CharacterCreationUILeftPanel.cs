using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class CharacterCreationUILeftPanel : MonoBehaviour
    {
        private Text _characterMenuTitle;
        private CharacterMenu _characterMenu;

        private void Awake()
        {
            _characterMenuTitle = GetComponentInChildren<Text>();
            _characterMenu = GetComponentInChildren<CharacterMenu>();
            _characterMenuTitle.text = "Character Menu";
        }

        public void SetAllCharacterMenuButtonsInteractable(bool value)
        {
            _characterMenu.SetAllCharacterMenuButtonsInteractable(value);
        }

        public void SelectLastCharacterMenuButtonSelected()
        {
            _characterMenu.SelectLastButtonSelected();
        }
    }
}

