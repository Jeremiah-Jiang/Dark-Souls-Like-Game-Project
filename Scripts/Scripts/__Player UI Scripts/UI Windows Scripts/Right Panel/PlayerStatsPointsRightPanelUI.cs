using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class PlayerStatsPointsRightPanelUI : MonoBehaviour
    {
        [SerializeField]
        Text _playerCurrentHealthText;
        [SerializeField]
        Text _playerMaxHealthText;

        [SerializeField]
        Text _playerCurrentStaminaText;
        [SerializeField]
        Text _playerMaxStaminaText;

        [SerializeField]
        Text _playerCurrentFocusPointsText;
        [SerializeField]
        Text _playerMaxFocusPointsText;
        [SerializeField]
        Text _playerEquipmentPoiseText;

        private int _playerCurrentHealth;
        private int _playerMaxHealth;

        private int _playerCurrentStamina;
        private int _playerMaxStamina;

        private int _playerCurrentFocusPoints;
        private int _playerMaxFocusPoints;

        private Color defaultColor = new Color(1, 1, 1, 1);
        private Color warningColor = new Color(1, 0, 0, 1);

        public void SetCurrentHealthText(int currentHealth)
        {
            _playerCurrentHealth = currentHealth;
            _playerCurrentHealthText.text = currentHealth.ToString();
            if(_playerCurrentHealth <= Mathf.RoundToInt(_playerMaxHealth*0.2f))
            {
                _playerCurrentHealthText.color = warningColor;
            }
            else
            {
                _playerCurrentHealthText.color = defaultColor;
            }
        }

        public void SetMaxHealthText(int maxHealth)
        {
            _playerMaxHealth = maxHealth;
            _playerMaxHealthText.text = maxHealth.ToString();
        }

        public void SetCurrentStaminaText(int currentStamina)
        {
            _playerCurrentStamina = currentStamina;
            _playerCurrentStaminaText.text = currentStamina.ToString();
            if (_playerCurrentStamina <= Mathf.RoundToInt(_playerMaxStamina * 0.2f))
            {
                _playerCurrentStaminaText.color = warningColor;
            }
            else
            {
                _playerCurrentStaminaText.color = defaultColor;
            }
        }

        public void SetMaxStaminaText(int maxStamina)
        {
            _playerMaxStamina = maxStamina;
            _playerMaxStaminaText.text = maxStamina.ToString();
        }

        public void SetCurrentFocusPointsText(int currentFocusPoints)
        {
            _playerCurrentFocusPoints = currentFocusPoints;
            _playerCurrentFocusPointsText.text = currentFocusPoints.ToString();
            if (_playerCurrentFocusPoints <= Mathf.RoundToInt(_playerMaxFocusPoints * 0.2f))
            {
                _playerCurrentFocusPointsText.color = warningColor;
            }
            else
            {
                _playerCurrentFocusPointsText.color = defaultColor;
            }
        }

        public void SetMaxFocusPointsText(int maxFocusPoints)
        {
            _playerMaxFocusPoints = maxFocusPoints;
            _playerMaxFocusPointsText.text = maxFocusPoints.ToString();
        }

        public void SetPlayerEquipmentPoiseText(float poise)
        {
            _playerEquipmentPoiseText.text = poise.ToString();
        }

    }
}

