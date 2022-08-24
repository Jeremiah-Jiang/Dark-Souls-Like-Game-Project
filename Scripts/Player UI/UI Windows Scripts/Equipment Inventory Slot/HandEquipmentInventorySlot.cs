using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HandEquipmentInventorySlot : InventorySlot
    {
        public override void AddItem(EquipmentItem handEquipment)
        {
            item = item as HandEquipment;
            base.AddItem(handEquipment);
        }

        public void EquipThisItem()
        {
            if (uiManager.handEquipmentSlotSelected)
            {
                HandEquipment currentHandEquipment = uiManager.playerManager.GetCurrentHandEquipment();
                if (currentHandEquipment != null)
                {
                    uiManager.playerManager.GetHandEquipmentInventory().Add(currentHandEquipment);
                }
                uiManager.playerManager.SetCurrentHandEquipment(item as HandEquipment);
                uiManager.playerManager.GetHandEquipmentInventory().Remove(item as HandEquipment);
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
            uiManager.uiWindowsLeftPanel.SetHandEquipmentInventoryWindowActive(false);
            base.ClickedThisSlot();
        }
    }
}

