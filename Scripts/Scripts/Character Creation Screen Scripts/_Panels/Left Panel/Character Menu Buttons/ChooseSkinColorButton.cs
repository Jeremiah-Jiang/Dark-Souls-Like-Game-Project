using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ChooseSkinColorButton : CharacterMenuButton
    {
        /// <summary>
        /// Enable Zoomed-In preview of Character's facial features<br />
        /// Set the last button selected as this button<br />
        /// Enable Skin Color Menu and select the first Skin Color button
        /// </summary>
        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterMenu.GetCharacterCreationUIManager().ZoomInOnFace(false);
            _characterMenu.SetLastButtonSelected(this);
            _characterMenu.GetCharacterCreationUIManager().SetMiddlePanelSkinColorMenuActive(true);
            _characterMenu.GetCharacterCreationUIManager().SelectMiddlePanelFirstSkinColorButton();
        }
    }
}

