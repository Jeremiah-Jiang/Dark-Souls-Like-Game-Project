using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Aim Action")]

    public class AimAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            PlayerManager playerManager = characterManager as PlayerManager;
            if (characterManager.isAiming)
                return;
            if(playerManager != null)
            {
                playerManager.uiManager.SetCrossHairActive(true);
            }
            characterManager.isAiming = true;
        }
    }
}

