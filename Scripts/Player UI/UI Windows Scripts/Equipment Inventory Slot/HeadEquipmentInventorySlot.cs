using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HeadEquipmentInventorySlot : InventorySlot
    {

        public override void AddItem(EquipmentItem helmetEquipment)
        {
            item = item as HelmetEquipment;
            base.AddItem(helmetEquipment);
        }

        public void EquipThisItem()
        {
            if (uiManager.headEquipmentSlotSelected)
            {
                HelmetEquipment currentHelmetEquipment = uiManager.playerManager.GetCurrentHelmetEquipment();
                if (currentHelmetEquipment != null)
                {
                    uiManager.playerManager.GetHeadEquipmentInventory().Add(currentHelmetEquipment);
                }
                uiManager.playerManager.SetCurrentHelmetEquipment(item as HelmetEquipment);
                uiManager.playerManager.GetHeadEquipmentInventory().Remove(item as HelmetEquipment);
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
            uiManager.uiWindowsLeftPanel.SetHeadEquipmentInventoryWindowActive(false);
            base.ClickedThisSlot();
        }
    }
}

