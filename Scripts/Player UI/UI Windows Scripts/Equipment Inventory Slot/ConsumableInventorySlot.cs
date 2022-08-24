using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ConsumableInventorySlot : QuickSlotInventorySlot
    {
        public override void AddItem(Item consumableItem)
        {
            item = item as ConsumableItem;
            base.AddItem(consumableItem);
        }

        /// <summary>
        /// <para>This function is an OnClickEvent for any of the buttons belonging to an ConsumableInventorySlot</para>
        /// <para>If consumableSlot0X is selected and the weapon in the slot is not null, add the weapon into the ConsumableInventory</para>
        /// <para>The current item that is selected will become the new consumable in the consumableSlot0X</para>
        /// <para>Remove the item from ConsumableInventory to prevent duplicates</para>
        /// <para>Update UI and Reset selected slot</para>
        /// </summary>
        public void EquipThisItem()
        {
            if (uiManager.consumableSlot01Selected)
            {
                ConsumableItem consumableInConsumableSlot01 = uiManager.playerManager.GetConsumableItemsEquipped()[0];
                if (consumableInConsumableSlot01 != null)
                {
                    uiManager.playerManager.GetConsumablesInventory().Add(consumableInConsumableSlot01);
                }
                uiManager.playerManager.GetConsumableItemsEquipped()[0] = item as ConsumableItem;
                uiManager.playerManager.GetConsumablesInventory().Remove(item as ConsumableItem);
            }
            else if (uiManager.consumableSlot02Selected)
            {
                ConsumableItem consumableInConsumableSlot02 = uiManager.playerManager.GetConsumableItemsEquipped()[1];
                if (consumableInConsumableSlot02 != null)
                {
                    uiManager.playerManager.GetConsumablesInventory().Add(consumableInConsumableSlot02);
                }
                uiManager.playerManager.GetConsumableItemsEquipped()[1] = item as ConsumableItem;
                uiManager.playerManager.GetConsumablesInventory().Remove(item as ConsumableItem);
            }
            else
            {
                return;
            }
            if (uiManager.playerManager.GetCurrentConsumableIndex() >= 0)
            {
                uiManager.playerManager.SetCurrentConsumableItem(uiManager.playerManager.GetConsumableItemsEquipped()[uiManager.playerManager.GetCurrentConsumableIndex()]);
                uiManager.UpdateCurrentConsumableIcon(uiManager.playerManager.GetCurrentConsumableItem());
            }
            uiManager.LoadConsumablesOnEquipmentScreen(uiManager.playerManager);
            
            uiManager.ResetAllSelectedSlots();
        }

        public override void SelectThisSlot()
        {
            uiManager.UpdateConsumableItemStats(item as ConsumableItem);
        }

        protected override void ClickedThisSlot()
        {
            base.ClickedThisSlot();
            uiManager.uiWindowsLeftPanel.SetConsumableInventoryWindowActive(false);
            EquipThisItem();
        }
    }
}

