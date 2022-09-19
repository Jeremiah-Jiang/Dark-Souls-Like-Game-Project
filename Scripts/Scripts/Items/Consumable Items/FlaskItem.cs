using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JJ
{
    [CreateAssetMenu(menuName = "Items/Consumables/Flask")]

    public class FlaskItem : ConsumableItem
    {
        [Header("Flask Type")]
        public bool isHpFlask;
        public bool isMpFlask;

        [Header("Recovery Amount")]
        public int healthRecoveryAmount;
        public int mpRecoveryAmount;

        [Header("Recovery FX")]
        public GameObject recoveryFX;

        public override void AttemptToConsumeItem(PlayerManager player)
        {
            base.AttemptToConsumeItem(player);
            GameObject flask = Instantiate(itemModel, player.GetRightHandSlot().transform);
            player.SetCurrentParticleFX(recoveryFX);
            player.SetAmountToHeal(healthRecoveryAmount);
            player.SetInstantiatedFXModel(flask);
            player.GetRightHandSlot().UnloadWeapon();
        }

        public bool IsHPFlask()
        {
            return isHpFlask;
        }

        public bool IsMPFlask()
        {
            return isMpFlask;
        }
    }
}

