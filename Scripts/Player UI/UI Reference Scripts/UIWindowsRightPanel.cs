using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class UIWindowsRightPanel : MonoBehaviour
    {
        [SerializeField]
        PlayerStatsWindow _playerStatsWindow;

        private void Awake()
        {
            _playerStatsWindow = GetComponentInChildren<PlayerStatsWindow>();
        }

        private void Start()
        {
            SetPlayerStatsWindowActive(false);
        }

        public void SetPlayerStatsWindowActive(bool value)
        {
            _playerStatsWindow.SetPlayerStatsWindowActive(value);
        }

        public void SetPlayerName(string playerName)
        {
            _playerStatsWindow.SetPlayerName(playerName);
        }

        public void SetDarknessMoralAlignmentIconActive()
        {
            _playerStatsWindow.SetDarknessMoralAlignmentIconActive();
        }

        public void SetNeutralAlignmentIconActive()
        {
            _playerStatsWindow.SetNeutralMoralAlignmentIconActive();
        }

        public void SetHolyAlignmentIconActive()
        {
            _playerStatsWindow.SetHolyMoralAlignmentIconActive();
        }

        public void SetPlayerLevelValueText(string playerLevel)
        {
            _playerStatsWindow.SetPlayerLevelValueText(playerLevel);
        }

        public void SetPlayerSoulsValueText(string souls)
        {
            _playerStatsWindow.SetPlayerSoulsValueText(souls);
        }

        public void SetHealthLevelValue(int healthLevel)
        {
            _playerStatsWindow.SetHealthLevelValue(healthLevel);
        }

        public void SetStaminaLevelValue(int staminaLevel)
        {
            _playerStatsWindow.SetStaminaLevelValue(staminaLevel);
        }

        public void SetFocusPointsLevelValue(int focusPointsLevel)
        {
            _playerStatsWindow.SetFocusPointsLevelValue(focusPointsLevel);
        }

        public void SetPoiseLevelValue(int poiseLevel)
        {
            _playerStatsWindow.SetPoiseLevelValue(poiseLevel);
        }

        public void SetStrengthLevelValue(int strengthLevel)
        {
            _playerStatsWindow.SetStrengthLevelValue(strengthLevel);
        }

        public void SetDexterityLevelValue(int dexterityLevel)
        {
            _playerStatsWindow.SetDexterityLevelValue(dexterityLevel);
        }

        public void SetFaithLevelValue(int faithLevel)
        {
            _playerStatsWindow.SetFaithLevelValue(faithLevel);
        }

        public void SetIntelligenceLevelValue(int intelligenceLevel)
        {
            _playerStatsWindow.SetIntelligenceLevelValue(intelligenceLevel);
        }

        public void SetCurrentHealthText(int currentHealth)
        {
            _playerStatsWindow.SetCurrentHealthText(currentHealth);
        }

        public void SetMaxHealthText(int maxHealth)
        {
            _playerStatsWindow.SetMaxHealthText(maxHealth);
        }

        public void SetCurrentStaminaText(int currentStamina)
        {
            _playerStatsWindow.SetCurrentStaminaText(currentStamina);
        }

        public void SetMaxStaminaText(int maxStamina)
        {
            _playerStatsWindow.SetMaxStaminaText(maxStamina);
        }

        public void SetCurrentFocusPointsText(int currentFocusPoints)
        {
            _playerStatsWindow.SetCurrentFocusPointsText(currentFocusPoints);
        }

        public void SetMaxFocusPointsText(int maxFocusPoints)
        {
            _playerStatsWindow.SetMaxFocusPointsText(maxFocusPoints);
        }

        public void SetPlayerEquipmentPoiseText(float poise)
        {
            _playerStatsWindow.SetPlayerEquipmentPoiseText(poise);
        }

        public void SetPhysicalDamageAbsorptionValueText(float physicalDamageAbsorptionValue)
        {
            _playerStatsWindow.SetPhysicalDamageAbsorptionValueText(physicalDamageAbsorptionValue);
        }

        public void SetFireDamageAbsorptionValueText(float fireDamageAbsorptionValue)
        {
            _playerStatsWindow.SetFireDamageAbsorptionValueText(fireDamageAbsorptionValue);
        }

        public void SetLightningDamageAbsorptionValueText(float lightningDamageAbsorptionValue)
        {
            _playerStatsWindow.SetLightningDamageAbsorptionValueText(lightningDamageAbsorptionValue);
        }

        public void SetUmbraDamageAbsorptionValueText(float umbraDamageAbsorptionValue)
        {
            _playerStatsWindow.SetUmbraDamageAbsorptionValueText(umbraDamageAbsorptionValue);
        }

        public void SetMagicDamageAbsorptionValueText(float magicDamageAbsorptionValue)
        {
            _playerStatsWindow.SetMagicDamageAbsorptionValueText(magicDamageAbsorptionValue);
        }
    }
}

