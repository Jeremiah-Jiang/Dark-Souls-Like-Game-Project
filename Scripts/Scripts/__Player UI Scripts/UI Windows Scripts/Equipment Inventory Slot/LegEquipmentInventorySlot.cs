using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class LegEquipmentInventorySlot : InventorySlot
    {
        public override void AddItem(EquipmentItem legEquipment)
        {
            item = item as LegEquipment;
            base.AddItem(legEquipment);
        }

        public void EquipThisItem()
        {
            if (uiManager.legEquipmentSlotSelected)
            {
                LegEquipment currentLegEquipment = uiManager.playerManager.GetCurrentLegEquipment();
                if (currentLegEquipment != null)
                {
                    uiManager.playerManager.GetLegEquipmentInventory().Add(currentLegEquipment);
                }
                uiManager.playerManager.SetCurrentLegEquipment(item as LegEquipment);
                uiManager.playerManager.GetLegEquipmentInventory().Remove(item as LegEquipment);
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
            uiManager.uiWindowsLeftPanel.SetLegEquipmentInventoryWindowActive(false);
            base.ClickedThisSlot();
        }
    }
}