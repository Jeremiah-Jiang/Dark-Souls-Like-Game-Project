using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ChooseBodyArtColorButton : CharacterMenuButton
    {
        /// <summary>
        /// Enable Zoomed-In preview of Character's facial features<br />
        /// Set the last button selected as this button<br />
        /// Enable Marking Color Menu and select the first Marking Color button
        /// </summary>
        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterMenu.GetCharacterCreationUIManager().ZoomInOnFace(true);
            _characterMenu.SetLastButtonSelected(this);
            _characterMenu.GetCharacterCreationUIManager().SetMiddlePanelBodyArtColorMenuActive(true);
            _characterMenu.GetCharacterCreationUIManager().SelectMiddlePanelFirstBodyArtColorButton();
        }
    }

}
