using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Block")]
    public class BlockingAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            if (characterManager.IsInteracting())
            {
                characterManager.SetIsBlocking(false);
                return;
            }

            if (characterManager.IsBlocking()) //prevent unintentional repeated calls to Block Start
                return;
            characterManager.SetBlockingAbsorptionFromBlockingWeapon();
            characterManager.SetIsBlocking(true);
        }
    }
}

