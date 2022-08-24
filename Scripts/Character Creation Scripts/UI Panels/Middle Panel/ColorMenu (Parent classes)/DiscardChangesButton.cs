using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class DiscardChangesButton : MonoBehaviour
    {
        [Header("Essential Components")]
        [SerializeField]
        protected CharacterCreationUIManager _characterCreationUIManager;
        [SerializeField]
        protected ColorMenu _colorMenu;
        [SerializeField]
        protected Button _button;

        protected virtual void Awake()
        {
            _characterCreationUIManager = GetComponentInParent<CharacterCreationUIManager>();
            _colorMenu = GetComponentInParent<ColorMenu>();
            _button = GetComponent<Button>();
        }

        protected virtual void ClickedThisButton()
        {
            _characterCreationUIManager.SetAllCharacterMenuButtonsInteractable(true);
            _characterCreationUIManager.SelectLastCharacterMenuButtonSelected();
            _characterCreationUIManager.ZoomInOnFace(false);
        }
    }
}

