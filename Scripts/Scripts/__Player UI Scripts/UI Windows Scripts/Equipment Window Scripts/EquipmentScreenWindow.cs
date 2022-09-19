using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class EquipmentScreenWindow : MonoBehaviour
    {
        //Assign all these in inspector instead of calling on awake, the game object attached to this script will be inactive
        public WeaponEquipmentSlotUI[] weaponEquipmentSlotUI;
        public ConsumableEquipmentSlotUI[] consumableEquipmentSlotUI;
        public HeadEquipmentSlotUI headEquipmentSlotUI;
        public TorsoEquipmentSlotUI torsoEquipmentSlotUI;
        public BackEquipmentSlotUI backEquipmentSlotUI;
        public HandEquipmentSlotUI handEquipmentSlotUI;
        public LegEquipmentSlotUI legEquipmentSlotUI;

        /// <summary>
        /// Method to load player's weapons on Equipment Screen
        /// </summary>
        /// <param name="player">The player for whom to load the weapons for</param>
        public void LoadWeaponsOnEquipmentScreen(PlayerManager player)
        {
            for (int i = 0; i < weaponEquipmentSlotUI.Length; i++)
            {
                if (weaponEquipmentSlotUI[i].rightHandSlot01)
                {
                    weaponEquipmentSlotUI[i].AddItem(player.GetWeaponsInRightHandSlots()[0]);
                }
                else if (weaponEquipmentSlotUI[i].rightHandSlot02)
                {
                    weaponEquipmentSlotUI[i].AddItem(player.GetWeaponsInRightHandSlots()[1]);
                }
                else if (weaponEquipmentSlotUI[i].leftHandSlot01)
                {
                    weaponEquipmentSlotUI[i].AddItem(player.GetWeaponsInLeftHandSlots()[0]);
                }
                else if (weaponEquipmentSlotUI[i].leftHandSlot02)
                {
                    weaponEquipmentSlotUI[i].AddItem(player.GetWeaponsInLeftHandSlots()[1]);
                }
            }
        }

        /// <summary>
        /// Method to load player's consumable items on Equipment Screen
        /// </summary>
        /// <param name="player">The player for whom to load the consumable items for</param>
        public void LoadConsumablesOnEquipmentScreen(PlayerManager playerManager)
        {
            for(int i = 0; i < consumableEquipmentSlotUI.Length; i++)
            {
                if (consumableEquipmentSlotUI[i].consumableSlot01)
                {
                    consumableEquipmentSlotUI[i].AddItem(playerManager.GetConsumableItemsEquipped()[0]);
                }
                else if (consumableEquipmentSlotUI[i].consumableSlot02)
                {
                    consumableEquipmentSlotUI[i].AddItem(playerManager.GetConsumableItemsEquipped()[1]);
                }
            }
        }

        /// <summary>
        /// Method to load player's armor on Equipment Screen
        /// </summary>
        /// <param name="player">The player for whom to load the armor for</param>
        public void LoadArmorOnEquipmentScreen(PlayerManager player)
        {
            HelmetEquipment currentHelmetEquipment = player.GetCurrentHelmetEquipment();
            TorsoEquipment currentTorsoEquipment = player.GetCurrentTorsoEquipment();
            BackEquipment currentBackEquipment = player.GetCurrentBackEquipment();
            HandEquipment currentHandEquipment = player.GetCurrentHandEquipment();
            LegEquipment currentLegEquipment = player.GetCurrentLegEquipment();

            if(currentHelmetEquipment != null)
            {
                headEquipmentSlotUI.AddItem(currentHelmetEquipment);
            }
            else
            {
                headEquipmentSlotUI.ClearItem();
            }

            if(currentTorsoEquipment != null)
            {
                torsoEquipmentSlotUI.AddItem(currentTorsoEquipment);
            }
            else
            {
                torsoEquipmentSlotUI.ClearItem();
            }

            if(currentBackEquipment != null)
            {
                backEquipmentSlotUI.AddItem(currentBackEquipment);
            }
            else
            {
                backEquipmentSlotUI.ClearItem();
            }
            if(currentHandEquipment != null)
            {
                handEquipmentSlotUI.AddItem(currentHandEquipment);
            }
            else
            {
                handEquipmentSlotUI.ClearItem();
            }

            if(currentLegEquipment != null)
            {
                legEquipmentSlotUI.AddItem(currentLegEquipment);
            }
            else
            {
                legEquipmentSlotUI.ClearItem();
            }

        }

    }
}

