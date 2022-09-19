using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager characterManager;
        [Header("Name")]
        public string characterName;

        [Header("Character Level")]
        public int playerLevel = 1;

        [Header("Team I.D")]
        public int teamID = 0;
        public List<Allies> allies = new List<Allies>();

        [Header("Moral Alignment")]
        public float darknessInfluence = 0;
        public float holyInfluence = 0;
        public bool isNeutral;
        public bool isDark;
        public bool isHoly;

        [Header("Maximum Stat Values")]
        public int maxHealth;
        public float maxStamina;
        public float maxFocusPoints;
        public float maxPoise;
        public float maxStrength;
        public float maxDexterity;
        public float maxIntelligence;
        public float maxFaith;

        [Header("Current Stat Values")]
        public int currentHealth;
        public float currentStamina;
        public float currentFocusPoints;
        public float currentPoise;
        public float currentStrength;
        public float currentDexterity;
        public float currentIntelligence;
        public float currentFaith;

        [Header("Soul values")]
        public int currentSoulCount;
        public int soulsAwardedOnDeath = 50;

        [Header("Stats Levels")]
        public int healthLevel = 10;
        public int staminaLevel = 10;
        public int focusLevel = 10;
        public int poiseLevel = 10;
        public int strengthLevel = 10;
        public int dexterityLevel = 10;
        public int intelligenceLevel = 10;
        public int faithLevel = 10;

        [Header("Charged Attack Modifier")]
        [SerializeField] private float _chargeAttackModifier = 1;
        /* Charge Attack Explained
        Charge attack modifier is 1 by default, so if you don't perform a charge attack, your damage is unaffected
        If you are performing a charge attack, the charge attack is modified based on the time spent charging
        Less than 50% charge means your damage is actually less than 100%, 50% charge means your damage is 100% and 100% charge means your damage is 150%
        */

        [Header("Poise")]
        [SerializeField] protected float totalPoiseDefense; //The total poise during damage calculation
        [SerializeField] protected float offensivePoiseBonus; //Poise you gain during an attack with a weapon
        [SerializeField] protected float armorPoiseBonus; //The poise you gain from wearing what you have equip
        [SerializeField] protected float totalPoiseResetTime = 15;
        [SerializeField] protected float poiseResetTimer = 0;
        [SerializeField] protected float poiseHead;
        [SerializeField] protected float poiseBody;
        [SerializeField] protected float poiseLegs;
        [SerializeField] protected float poiseHands;
        [SerializeField] protected float poiseBack;

        [Header("Armor Damage Negation")]
        [SerializeField] protected float physicalDamageNegationHead;
        [SerializeField] protected float physicalDamageNegationBody;
        [SerializeField] protected float physicalDamageNegationLegs;
        [SerializeField] protected float physicalDamageNegationHands;
        [SerializeField] protected float physicalDamageNegationBack;

        [SerializeField] protected float magicDamageNegationHead;
        [SerializeField] protected float magicDamageNegationBody;
        [SerializeField] protected float magicDamageNegationLegs;
        [SerializeField] protected float magicDamageNegationHands;
        [SerializeField] protected float magicDamageNegationBack;

        [SerializeField] protected float fireDamageNegationHead;
        [SerializeField] protected float fireDamageNegationBody;
        [SerializeField] protected float fireDamageNegationLegs;
        [SerializeField] protected float fireDamageNegationHands;
        [SerializeField] protected float fireDamageNegationBack;

        [SerializeField] protected float lightningDamageNegationHead;
        [SerializeField] protected float lightningDamageNegationBody;
        [SerializeField] protected float lightningDamageNegationLegs;
        [SerializeField] protected float lightningDamageNegationHands;
        [SerializeField] protected float lightningDamageNegationBack;

        [SerializeField] protected float umbraDamageNegationHead;
        [SerializeField] protected float umbraDamageNegationBody;
        [SerializeField] protected float umbraDamageNegationLegs;
        [SerializeField] protected float umbraDamageNegationHands;
        [SerializeField] protected float umbraDamageNegationBack;

        [SerializeField] protected float holyDamageNegationHead;
        [SerializeField] protected float holyDamageNegationBody;
        [SerializeField] protected float holyDamageNegationLegs;
        [SerializeField] float holyDamageNegationHands;
        [SerializeField] protected float holyDamageNegationBack;

        [Header("Guarded Damage Negation")]
        [SerializeField] protected float guardedPhysicalDamageNegation;
        [SerializeField] protected float guardedMagicDamageNegation;
        [SerializeField] protected float guardedFireDamageNegation;
        [SerializeField] protected float guardedLightningDamageNegation;
        [SerializeField] protected float guardedUmbraDamageNegation;
        [SerializeField] protected float guardedHolyDamageNegation;
        [SerializeField] protected float stability;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        {
            HandlePoiseResetTimer();
            //DetermineMoralAlignment();
        }

        private void Start()
        {
            totalPoiseDefense = armorPoiseBonus;
        }

        public virtual float GetChargeAttackModifier()
        {
            return _chargeAttackModifier;
        }

        public virtual void SetChargeAttackModifier(float modifier)
        {
            _chargeAttackModifier = modifier;
        }

        public virtual void ResetChargeAttackModifier()
        {
            _chargeAttackModifier = 1;
        }

        /// <summary>
        /// Function to calculate damage that this instance of CharacterStatsManager should receive and set the boolean flag [isDead] accordingly.<br />.
        /// </summary>
        /// <param name="physicalDamage">An integer value denoting the physical damage of the damage dealing weapon.</param>
        /// <param name="fireDamage">An integer value denoting the fire damage of the damage dealing weapon.</param>
        /* Implementation Logic:
         * Check if character is already dead, return immediately if so. Otherwise,
         * Calculate the damage for the respective damage types and the final damage is the summation of all damage types.
         * Reduce currentHealth and set boolean flag [isDead] accordingly.
         * Play damage sound FX.
         */
        public virtual void TakeDamageNoAnimation(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage)
        {
            if (characterManager.isDead)
                return;
            physicalDamage = CalculateDamageAfterAbsorption(physicalDamage, physicalDamageNegationHead, physicalDamageNegationBody, physicalDamageNegationLegs, physicalDamageNegationBack, physicalDamageNegationHands);
            magicDamage = CalculateDamageAfterAbsorption(magicDamage, magicDamageNegationHead, magicDamageNegationBody, magicDamageNegationLegs, magicDamageNegationLegs, magicDamageNegationHands);
            fireDamage = CalculateDamageAfterAbsorption(fireDamage, fireDamageNegationHead, fireDamageNegationBody, fireDamageNegationLegs, fireDamageNegationBack, fireDamageNegationHands);
            lightningDamage = CalculateDamageAfterAbsorption(lightningDamage, lightningDamageNegationHead, lightningDamageNegationBody, lightningDamageNegationLegs, lightningDamageNegationBack, lightningDamageNegationHands);
            umbraDamage = CalculateDamageAfterAbsorption(umbraDamage, umbraDamageNegationHead, umbraDamageNegationBody, umbraDamageNegationLegs, umbraDamageNegationBack, umbraDamageNegationLegs);
            holyDamage = CalculateDamageAfterAbsorption(holyDamage, holyDamageNegationHead, holyDamageNegationBody, holyDamageNegationLegs, holyDamageNegationBack, holyDamageNegationHands);
            float finalDamage = physicalDamage + magicDamage + fireDamage + lightningDamage + umbraDamage + holyDamage;

            Debug.Log("Final Damage BEFORE charge attack multiplier = " + finalDamage);
            if (damageDealer != null)
            {
                finalDamage *= damageDealer.GetChargeAttackModifier();
                //enemyCharacterDa
            }
            Debug.Log("Final Damage AFTER charge attack multiplier = " + finalDamage);

            currentHealth -= Mathf.RoundToInt(finalDamage);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                characterManager.isDead = true;
            }
            //character.characterSoundFXManager.PlayRandomDamageSoundFX();
        }

        /// <summary>
        /// Function to inflict poison damage on this instance of CharacterStatsManager and set the boolean flag [isDead] accordingly
        /// </summary>
        /// <param name="damage">An integer value denoting the amount of poison damage per tick.</param>
        public virtual void TakePoisonDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                characterManager.isDead = true;
            }
        }

        /// <summary>
        /// Function to calculate damage that the character receives and set the boolean flag [isDead] accordingly.<br /><br />
        /// </summary>
        /// <param name="physicalDamage">An integer value denoting the physical damage of the damage dealing weapon.</param>
        /// <param name="fireDamage">An integer value denoting the fire damage of the damage dealing weapon.</param>
        /// <param name="damageAnimation">A string denoting the name of the damageAnimation.</param>
        /// <param name="damageDealer">The instance of the damage dealing CharacterManager</param>
        /* Implementation Logic:
         * Check if character is already dead, return immediately if so. Otherwise,
         * Erase handIK weights for weapons to ensure damage animations are played properly.
         * Calculate the damage for the respective damage types and the final damage is the summation of all damage types.
         * If the damage dealing character is performing a fully charged attack, multiply the final damage.
         * Reduce currentHealth and set boolean flag [isDead] accordingly.
         * Play damage sound FX.
         */
        public virtual void TakeDamage(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage, string damageAnimation)
        {
            if (characterManager.isDead)
                return;
            characterManager.EraseHandIKForWeapon();
            physicalDamage = CalculateDamageAfterAbsorption(physicalDamage, physicalDamageNegationHead, physicalDamageNegationBody, physicalDamageNegationLegs, physicalDamageNegationBack, physicalDamageNegationHands);
            magicDamage = CalculateDamageAfterAbsorption(magicDamage, magicDamageNegationHead, magicDamageNegationBody, magicDamageNegationLegs, magicDamageNegationLegs, magicDamageNegationHands);
            fireDamage = CalculateDamageAfterAbsorption(fireDamage, fireDamageNegationHead, fireDamageNegationBody, fireDamageNegationLegs, fireDamageNegationBack, fireDamageNegationHands);
            lightningDamage = CalculateDamageAfterAbsorption(lightningDamage, lightningDamageNegationHead, lightningDamageNegationBody, lightningDamageNegationLegs, lightningDamageNegationBack, lightningDamageNegationHands);
            umbraDamage = CalculateDamageAfterAbsorption(umbraDamage, umbraDamageNegationHead, umbraDamageNegationBody, umbraDamageNegationLegs, umbraDamageNegationBack, umbraDamageNegationLegs);
            holyDamage = CalculateDamageAfterAbsorption(holyDamage, holyDamageNegationHead, holyDamageNegationBody, holyDamageNegationLegs, holyDamageNegationBack, holyDamageNegationHands);
            float finalDamage = physicalDamage + magicDamage + fireDamage + lightningDamage + umbraDamage + holyDamage;

            Debug.Log("Final Damage BEFORE charge attack multiplier = " + finalDamage);
            if (damageDealer != null)
            {
                finalDamage *= damageDealer.GetChargeAttackModifier();
                //enemyCharacterDa
            }
            Debug.Log("Final Damage AFTER charge attack multiplier = " + finalDamage);

            currentHealth -= Mathf.RoundToInt(finalDamage);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                characterManager.isDead = true;
            }
            //character.characterSoundFXManager.PlayRandomDamageSoundFX();
        }

        /// <summary>
        /// Function to calculate damage that this instance of CharacterStatsManager should receive after performing a Block action, and set the boolean flag [isDead] accordingly.<br />
        /// </summary>
        /// <param name="physicalDamage">An integer value denoting the physical damage of the damage dealing weapon.</param>
        /// <param name="fireDamage">An integer value denoting the fire damage of the damage dealing weapon.</param>
        /// <param name="damageAnimation">A string denoting the name of the damageAnimation.</param>
        /// <param name="damageDealer">The instance of the damage dealing CharacterManager</param>
        /* Implementation Logic:
         * Check if character is already dead, return immediately if so.Otherwise,
         * Erase handIK weights for weapons to ensure damage animations are played properly.
         * Calculate the damage for the respective damage types and the final damage is the summation of all damage types.
         * If the damage dealing character is performing a fully charged attack, multiply the final damage.
         * Reduce currentHealth and set boolean flag [isDead] accordingly.
         * Play blocking sound FX.
         */
        public virtual void TakeDamageAfterBlocking(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage)
        {
            if (characterManager.isDead)
                return;
            characterManager.EraseHandIKForWeapon();
            physicalDamage = CalculateDamageAfterAbsorption(physicalDamage, physicalDamageNegationHead, physicalDamageNegationBody, physicalDamageNegationLegs, physicalDamageNegationBack, physicalDamageNegationHands);
            magicDamage = CalculateDamageAfterAbsorption(magicDamage, magicDamageNegationHead, magicDamageNegationBody, magicDamageNegationLegs, magicDamageNegationLegs, magicDamageNegationHands);
            fireDamage = CalculateDamageAfterAbsorption(fireDamage, fireDamageNegationHead, fireDamageNegationBody, fireDamageNegationLegs, fireDamageNegationBack, fireDamageNegationHands);
            lightningDamage = CalculateDamageAfterAbsorption(lightningDamage, lightningDamageNegationHead, lightningDamageNegationBody, lightningDamageNegationLegs, lightningDamageNegationBack, lightningDamageNegationHands);
            umbraDamage = CalculateDamageAfterAbsorption(umbraDamage, umbraDamageNegationHead, umbraDamageNegationBody, umbraDamageNegationLegs, umbraDamageNegationBack, umbraDamageNegationLegs);
            holyDamage = CalculateDamageAfterAbsorption(holyDamage, holyDamageNegationHead, holyDamageNegationBody, holyDamageNegationLegs, holyDamageNegationBack, holyDamageNegationHands);
            float finalDamage = physicalDamage + magicDamage + fireDamage + lightningDamage + umbraDamage + holyDamage;

            Debug.Log("Final Damage BEFORE charge attack multiplier = " + finalDamage);
            if(damageDealer != null)
            {
                finalDamage *= damageDealer.GetChargeAttackModifier();
                //enemyCharacterDa
            }
            Debug.Log("Final Damage AFTER charge attack multiplier = " + finalDamage);

            //ResetChargeAttackModifier();

            currentHealth -= Mathf.RoundToInt(finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                characterManager.isDead = true;
            }
            //Play Blocking Noise
        }

        /// <summary>
        /// Function acting as a timer to handle poise reset.
        /// </summary>
        protected virtual void HandlePoiseResetTimer()
        {
            if(poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else
            {
                totalPoiseDefense = GetTotalPoiseFromEquipment();
            }
        }

        protected virtual void DetermineMoralAlignment()
        {
            if(darknessInfluence == holyInfluence)
            {
                isNeutral = true;
                isDark = false;
                isHoly = false;
            }
            else if(darknessInfluence >= 100 && darknessInfluence > holyInfluence*2)
            {
                isDark = true;
                isHoly = false;
                isNeutral = false;
            }
            else if(holyInfluence >= 100 && holyInfluence > darknessInfluence*2)
            {
                isHoly = true;
                isDark = false;
                isNeutral = false;
            }
            else
            {
                isNeutral = true;
                isDark = false;
                isNeutral = false;
            }
        }

        /// <summary>
        /// A function which applies the DamageAbsorption formula to determine the Damage received after Damage Absorption for Equipment Items
        /// </summary>
        /// <param name="damage"> Damage of the Attack</param>
        /// <param name="headAbsorption">Damage absorption value of Head Equipment</param>
        /// <param name="bodyAbsorption">Damage absorption value of Body Equipment</param>
        /// <param name="legAbsorption">Damage absorption value of Leg Equipment</param>
        /// <param name="backAbsorption">Damage absorption value of Back Equipment</param>
        /// <param name="handAbsorption">Damage absorption value of Hand Equipment</param>
        /// <returns>An Integer denoting the final damage received after absorption</returns>
        private int CalculateDamageAfterAbsorption(int damage, float headAbsorption, float bodyAbsorption, float legAbsorption, float backAbsorption, float handAbsorption)
        {
            //Debug.Log("Initial Damage Received = " + damage);
            /* Old damage calculation formula
            float totalDamageAbsorption = 1 -
                (1 - headAbsorption / 100) *
                (1 - bodyAbsorption / 100) *
                (1 - legAbsorption / 100) *
                (1 - backAbsorption / 100) *
                (1 - handAbsorption / 100);
            */
            float totalDamageAbsorption = (headAbsorption + bodyAbsorption + legAbsorption + backAbsorption + handAbsorption) / 100;
            //Debug.Log("Total Damage Absorbed = " + totalDamageAbsorption);
            damage = Mathf.RoundToInt(damage * (1 - totalDamageAbsorption));
            //Debug.Log("Final Damage Received = " + damage);
            return damage;
        }
        
        public virtual void RegenerateHealthOverTime(int healthRegenRate)
        {
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                return;
            }
            currentHealth += Mathf.RoundToInt(healthRegenRate * Time.deltaTime);
        }

        public virtual void RegenerateStaminaOverTime(float staminaRegenRate)
        {
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
                return;
            }
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        public virtual void DeductStamina(float staminaToDeduct)
        {
            currentStamina -= staminaToDeduct;
            if(currentStamina < 0)
            {
                currentStamina = 0;
            }
        }

        public virtual void RegenerateFocusPointsOverTime(float focusPointsRegenRate)
        {
            if (currentFocusPoints >= maxFocusPoints)
            {
                currentFocusPoints = maxFocusPoints;
                return;
            }
            currentFocusPoints += focusPointsRegenRate * Time.deltaTime;
        }

        public virtual void DeductFocusPoints(float focusPointsToDeduct)
        {
            currentFocusPoints -= focusPointsToDeduct;
            if(currentFocusPoints < 0)
            {
                currentFocusPoints = 0;
            }
        }

        /// <summary>
        /// Function to set the maximum stat points (e.g health, stamina etc) from the stat level.<br />
        /// maxStat = statLevel * 10
        /// </summary>
        /// <param name="maxStat">The maximum stat points</param>
        /// <param name="statLevel">The character's stat level</param>
        /// <returns>An integer maxStat, which should be assigned to the corresponding stat for which this function is invoked on</returns>
        protected int SetMaxStatFromStatLevel(int maxStat, int statLevel)
        {
            maxStat = statLevel * 10;
            return maxStat;
        }

        /// <summary>
        /// Overloaded Method where maxStat and statLevel are of type floats
        /// </summary>
        /// <param name="maxStat"></param>
        /// <param name="statLevel"></param>
        /// <returns>A float maxStat, which should be assigned to the corresponding stat for which this function is invoked on</returns>
        protected float SetMaxStatFromStatLevel(float maxStat, float statLevel)
        {
            maxStat = statLevel * 10;
            return maxStat;
        }

        #region Public Methods to Set Stat Values

        /// <summary>
        /// Function to set character's level while levelling up
        /// </summary>
        /// <param name="playerLevel">An integer value representing the character level to set</param>
        public virtual void SetPlayerLevel(int playerLevel)
        {
            this.playerLevel = playerLevel;
        }

        /// <summary>
        /// Function to set character's health level and max health based on the value of the slider while levelling up.
        /// </summary>
        /// <param name="healthLevel">An integer value representing the health level to set.</param>
        public virtual void SetHealthLevelAndMaxHealth(int healthLevel)
        {
            this.healthLevel = healthLevel;
            maxHealth = SetMaxStatFromStatLevel(maxHealth, healthLevel);
        }

        /// <summary>
        /// Function to set character's stamina level and max health based on the value of the slider while levelling up.
        /// </summary>
        /// <param name="staminaLevel">An integer value representing the stamina level to set.</param>
        public virtual void SetStaminaLevelAndMaxStamina(int staminaLevel)
        {
            this.staminaLevel = staminaLevel;
            maxStamina = SetMaxStatFromStatLevel(maxStamina, staminaLevel);
        }

        /// <summary>
        /// Function to set character's focus level and max focus points based on the value of the slider while levelling up.
        /// </summary>
        /// <param name="focusLevel">An integer value representing the focus level to set.</param>
        public virtual void SetFocusLevelAndMaxFocusPoints(int focusLevel)
        {
            this.focusLevel = focusLevel;
            maxFocusPoints = SetMaxStatFromStatLevel(maxFocusPoints, focusLevel);
        }

        /// <summary>
        /// Function to set character's focus level and max poise based on the value of the slider while levelling up.
        /// </summary>
        /// <param name="poiseLevel">An integer value representing the poise level to set.</param>
        public virtual void SetPoiseLevelAndMaxPoise(int poiseLevel)
        {
            this.poiseLevel = poiseLevel;
            maxPoise = SetMaxStatFromStatLevel(maxPoise, poiseLevel);
        }

        /// <summary>
        /// Function to set character's strength level and max strength based on the value of the slider while levelling up.
        /// </summary>
        /// <param name="strengthLevel">An integer value representing the strength level to set.</param>
        public virtual void SetStrengthLevelAndMaxStrength(int strengthLevel)
        {
            this.strengthLevel = strengthLevel;
            maxStrength = SetMaxStatFromStatLevel(maxStrength, strengthLevel);
        }

        /// <summary>
        /// Function to set character's dexterity level and max dexterity based on the value of the slider while levelling up.
        /// </summary>
        /// <param name="dexterityLevel">An integer value representing the dexterity level to set.</param>
        public virtual void SetDexterityLevelAndMaxDexterity(int dexterityLevel)
        {
            this.dexterityLevel = dexterityLevel;
            maxDexterity = SetMaxStatFromStatLevel(maxDexterity, dexterityLevel);
        }

        /// <summary>
        /// Function to set character's intelligence level and max intelligence based on the value of the slider while levelling up.
        /// </summary>
        /// <param name="intelligenceLevel">An integer value representing the intelligence level to set.</param>
        public virtual void SetIntelligenceLevelAndMaxIntelligence(int intelligenceLevel)
        {
            this.intelligenceLevel = intelligenceLevel;
            maxIntelligence = SetMaxStatFromStatLevel(maxIntelligence, intelligenceLevel);
        }

        /// <summary>
        /// Function to set character's faith level and max faith based on the value of the slider while levelling up.
        /// </summary>
        /// <param name="faithLevel">An integer value representing the faith level to set.</param>
        public virtual void SetFaithLevelAndMaxFaith(int faithLevel)
        {
            this.faithLevel = faithLevel;
            maxFaith = SetMaxStatFromStatLevel(maxFaith, faithLevel);
        }
        #endregion

        #region Methods to Get Damage Negation Values
        public virtual float GetTotalPoise()
        {
            return totalPoiseDefense;
        }

        public virtual void SetTotalPoise(float value)
        {
            totalPoiseDefense = value;
        }

        public virtual void ResetTotalPoise()
        {
            totalPoiseDefense = GetTotalPoiseFromEquipment();
        }

        public virtual float GetTotalPoiseFromEquipment()
        {
            float totalPoise = poiseHead + poiseBody + poiseBack + poiseHands + poiseLegs;
            return totalPoise;
        }

        public virtual void ResetPoiseTimer()
        {
            poiseResetTimer = totalPoiseResetTime;
        }

        public virtual float GetTotalUnguardedPhysicalDamageNegation()
        {
            float totalUnguardedPhysicalDamageNegation = physicalDamageNegationHead
                + physicalDamageNegationBody
                + physicalDamageNegationBack
                + physicalDamageNegationHands
                + physicalDamageNegationLegs;
            return totalUnguardedPhysicalDamageNegation;
        }

        public virtual float GetTotalUnguardedMagicDamageNegation()
        {
            float totalUnguardedMagicDamageNegation = magicDamageNegationHead
                + magicDamageNegationBody
                + magicDamageNegationBack
                + magicDamageNegationHands
                + magicDamageNegationLegs;
            return totalUnguardedMagicDamageNegation;
        }

        public virtual float GetTotalUnguardedFireDamageNegation()
        {
            float totalUnguardedFireDamageNegation = fireDamageNegationHead
                + fireDamageNegationBody
                + fireDamageNegationBack
                + fireDamageNegationHands
                + fireDamageNegationLegs;
            return totalUnguardedFireDamageNegation;
        }

        public virtual float GetTotalUnguardedLightningDamageNegation()
        {
            float totalUnguardedLightningDamageNegation = lightningDamageNegationHead
                + lightningDamageNegationBody
                + lightningDamageNegationBack
                + lightningDamageNegationHands
                + lightningDamageNegationLegs;
            return totalUnguardedLightningDamageNegation;
        }

        public virtual float GetTotalUnguardedUmbraDamageNegation()
        {
            float totalUnguardedUmbraDamageNegation = umbraDamageNegationHead
                + umbraDamageNegationBody
                + umbraDamageNegationBack
                + umbraDamageNegationHands
                + umbraDamageNegationLegs;
            return totalUnguardedUmbraDamageNegation;
        }

        public virtual float GetTotalUnguardedHolyDamageNegation()
        {
            float totalUnguardedHolyDamageNegation = holyDamageNegationHead
                + holyDamageNegationBody
                + holyDamageNegationBack
                + holyDamageNegationHands
                + holyDamageNegationLegs;
            return totalUnguardedHolyDamageNegation;
        }
        #endregion

        #region Methods to Set Damage Negation Values
        public virtual void SetDamageNegationHead(float physical, float magic, float fire, float lightning, float umbra, float holy, float poise)
        {
            physicalDamageNegationHead = physical;
            magicDamageNegationHead = magic;
            fireDamageNegationHead = fire;
            lightningDamageNegationHead = lightning;
            umbraDamageNegationHead = umbra;
            holyDamageNegationHead = holy;
            poiseHead = poise;
        }

        public virtual void SetDamageNegationBody(float physical, float magic, float fire, float lightning, float umbra, float holy, float poise)
        {
            physicalDamageNegationBody = physical;
            magicDamageNegationBody = magic;
            fireDamageNegationBody = fire;
            lightningDamageNegationBody = lightning;
            umbraDamageNegationBody = umbra;
            holyDamageNegationBody = holy;
            poiseBody = poise;
        }

        public virtual void SetDamageNegationBack(float physical, float magic, float fire, float lightning, float umbra, float holy, float poise)
        {
            physicalDamageNegationBack = physical;
            magicDamageNegationBack = magic;
            fireDamageNegationBack = fire;
            lightningDamageNegationBack = lightning;
            umbraDamageNegationBack = umbra;
            holyDamageNegationBack = holy;
            poiseBack = poise;
        }

        public virtual void SetDamageNegationHands(float physical, float magic, float fire, float lightning, float umbra, float holy, float poise)
        {
            physicalDamageNegationHands = physical;
            magicDamageNegationHands = magic;
            fireDamageNegationHands = fire;
            lightningDamageNegationHands = lightning;
            umbraDamageNegationHands = umbra;
            holyDamageNegationHands = holy;
            poiseHands = poise;
        }

        public virtual void SetDamageNegationLegs(float physical, float magic, float fire, float lightning, float umbra, float holy, float poise)
        {
            physicalDamageNegationLegs = physical;
            magicDamageNegationLegs = magic;
            fireDamageNegationLegs = fire;
            lightningDamageNegationLegs = lightning;
            umbraDamageNegationLegs = umbra;
            holyDamageNegationLegs = holy;
            poiseLegs = poise;
        }
        #endregion

        #region Methods to Get Guarded Damage Negation Values
        public virtual float GetGuardedPhysicalDamageNegation()
        {
            return guardedPhysicalDamageNegation;
        }

        public virtual float GetGuardedMagicDamageNegation()
        {
            return guardedMagicDamageNegation;
        }

        public virtual float GetGuardedFireDamageNegation()
        {
            return guardedFireDamageNegation;
        }

        public virtual float GetGuardedLightningDamageNegation()
        {
            return guardedLightningDamageNegation;
        }

        public virtual float GetGuardedUmbraDamageNegation()
        {
            return guardedUmbraDamageNegation;
        }

        public virtual float GetGuardedHolyDamageNegation()
        {
            return guardedHolyDamageNegation;
        }

        public virtual float GetStability()
        {
            return stability;
        }
        #endregion

        #region Methods to set Guarded Damage Negation Values
        public virtual void SetGuardedPhysicalDamageNegation(float value)
        {
            guardedPhysicalDamageNegation = value;
        }

        public virtual void SetGuardedMagicDamageNegation(float value)
        {
            guardedMagicDamageNegation = value;
        }

        public virtual void SetGuardedFireDamageNegation(float value)
        {
            guardedFireDamageNegation = value;
        }

        public virtual void SetGuardedLightningDamageNegation(float value)
        {
            guardedLightningDamageNegation = value;
        }

        public virtual void SetGuardedUmbraDamageNegation(float value)
        {
            guardedUmbraDamageNegation = value;
        }

        public virtual void SetGuardedHolyDamageNegation(float value)
        {
            guardedHolyDamageNegation = value;
        }

        public virtual void SetStability(float value)
        {
            stability = value;
        }
        #endregion

    }
}

