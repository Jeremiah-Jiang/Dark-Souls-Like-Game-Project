using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class UIManager : MonoBehaviour
    {
        public PlayerManager playerManager;
        

        [Header("HUD")]
        public HUDWindow hudWindow;

        [Header("UI Windows")]
        public UIWindows uiWindows;
        public SelectWindow selectWindow;
        public UIWindowsLeftPanel uiWindowsLeftPanel;
        public UIWindowsMiddlePanel uiWindowsMiddlePanel;
        public UIWindowsRightPanel uiWindowsRightPanel;
        public LevelUpWindow levelUpWindow;

        [Header("Pop Ups")]
        PopUpUI popUpUI;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;
        public bool consumableSlot01Selected;
        public bool consumableSlot02Selected;

        public bool headEquipmentSlotSelected;
        public bool torsoEquipmentSlotSelected;
        public bool backEquipmentSlotSelected;
        public bool handEquipmentSlotSelected;
        public bool legEquipmentSlotSelected;
        

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParent;
        WeaponInventorySlot[] weaponInventorySlots;

        [Header("Consumable Inventory")]
        public GameObject consumableInventorySlotPrefab;
        public Transform consumableInventorySlotsParent;
        ConsumableInventorySlot[] consumableInventorySlots;

        [Header("Head Equipment Inventory")]
        public GameObject headEquipmentInventorySlotPrefab;
        public Transform headEquipmentInventorySlotParent;
        HeadEquipmentInventorySlot[] headEquipmentInventorySlots;

        [Header("Torso Equipment Inventory")]
        public GameObject torsoEquipmentInventorySlotPrefab;
        public Transform torsoEquipmentInventorySlotParent;
        TorsoEquipmentInventorySlot[] torsoEquipmentInventorySlots;

        [Header("Back Equipment Inventory")]
        public GameObject backEquipmentInventorySlotPrefab;
        public Transform backEquipmentInventorySlotParent;
        BackEquipmentInventorySlot[] backEquipmentInventorySlots;

        [Header("Leg Equipment Inventory")]
        public GameObject legEquipmentInventorySlotPrefab;
        public Transform legEquipmentInventorySlotParent;
        LegEquipmentInventorySlot[] legEquipmentInventorySlots;

        [Header("Hand Equipment Inventory")]
        public GameObject handEquipmentInventorySlotPrefab;
        public Transform handEquipmentInventorySlotParent;
        HandEquipmentInventorySlot[] handEquipmentInventorySlots;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();

            hudWindow = GetComponentInChildren<HUDWindow>();

            uiWindows = GetComponentInChildren<UIWindows>();
            selectWindow = uiWindows.GetComponentInChildren<SelectWindow>(true);
            uiWindowsLeftPanel = uiWindows.GetComponentInChildren<UIWindowsLeftPanel>();
            uiWindowsMiddlePanel = uiWindows.GetComponentInChildren<UIWindowsMiddlePanel>();
            uiWindowsRightPanel = uiWindows.GetComponentInChildren<UIWindowsRightPanel>();
            levelUpWindow = GetComponentInChildren<LevelUpWindow>(true);

            popUpUI = GetComponentInChildren<PopUpUI>();

            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
            consumableInventorySlots = consumableInventorySlotsParent.GetComponentsInChildren<ConsumableInventorySlot>();
            headEquipmentInventorySlots = headEquipmentInventorySlotParent.GetComponentsInChildren<HeadEquipmentInventorySlot>();
            torsoEquipmentInventorySlots = torsoEquipmentInventorySlotParent.GetComponentsInChildren<TorsoEquipmentInventorySlot>();
            backEquipmentInventorySlots = backEquipmentInventorySlotParent.GetComponentsInChildren<BackEquipmentInventorySlot>();
            legEquipmentInventorySlots = legEquipmentInventorySlotParent.GetComponentsInChildren<LegEquipmentInventorySlot>();
            handEquipmentInventorySlots = handEquipmentInventorySlotParent.GetComponentsInChildren<HandEquipmentInventorySlot>();
        }
        private void Start()
        {
            LoadArmorOnEquipmentScreen(playerManager);
            LoadWeaponsOnEquipmentScreen(playerManager);
            LoadConsumablesOnEquipmentScreen(playerManager);
            UpdateCurrentConsumableIcon(playerManager.GetCurrentConsumableItem());
            UpdateCurrentSpellIcon(playerManager.GetCurrentSpell());
            SetSoulCountText(playerManager.GetCurrentSoulCount());

            SetMoralAlignmentIcon();
            SetPlayerName(playerManager.GetCharacterName());
            SetCoreStatLevelsOnRightPanel();
            SetCurrentAndMaxStatPointsOnRightPanel();
            SetPlayerEquipmentPoiseOnRightPanel();
            SetDamageAbsorptionValues();
        }

        public void ActivateBonFirePopUp()
        {
            popUpUI.DisplayBonfireLitTextPopUP();
        }

        public void UpdateUI()
        {
            #region Weapon Inventory Slots
            for(int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if(i < playerManager.GetWeaponsInventory().Count)
                {
                    if(weaponInventorySlots.Length < playerManager.GetWeaponsInventory().Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerManager.GetWeaponsInventory()[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            #region Consumable InventorySlots
            for (int i = 0; i < consumableInventorySlots.Length; i++)
            {
                if(i < playerManager.GetConsumablesInventory().Count)
                {
                    if(consumableInventorySlots.Length < playerManager.GetConsumablesInventory().Count)
                    {
                        Instantiate(consumableInventorySlotPrefab, consumableInventorySlotsParent);
                        consumableInventorySlots = consumableInventorySlotsParent.GetComponentsInChildren<ConsumableInventorySlot>();
                    }
                    consumableInventorySlots[i].AddItem(playerManager.GetConsumablesInventory()[i]);
                }
                else
                {
                    consumableInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            #region Head Equipment Inventory Slots
            for (int i = 0; i < headEquipmentInventorySlots.Length; i++)
            {
                if(i < playerManager.GetHeadEquipmentInventory().Count)
                {
                    if(headEquipmentInventorySlots.Length < playerManager.GetHeadEquipmentInventory().Count)
                    {
                        Instantiate(headEquipmentInventorySlotPrefab, headEquipmentInventorySlotParent);
                        headEquipmentInventorySlots = headEquipmentInventorySlotParent.GetComponentsInChildren<HeadEquipmentInventorySlot>();
                    }
                    headEquipmentInventorySlots[i].AddItem(playerManager.GetHeadEquipmentInventory()[i]);
                }
                else
                {
                    headEquipmentInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            #region Torso Equipment Inventory Slots
            for (int i = 0; i < torsoEquipmentInventorySlots.Length; i++)
            {
                if (i < playerManager.GetTorsoEquipmentInventory().Count)
                {
                    if (torsoEquipmentInventorySlots.Length < playerManager.GetTorsoEquipmentInventory().Count)
                    {
                        Instantiate(torsoEquipmentInventorySlotPrefab, torsoEquipmentInventorySlotParent);
                        torsoEquipmentInventorySlots = torsoEquipmentInventorySlotParent.GetComponentsInChildren<TorsoEquipmentInventorySlot>();
                    }
                    torsoEquipmentInventorySlots[i].AddItem(playerManager.GetTorsoEquipmentInventory()[i]);
                }
                else
                {
                    torsoEquipmentInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            #region Back Equipment Inventory Slots
            for (int i = 0; i < backEquipmentInventorySlots.Length; i++)
            {
                if (i < playerManager.GetBackEquipmentInventory().Count)
                {
                    if (backEquipmentInventorySlots.Length < playerManager.GetBackEquipmentInventory().Count)
                    {
                        Instantiate(backEquipmentInventorySlotPrefab, backEquipmentInventorySlotParent);
                        backEquipmentInventorySlots = backEquipmentInventorySlotParent.GetComponentsInChildren<BackEquipmentInventorySlot>();
                    }
                    backEquipmentInventorySlots[i].AddItem(playerManager.GetBackEquipmentInventory()[i]);
                }
                else
                {
                    backEquipmentInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            #region Leg Equipment InventorySlots
            for (int i = 0; i < legEquipmentInventorySlots.Length; i++)
            {
                if (i < playerManager.GetLegEquipmentInventory().Count)
                {
                    if (legEquipmentInventorySlots.Length < playerManager.GetLegEquipmentInventory().Count)
                    {
                        Instantiate(legEquipmentInventorySlotPrefab, legEquipmentInventorySlotParent);
                        legEquipmentInventorySlots = legEquipmentInventorySlotParent.GetComponentsInChildren<LegEquipmentInventorySlot>();
                    }
                    legEquipmentInventorySlots[i].AddItem(playerManager.GetLegEquipmentInventory()[i]);
                }
                else
                {
                    legEquipmentInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            #region Hand Equipment Inventory Slots
            for (int i = 0; i < handEquipmentInventorySlots.Length; i++)
            {
                if (i < playerManager.GetHandEquipmentInventory().Count)
                {
                    if (handEquipmentInventorySlots.Length < playerManager.GetHandEquipmentInventory().Count)
                    {
                        Instantiate(handEquipmentInventorySlotPrefab, handEquipmentInventorySlotParent);
                        handEquipmentInventorySlots = handEquipmentInventorySlotParent.GetComponentsInChildren<HandEquipmentInventorySlot>();
                    }
                    handEquipmentInventorySlots[i].AddItem(playerManager.GetHandEquipmentInventory()[i]);
                }
                else
                {
                    handEquipmentInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion

            #region Player Stats
            //SetPlayerName(playerManager.GetCharacterName());
            SetMoralAlignmentIcon();
            SetCoreStatLevelsOnRightPanel();
            SetCurrentAndMaxStatPointsOnRightPanel();
            SetPlayerEquipmentPoiseOnRightPanel();
            SetDamageAbsorptionValues();
            #endregion
        }

        public void OpenSelectWindow(bool value)
        {
            selectWindow.SetSelectWindowActive(value);
        }

        public void CloseAllInventoryWindows()
        {
            ResetAllSelectedSlots();
            uiWindowsLeftPanel.CloseAllLeftPanelWindows();
            uiWindowsMiddlePanel.CloseAllMiddlePanelWindows();
            uiWindowsRightPanel.SetPlayerStatsWindowActive(false);
        }

        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
            
            consumableSlot01Selected = false;
            consumableSlot02Selected = false;

            headEquipmentSlotSelected = false;
            legEquipmentSlotSelected = false;
            handEquipmentSlotSelected = false;
            torsoEquipmentSlotSelected = false;
            backEquipmentSlotSelected = false;
            
        }

    //HUDWindow Methods
        #region HUDWindow Methods
        public void SetCrossHairActive(bool value)
        {
            hudWindow.SetCrossHairActive(value);
        }

        public void SetMaxHealthOnSlider(int maxHealth)
        {
            hudWindow.SetMaxHealthOnSlider(maxHealth);
        }

        public void SetCurrentHealthOnSlider(int currentHealth)
        {
            hudWindow.SetCurrentHealthOnSlider(currentHealth);
        }

        public void SetCurrentHealthOnSliderNoDamageTaken(int currentHealth)
        {
            hudWindow.SetCurrentHealthOnSliderNoDamageTaken(currentHealth);
        }

        public void SetMaxStaminaOnSlider(float maxStamina)
        {
            hudWindow.SetMaxStaminaOnSlider(maxStamina);
        }

        public void SetCurrentStaminaOnSlider(float currentStamina)
        {
            hudWindow.SetCurrentStaminaOnSlider(currentStamina);
        }
 
        public void SetMaxFocusPointsOnSlider(float maxFocusPoints)
        {
            hudWindow.SetMaxFocusPointsOnSlider(maxFocusPoints);
        }

        public void SetCurrentStaminaOnSliderNoDamageTaken(float currentStamina)
        {
            hudWindow.SetCurrentStaminaOnSliderNoDamageTaken(currentStamina);
        }

        public void SetCurrentFocusPointsOnSlider(float currentFocusPoints)
        {
            hudWindow.SetCurrentFocusPointsOnSlider(currentFocusPoints);
        }

        public void SetCurrentFocusPointsOnSliderNoDamageTaken(float currentFocusPoints)
        {
            hudWindow.SetCurrentFocusPointsOnSliderNoDamageTaken(currentFocusPoints);
        }

        public void SetPoisonBuildUpBarActive(bool value)
        {
            hudWindow.SetPoisonBuildUpBarActive(value);
        }

        public void SetPoisonBuildUpAmount(int currentPoisonBuildUp)
        {
            hudWindow.SetPoisonBuildUpAmount(currentPoisonBuildUp);
        }

        public void SetPoisonAmountBarActive(bool value)
        {
            hudWindow.SetPoisonAmountBarActive(value);
        }

        public void SetPoisonAmountOnSlider(int poisonAmount)
        {
            hudWindow.SetPoisonAmountOnSlider(poisonAmount);
        }

        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weaponItem)
        {
            hudWindow.UpdateWeaponQuickSlotsUI(isLeft, weaponItem);
        }

        public void UpdateCurrentSpellIcon(SpellItem spellItem)
        {
            hudWindow.UpdateCurrentSpellIcon(spellItem);
        }

        public void UpdateCurrentConsumableIcon(ConsumableItem consumableItem)
        {
            hudWindow.UpdateCurrentConsumableIcon(consumableItem);
        }

        public void SetHUDNeutralMoralAlignmentIconActive()
        {
            hudWindow.SetNeutralMoralAlignmentIconActive();
        }

        public void SetHUDDarknessMoralAlignmentIconActive()
        {
            hudWindow.SetDarknessMoralAlignmentIconActive();
        }

        public void SetHUDHolyMoralAlignmentIconActive()
        {
            hudWindow.SetHolyMoralAlignmentIconActive();
        }
        public void SetHUDWindowActive(bool value)
        {
            hudWindow.SetHUDWindowActive(value);
        }

        public void SetSoulCountText(int souls)
        {
            hudWindow.SetSoulCountText(souls);
        }
        #endregion

    //UIWindowsLeftPanel Methods
        #region UIWindowsLeftPanel Methods
        public void LoadWeaponsOnEquipmentScreen(PlayerManager playerManager)
        {
            uiWindowsLeftPanel.LoadWeaponsOnEquipmentScreen(playerManager);
        }

        public void LoadConsumablesOnEquipmentScreen(PlayerManager playerManager)
        {
            uiWindowsLeftPanel.LoadConsumablesOnEquipmentScreen(playerManager);
        }

        public void LoadArmorOnEquipmentScreen(PlayerManager playerManager)
        {
            uiWindowsLeftPanel.LoadArmorOnEquipmentScreen(playerManager);
        }
        #endregion

    //UIWindowsMiddlePanel Methods
        #region UIWindowsMiddlePanel Methods
        public void UpdateWeaponItemStats(WeaponItem weaponItem)
        {
            uiWindowsMiddlePanel.UpdateWeaponItemStats(weaponItem);
        }

        public void UpdateArmourItemStats(EquipmentItem equipmentItem)
        {
            uiWindowsMiddlePanel.UpdateArmourItemStats(equipmentItem);
        }

        public void UpdateConsumableItemStats(ConsumableItem consumableItem, int currentCapacity = 0)
        {
            uiWindowsMiddlePanel.UpdateConsumableItemStats(consumableItem, currentCapacity);
        }
        #endregion

    //UIWindowsRightPanel Methods
        #region UIWindowsRightPanel Methods
        public void SetPlayerStatsWindowActive(bool value)
        {
            uiWindowsRightPanel.SetPlayerStatsWindowActive(value);
        }

        public void SetPlayerName(string playerName)
        {
            uiWindowsRightPanel.SetPlayerName(playerName);
        }

        public void SetRightPanelDarknessMoralAlignmentIconActive()
        {
            uiWindowsRightPanel.SetDarknessMoralAlignmentIconActive();
        }

        public void SetRightPanelNeutralMoralAlignmentIconActive()
        {
            uiWindowsRightPanel.SetNeutralAlignmentIconActive();
        }

        public void SetRightPanelHolyMoralAlignmentIconActive()
        {
            uiWindowsRightPanel.SetHolyAlignmentIconActive();
        }

        public void SetPlayerLevelValueText(int playerLevel)
        {
            uiWindowsRightPanel.SetPlayerLevelValueText(playerLevel.ToString());
        }

        public void SetPlayerSoulsValueText(int souls)
        {
            uiWindowsRightPanel.SetPlayerSoulsValueText(souls.ToString());
        }

        public void SetHealthLevelValue(int healthLevel)
        {
            uiWindowsRightPanel.SetHealthLevelValue(healthLevel);
        }

        public void SetStaminaLevelValue(int staminaLevel)
        {
            uiWindowsRightPanel.SetStaminaLevelValue(staminaLevel);
        }

        public void SetFocusPointsLevelValue(int focusPointsLevel)
        {
            uiWindowsRightPanel.SetFocusPointsLevelValue(focusPointsLevel);
        }

        public void SetPoiseLevelValue(int poiseLevel)
        {
            uiWindowsRightPanel.SetPoiseLevelValue(poiseLevel);
        }

        public void SetStrengthLevelValue(int strengthLevel)
        {
            uiWindowsRightPanel.SetStrengthLevelValue(strengthLevel);
        }

        public void SetDexterityLevelValue(int dexterityLevel)
        {
            uiWindowsRightPanel.SetDexterityLevelValue(dexterityLevel);
        }

        public void SetFaithLevelValue(int faithLevel)
        {
            uiWindowsRightPanel.SetFaithLevelValue(faithLevel);
        }

        public void SetIntelligenceLevelValue(int intelligenceLevel)
        {
            uiWindowsRightPanel.SetIntelligenceLevelValue(intelligenceLevel);
        }

        public void SetCurrentHealthText(int currentHealth)
        {
            uiWindowsRightPanel.SetCurrentHealthText(currentHealth);
        }

        public void SetMaxHealthText(int maxHealth)
        {
            uiWindowsRightPanel.SetMaxHealthText(maxHealth);
        }

        public void SetCurrentStaminaText(int currentStamina)
        {
            uiWindowsRightPanel.SetCurrentStaminaText(currentStamina);
        }

        public void SetMaxStaminaText(int maxStamina)
        {
            uiWindowsRightPanel.SetMaxStaminaText(maxStamina);
        }

        public void SetCurrentFocusPointsText(int currentFocusPoints)
        {
            uiWindowsRightPanel.SetCurrentFocusPointsText(currentFocusPoints);
        }

        public void SetMaxFocusPointsText(int maxFocusPoints)
        {
            uiWindowsRightPanel.SetMaxFocusPointsText(maxFocusPoints);
        }

        public void SetPlayerEquipmentPoiseText(float poise)
        {
            uiWindowsRightPanel.SetPlayerEquipmentPoiseText(poise);
        }

        public void SetPhysicalDamageAbsorptionValueText(float physicalDamageAbsorptionValue)
        {
            uiWindowsRightPanel.SetPhysicalDamageAbsorptionValueText(physicalDamageAbsorptionValue);
        }

        public void SetFireDamageAbsorptionValueText(float fireDamageAbsorptionValue)
        {
            uiWindowsRightPanel.SetFireDamageAbsorptionValueText(fireDamageAbsorptionValue);
        }

        public void SetLightningDamageAbsorptionValueText(float lightningDamageAbsorptionValue)
        {
            uiWindowsRightPanel.SetLightningDamageAbsorptionValueText(lightningDamageAbsorptionValue);
        }

        public void SetUmbraDamageAbsorptionValueText(float umbraDamageAbsorptionValue)
        {
            uiWindowsRightPanel.SetUmbraDamageAbsorptionValueText(umbraDamageAbsorptionValue);
        }

        public void SetMagicDamageAbsorptionValueText(float magicDamageAbsorptionValue)
        {
            uiWindowsRightPanel.SetMagicDamageAbsorptionValueText(magicDamageAbsorptionValue);
        }

        public void SetMoralAlignmentIcon()
        {
            if(playerManager.IsNeutral())
            {
                SetHUDNeutralMoralAlignmentIconActive();
                SetRightPanelNeutralMoralAlignmentIconActive();
            }
            else if(playerManager.IsDark())
            {
                SetHUDDarknessMoralAlignmentIconActive();
                SetRightPanelDarknessMoralAlignmentIconActive();
            }
            else if(playerManager.IsHoly())
            {
                SetHUDHolyMoralAlignmentIconActive();
                SetRightPanelHolyMoralAlignmentIconActive();
            }
            else
            {
                SetHUDNeutralMoralAlignmentIconActive();
                SetRightPanelNeutralMoralAlignmentIconActive();
            }
        }
        public void SetCoreStatLevelsOnRightPanel()
        {
            SetPlayerLevelValueText(playerManager.GetPlayerLevel());
            SetPlayerSoulsValueText(playerManager.GetCurrentSoulCount());
            SetHealthLevelValue(playerManager.GetHealthLevel());
            SetStaminaLevelValue(playerManager.GetStaminaLevel());
            SetFocusPointsLevelValue(playerManager.GetFocusLevel());
            SetPoiseLevelValue(playerManager.GetPoiseLevel());
            SetStrengthLevelValue(playerManager.GetStrengthLevel());
            SetDexterityLevelValue(playerManager.GetDexterityLevel());
            SetFaithLevelValue(playerManager.GetFaithLevel());
            SetIntelligenceLevelValue(playerManager.GetIntelligenceLevel());
        }

        //Order matters, set max stat points then current stat points because the text color change logic occurs when setting current stats
        public void SetCurrentAndMaxStatPointsOnRightPanel()
        {
            SetMaxHealthText(playerManager.GetMaxHealth());
            SetMaxStaminaText(Mathf.RoundToInt(playerManager.GetMaxStamina()));
            SetMaxFocusPointsText(Mathf.RoundToInt(playerManager.GetMaxFocusPoints()));

            SetCurrentHealthText(playerManager.GetCurrentHealth());
            SetCurrentStaminaText(Mathf.RoundToInt(playerManager.GetCurrentStamina()));
            SetCurrentFocusPointsText(Mathf.RoundToInt(playerManager.GetCurrentFocusPoints()));
        }

        public void SetPlayerEquipmentPoiseOnRightPanel()
        {
            SetPlayerEquipmentPoiseText(playerManager.GetTotalPoiseFromEquipment());
        }

        public void SetDamageAbsorptionValues()
        {
            SetPhysicalDamageAbsorptionValueText(playerManager.GetTotalUnguardedPhysicalDamageNegation());
            SetFireDamageAbsorptionValueText(playerManager.GetTotalUnguardedFireDamageNegation());
            SetLightningDamageAbsorptionValueText(playerManager.GetTotalUnguardedLightningDamageNegation());
            SetUmbraDamageAbsorptionValueText(playerManager.GetTotalUnguardedUmbraDamageNegation());
            SetMagicDamageAbsorptionValueText(playerManager.GetTotalUnguardedMagicDamageNegation());
        }
        #endregion



    }
}

