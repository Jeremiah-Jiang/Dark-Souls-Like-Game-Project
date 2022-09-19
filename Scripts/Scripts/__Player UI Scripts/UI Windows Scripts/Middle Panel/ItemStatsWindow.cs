using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class ItemStatsWindow : MonoBehaviour
    {
        public Text itemNameText;
        public Image itemIconImage;
        [Header("Equipment Stats Window")]
        public GameObject consumableStats;
        [SerializeField] private WeaponStatsMiddlePanelUI _weaponStatsMiddlePanelUI;
        [SerializeField] private ArmourStatsMiddlePanelUI _armourStatsMiddlePanelUI;
        [SerializeField] private ConsumableStatsMiddlePanelUI _consumableStatsMiddlePanelUI;

        private void Awake()
        {
            _weaponStatsMiddlePanelUI = GetComponentInChildren<WeaponStatsMiddlePanelUI>(true);
            _armourStatsMiddlePanelUI = GetComponentInChildren<ArmourStatsMiddlePanelUI>(true);
            _consumableStatsMiddlePanelUI = GetComponentInChildren<ConsumableStatsMiddlePanelUI>(true);
        }

        private void Start()
        {
            CloseAllStatWindows();
        }

        //Update Weapon Item Stats
        public void UpdateWeaponItemStats(WeaponItem weaponItem)
        {
            CloseAllStatWindows();
            if(weaponItem != null)
            {
                if (weaponItem.itemName != null)
                {
                    itemNameText.text = weaponItem.itemName;
                }
                else
                {
                    itemNameText.text = "Missing Weapon Name!";
                }
                if (weaponItem.itemIcon != null)
                {
                    itemIconImage.enabled = true;
                    itemIconImage.sprite = weaponItem.itemIcon;
                }
                else
                {
                    itemIconImage.enabled = false;
                    itemIconImage.sprite = null;
                }
                _weaponStatsMiddlePanelUI.SetAllWeaponStatTexts(weaponItem);
            }
            else
            {
                itemNameText.text = "";
                itemIconImage.enabled = false;
                itemIconImage.sprite = null;
                SetWeaponStatsWindowActive(false);
            }

        }

        //Update Armor Item Stats
        public void UpdateArmourItemStats(EquipmentItem armour)
        {
            CloseAllStatWindows();
            if (armour != null)
            {
                if (armour.itemName != null)
                {
                    itemNameText.text = armour.itemName;
                }
                else
                {
                    itemNameText.text = "Missing Armour Name!";
                }
                if (armour.itemIcon != null)
                {
                    itemIconImage.enabled = true;
                    itemIconImage.sprite = armour.itemIcon;
                }
                else
                {
                    itemIconImage.enabled = false;
                    itemIconImage.sprite = null;
                }
                _armourStatsMiddlePanelUI.SetAllArmourStatTexts(armour);
            }
            else
            {
                itemNameText.text = "";
                itemIconImage.enabled = false;
                itemIconImage.sprite = null;
                SetArmourStatsWindowActive(false);
            }

        }

        //Update Consumable Item Stats
        public void UpdateConsumableItemStats(ConsumableItem consumableItem, int currentCapacity = 0)
        {
            CloseAllStatWindows();
            if (consumableItem != null)
            {
                if (consumableItem.itemName != null)
                {
                    itemNameText.text = consumableItem.itemName;
                }
                else
                {
                    itemNameText.text = "Missing Consumable Name!";
                }
                if (consumableItem.itemIcon != null)
                {
                    itemIconImage.enabled = true;
                    itemIconImage.sprite = consumableItem.itemIcon;
                }
                else
                {
                    itemIconImage.enabled = false;
                    itemIconImage.sprite = null;
                }
                _consumableStatsMiddlePanelUI.SetAllConsumableItemStatsTexts(consumableItem, currentCapacity);
            }
            else
            {
                itemNameText.text = "";
                itemIconImage.enabled = false;
                itemIconImage.sprite = null;
                SetConsumableStatsWindowActive(false);
            }
        }

        //Update Ring Item Stats Etc Eetc
        private void CloseAllStatWindows()
        {
            SetWeaponStatsWindowActive(false);
            SetArmourStatsWindowActive(false);
            SetConsumableStatsWindowActive(false);
        }

        private void SetWeaponStatsWindowActive(bool value)
        {
            _weaponStatsMiddlePanelUI.SetWeaponStatsWindowActive(value);
        }

        private void SetArmourStatsWindowActive(bool value)
        {
            _armourStatsMiddlePanelUI.SetArmourStatsWindowActive(value);
        }

        private void SetConsumableStatsWindowActive(bool value)
        {
            _consumableStatsMiddlePanelUI.SetConsumableItemStatsWindowActive(value);
        }
    }
}

