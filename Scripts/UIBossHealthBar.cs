using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class UIBossHealthBar : MonoBehaviour
    {
        Text bossName;
        Slider slider;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            bossName = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            SetUIHealthBarToInactive();
        }
        public void SetBossName(string name)
        {
            bossName.text = name;
        }

        public void SetUIHealthBarToActive()
        {
            this.gameObject.SetActive(true);
        }

        public void SetUIHealthBarToInactive()
        {
            this.gameObject.SetActive(false);
        }

        public void SetBossMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetBossCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }
    }
}

