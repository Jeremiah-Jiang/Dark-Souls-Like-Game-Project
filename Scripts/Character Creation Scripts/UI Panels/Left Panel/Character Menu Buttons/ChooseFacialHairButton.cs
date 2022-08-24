using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ChooseFacialHairButton : CharacterMenuButton
    {
        /// <summary>
        /// Enable Zoomed-In preview of Character's facial features<br />
        /// Set the last button selected as this button<br />
        /// Enable Facial Hair Menu and select the first Facial Hair button
        /// </summary>
        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterMenu.GetCharacterCreationUIManager().ZoomInOnFace(true);
            _characterMenu.SetLastButtonSelected(this);
            _characterMenu.GetCharacterCreationUIManager().SetMiddlePanelFacialHairMenuActive(true);
            _characterMenu.GetCharacterCreationUIManager().SelectMiddlePanelFirstFacialHairButton();
        }
    }
}

