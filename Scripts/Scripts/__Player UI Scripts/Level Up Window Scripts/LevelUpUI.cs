using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class LevelUpUI : MonoBehaviour
    {
        UIManager uiManager;
        PlayerManager playerManager;

        public Button confirmLevelUpButton;
        [Header("Player Level")]
        public int currentPlayerLevel;
        public int projectedPlayerLevel;
        public Text currentPlayerLevelText;
        public Text projectedPlayerLevelText;

        [Header("Souls")]
        public Text currentSoulsText;
        public int currentSouls;
        public Text soulsRequiredToLevelUpText;
        public int soulsRequiredToLevelUp;
        public int baseLevelUpCost;

        [Header("Health")]
        public Slider healthSlider;
        public Text currentHealthLevelText;
        public Text projectedHealthLevelText;

        [Header("Stamina")]
        public Slider staminaSlider;
        public Text currentStaminaLevelText;
        public Text projectedStaminaLevelText;

        [Header("Focus")]
        public Slider focusSlider;
        public Text currentFocusLevelText;
        public Text projectedFocusLevelText;

        [Header("Poise")]
        public Slider poiseSlider;
        public Text currentPoiseLevelText;
        public Text projectedPoiseLevelText;

        [Header("Strength")]
        public Slider strengthSlider;
        public Text currentStrengthLevelText;
        public Text projectedStrengthLevelText;

        [Header("Dexterity")]
        public Slider dexteritySlider;
        public Text currentDexterityLevelText;
        public Text projectedDexterityLevelText;

        [Header("Faith")]
        public Slider faithSlider;
        public Text currentFaithLevelText;
        public Text projectedFaithLevelText;

        [Header("Intelligence")]
        public Slider intelligenceSlider;
        public Text currentIntelligenceLevelText;
        public Text projectedIntelligenceLevelText;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            if(playerManager != null)
            {
                uiManager = playerManager.uiManager;
            }
            else
            {
                Debug.LogError("PlayerManager is NULL");
            }
        }

        private void Update()
        {
            if(playerManager.GetInventoryInput() == true)
            {
                gameObject.SetActive(false);
            }
        }
        private void OnEnable()
        {
            currentPlayerLevel = playerManager.GetPlayerLevel();
            currentPlayerLevelText.text = currentPlayerLevel.ToString();

            currentPlayerLevel = playerManager.GetPlayerLevel();
            projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

            int playerHealthLevel = playerManager.GetHealthLevel();
            healthSlider.value = playerHealthLevel;
            healthSlider.minValue = playerHealthLevel;
            healthSlider.maxValue = 99;
            currentHealthLevelText.text = playerHealthLevel.ToString();
            projectedHealthLevelText.text = playerHealthLevel.ToString();

            int playerStaminaLevel = playerManager.GetStaminaLevel();
            staminaSlider.value = playerStaminaLevel;
            staminaSlider.minValue = playerStaminaLevel;
            staminaSlider.maxValue = 99;
            currentStaminaLevelText.text = playerStaminaLevel.ToString();
            projectedStaminaLevelText.text = playerStaminaLevel.ToString();

            int playerFocusLevel = playerManager.GetFocusLevel();
            focusSlider.value = playerFocusLevel;
            focusSlider.minValue = playerFocusLevel;
            focusSlider.maxValue = 99;
            currentFocusLevelText.text = playerFocusLevel.ToString();
            projectedFocusLevelText.text = playerFocusLevel.ToString();

            int playerPoiseLevel = playerManager.GetPoiseLevel();
            poiseSlider.value = playerPoiseLevel;
            poiseSlider.minValue = playerPoiseLevel;
            poiseSlider.maxValue = 99;
            currentPoiseLevelText.text = playerPoiseLevel.ToString();
            projectedPoiseLevelText.text = playerPoiseLevel.ToString();

            int playerStrengthLevel = playerManager.GetStrengthLevel();
            strengthSlider.value = playerStrengthLevel;
            strengthSlider.minValue = playerStrengthLevel;
            strengthSlider.maxValue = 99;
            currentStrengthLevelText.text = playerStrengthLevel.ToString();
            projectedStrengthLevelText.text = playerStrengthLevel.ToString();

            int playerDexterityLevel = playerManager.GetDexterityLevel();
            dexteritySlider.value = playerDexterityLevel;
            dexteritySlider.minValue = playerDexterityLevel;
            dexteritySlider.maxValue = 99;
            currentDexterityLevelText.text = playerDexterityLevel.ToString();
            projectedDexterityLevelText.text = playerDexterityLevel.ToString();

            int playerIntelligenceLevel = playerManager.GetIntelligenceLevel();
            intelligenceSlider.value = playerIntelligenceLevel;
            intelligenceSlider.minValue = playerIntelligenceLevel;
            intelligenceSlider.maxValue = 99;
            currentIntelligenceLevelText.text = playerIntelligenceLevel.ToString();
            projectedIntelligenceLevelText.text = playerIntelligenceLevel.ToString();

            int playerFaithLevel = playerManager.GetFaithLevel();
            faithSlider.value = playerFaithLevel;
            faithSlider.minValue = playerFaithLevel;
            faithSlider.maxValue = 99;
            currentFaithLevelText.text = playerFaithLevel.ToString();
            projectedFaithLevelText.text = playerFaithLevel.ToString();

            currentSoulsText.text = playerManager.GetCurrentSoulCount().ToString();
            UpdateProjectedPlayerLevel();
        }

        /// <summary>
        /// Function is an OnClickEvent for the Confirm Button on the Level Up Window
        /// </summary>
        public void ConfirmPlayerLevelUpStats()
        {
            playerManager.SetPlayerLevel(projectedPlayerLevel);
            playerManager.SetHealthLevelAndMaxHealth(Mathf.RoundToInt(healthSlider.value));
            playerManager.SetStaminaLevelAndMaxStamina(Mathf.RoundToInt(staminaSlider.value));
            playerManager.SetFocusLevelAndMaxFocusPoints(Mathf.RoundToInt(focusSlider.value));
            playerManager.SetPoiseLevelAndMaxPoise(Mathf.RoundToInt(poiseSlider.value));
            playerManager.SetStrengthLevelAndMaxStrength(Mathf.RoundToInt(strengthSlider.value));
            playerManager.SetDexterityLevelAndMaxDexterity(Mathf.RoundToInt(dexteritySlider.value));
            playerManager.SetIntelligenceLevelAndMaxIntelligence(Mathf.RoundToInt(intelligenceSlider.value));
            playerManager.SetFaithLevelAndMaxFaith(Mathf.RoundToInt(faithSlider.value));

            playerManager.DeductSouls(soulsRequiredToLevelUp);
            uiManager.SetSoulCountText(playerManager.GetCurrentSoulCount());

            gameObject.SetActive(false);
        }

        private void CalculateSoulCostToLevelUp()
        {
            for(int i = 0; i < projectedPlayerLevel; i++)
            {
                soulsRequiredToLevelUp += Mathf.RoundToInt((projectedPlayerLevel * baseLevelUpCost) * 1.5f);
            }

        }

        private void UpdateProjectedPlayerLevel()
        {
            soulsRequiredToLevelUp = 0;

            projectedPlayerLevel = currentPlayerLevel;
            projectedPlayerLevel += Mathf.RoundToInt(healthSlider.value) - playerManager.GetHealthLevel();
            projectedPlayerLevel += Mathf.RoundToInt(staminaSlider.value) - playerManager.GetStaminaLevel();
            projectedPlayerLevel += Mathf.RoundToInt(focusSlider.value) - playerManager.GetFocusLevel();
            projectedPlayerLevel += Mathf.RoundToInt(poiseSlider.value) - playerManager.GetPoiseLevel();
            projectedPlayerLevel += Mathf.RoundToInt(strengthSlider.value) - playerManager.GetStrengthLevel();
            projectedPlayerLevel += Mathf.RoundToInt(dexteritySlider.value) - playerManager.GetDexterityLevel();
            projectedPlayerLevel += Mathf.RoundToInt(intelligenceSlider.value) - playerManager.GetIntelligenceLevel();
            projectedPlayerLevel += Mathf.RoundToInt(faithSlider.value) - playerManager.GetFaithLevel();

            projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

            CalculateSoulCostToLevelUp();
            soulsRequiredToLevelUpText.text = soulsRequiredToLevelUp.ToString();

            if (playerManager.GetCurrentSoulCount() < soulsRequiredToLevelUp)
            {
                confirmLevelUpButton.interactable = false;
            }
            else
            {
                confirmLevelUpButton.interactable = true;
            }
        }

        /// <summary>
        /// Function is an OnValueChanged event for the Health Level Slider in the Level Up Window
        /// </summary>
        public void UpdateHealthLevelSlider()
        {
            projectedHealthLevelText.text = healthSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        /// <summary>
        /// Function is an OnValueChanged event for the Stamina Level Slider in the Level Up Window
        /// </summary>
        public void UpdateStaminaLevelSlider()
        {
            projectedStaminaLevelText.text = staminaSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        /// <summary>
        /// Function is an OnValueChanged event for the Focus Level Slider in the Level Up Window
        /// </summary>
        public void UpdateFocusLevelSlider()
        {
            projectedFocusLevelText.text = focusSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        /// <summary>
        /// Function is an OnValueChanged event for the Poise Level Slider in the Level Up Window
        /// </summary>
        public void UpdatePoiseLevelSlider()
        {
            projectedPoiseLevelText.text = poiseSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        /// <summary>
        /// Function is an OnValueChanged event for the Strength Level Slider in the Level Up Window
        /// </summary>
        public void UpdateStrengthLevelSlider()
        {
            projectedStrengthLevelText.text = strengthSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        /// <summary>
        /// Function is an OnValueChanged event for the Dexterity Level Slider in the Level Up Window
        /// </summary>
        public void UpdateDexterityLevelSlider()
        {
            projectedDexterityLevelText.text = dexteritySlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        /// <summary>
        /// Function is an OnValueChanged event for the Faith Level Slider in the Level Up Window
        /// </summary>
        public void UpdateFaithLevelSlider()
        {
            projectedFaithLevelText.text = faithSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        /// <summary>
        /// Function is an OnValueChanged event for the Intelligence Level Slider in the Level Up Window
        /// </summary>
        public void UpdateIntelligenceLevelSlider()
        {
            projectedIntelligenceLevelText.text = intelligenceSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }
    }
}

