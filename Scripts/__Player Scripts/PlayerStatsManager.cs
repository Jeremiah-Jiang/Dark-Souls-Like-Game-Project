using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        PlayerManager playerManager;

        public float staminaRegenerationAmount = 30.0f;
        public float staminaRegenerationAmountWhileBlocking = 1.0f;
        public float staminaRegenerationTimer = 0;
        protected override void Awake()
        {
            base.Awake();
            playerManager = GetComponent<PlayerManager>();
            allies.Add(Allies.Player);
        }
        void Start()
        {
            maxHealth = SetMaxStatFromStatLevel(maxHealth, healthLevel);
            currentHealth = maxHealth;
            playerManager.uiManager.SetMaxHealthOnSlider(maxHealth);

            maxStamina = SetMaxStatFromStatLevel(maxStamina, staminaLevel);
            currentStamina = maxStamina;
            playerManager.uiManager.SetMaxStaminaOnSlider(maxStamina);

            maxFocusPoints = SetMaxStatFromStatLevel(maxFocusPoints, focusLevel);
            currentFocusPoints = maxFocusPoints;
            playerManager.uiManager.SetMaxFocusPointsOnSlider(maxFocusPoints);
        }

        protected override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else if(poiseResetTimer <= 0 && !playerManager.isInteracting)
            {
                totalPoiseDefense = GetTotalPoiseFromEquipment();
            }
        }


        public override void TakePoisonDamage(int damage)
        {
            if (playerManager.isDead)
                return;
            base.TakePoisonDamage(damage);
            playerManager.uiManager.SetCurrentHealthOnSlider(currentHealth);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                playerManager.isDead = true;
                playerManager.PlayTargetAnimation("Dead_01", true);
            }
        }

        public override void TakeDamageNoAnimation(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage)
        {
            if (playerManager.isDead)
                return;
            if (playerManager.isInvulnerable)
                return;
            base.TakeDamageNoAnimation(damageDealer, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);

            playerManager.uiManager.SetCurrentHealthOnSlider(currentHealth);
        }

        public override void TakeDamage(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage, string damageAnimation)
        {
            if (playerManager.isDead)
                return;
            if (playerManager.isInvulnerable)
                return;
            base.TakeDamage(damageDealer, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage, damageAnimation);
            playerManager.uiManager.SetCurrentHealthOnSlider(currentHealth);
            playerManager.PlayTargetAnimation(damageAnimation, true);
            if (playerManager.isDead == true)
            {
                currentHealth = 0;
                playerManager.isDead = true;
                playerManager.PlayTargetAnimation("Dead_01", true);
            }
        }

        public override void TakeDamageAfterBlocking(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage)
        {
            if (playerManager.isDead)
                return;
            if (playerManager.isInvulnerable)
                return;
            base.TakeDamageAfterBlocking(damageDealer, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);
            playerManager.uiManager.SetCurrentHealthOnSlider(currentHealth);
            if(playerManager.isDead == true)
            {
                currentHealth = 0;
                playerManager.isDead = true;
                playerManager.PlayTargetAnimation("Dead_01", true);
            }
        }

        public void RegenerateStamina()
        {
            if(playerManager.isInteracting || playerManager.isSprinting)
            {
                staminaRegenerationTimer = 0;
            }
            else
            {
                staminaRegenerationTimer += Time.deltaTime;
                if (currentStamina < maxStamina && staminaRegenerationTimer > 1.0f)
                {
                    if(playerManager.isBlocking)
                    {
                        currentStamina += staminaRegenerationAmountWhileBlocking * Time.deltaTime;
                    }
                    else
                    {
                        currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    }
                    playerManager.uiManager.SetCurrentStaminaOnSlider(Mathf.RoundToInt(currentStamina));
                }
                if(currentStamina >= maxStamina)
                {
                    currentStamina = maxStamina;
                    staminaRegenerationTimer = 1.0f;
                }
            }
        }

        public override void RegenerateStaminaOverTime(float staminaRegenRate)
        {
            base.RegenerateStaminaOverTime(staminaRegenRate);
            playerManager.uiManager.SetCurrentStaminaOnSlider(Mathf.RoundToInt(currentStamina));
        }

        public void HealPlayer(int healAmount)
        {
            currentHealth += healAmount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            playerManager.uiManager.SetCurrentHealthOnSlider(currentHealth);
        }

        public override void RegenerateHealthOverTime(int healthRegenRate)
        {
            base.RegenerateHealthOverTime(healthRegenRate);
            playerManager.uiManager.SetCurrentHealthOnSlider(Mathf.RoundToInt(currentHealth));
        }
        public void TakeFocusPointsDamage(int focusPoints)
        {
            currentFocusPoints -= focusPoints;
            if(currentFocusPoints <= 0)
            {
                currentFocusPoints = 0;
            }
            playerManager.uiManager.SetCurrentFocusPointsOnSlider(currentFocusPoints);

        }

        public override void RegenerateFocusPointsOverTime(float focusPointsRegenRate)
        {
            base.RegenerateFocusPointsOverTime(focusPointsRegenRate);
            playerManager.uiManager.SetCurrentFocusPointsOnSlider(Mathf.RoundToInt(currentFocusPoints));
        }

        public void AddSouls(int souls)
        {
            currentSoulCount += souls;
        }

        public void DeductSouls(int souls)
        {
            currentSoulCount -= souls;
        }

        /// <summary>
        /// An overriden method from CharacterStatsManager<br /> 
        /// Base function is a setter method to Set healthLevel as well as calculate and set the new maxHealth value from healthLevel<br />
        /// Override updates the UI to display the new maxHealth on HealthBar
        /// </summary>
        /// <param name="healthLevel">The new healthLevel, which maxHealth will get its updated value from</param>
        public override void SetHealthLevelAndMaxHealth(int healthLevel)
        {
            base.SetHealthLevelAndMaxHealth(healthLevel);
            playerManager.uiManager.SetMaxHealthOnSlider(maxHealth);
            playerManager.uiManager.SetCurrentHealthOnSliderNoDamageTaken(currentHealth);
        }

        /// <summary>
        /// An overriden method from CharacterStatsManager<br /> 
        /// Base function is a setter method to Set staminaLevel as well as calculate and set the new maxStamina value from staminaLevel<br />
        /// Override updates the UI to display the new maxStamina on StaminaBar
        /// </summary>
        /// <param name="staminaLevel">The new staminaLevel, which maxStamina will get its updated value from</param>
        public override void SetStaminaLevelAndMaxStamina(int staminaLevel)
        {
            base.SetStaminaLevelAndMaxStamina(staminaLevel);
            playerManager.uiManager.SetMaxStaminaOnSlider(maxStamina);
            playerManager.uiManager.SetCurrentStaminaOnSliderNoDamageTaken(currentStamina);
        }

        /// <summary>
        /// An overriden method from CharacterStatsManager<br />
        /// Base function is a setter method to Set focusLevel as well as calculate and set the new maxFocusPoints value from focusLevel
        /// Override updates the UI to display the new maxFocusPoints on focusPointsBar
        /// </summary>
        /// <param name="focusLevel">The new focusLevel, which maxFocusPoints will get its updated value from</param>
        public override void SetFocusLevelAndMaxFocusPoints(int focusLevel)
        {
            base.SetFocusLevelAndMaxFocusPoints(focusLevel);
            playerManager.uiManager.SetMaxFocusPointsOnSlider(maxFocusPoints);
            playerManager.uiManager.SetCurrentFocusPointsOnSliderNoDamageTaken(currentFocusPoints);
        }
    }
}

