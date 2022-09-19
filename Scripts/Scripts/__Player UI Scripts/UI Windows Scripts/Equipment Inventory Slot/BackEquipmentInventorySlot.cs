using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class BackEquipmentInventorySlot : InventorySlot
    {
        public override void AddItem(EquipmentItem equipmentItem)
        {
            item = item as BackEquipment;
            base.AddItem(equipmentItem);
        }

        public void EquipThisItem()
        {
            if (uiManager.backEquipmentSlotSelected)
            {
                BackEquipment currentBackEquipment = uiManager.playerManager.GetCurrentBackEquipment();
                if (currentBackEquipment != null)
                {
                    uiManager.playerManager.GetBackEquipmentInventory().Add(currentBackEquipment);
                }
                uiManager.playerManager.SetCurrentBackEquipment(item as BackEquipment);
                uiManager.playerManager.GetBackEquipmentInventory().Remove(item as BackEquipment);
                uiManager.playerManager.EquipAllEquipmentModels();
            }
            else
            {
                return;
            }
            uiManager.LoadArmorOnEquipmentScreen(uiManager.playerManager);
            uiManager.ResetAllSelectedSlots();
        }

        protected override void ClickedThisSlot()
        {
            EquipThisItem();
            uiManager.uiWindowsLeftPanel.SetBackEquipmentInventoryWindowActive(false);
            base.ClickedThisSlot();
        }
    }
}

