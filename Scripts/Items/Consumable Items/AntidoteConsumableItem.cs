using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Items/Consumables/Antidote")]
    public class AntidoteConsumableItem : ConsumableItem
    {
        [Header("Recovery FX")]
        public GameObject antidoteConsumeFX;

        [Header("Cure FX")]
        public bool curePoison;
        public override void AttemptToConsumeItem(PlayerManager player)
        {
            base.AttemptToConsumeItem(player);
            GameObject antidote = Instantiate(itemModel, player.GetRightHandSlot().transform);
            player.SetCurrentParticleFX(antidoteConsumeFX);
            player.SetInstantiatedFXModel(antidote);
            /*
            if(curePoison)
            {
                player.SetPoisonBuildUp(0);
                player.SetPoisonAmount(player.GetDefaultPoisonAmount());
                player.SetIsPoisonedBool(false);
                GameObject currentPoisonParticleFX = player.GetCurrentPoisonParticleFX();  
                if(currentPoisonParticleFX != null)
                {
                    Destroy(currentPoisonParticleFX);
                }
            }
            */
            player.GetRightHandSlot().UnloadWeapon();
        }

        public bool IsPoisonAntidote()
        {
            return curePoison;
        }
    }
}

