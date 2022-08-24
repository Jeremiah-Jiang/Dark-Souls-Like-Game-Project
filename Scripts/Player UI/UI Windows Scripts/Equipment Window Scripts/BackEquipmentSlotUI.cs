using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class BackEquipmentSlotUI : EquipmentSlotUI
    {
        public override void AddItem(EquipmentItem backEquipment)
        {
            _item = _item as BackEquipment;
            base.AddItem(backEquipment);
        }

        protected override void SelectThisSlot()
        {
            base.SelectThisSlot();
            _uiManager.backEquipmentSlotSelected = true;
        }

        protected override void ClickedThisSlot()
        {
            base.ClickedThisSlot();
            SelectThisSlot();
            _uiManager.uiWindowsLeftPanel.SetBackEquipmentInventoryWindowActive(true);
        }
    }
}

