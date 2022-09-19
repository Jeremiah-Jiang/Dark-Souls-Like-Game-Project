using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class TorsoEquipmentSlotUI : EquipmentSlotUI
    {
        public override void AddItem(EquipmentItem torsoEquipment)
        {
            _item = _item as TorsoEquipment;
            base.AddItem(torsoEquipment);
        }

        protected override void  SelectThisSlot()
        {
            base.SelectThisSlot();
            _uiManager.torsoEquipmentSlotSelected = true;
        }

        protected override void ClickedThisSlot()
        {
            base.ClickedThisSlot();
            SelectThisSlot();
            _uiManager.uiWindowsLeftPanel.SetTorsoEquipmentInventoryWindowActive(true);
        }
    }
}

