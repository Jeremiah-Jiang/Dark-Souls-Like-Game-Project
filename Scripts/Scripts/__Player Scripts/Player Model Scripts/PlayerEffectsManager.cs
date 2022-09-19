using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        PlayerManager playerManager;

        public GameObject currentParticleFX;
        public GameObject instantiatedFXModel;
        GameObject effectParticleFX;
        public int amountToHeal;

        protected override void Awake()
        {
            base.Awake();
            playerManager = GetComponent<PlayerManager>();
        }

        public void ApplyConsumableEffect()
        {
            ConsumableItem currentConsumable = playerManager.GetCurrentConsumableItem();
            if (currentConsumable is FlaskItem)
            {
                FlaskItem currentFlaskItem = currentConsumable as FlaskItem;
                if (currentFlaskItem.IsHPFlask())
                {
                    HealPlayerFromHPFlask();
                }
                else if (currentFlaskItem.IsMPFlask())
                {
                    Debug.Log("MP Flask functionality Not implemented yet");
                }
            }
            else if (currentConsumable is AntidoteConsumableItem)
            {
                AntidoteConsumableItem currentAntidoteItem = currentConsumable as AntidoteConsumableItem;
                if (currentAntidoteItem.IsPoisonAntidote())
                {
                    CurePlayerFromPoisonEffect();
                }
            }
        }

        private void HealPlayerFromHPFlask()
        {
            playerManager.HealPlayer(amountToHeal);
            effectParticleFX = Instantiate(currentParticleFX, playerManager.transform);
        }


        private void CurePlayerFromPoisonEffect()
        {
            playerManager.SetPoisonBuildUp(0);
            playerManager.SetPoisonAmount(playerManager.GetDefaultPoisonAmount());
            playerManager.SetIsPoisonedBool(false);
            effectParticleFX = Instantiate(currentParticleFX, playerManager.transform);
            GameObject currentPoisonParticleFX = playerManager.GetCurrentPoisonParticleFX();
            if (currentPoisonParticleFX != null)
            {
                Destroy(currentPoisonParticleFX);
            }
        }

        public void DestroyConsumableAndReloadRightHand()
        {
            /*
            if(healFX != null)
            {
                Destroy(healFX, 1.1f);
            }
            */
            if(instantiatedFXModel != null)
            {
                Destroy(instantiatedFXModel.gameObject);
            }
            playerManager.LoadBothWeaponsOnSlots();
        }

        protected override void HandlePoisonBuildUp()
        {
            if(poisonBuildUp <= 0)
            {
                playerManager.uiManager.SetPoisonBuildUpBarActive(false);
            }
            else
            {
                playerManager.uiManager.SetPoisonBuildUpBarActive(true);

            }
            base.HandlePoisonBuildUp();
            playerManager.uiManager.SetPoisonBuildUpAmount(Mathf.RoundToInt(poisonBuildUp));
        }

        protected override void HandleIsPoisonedEffect()
        {
            if(!isPoisoned)
            {
                playerManager.uiManager.SetPoisonAmountBarActive(false);
            }
            else
            {
                playerManager.uiManager.SetPoisonAmountBarActive(true);
            }
            base.HandleIsPoisonedEffect();
            playerManager.uiManager.SetPoisonAmountOnSlider(Mathf.RoundToInt(poisonAmount));
        }
    }
}

