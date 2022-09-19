using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJ
{
    public class StartGameButton : CharacterMenuButton
    {
        protected override void Awake()
        {
            base.Awake();
            _button.onClick.AddListener(ClickedThisButton);
        }

        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterMenu.GetCharacterCreationUIManager().StartGame();
        }
    }
}

