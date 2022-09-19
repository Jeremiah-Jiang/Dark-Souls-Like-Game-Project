using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class CharacterMenu : MonoBehaviour
    {
        [SerializeField]
        CharacterCreationUIManager _characterCreationUIManager;

        [SerializeField]
        ChooseNameInputField _chooseNameInputField;

        [Header("Character Menu Buttons")]
        [SerializeField]
        List<CharacterMenuButton> _characterMenuButtons = new List<CharacterMenuButton>();

        [SerializeField]
        CharacterMenuButton _lastSelectedButton;
        private void Awake()
        {
            _characterCreationUIManager = GetComponentInParent<CharacterCreationUIManager>();
            _chooseNameInputField = GetComponentInChildren<ChooseNameInputField>(true);
            //_nameInputField.onEndEdit.AddListener(EnteredName);
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                if(gameObject.transform.GetChild(i).GetComponent<CharacterMenuButton>() != null)
                {
                    _characterMenuButtons.Add(gameObject.transform.GetChild(i).GetComponent<CharacterMenuButton>());
                }
            }
        }

        private void Start()
        {
            _chooseNameInputField.SetChooseNameInputFieldActive(false);
        }
        public void SetChooseNameButtonText(string text)
        {
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                ChooseNameButton currentButton = gameObject.transform.GetChild(i).GetComponent<ChooseNameButton>();
                if(currentButton != null)
                {
                    currentButton.SetChooseNameButtonText(text);
                }
            }
        }

        public void SetLastButtonSelected(CharacterMenuButton characterMenuButton)
        {
            _lastSelectedButton = characterMenuButton;
        }

        public void SetLastButtonSelectedInteractable(bool value)
        {
            _lastSelectedButton.SetButtonInteractable(value);
        }

        public void SelectLastButtonSelected()
        {
            _lastSelectedButton.SelectThisButton();
        }

        public void SetLastButtonSelectedActive(bool value)
        {
            _lastSelectedButton.SetButtonActive(value);
        }

        public void SetChooseNameInputFieldActive(bool value)
        {
            _chooseNameInputField.SetChooseNameInputFieldActive(value);
        }

        public void SelectChooseNameInputField()
        {
            _chooseNameInputField.SelectChooseNameInputField();
        }

        public void SetAllCharacterMenuButtonsInteractable(bool value)
        {
            foreach(var button in _characterMenuButtons)
            {
                button.SetButtonInteractable(value);
            }
        }

        public CharacterCreationUIManager GetCharacterCreationUIManager()
        {
            return _characterCreationUIManager;
        }
    }
}

