using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Critical Attack Action")]
    public class CriticalAttackAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            if (characterManager.IsInteracting())
                return;
            PlayerManager playerManager = characterManager as PlayerManager;
            playerManager.AttemptBackstabOrRiposte();
        }
        //Need to modularize this to include enemy, AttemptBackstabOrRiposte() logic is only applicable to playermanager now
    }
}

