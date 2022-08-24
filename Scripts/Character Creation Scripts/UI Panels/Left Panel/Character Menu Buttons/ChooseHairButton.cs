using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ChooseHairButton : CharacterMenuButton
    {
        /// <summary>
        /// Enable Zoomed-In preview of Character's facial features<br />
        /// Set the last button selected as this button<br />
        /// Enable Hairstyle Menu and select the first hairstyle button
        /// </summary>
        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterMenu.GetCharacterCreationUIManager().ZoomInOnFace(true);
            _characterMenu.SetLastButtonSelected(this);
            _characterMenu.GetCharacterCreationUIManager().SetMiddlePanelHairstyleMenuActive(true);
            _characterMenu.GetCharacterCreationUIManager().SelectMiddlePanelFirstHairstyleButton();
        }
    }
}

