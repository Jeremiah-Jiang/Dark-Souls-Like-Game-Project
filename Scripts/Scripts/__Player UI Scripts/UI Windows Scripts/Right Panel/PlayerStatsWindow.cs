using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class PlayerStatsWindow : MonoBehaviour
    {
        [SerializeField]
        PlayerNameRightPanelUI _playerNameRightPanelUI;
        [SerializeField]
        PlayerMoralAlignmentRightPanelUI _playerMoralAlignmentRightPanelUI;
        [SerializeField]
        PlayerLevelAndRunesRightPanelUI _playerLevelAndRunesRightPanelUI;
        [SerializeField]
        PlayerCoreStatsRightPanelUI _playerCoreStatsRightPanelUI;
        [SerializeField]
        PlayerStatsPointsRightPanelUI _playerStatsPointsRightPanelUI;
        [SerializeField]
        PlayerDamageAbsorptionRightPanelUI _playerDamageAbsorptionRightPanelUI;
        private void Awake()
        {
            _playerNameRightPanelUI = GetComponentInChildren<PlayerNameRightPanelUI>();
            _playerMoralAlignmentRightPanelUI = GetComponentInChildren<PlayerMoralAlignmentRightPanelUI>();
            _playerLevelAndRunesRightPanelUI = GetComponentInChildren<PlayerLevelAndRunesRightPanelUI>();
            _playerCoreStatsRightPanelUI = GetComponentInChildren<PlayerCoreStatsRightPanelUI>();
            _playerStatsPointsRightPanelUI = GetComponentInChildren<PlayerStatsPointsRightPanelUI>();
            _playerDamageAbsorptionRightPanelUI = GetComponentInChildren<PlayerDamageAbsorptionRightPanelUI>();
        }

        public void SetPlayerName(string playerName)
        {
            _playerNameRightPanelUI.SetPlayerName(playerName);
        }

        public void SetDarknessMoralAlignmentIconActive()
        {
            _playerMoralAlignmentRightPanelUI.SetDarknessMoralAlignmentIconActive();
        }

        public void SetNeutralMoralAlignmentIconActive()
        {
            _playerMoralAlignmentRightPanelUI.SetNeutralMoralAlignmentIconActive();
        }

        public void SetHolyMoralAlignmentIconActive()
        {
            _playerMoralAlignmentRightPanelUI.SetHolyMoralAlignmentIconActive();
        }

        public void SetPlayerLevelValueText(string playerLevel)
        {
            _playerLevelAndRunesRightPanelUI.SetPlayerLevelValueText(playerLevel);
        }

        public void SetPlayerSoulsValueText(string souls)
        {
            _playerLevelAndRunesRightPanelUI.SetPlayerSoulsValueText(souls);
        }

        public void SetHealthLevelValue(int healthLevel)
        {
            _playerCoreStatsRightPanelUI.SetHealthLevelValue(healthLevel);
        }

        public void SetStaminaLevelValue(int staminaLevel)
        {
            _playerCoreStatsRightPanelUI.SetStaminaLevelValue(staminaLevel);
        }

        public void SetFocusPointsLevelValue(int focusPointsLevel)
        {
            _playerCoreStatsRightPanelUI.SetFocusPointsLevelValue(focusPointsLevel);
        }

        public void SetPoiseLevelValue(int poiseLevel)
        {
            _playerCoreStatsRightPanelUI.SetPoiseLevelValue(poiseLevel);
        }

        public void SetStrengthLevelValue(int strengthLevel)
        {
            _playerCoreStatsRightPanelUI.SetStrengthLevelValue(strengthLevel);
        }

        public void SetDexterityLevelValue(int dexterityLevel)
        {
            _playerCoreStatsRightPanelUI.SetDexterityLevelValue(dexterityLevel);
        }

        public void SetFaithLevelValue(int faithLevel)
        {
            _playerCoreStatsRightPanelUI.SetFaithLevelValue(faithLevel);
        }

        public void SetIntelligenceLevelValue(int intelligenceLevel)
        {
            _playerCoreStatsRightPanelUI.SetIntelligenceLevelValue(intelligenceLevel);
        }

        public void SetCurrentHealthText(int currentHealth)
        {
            _playerStatsPointsRightPanelUI.SetCurrentHealthText(currentHealth);
        }

        public void SetMaxHealthText(int maxHealth)
        {
            _playerStatsPointsRightPanelUI.SetMaxHealthText(maxHealth);
        }

        public void SetCurrentStaminaText(int currentStamina)
        {
            _playerStatsPointsRightPanelUI.SetCurrentStaminaText(currentStamina);
        }

        public void SetMaxStaminaText(int maxStamina)
        {
            _playerStatsPointsRightPanelUI.SetMaxStaminaText(maxStamina);
        }

        public void SetCurrentFocusPointsText(int currentFocusPoints)
        {
            _playerStatsPointsRightPanelUI.SetCurrentFocusPointsText(currentFocusPoints);
        }

        public void SetMaxFocusPointsText(int maxFocusPoints)
        {
            _playerStatsPointsRightPanelUI.SetMaxFocusPointsText(maxFocusPoints);
        }

        public void SetPlayerEquipmentPoiseText(float poise)
        {
            _playerStatsPointsRightPanelUI.SetPlayerEquipmentPoiseText(poise);
        }

        public void SetPhysicalDamageAbsorptionValueText(float physicalDamageAbsorptionValue)
        {
            if(_playerDamageAbsorptionRightPanelUI != null)
            {
                _playerDamageAbsorptionRightPanelUI.SetPhysicalDamageAbsorptionValueText(physicalDamageAbsorptionValue);
            }
        }

        public void SetFireDamageAbsorptionValueText(float fireDamageAbsorptionValue)
        {
            if(_playerDamageAbsorptionRightPanelUI != null)
            {
                _playerDamageAbsorptionRightPanelUI.SetFireDamageAbsorptionValueText(fireDamageAbsorptionValue);
            }
        }

        public void SetLightningDamageAbsorptionValueText(float lightningDamageAbsorptionValue)
        {
            if(_playerDamageAbsorptionRightPanelUI!= null)
            {
                _playerDamageAbsorptionRightPanelUI.SetLightningDamageAbsorptionValueText(lightningDamageAbsorptionValue);
            }
        }

        public void SetUmbraDamageAbsorptionValueText(float umbraDamageAbsorptionValue)
        {
            if(_playerDamageAbsorptionRightPanelUI != null)
            {
                _playerDamageAbsorptionRightPanelUI.SetUmbraDamageAbsorptionValueText(umbraDamageAbsorptionValue);
            }
        }

        public void SetMagicDamageAbsorptionValueText(float magicDamageAbsorptionValue)
        {
            if(_playerDamageAbsorptionRightPanelUI != null)
            {
                _playerDamageAbsorptionRightPanelUI.SetMagicDamageAbsorptionValueText(magicDamageAbsorptionValue);
            }
        }

        public void SetPlayerStatsWindowActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

