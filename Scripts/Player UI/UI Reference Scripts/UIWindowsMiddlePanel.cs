using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class UIWindowsMiddlePanel : MonoBehaviour
    {
        public ItemStatsWindow itemStatsWindow;

        private void Awake()
        {
            itemStatsWindow = GetComponentInChildren<ItemStatsWindow>(true);
        }

        private void Start()
        {
            CloseAllMiddlePanelWindows();
        }

        public void CloseAllMiddlePanelWindows()
        {
            SetItemStatsWindowActive(false);
        }
        public void SetItemStatsWindowActive(bool value)
        {
            itemStatsWindow.gameObject.SetActive(value);
        }

    //ItemStatsWindow Methods
        #region ItemStatsWindow Methods
        public void UpdateWeaponItemStats(WeaponItem weaponItem)
        {
            itemStatsWindow.UpdateWeaponItemStats(weaponItem);
        }

        public void UpdateArmourItemStats(EquipmentItem armour)
        {
            itemStatsWindow.UpdateArmourItemStats(armour);
        }

        public void UpdateConsumableItemStats(ConsumableItem consumableItem, int currentCapacity = 0)
        {
            itemStatsWindow.UpdateConsumableItemStats(consumableItem, currentCapacity);
        }
        #endregion
    }
}

