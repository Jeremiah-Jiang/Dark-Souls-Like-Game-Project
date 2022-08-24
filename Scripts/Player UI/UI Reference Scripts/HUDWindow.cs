using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HUDWindow : MonoBehaviour
    {
        [Header("HUD Components")]
        public HealthBar healthBar;
        public StaminaBar staminaBar;
        public FocusPointsBar focusPointsBar;
        public QuickSlotsUI quickSlotsUI;
        public SoulCount soulCount;
        public Crosshair crossHair;
        public PoisonBuildUpBar poisonBuildUpBar;
        public PoisonAmountBar poisonAmountBar;
        public NeutralMoralAlignmentIcon neutralMoralAlignmentIcon;
        public DarknessMoralAlignmentIcon darknessMoralAlignmentIcon;
        public HolyMoralAlignmentIcon holyMoralAlignmentIcon;

        private void Awake()
        {
            healthBar = GetComponentInChildren<HealthBar>();
            staminaBar = GetComponentInChildren<StaminaBar>();
            focusPointsBar = GetComponentInChildren<FocusPointsBar>();
            quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
            soulCount = GetComponentInChildren<SoulCount>();
            crossHair = GetComponentInChildren<Crosshair>(true);
            poisonAmountBar = GetComponentInChildren<PoisonAmountBar>();
            poisonBuildUpBar = GetComponentInChildren<PoisonBuildUpBar>();
            neutralMoralAlignmentIcon = GetComponentInChildren<NeutralMoralAlignmentIcon>(true);
            darknessMoralAlignmentIcon = GetComponentInChildren<DarknessMoralAlignmentIcon>(true);
            holyMoralAlignmentIcon = GetComponentInChildren<HolyMoralAlignmentIcon>(true);
        }

        //HealthBar Methods
        #region HealthBar methods
        public void SetMaxHealthOnSlider(int maxHealth)
        {
            healthBar.SetMaxHealthOnSlider(maxHealth);
        }

        public void SetCurrentHealthOnSlider(int currentHealth)
        {
            healthBar.SetCurrentHealthOnSlider(currentHealth);
        }

        public void SetCurrentHealthOnSliderNoDamageTaken(int currentHealth)
        {
            healthBar.SetCurrentHealthOnSliderNoDamageTaken(currentHealth);
        }
        #endregion

        //StaminaBar Methods
        #region StaminaBar Methods
        public void SetMaxStaminaOnSlider(float maxStamina)
        {
            staminaBar.SetMaxStaminaOnSlider(maxStamina);
        }

        public void SetCurrentStaminaOnSlider(float currentStamina)
        {
            staminaBar.SetCurrentStaminaOnSlider(currentStamina);
        }

        public void SetCurrentStaminaOnSliderNoDamageTaken(float currentStamina)
        {
            staminaBar.SetCurrentStaminaOnSliderNoDamageTaken(currentStamina);
        }
        #endregion

        //FocusPointsBar Methods
        #region FocusPointsBar Methods
        public void SetMaxFocusPointsOnSlider(float maxFocusPoints)
        {
            focusPointsBar.SetMaxFocusPointsOnSlider(maxFocusPoints);
        }

        public void SetCurrentFocusPointsOnSlider(float currentFocusPoints)
        {
            focusPointsBar.SetCurrentFocusPointsOnSlider(currentFocusPoints);
        }

        public void SetCurrentFocusPointsOnSliderNoDamageTaken(float currentFocusPoints)
        {
            focusPointsBar.SetCurrentFocusPointsOnSliderNoDamageTaken(currentFocusPoints);
        }
        #endregion

        //PoisonBuildUpBar Methods
        #region PoisonBuildUpBar Methods
        public void SetPoisonBuildUpBarActive(bool value)
        {
            poisonBuildUpBar.gameObject.SetActive(value);
        }
        public void SetPoisonBuildUpAmount(int currentPoisonBuildUp)
        {
            poisonBuildUpBar.SetPoisonBuildUpAmount(currentPoisonBuildUp);
        }
        #endregion

        //PoisonAmountBar Methods
        #region PoisonAmountBar Methods
        public void SetPoisonAmountBarActive(bool value)
        {
            poisonAmountBar.gameObject.SetActive(value);
        }
        public void SetPoisonAmountOnSlider(int poisonAmount)
        {
            poisonAmountBar.SetPoisonAmountOnSlider(poisonAmount);
        }
        #endregion

        //QuickSlotsUI Methods
        #region QuickSlotsUI Methods
        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weaponItem)
        {
            quickSlotsUI.UpdateWeaponQuickSlotsUI(isLeft, weaponItem);
        }

        public void UpdateCurrentSpellIcon(SpellItem spellItem)
        {
            quickSlotsUI.UpdateCurrentSpellIcon(spellItem);
        }

        public void UpdateCurrentConsumableIcon(ConsumableItem consumableItem)
        {
            quickSlotsUI.UpdateCurrentConsumableIcon(consumableItem);
        }
        #endregion

        //Soul Count Methods
        #region Soul Count Methods
        public Text GetSoulCountText()
        {
            return soulCount.GetSoulCountText();
        }

        public void SetSoulCountText(int souls)
        {
            soulCount.SetSoulCountText(souls);
        }
        #endregion

        public void SetCrossHairActive(bool value)
        {
            crossHair.gameObject.SetActive(value);
        }

        public void SetNeutralMoralAlignmentIconActive()
        {
            neutralMoralAlignmentIcon.gameObject.SetActive(true);
            darknessMoralAlignmentIcon.gameObject.SetActive(false);
            holyMoralAlignmentIcon.gameObject.SetActive(false);
        }

        public void SetDarknessMoralAlignmentIconActive()
        {
            neutralMoralAlignmentIcon.gameObject.SetActive(false);
            darknessMoralAlignmentIcon.gameObject.SetActive(true);
            holyMoralAlignmentIcon.gameObject.SetActive(false);
        }

        public void SetHolyMoralAlignmentIconActive()
        {
            neutralMoralAlignmentIcon.gameObject.SetActive(false);
            darknessMoralAlignmentIcon.gameObject.SetActive(false);
            holyMoralAlignmentIcon.gameObject.SetActive(true);
        }
        public void SetHUDWindowActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

