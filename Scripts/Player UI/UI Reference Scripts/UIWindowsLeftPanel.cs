using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class UIWindowsLeftPanel : MonoBehaviour
    {
        public WeaponInventoryWindow weaponInventoryWindow;
        public EquipmentScreenWindow equipmentScreenWindow;
        public ConsumableInventoryWindow consumableInventoryWindow;
        public HeadEquipmentInventoryWindow headEquipmentInventoryWindow;
        public TorsoEquipmentInventoryWindow torsoEquipmentInventoryWindow;
        public BackEquipmentInventoryWindow backEquipmentInventoryWindow;
        public HandEquipmentInventoryWindow handEquipmentInventoryWindow;
        public LegEquipmentInventoryWindow legEquipmentInventoryWindow;

        private void Awake()
        {
            weaponInventoryWindow = GetComponentInChildren<WeaponInventoryWindow>(true);
            equipmentScreenWindow = GetComponentInChildren<EquipmentScreenWindow>(true);
            consumableInventoryWindow = GetComponentInChildren<ConsumableInventoryWindow>(true);
            headEquipmentInventoryWindow = GetComponentInChildren<HeadEquipmentInventoryWindow>(true);
            torsoEquipmentInventoryWindow = GetComponentInChildren<TorsoEquipmentInventoryWindow>(true);
            backEquipmentInventoryWindow = GetComponentInChildren<BackEquipmentInventoryWindow>(true);
            handEquipmentInventoryWindow = GetComponentInChildren<HandEquipmentInventoryWindow>(true);
            legEquipmentInventoryWindow = GetComponentInChildren<LegEquipmentInventoryWindow>(true);
        }

        private void Start()
        {
            CloseAllLeftPanelWindows();
        }
        //Methods for EquipmentScreenWindow
        #region Methods for EquipmentScreenWindow
        public void LoadWeaponsOnEquipmentScreen(PlayerManager player)
        {
            equipmentScreenWindow.LoadWeaponsOnEquipmentScreen(player);
        }

        public void LoadConsumablesOnEquipmentScreen(PlayerManager playerManager)
        {
            equipmentScreenWindow.LoadConsumablesOnEquipmentScreen(playerManager);
        }

        public void LoadArmorOnEquipmentScreen(PlayerManager player)
        {
            equipmentScreenWindow.LoadArmorOnEquipmentScreen(player);
        }
        #endregion

    //Getter Methods for UI Windows Left Panel Children Windows
        #region Getter Methods for UI Windows Left Panel Children Windows
        public WeaponInventoryWindow GetWeaponInventoryWindow()
        {
            return weaponInventoryWindow;
        }

        public EquipmentScreenWindow GetEquipmentScreenWindow()
        {
            return equipmentScreenWindow;
        }

        public ConsumableInventoryWindow GetConsumableInventoryWindow()
        {
            return consumableInventoryWindow;
        }

        public HeadEquipmentInventoryWindow GetHeadEquipmentInventoryWindow()
        {
            return headEquipmentInventoryWindow;
        }

        public TorsoEquipmentInventoryWindow GetTorsoEquipmentInventoryWindow()
        {
            return torsoEquipmentInventoryWindow;
        }

        public BackEquipmentInventoryWindow GetBackEquipmentInventoryWindow()
        {
            return backEquipmentInventoryWindow;
        }

        public HandEquipmentInventoryWindow GetHandEquipmentInventoryWindow()
        {
            return handEquipmentInventoryWindow;
        }

        public LegEquipmentInventoryWindow GetLegEquipmentInventoryWindow()
        {
            return legEquipmentInventoryWindow;
        }
        #endregion

        public void CloseAllLeftPanelWindows()
        {
            SetWeaponInventoryWindowActive(false);
            SetEquipmentScreenWindowActive(false);
            SetConsumableInventoryWindowActive(false);
            SetHeadEquipmentInventoryWindowActive(false);
            SetTorsoEquipmentInventoryWindowActive(false);
            SetBackEquipmentInventoryWindowActive(false);
            SetHandEquipmentInventoryWindowActive(false);
            SetLegEquipmentInventoryWindowActive(false);
        }
    //Individual Methods to Enable/Disable UI Windows Left Panel Children Windows
        #region Individual Methods to Enable/Disable UI Windows Left Panel Children Windows

        public void SetWeaponInventoryWindowActive(bool value)
        {
            weaponInventoryWindow.gameObject.SetActive(value);
        }

        public void SetEquipmentScreenWindowActive(bool value)
        {
            equipmentScreenWindow.gameObject.SetActive(value);
        }
        public void SetConsumableInventoryWindowActive(bool value)
        {
            consumableInventoryWindow.gameObject.SetActive(value);
        }

        public void SetHeadEquipmentInventoryWindowActive(bool value)
        {
            headEquipmentInventoryWindow.gameObject.SetActive(value);
        }

        public void SetTorsoEquipmentInventoryWindowActive(bool value)
        {
            torsoEquipmentInventoryWindow.gameObject.SetActive(value);
        }

        public void SetBackEquipmentInventoryWindowActive(bool value)
        {
            backEquipmentInventoryWindow.gameObject.SetActive(value);
        }

        public void SetHandEquipmentInventoryWindowActive(bool value)
        {
            handEquipmentInventoryWindow.gameObject.SetActive(value);
        }

        public void SetLegEquipmentInventoryWindowActive(bool value)
        {
            legEquipmentInventoryWindow.gameObject.SetActive(value);
        }
        #endregion
    }
}

