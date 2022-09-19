using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ChooseEyeColorButton : CharacterMenuButton
    {
        /// <summary>
        /// Enable Zoomed-In preview of Character's facial features<br />
        /// Set the last button selected as this button<br />
        /// Enable Eye Color Menu and select the first Eye Color button
        /// </summary>
        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterMenu.GetCharacterCreationUIManager().ZoomInOnFace(true);
            _characterMenu.SetLastButtonSelected(this);
            _characterMenu.GetCharacterCreationUIManager().SetMiddlePanelEyeColorMenuActive(true);
            _characterMenu.GetCharacterCreationUIManager().SelectMiddlePanelFirstEyeColorButton();
        }
    }
}
