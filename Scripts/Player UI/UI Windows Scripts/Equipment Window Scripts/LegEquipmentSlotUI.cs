using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class LegEquipmentSlotUI : EquipmentSlotUI
    {
        public override void AddItem(EquipmentItem legEquipment)
        {
            _item = _item as LegEquipment;
            base.AddItem(legEquipment);
        }

        protected override  void SelectThisSlot()
        {
            base.SelectThisSlot();
            _uiManager.legEquipmentSlotSelected = true;
        }

        protected override void ClickedThisSlot()
        {
            base.ClickedThisSlot();
            SelectThisSlot();
            _uiManager.uiWindowsLeftPanel.SetLegEquipmentInventoryWindowActive(true);
        }
    }
}

