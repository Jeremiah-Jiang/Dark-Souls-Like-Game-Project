using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HeadEquipmentSlotUI : EquipmentSlotUI
    {

        public override void AddItem(EquipmentItem headEquipment)
        {
            _item = _item as HelmetEquipment;
            base.AddItem(headEquipment);
        }

        protected override void SelectThisSlot()
        {
            base.SelectThisSlot();
            _uiManager.headEquipmentSlotSelected = true;
            
        }

        protected override void ClickedThisSlot()
        {
            base.ClickedThisSlot();
            SelectThisSlot();
            _uiManager.uiWindowsLeftPanel.SetHeadEquipmentInventoryWindowActive(true);
        }
    }
}

