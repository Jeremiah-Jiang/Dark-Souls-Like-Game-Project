using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class TorsoEquipmentInventorySlot : InventorySlot
    {
        public override void AddItem(EquipmentItem torsoEquipment)
        {
            item = item as TorsoEquipment;
            base.AddItem(torsoEquipment);
        }

        public void EquipThisItem()
        {
            if (uiManager.torsoEquipmentSlotSelected)
            {
                TorsoEquipment currentTorsoEquipment = uiManager.playerManager.GetCurrentTorsoEquipment();
                if (currentTorsoEquipment != null)
                {
                    uiManager.playerManager.GetTorsoEquipmentInventory().Add(currentTorsoEquipment);
                }
                uiManager.playerManager.SetCurrentTorsoEquipment(item as TorsoEquipment);
                uiManager.playerManager.GetTorsoEquipmentInventory().Remove(item as TorsoEquipment);
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
            uiManager.uiWindowsLeftPanel.SetTorsoEquipmentInventoryWindowActive(false);
            base.ClickedThisSlot();
        }
    }
}

