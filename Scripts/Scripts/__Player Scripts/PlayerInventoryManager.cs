using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PlayerInventoryManager : CharacterInventoryManager
    {
        PlayerManager playerManager;
        public List<WeaponItem> weaponsInventory;
        public List<LegEquipment> legEquipmentInventory;
        public List<HandEquipment> handEquipmentInventory;
        public List<HelmetEquipment> headEquipmentInventory;
        public List<TorsoEquipment> torsoEquipmentInventory;
        public List<BackEquipment> backEquipmentInventory;
        public List<ConsumableItem> consumablesInventory;

        protected override void Awake()
        {
            base.Awake();
            playerManager = GetComponent<PlayerManager>();
        }
        /// <summary>
        /// Method to cycle through Right Hand Quickslot<br />
        /// If index out of bounds, rightWeapon is set to unarmed Weapon by default
        /// </summary>
        public void ChangeRightWeapon()
        {
            currRightWeaponIdx++;
            if(currRightWeaponIdx == 0 && weaponsInRightHandSlots[0] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currRightWeaponIdx];
                playerManager.LoadWeaponOnSlot(rightWeapon, false);
            }
            else if(currRightWeaponIdx == 0 && weaponsInRightHandSlots[0] == null)
            {
                currRightWeaponIdx++;
            }
            else if(currRightWeaponIdx == 1 && weaponsInRightHandSlots[1] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currRightWeaponIdx];
                playerManager.LoadWeaponOnSlot(rightWeapon, false);
            }
            else
            {
                currRightWeaponIdx++;
            }
            if (currRightWeaponIdx > weaponsInRightHandSlots.Length - 1)
            {
                currRightWeaponIdx = -1;
                playerManager.SetRightWeapon(playerManager.GetUnarmedWeapon());
                playerManager.LoadWeaponOnSlot(rightWeapon, false);
            }
        }

        /// <summary>
        /// Method to cycle through Left Hand Quickslot<br />
        /// If index out of bounds, leftWeapon is set to unarmed Weapon by default
        /// </summary>
        public void ChangeLeftWeapon()
        {
            currLeftWeaponIdx++;
            if (currLeftWeaponIdx == 0 && weaponsInLeftHandSlots[0] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currLeftWeaponIdx];
                playerManager.LoadWeaponOnSlot(leftWeapon, true);
            }
            else if (currLeftWeaponIdx == 0 && weaponsInLeftHandSlots[0] == null)
            {
                currLeftWeaponIdx++;
            }
            else if (currLeftWeaponIdx == 1 && weaponsInLeftHandSlots[1] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currLeftWeaponIdx];
                playerManager.LoadWeaponOnSlot(leftWeapon, true);
            }
            else
            {
                currLeftWeaponIdx++;
            }
            if (currLeftWeaponIdx > weaponsInLeftHandSlots.Length - 1)
            {
                currLeftWeaponIdx = -1;
                playerManager.SetLeftWeapon(playerManager.GetUnarmedWeapon());
                playerManager.LoadWeaponOnSlot(leftWeapon, true);
            }

        }

        public void ChangeConsumableItem()
        {
            currConsumableIdx++;

            switch(currConsumableIdx)
            {
                case 0:
                    ProcessNewCurrentConsumableItem(0);
                    break;
                case 1:
                    ProcessNewCurrentConsumableItem(1);
                    break;
                default:
                    currConsumableIdx = 0;
                    ProcessNewCurrentConsumableItem(0);
                    break;
            }
        }

        private void ProcessNewCurrentConsumableItem(int consumableIdx)
        {
            if (consumableItemsEquipped[consumableIdx] != null)
            {
                currentConsumable = consumableItemsEquipped[consumableIdx];
                playerManager.uiManager.UpdateCurrentConsumableIcon(currentConsumable);
            }
            else
            {
                currConsumableIdx++;
            }
        }
    }
}

