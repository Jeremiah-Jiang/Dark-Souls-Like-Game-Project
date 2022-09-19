using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    [RequireComponent(typeof(InputField))]
    public class ChooseNameInputField : MonoBehaviour
    {
        PlayerManager playerManager;
        CharacterMenu _characterMenu;
        InputField _nameInputField;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            _characterMenu = GetComponentInParent<CharacterMenu>();
            _nameInputField = GetComponent<InputField>();
            _nameInputField.onEndEdit.AddListener(EnteredName);
        }

        private void EnteredName(string name)
        {
            NameMyCharacter(name);
            _characterMenu.SetLastButtonSelectedActive(true);
            _characterMenu.SelectLastButtonSelected();
            _characterMenu.SetAllCharacterMenuButtonsInteractable(true);
            SetChooseNameInputFieldActive(false);
        }

        public void SelectChooseNameInputField()
        {
            _nameInputField.Select();
        }

        public void SetChooseNameInputFieldActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void NameMyCharacter(string name)
        {
            playerManager.SetCharacterName(name);
            playerManager.uiManager.SetPlayerName(name);
            if (playerManager.GetCharacterName() == "")
            {
                playerManager.SetCharacterName("Nameless");
                playerManager.uiManager.SetPlayerName("Nameless");
            }
            _characterMenu.SetChooseNameButtonText(playerManager.GetCharacterName());
        }
    }
}

