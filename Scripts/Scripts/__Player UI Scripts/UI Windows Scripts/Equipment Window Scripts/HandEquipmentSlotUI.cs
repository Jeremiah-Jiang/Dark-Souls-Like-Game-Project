using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HandEquipmentSlotUI : EquipmentSlotUI
    {
        public override void AddItem(EquipmentItem handEquipment)
        {
            _item = _item as HandEquipment;
            base.AddItem(handEquipment);
        }

        protected override void SelectThisSlot()
        {
            base.SelectThisSlot();
            _uiManager.handEquipmentSlotSelected = true;
        }

        protected override void ClickedThisSlot()
        {
            base.ClickedThisSlot();
            SelectThisSlot();
            _uiManager.uiWindowsLeftPanel.SetHandEquipmentInventoryWindowActive(true);
        }
    }
}

