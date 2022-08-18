using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterStatsManager))]
    [RequireComponent(typeof(CharacterCombatManager))]
    [RequireComponent(typeof(CharacterEffectsManager))]
    [RequireComponent(typeof(CharacterSoundFXManager))]
    [RequireComponent(typeof(CharacterAnimatorManager))]
    [RequireComponent(typeof(CharacterInventoryManager))]
    [RequireComponent(typeof(CharacterWeaponSlotManager))]
    public class CharacterManager : MonoBehaviour
    {
        protected Animator _animator;
        protected CharacterStatsManager _characterStatsManager;
        protected CharacterCombatManager _characterCombatManager;
        protected CharacterEffectsManager _characterEffectsManager;
        protected CharacterSoundFXManager _characterSoundFXManager;
        protected CharacterAnimatorManager _characterAnimatorManager;
        protected CharacterInventoryManager _characterInventoryManager;
        protected CharacterWeaponSlotManager _characterWeaponSlotManager;

        [Header("Lock On Transform")]
        public Transform lockOnTransform;

        [Header("Combat Colliders")]
        public CriticalDamageCollider backstabCollider;
        public CriticalDamageCollider riposteCollider;

        [Header("Interaction")]
        public bool isInteracting;

        [Header("Status")]
        public bool isDead;

        [Header("Movement Flags")]
        public bool isRotatingWithRootMotion;
        public bool canRotate;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;

        [Header("Spells")]
        public bool isFiringSpell;

        [Header("Combat Flags")]
        public bool isAiming;
        public bool isParrying;
        public bool isBlocking;
        public bool canDoCombo;
        public bool isTwoHanding;
        public bool canBeParried;
        public bool canBeRiposted;
        public bool isInvulnerable;
        public bool isHoldingArrow;
        public bool isUsingLeftHand;
        public bool isUsingRightHand;
        public bool isPerformingFullyChargedAttack;
        public bool isUsingConsumable;

        //Damage will be inflicted during an animation event
        //Used in backstab or ripose animation
        protected int _pendingPhysicalCriticalDamage;
        protected int _pendingMagicCriticalDamage;
        protected int _pendingFireCriticalDamage;
        protected int _pendingLightningCriticalDamage;
        protected int _pendingUmbraCriticalDamage;
        protected int _pendingHolyCriticalDamage;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterStatsManager = GetComponent<CharacterStatsManager>();
            _characterCombatManager = GetComponent<CharacterCombatManager>();
            _characterEffectsManager = GetComponent<CharacterEffectsManager>();
            _characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            _characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
            _characterInventoryManager = GetComponent<CharacterInventoryManager>();
            _characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();
        }

        protected virtual void Update()
        {
            if(!isBlocking)
            {
                ResetBlockingAbsorptionFromBlockingWeapon();
            }
        }
        protected virtual void FixedUpdate()
        {
            _characterAnimatorManager.CheckHandIKWeight(_characterWeaponSlotManager.rightHandIKTarget, _characterWeaponSlotManager.leftHandIKTarget, isTwoHanding);
        }

        public virtual int GetPendingPhysicalCriticalDamage()
        {
            return _pendingPhysicalCriticalDamage;
        }

        public virtual int GetPendingMagicCriticalDamage()
        {
            return _pendingMagicCriticalDamage;
        }

        public virtual int GetPendingFireCriticalDamage()
        {
            return _pendingFireCriticalDamage;
        }

        public virtual int GetPendingLightningCriticalDamage()
        {
            return _pendingLightningCriticalDamage;
        }

        public virtual int GetPendingUmbraCriticalDamage()
        {
            return _pendingUmbraCriticalDamage;
        }

        public virtual int GetPendingHolyCriticalDamage()
        {
            return _pendingHolyCriticalDamage;
        }

        public virtual void SetPendingPhysicalCriticalDamage(int physicalCriticalDamage)
        {
            _pendingPhysicalCriticalDamage = physicalCriticalDamage;
        }
        public virtual void SetPendingMagicCriticalDamage(int magicCriticalDamage)
        {
            _pendingMagicCriticalDamage = magicCriticalDamage;
        }
        public virtual void SetPendingFireCriticalDamage(int fireCriticalDamage)
        {
            _pendingFireCriticalDamage = fireCriticalDamage;
        }
        public virtual void SetPendingLightningCriticalDamage(int lightningCriticalDamage)
        {
            _pendingLightningCriticalDamage = lightningCriticalDamage;
        }
        public virtual void SetPendingUmbraCriticalDamage(int umbraCriticalDamage)
        {
            _pendingUmbraCriticalDamage = umbraCriticalDamage;
        }
        public virtual void SetPendingHolyCriticalDamage(int holyCriticalDamage)
        {
            _pendingHolyCriticalDamage = holyCriticalDamage;
        }

        public virtual void ResetPendingCriticalDamages()
        {
            _pendingPhysicalCriticalDamage = 0;
            _pendingMagicCriticalDamage = 0;
            _pendingFireCriticalDamage = 0;
            _pendingLightningCriticalDamage = 0;
            _pendingUmbraCriticalDamage = 0;
            _pendingHolyCriticalDamage = 0;
        }
        /// <summary>
        /// Method to directly influence which hand the character is using by setting the boolean fields [isUsingLeftHand] and [isUsingRightHand]<br />
        /// Mainly to assure logic flows correctly in other scripts where the [isUsingXHand] boolean field is used
        /// </summary>
        /// <param name="usingRightHand">Indicate whether the character is using the right hand for the current action</param>
        public virtual void UpdateWhichHandCharacterIsUsing(bool usingRightHand)
        {
            if(usingRightHand)
            {
                isUsingLeftHand = false;
                isUsingRightHand = true;
            }
            else
            {
                isUsingLeftHand = true;
                isUsingRightHand = false;
            }
        }

    //Methods for Character Flags
        #region Methods for Character Flags

        public bool IsInteracting()
        {
            return isInteracting;
        }

        public bool CanDoCombo()
        {
            return canDoCombo;
        }

        public bool IsInvulnerable()
        {
            return isInvulnerable;
        }

        public bool IsUsingRightHand()
        {
            return isUsingRightHand;
        }

        public bool IsUsingLeftHand()
        {
            return isUsingLeftHand;
        }

        public bool IsTwoHanding()
        {
            return isTwoHanding;
        }

        public bool IsAiming()
        {
            return isAiming;
        }

        public bool IsParrying()
        {
            return isParrying;
        }

        public bool IsBlocking()
        {
            return isBlocking;
        }

        public void SetIsBlocking(bool value)
        {
            isBlocking = value;
        }

        public bool IsHoldingArrow()
        {
            return isHoldingArrow;
        }

        public bool IsPerformingFullyChargedAttack()
        {
            return isPerformingFullyChargedAttack;
        }

        public bool IsUsingConsumable()
        {
            return isUsingConsumable;
        }
        public bool CanBeParried()
        {
            return canBeParried;
        }

        public bool CanBeRiposted()
        {
            return canBeRiposted;
        }
        #endregion

    //Methods for Animator
        #region Methods for Animator
        /// <summary>
        /// Getter function to return the instance of Animator located on the same GameObject as this Character Manager
        /// </summary>
        /// <returns>The Animator instance attached to the same GameObject as this CharacterManager</returns>
        public Animator GetAnimator()
        {
            return _animator;
        }

        /// <summary>
        /// Sets the value of the Animator's RuntimeAnimatorController as the given parameter.
        /// </summary>
        /// <param name="animatorOverrideController">The parameter to set the RuntimeAnimatorController as</param>
        public void SetAnimatorOverrideController(AnimatorOverrideController animatorOverrideController)
        {
            _animator.runtimeAnimatorController = animatorOverrideController;
        }

        /// <summary>
        /// Function to invoke Animator.SetBool(string name, bool value).<br />
        /// Sets the value of the given boolean parameter.
        /// </summary>
        /// <param name="boolName">The parameter name.</param>
        /// <param name="boolStatus">The new paramter value.</param>
        public void SetAnimatorBool(string boolName, bool boolStatus)
        {
            _animator.SetBool(boolName, boolStatus);
        }

        /// <summary>
        /// Function to invoke Animator.GetBool(string name).
        /// </summary>
        /// <param name="boolName">The parameter name.</param>
        /// <returns>The value of the boolean parameter</returns>
        public bool GetAnimatorBool(string boolName)
        {
            return _animator.GetBool(boolName);
        }

        /// <summary>
        /// Function to invoke Animator.SetFloat(string name, float value). <br />
        /// Send float values to the Animator to affect transitions.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The new parameter value.</param>
        public void SetAnimatorFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        /// <summary>
        /// Overloaded method to invoke Animator.SetFloat(string name, float value, float dampTime, float deltaTime).<br />
        /// Sends float values to the Animator to affect transitions.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The new parameter value.</param>
        /// <param name="dampTime">The damper total time.</param>
        /// <param name="deltaTime">The deltaTime to give to the damper.</param>
        public void SetAnimatorFloat(string name, float value, float dampTime, float deltaTime)
        {
            _animator.SetFloat(name, value, dampTime, deltaTime);
        }

        /// <summary>
        /// Overloaded method to invoke Animator.SetFloat(int id, float value, float dampTime, float deltaTime).<br />
        /// Sends float values to the Animator to affect transitions.
        /// </summary>
        /// <param name="id">The parameter id.</param>
        /// <param name="value">The new parameter value.</param>
        /// <param name="dampTime">The damper total time.</param>
        /// <param name="deltaTime">The deltaTime to give to the damper.</param>
        public void SetAnimatorFloat(int id, float value, float dampTime, float deltaTime)
        {
            _animator.SetFloat(id, value, dampTime, deltaTime);
        }
        #endregion

    //Methods for CharacterStatsManager
        #region Methods for CharacterStatsManager

        /// <summary>
        /// Getter method to return the instance of CharacterStatsManager located on the same GameObject as this CharacterManager
        /// </summary>
        /// <returns>The CharacterStatsManager instance attached to the same GameObject as this CharacterManager</returns>
        public CharacterStatsManager GetCharacterStatsManager()
        {
            return _characterStatsManager;
        }

        public float GetChargeAttackModifier()
        {
            return _characterStatsManager.GetChargeAttackModifier();
        }

        public void SetChargeAttackModifier(float modifier)
        {
            _characterStatsManager.SetChargeAttackModifier(modifier);
        }

        public void ResetChargeAttackModifier()
        {
            _characterStatsManager.ResetChargeAttackModifier();
        }
        #region Getter Methods for Moral Alignment Levels in CharacterStatsManager
        public bool IsNeutral()
        {
            return _characterStatsManager.isNeutral;
        }

        public bool IsDark()
        {
            return _characterStatsManager.isDark;
        }

        public bool IsHoly()
        {
            return _characterStatsManager.isHoly;
        }

        public float GetHolyInfluence()
        {
            return _characterStatsManager.holyInfluence;
        }

        public float GetDarknessInfluence()
        {
            return _characterStatsManager.darknessInfluence;
        }
        #endregion

        #region Getter methods for Stat Levels in CharacterStatsManager

        public void SetCharacterName(string name)
        {
            _characterStatsManager.characterName = name;
        }

        public string GetCharacterName()
        {
            return _characterStatsManager.characterName;
        }

        public int GetPlayerLevel()
        {
            return _characterStatsManager.playerLevel;
        }

        public int GetTeamID()
        {
            return _characterStatsManager.teamID;
        }

        public List<Allies> GetAllies()
        {
            return _characterStatsManager.allies;
        }

        public int GetHealthLevel()
        {
            return _characterStatsManager.healthLevel;
        }

        public int GetStaminaLevel()
        {
            return _characterStatsManager.staminaLevel;
        }

        public int GetFocusLevel()
        {
            return _characterStatsManager.focusLevel;
        }

        public int GetPoiseLevel()
        {
            return _characterStatsManager.poiseLevel;
        }

        public int GetStrengthLevel()
        {
            return _characterStatsManager.strengthLevel;
        }

        public int GetDexterityLevel()
        {
            return _characterStatsManager.dexterityLevel;
        }

        public int GetIntelligenceLevel()
        {
            return _characterStatsManager.intelligenceLevel;
        }

        public int GetFaithLevel()
        {
            return _characterStatsManager.faithLevel;
        }

        #endregion

        #region Getter metods for Maxmimum Stat values in CharacterStatsManager
        public int GetMaxHealth()
        {
            return _characterStatsManager.maxHealth;
        }

        public float GetMaxStamina()
        {
            return _characterStatsManager.maxStamina;
        }

        public float GetMaxFocusPoints()
        {
            return _characterStatsManager.maxFocusPoints; 
        }

        public float GetMaxPoise()
        {
            return _characterStatsManager.maxPoise;
        }

        public float GetMaxStrength()
        {
            return _characterStatsManager.maxStrength; 
        }

        public float GetMaxDexterity()
        {
            return _characterStatsManager.maxDexterity;
        }

        public float GetMaxIntelligence()
        {
            return _characterStatsManager.maxIntelligence; ;
        }

        public float GetMaxFaith()
        {
            return _characterStatsManager.maxFaith;
        }
        #endregion

        #region Getter methods for Current Stat values in CharacterStatsManager

        public int GetCurrentHealth()
        {
            return _characterStatsManager.currentHealth;
        }

        public float GetCurrentStamina()
        {
            return _characterStatsManager.currentStamina;
        }

        public float GetCurrentFocusPoints()
        {
            return _characterStatsManager.currentFocusPoints;
        }

        public float GetCurrentPoise()
        {
            return _characterStatsManager.currentPoise;
        }

        public float GetCurrentStrength()
        {
            return _characterStatsManager.currentStrength;
        }

        public float GetCurrentDexterity()
        {
            return _characterStatsManager.currentDexterity;
        }

        public float GetCurrentIntelligence()
        {
            return _characterStatsManager.currentIntelligence;
        }

        public float GetCurrentFaith()
        {
            return _characterStatsManager.currentFaith;
        }

        #endregion

        public virtual int GetCurrentSoulCount()
        {
            return _characterStatsManager.currentSoulCount;
        }

        public virtual int GetSoulsAwardedOnDeath()
        {
            return _characterStatsManager.soulsAwardedOnDeath;
        }

        public virtual float GetStability()
        {
            return _characterStatsManager.GetStability();
        }

        public virtual float GetTotalPoise()
        {
            return _characterStatsManager.GetTotalPoise();
        }

        public virtual void SetTotalPoise(float value)
        {
            _characterStatsManager.SetTotalPoise(value);
        }

        public virtual void ResetTotalPoise()
        {
            _characterStatsManager.ResetTotalPoise();
        }

        public virtual void ResetPoiseTimer()
        {
            _characterStatsManager.ResetPoiseTimer();
        }

        public virtual float GetTotalPoiseFromEquipment()
        {
            return _characterStatsManager.GetTotalPoiseFromEquipment();
        }

        /// <summary>
        /// Function to invoke CharacterStatsManager.TakeDamageNoAnimation(int physicalDamage, int fireDamage).
        /// </summary>
        /// <param name="physicalDamage">An integer value denoting the physical damage received</param>
        /// <param name="fireDamage">An integer value denoting the fire damage received</param>
        public virtual void TakeDamageNoAnimation(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage)
        {
            _characterStatsManager.TakeDamageNoAnimation(damageDealer, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);
        }

        /// <summary>
        /// Function to invoke CharacterStatsManager.TakeDamage(int physicalDamage, int fireDamage, string damageAnimation, CharacterManager enemyCharacterDamagingMe).
        /// </summary>
        /// <param name="physicalDamage">An integer value denoting the physical damage received</param>
        /// <param name="fireDamage">An integer value denoting the fire damage received</param>
        /// <param name="damageAnimation">A string denoting the damage animation to be played</param>
        /// <param name="enemyCharacterDamagingMe">The instance of CharacterManager dealing the damage to this CharacterManager</param>
        public virtual void TakeDamage(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage, string damageAnimation)
        {
            _characterStatsManager.TakeDamage(damageDealer, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage, damageAnimation); 
        }

        /// <summary>
        /// Function to invoke CharacterStatsManager.TakeDamageAfterBlocking(int physicalDamage, int fireDamage, string damageAnimation, CharacterManager enemyCharacterDamagingMe).
        /// </summary>
        /// <param name="physicalDamage">An integer value denoting the physical damage received</param>
        /// <param name="fireDamage">An integer value denoting the fire damage received</param>
        /// <param name="damageDealer">The instance of CharacterManager dealing the damage to this CharacterManager</param>
        public virtual void TakeDamageAfterBlocking(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage)
        {
            _characterStatsManager.TakeDamageAfterBlocking(damageDealer, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);
        }

        /// <summary>
        /// Function to invoke CharacterStatsManager's TakePoisonDamage function
        /// </summary>
        /// <param name="poisonDamage">An integer value denoting the poison damage</param>
        public virtual void TakePoisonDamage(int poisonDamage)
        {
            _characterStatsManager.TakePoisonDamage(poisonDamage);
        }

        /// <summary>
        /// Function to invoke CharacterStatsManager's DeductStamina function
        /// </summary>
        /// <param name="staminaToDeduct">A float denoting the stamina to deduct from CharacterStats</param>
        public virtual void DeductStamina(float staminaToDeduct)
        {
            _characterStatsManager.DeductStamina(staminaToDeduct);
        }

        /// <summary>
        /// Function to invoke CharacterStatsManager's DeductFocusPoints function
        /// </summary>
        /// <param name="focusPointsToDeduct">A float denoting the stamina to deduct from CharacterStats</param>
        public virtual void DeductFocusPoints(float focusPointsToDeduct)
        {
            _characterStatsManager.DeductFocusPoints(focusPointsToDeduct);
        }

        /// <summary>
        /// Getter method to retrieve the value of CharacterStatsManager.blockingPhysicalDamageAbsorption
        /// </summary>
        /// <returns>this.characterStatsManager.blockingPhysicalDamageAbsorption</returns>
        public virtual float GetGuardedPhysicalDamageNegation()
        {
            return _characterStatsManager.GetGuardedPhysicalDamageNegation();
        }

        /// <summary>
        /// Setter method to set the value of CharacterStatsManager's field [blockingPhysicalDamageAbsorption]
        /// </summary>
        /// <param name="negationValue">The float value that will be assigned to characterStatsManager.blockingPhysicalDamageAbsorption</param>
        public virtual void SetGuardedPhysicalDamageNegation(float negationValue)
        {
            _characterStatsManager.SetGuardedPhysicalDamageNegation(negationValue);
        }

        public virtual float GetGuardedMagicDamageNegation()
        {
            return _characterStatsManager.GetGuardedMagicDamageNegation();
        }

        public virtual void SetGuardedMagicDamageNegation(float negationValue)
        {
            _characterStatsManager.SetGuardedMagicDamageNegation(negationValue);
        }

        /// <summary>
        /// Getter method to retrieve the value of CharacterStatsManager.blockingFireDamageAbsorption
        /// </summary>
        /// <returns>this.characterStatsManager.blockingFireDamageAbsorption</returns>
        public virtual float GetGuardedFireDamageNegation()
        {
            return _characterStatsManager.GetGuardedFireDamageNegation();
        }

        /// <summary>
        /// Setter method to set the value of CharacterStatsManager's field [blockingFireDamageAbsorption]
        /// </summary>
        /// <param name="negationValue">The float value that will be assigned to characterStatsManager.blockingFireDamageAbsorption</param>
        public virtual void SetGuardedFireDamageNegation(float negationValue)
        {
            _characterStatsManager.SetGuardedFireDamageNegation(negationValue);
        }

        public virtual float GetGuardedLightningDamageNegation()
        {
            return _characterStatsManager.GetGuardedLightningDamageNegation();
        }

        public virtual void SetGuardedLightningDamageNegation(float negationValue)
        {
            _characterStatsManager.SetGuardedLightningDamageNegation(negationValue);
        }

        public virtual float GetGuardedUmbraDamageNegation()
        {
            return _characterStatsManager.GetGuardedUmbraDamageNegation();
        }

        public virtual void SetGuardedUmbraDamageNegation(float negationValue)
        {
            _characterStatsManager.SetGuardedUmbraDamageNegation(negationValue);
        }

        public virtual float GetGuardedHolyDamageNegation()
        {
            return _characterStatsManager.GetGuardedHolyDamageNegation();
        }

        public virtual void SetGuardedHolyDamageNegation(float negationValue)
        {
            _characterStatsManager.SetGuardedHolyDamageNegation(negationValue);
        }

        /// <summary>
        /// Setter method to set the value of CharacterStatsManager's field [blockingStabilityRating]
        /// </summary>
        /// <param name="absorptionValue">The integer value that will be assigned to characterStatsManager.blockingStabilityRating</param>
        public virtual void SetStability(int stabilityRating)
        {
            _characterStatsManager.SetStability(stabilityRating);
        }

        public virtual float GetTotalUnguardedPhysicalDamageNegation()
        {
            return _characterStatsManager.GetTotalUnguardedPhysicalDamageNegation();
        }

        public virtual float GetTotalUnguardedMagicDamageNegation()
        {
            return _characterStatsManager.GetTotalUnguardedMagicDamageNegation();
        }

        public virtual float GetTotalUnguardedFireDamageNegation()
        {
            return _characterStatsManager.GetTotalUnguardedFireDamageNegation();
        }

        public virtual float GetTotalUnguardedLightningDamageNegation()
        {
            return _characterStatsManager.GetTotalUnguardedLightningDamageNegation();

        }

        public virtual float GetTotalUnguardedUmbraDamageNegation()
        {
            return _characterStatsManager.GetTotalUnguardedUmbraDamageNegation();
        }

        public virtual float GetTotalUnguardedHolyDamageNegation()
        {
            return _characterStatsManager.GetTotalUnguardedHolyDamageNegation();
        }

        public virtual void SetDamageNegationHead(float physicalNegation, float magicNegation, float fireNegation, float lightningNegation, float umbraNegation, float holyNegation, float poiseBonus)
        {
            _characterStatsManager.SetDamageNegationHead(physicalNegation, magicNegation, fireNegation, lightningNegation, umbraNegation, holyNegation, poiseBonus);
        }

        public virtual void SetDamageAbsorptionBody(float physicalNegation, float magicNegation, float fireNegation, float lightningNegation, float umbraNegation, float holyNegation, float poiseBonus)
        {
            _characterStatsManager.SetDamageNegationBody(physicalNegation, magicNegation, fireNegation, lightningNegation, umbraNegation, holyNegation, poiseBonus);
        }

        public virtual void SetDamageAbsorptionBack(float physicalNegation, float magicNegation, float fireNegation, float lightningNegation, float umbraNegation, float holyNegation, float poiseBonus)
        {
            _characterStatsManager.SetDamageNegationBack(physicalNegation, magicNegation, fireNegation, lightningNegation, umbraNegation, holyNegation, poiseBonus);
        }

        public virtual void SetDamageAbsorptionHands(float physicalNegation, float magicNegation, float fireNegation, float lightningNegation, float umbraNegation, float holyNegation, float poiseBonus)
        {
            _characterStatsManager.SetDamageNegationHands(physicalNegation, magicNegation, fireNegation, lightningNegation, umbraNegation, holyNegation, poiseBonus);
        }

        public virtual void SetDamageAbsorptionLegs(float physicalNegation, float magicNegation, float fireNegation, float lightningNegation, float umbraNegation, float holyNegation, float poiseBonus)
        {
            _characterStatsManager.SetDamageNegationLegs(physicalNegation, magicNegation, fireNegation, lightningNegation, umbraNegation, holyNegation, poiseBonus);
        }

        public virtual void SetPlayerLevel(int playerLevel)
        {
            _characterStatsManager.SetPlayerLevel(playerLevel);
        }

        public virtual void SetHealthLevelAndMaxHealth(int healthLevel)
        {
            _characterStatsManager.SetHealthLevelAndMaxHealth(healthLevel);
        }

        public virtual void SetStaminaLevelAndMaxStamina(int staminaLevel)
        {
            _characterStatsManager.SetStaminaLevelAndMaxStamina(staminaLevel);
        }

        public virtual void SetFocusLevelAndMaxFocusPoints(int focusLevel)
        {
            _characterStatsManager.SetFocusLevelAndMaxFocusPoints(focusLevel);
        }

        public virtual void SetPoiseLevelAndMaxPoise(int poiseLevel)
        {
            _characterStatsManager.SetPoiseLevelAndMaxPoise(poiseLevel);
        }

        public virtual void SetStrengthLevelAndMaxStrength(int strengthLevel)
        {
            _characterStatsManager.SetStrengthLevelAndMaxStrength(strengthLevel);
        }

        public virtual void SetDexterityLevelAndMaxDexterity(int dexterityLevel)
        {
            _characterStatsManager.SetDexterityLevelAndMaxDexterity(dexterityLevel);
        }

        public virtual void SetIntelligenceLevelAndMaxIntelligence(int intelligenceLevel)
        {
            _characterStatsManager.SetIntelligenceLevelAndMaxIntelligence(intelligenceLevel);
        }

        public virtual void SetFaithLevelAndMaxFaith(int faithLevel)
        {
            _characterStatsManager.SetFaithLevelAndMaxFaith(faithLevel);
        }

        #endregion

    //Methods for CharacterCombatManager
        #region Methods for CharacterCombatManager
        public string GetLastAttack()
        {
            return _characterCombatManager.lastAttack;
        }
        public void SetLastAttack(string attackName)
        {
            _characterCombatManager.lastAttack = attackName;
        }

        //Getter methods for One Handed Attack Variants
        #region Getter methods for One Handed Attack Variants
        public string GetOneHandedLightAttack01()
        {
            return _characterCombatManager.OH_Light_Attack_01;
        }

        public string GetOneHandedLightAttack02()
        {
            return _characterCombatManager.OH_Light_Attack_02;
        }

        public string GetOneHandedHeavyAttack01()
        {
            return _characterCombatManager.OH_Heavy_Attack_01;
        }

        public string GetOneHandedHeavyAttack02()
        {
            return _characterCombatManager.OH_Heavy_Attack_02;
        }

        public string GetOneHandedRunningAttack01()
        {
            return _characterCombatManager.OH_Running_Attack_01;
        }

        public string GetOneHandedJumpingAttack01()
        {
            return _characterCombatManager.OH_Jumping_Attack_01;
        }

        public string GetOneHandedChargeAttack01()
        {
            return _characterCombatManager.OH_Charge_Attack_01;
        }

        public string GetOneHandedChargeAttack02()
        {
            return _characterCombatManager.OH_Charge_Attack_02;
        }
        #endregion

        //Getter methods for One Handed Attack Variants
        #region Getter methods for Two Handed Attack Variants
        public string GetTwoHandedLightAttack01()
        {
            return _characterCombatManager.TH_Light_Attack_01;
        }

        public string GetTwoHandedLightAttack02()
        {
            return _characterCombatManager.TH_Light_Attack_02;
        }

        public string GetTwoHandedHeavyAttack01()
        {
            return _characterCombatManager.TH_Heavy_Attack_01;
        }

        public string GetTwoHandedHeavyAttack02()
        {
            return _characterCombatManager.TH_Heavy_Attack_02;
        }

        public string GetTwoHandedRunningAttack01()
        {
            return _characterCombatManager.TH_Running_Attack_01;
        }

        public string GetTwoHandedJumpingAttack01()
        {
            return _characterCombatManager.TH_Jumping_Attack_01;
        }

        public string GetTwoHandedChargeAttack01()
        {
            return _characterCombatManager.TH_Charge_Attack_01;
        }

        public string GetTwoHandedChargeAttack02()
        {
            return _characterCombatManager.TH_Charge_Attack_02;
        }
        #endregion
        /// <summary>
        /// Getter method to return the instance of CharacterCombatManager located on the same GameObject as this Character Manager
        /// </summary>
        /// <returns>The CharacterCombatManager instance attached to the same GameObject as this CharacterManager</returns>
        public CharacterCombatManager GetCharacterCombatManager()
        {
            return _characterCombatManager;
        }

        /// <summary>
        /// Getter method to return the currentAttackType field associated with the instance of CharacterCombatManager attached to this CharacterManager
        /// </summary>
        /// <returns>The currentAttackType of type AttackType</returns>
        public AttackType GetCurrentAttackType()
        {
            return _characterCombatManager.currentAttackType;
        }

        public void SetCurrentAttackType(AttackType currentAttackType)
        {
            _characterCombatManager.currentAttackType = currentAttackType;
        }

        /// <summary>
        /// Method to invoke CharacterCombatManager.SetBlockingAbsorptionFromBlockingWeapon() function
        /// </summary>
        public void SetBlockingAbsorptionFromBlockingWeapon()
        {
            _characterCombatManager.SetBlockingAbsorptionFromBlockingWeapon();
        }

        public void ResetBlockingAbsorptionFromBlockingWeapon()
        {
            _characterCombatManager.ResetBlockingAbsorptionFromBlockingWeapon();
        }

        /// <summary>
        /// Method to invoke CharacterCombatManager.AttemptBlock(...) function
        /// </summary>
        /// <param name="attackingWeapon">The damage collider of the attackingWeapon</param>
        /// <param name="physicalDamage">The physical damage associated with the attack</param>
        /// <param name="fireDamage">The fire damage associated with the attack</param>
        public void AttemptBlock(DamageCollider attackingWeapon, float physicalDamage, float fireDamage, float lightningDamage, float umbraDamage, float magicDamage)
        {
            _characterCombatManager.AttemptBlock(attackingWeapon, physicalDamage, fireDamage, lightningDamage, umbraDamage, magicDamage);  
        }

        public void AttemptBlock(float physicalDamage, float magicDamage, float fireDamage, float lightningDamage, float umbraDamage, float holyDamage)
        {
            _characterCombatManager.AttemptBlock(physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);
        }

        #endregion

    //Methods for CharacterEffectsManager
        #region Methods for CharacterEffectsManager

        /// <summary>
        /// Getter method to retrieve characterEffectsManager.currentRangeFX
        /// </summary>
        /// <returns></returns>
        public GameObject GetCurrentRangeFX()
        {
            return _characterEffectsManager.currentRangeFX;
        }

        /// <summary>
        /// Setter method to assign _characterEffectsManager.currentRangeFX with the parameter rangeFX
        /// </summary>
        /// <param name="rangeFX"></param>
        public void SetCurrentRangeFX(GameObject rangeFX)
        {
            _characterEffectsManager.currentRangeFX = rangeFX;
        }

        /// <summary>
        /// Setter method to set param of type WeaponFX to the rightWeaponFX field of the instance of CharacterEffectsManager attached to this CharacterManager
        /// </summary>
        /// <param name="weaponFX">The WeaponFX to set the field value as</param>
        public void SetRightWeaponFX(WeaponFX weaponFX)
        {
            _characterEffectsManager.rightWeaponFX = weaponFX;
        }

        /// <summary>
        /// Setter method to set param of type WeaponFX to the leftWeaponFX field of the instance of CharacterEffectsManager attached to this CharacterManager
        /// </summary>
        /// <param name="weaponFX">The WeaponFX to set the field value as</param>
        public void SetLeftWeaponFX(WeaponFX weaponFX)
        {
            _characterEffectsManager.leftWeaponFX = weaponFX;
        }

        /// <summary>
        /// Method to invoke _characterEffectsManager.PlayWeaponFX(.)
        /// </summary>
        /// <param name="isLeft"></param>
        public void PlayWeaponFX(bool isLeft)
        {
            _characterEffectsManager.PlayWeaponFX(isLeft);
        }

        public void PlayBloodSplatterFX(Vector3 bloodSplatterLocation)
        {
            _characterEffectsManager.PlayBloodSplatterFX(bloodSplatterLocation);
        }

        public void PlayBlockSparksFX(Vector3 blockSparksLocation)
        {
            _characterEffectsManager.PlayBlockSparksFX(blockSparksLocation);
        }
        public void SetPoisonBuildUp(float poisonBuildUp)
        {
            _characterEffectsManager.poisonBuildUp = poisonBuildUp;
        }

        public float GetDefaultPoisonAmount()
        {
            return _characterEffectsManager.defaultPoisonAmount;
        }

        public void SetPoisonAmount(float poisonAmount)
        {
            _characterEffectsManager.poisonAmount = poisonAmount;
        }

        public void SetIsPoisonedBool(bool isPoisoned)
        {
            _characterEffectsManager.isPoisoned = isPoisoned;
        }

        public GameObject GetCurrentPoisonParticleFX()
        {
            return _characterEffectsManager.currentPoisonParticleFX;
        }
        #endregion

    //Methods for CharacterInventoryManager
        #region Methods for CharacterInventoryManager

        /// <summary>
        /// Setter method to set the currentItemBeing used as the input parameter item<br />
        /// <br />_characterInventoryManager.currentItemBeingUsed = item
        /// </summary>
        /// <param name="item"></param>
        public void SetCurrentItemBeingUsed(Item item)
        {
            _characterInventoryManager.currentItemBeingUsed = item;
        }

        public ConsumableItem[] GetConsumableItemsEquipped()
        {
            return _characterInventoryManager.consumableItemsEquipped;
        }
        /// <summary>
        /// Getter method to retrieve the list of weapons in the inventory right hand slots
        /// </summary>
        /// <returns>_characterInventoryManager.weaponsInRightHandSlots</returns>
        public WeaponItem[] GetWeaponsInRightHandSlots()
        {
            return _characterInventoryManager.weaponsInRightHandSlots;
        }

        /// <summary>
        /// Getter method to retrieve the list of weapons in the inventory left hand slots
        /// </summary>
        /// <returns>_characterInventoryManager.weaponsInLeftHandSlots</returns>
        public WeaponItem[] GetWeaponsInLeftHandSlots()
        {
            return _characterInventoryManager.weaponsInLeftHandSlots;
        }

        /// <summary>
        /// Getter method to return the instance of CharacterInventoryManager located on the same GameObject as this Character Manager
        /// </summary>
        /// <returns>The CharacterInventoryManager instance attached to the same GameObject as this CharacterManager</returns>
        public CharacterInventoryManager GetCharacterInventoryManager()
        {
            return _characterInventoryManager;
        }

        /// <summary>
        /// Getter method to retrieve _characterInventoryManager.currentSpell
        /// </summary>
        /// <returns></returns>
        public SpellItem GetCurrentSpell()
        {
            return _characterInventoryManager.currentSpell;
        }

        public Item GetCurrentItemBeingUsed()
        {
            return _characterInventoryManager.currentItemBeingUsed;
        }

        /// <summary>
        /// Getter method to return the instance of the WeaponItem currently being used if the item currently being used in CharacterInventoryManager is of type WeaponItem
        /// </summary>
        /// <returns>The instance of WeaponItem if the current item being used is of type WeaponItem, else return null</returns>
        public WeaponItem GetCurrentWeapon()
        {
            Item currentItemBeingUsed = _characterInventoryManager.currentItemBeingUsed;
            if (currentItemBeingUsed is WeaponItem)
            {
                return currentItemBeingUsed as WeaponItem;
            }
            return null;
        }

        /// <summary>
        /// Getter method to return the right hand weapon in the instance of CharacterInventoryManager attached to this CharacterManager
        /// </summary>
        /// <returns>The right hand weapon of type WeaponItem</returns>
        public WeaponItem GetRightWeapon()
        {
            return _characterInventoryManager.rightWeapon;
        }

        /// <summary>
        /// Setter method to set the right hand weapon in the instance of CharacterInventoryManager attached to this CharacterManager
        /// </summary>
        /// <param name="weaponItem">The instance of WeaponItem to assign to the right weapon</param>
        public void SetRightWeapon(WeaponItem weaponItem)
        {
            _characterInventoryManager.rightWeapon = weaponItem;
        }

        public int GetCurrentRightWeaponIndex()
        {
            return _characterInventoryManager.currRightWeaponIdx;
        }

        public void SetCurrentRightWeaponIdx(int indexValue)
        {
            _characterInventoryManager.currRightWeaponIdx = indexValue;
        }

        /// <summary>
        /// Getter method to return the left hand weapon in the instance of CharacterInventoryManager attached to this CharacterManager
        /// </summary>
        /// <returns>The left hand weapon of type WeaponItem</returns>
        public WeaponItem GetLeftWeapon()
        {
            return _characterInventoryManager.leftWeapon;
        }
        
        /// <summary>
        /// Setter method to set the left hand weapon in the instance of CharacterInventoryManager attached to this CharacterManager
        /// </summary>
        /// <param name="weaponItem">The instance of WeaponItem to assign to the left weapon</param>
        public void SetLeftWeapon(WeaponItem weaponItem)
        {
            _characterInventoryManager.leftWeapon = weaponItem;
        }

        public int GetCurrentLeftWeaponIndex()
        {
            return _characterInventoryManager.currLeftWeaponIdx;
        }

        public ConsumableItem GetCurrentConsumableItem()
        {
            return _characterInventoryManager.currentConsumable;
        }

        public void SetCurrentConsumableItem(ConsumableItem consumableItem)
        {
            _characterInventoryManager.currentConsumable = consumableItem;
        }

        public void SetCurrentLeftWeaponIdx(int indexValue)
        {
            _characterInventoryManager.currLeftWeaponIdx = indexValue;
        }

        public int GetCurrentConsumableIndex()
        {
            return _characterInventoryManager.currConsumableIdx;
        }

        public void SetCurrentConsumableIdx(int indexValue)
        {
            _characterInventoryManager.currConsumableIdx = indexValue;
        }

        /// <summary>
        /// Getter method to return _characterInventoryManager.currentAmmo
        /// </summary>
        /// <returns>The currentAmmo of type RangedAmmoItem</returns>
        public RangedAmmoItem GetCurrentAmmo()
        {
            return _characterInventoryManager.currentAmmo;
        }

        /// <summary>
        /// Getter method to return _characterInventoryManager.currentAmmo.loadedItemModel
        /// </summary>
        /// <returns>The GameObject representing the currentAmmo's loaded item model</returns>
        public GameObject GetCurrentAmmoLoadedItemModel()
        {
            return _characterInventoryManager.currentAmmo.loadedItemModel;
        }

        /// <summary>
        /// Getter method to return _characterInventoryManager.currentAmmo.liveItemModel
        /// </summary>
        /// <returns>The GameObject representing the currentAmmo's live item model</returns>
        public GameObject GetCurrentAmmoLiveItemModel()
        {
            return _characterInventoryManager.currentAmmo.liveAmmoModel;
        }

        public HelmetEquipment GetCurrentHelmetEquipment()
        {
            return _characterInventoryManager.currentHelmetEquipment;
        }

        public void SetCurrentHelmetEquipment(HelmetEquipment helmetEquipment)
        {
            _characterInventoryManager.currentHelmetEquipment = helmetEquipment;
        }

        public TorsoEquipment GetCurrentTorsoEquipment()
        {
            return _characterInventoryManager.currentTorsoEquipment;
        }

        public void SetCurrentTorsoEquipment(TorsoEquipment torsoEquipment)
        {
            _characterInventoryManager.currentTorsoEquipment = torsoEquipment;
        }

        public BackEquipment GetCurrentBackEquipment()
        {
            return _characterInventoryManager.currentBackEquipment;
        }

        public void SetCurrentBackEquipment(BackEquipment backEquipment)
        {
            _characterInventoryManager.currentBackEquipment = backEquipment;
        }

        public HandEquipment GetCurrentHandEquipment()
        {
            return _characterInventoryManager.currentHandEquipment;
        }

        public void SetCurrentHandEquipment(HandEquipment handEquipment)
        {
            _characterInventoryManager.currentHandEquipment = handEquipment;
        }

        public LegEquipment GetCurrentLegEquipment()
        {
            return _characterInventoryManager.currentLegEquipment;
        }

        public void SetCurrentLegEquipment(LegEquipment legEquipment)
        {
            _characterInventoryManager.currentLegEquipment = legEquipment;
        }
        #endregion

    //Methods for CharacterAnimatorManager
        #region Methods for CharacterAnimatorManager
        public CharacterAnimatorManager GetCharacterAnimatorManager()
        {
            return _characterAnimatorManager;
        }
        
        public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false, bool mirrorAnim = false)
        {
            _characterAnimatorManager.PlayTargetAnimation(targetAnim, isInteracting, canRotate, mirrorAnim);   
        }

        public void PlayTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
        {
            _characterAnimatorManager.PlayTargetAnimationWithRootRotation(targetAnim, isInteracting);
        }

        public void SetHandIKForWeapon(RightHandIKTarget rightHandIKTarget, LeftHandIKTarget leftHandIKTarget, bool isTwoHanding)
        {
            _characterAnimatorManager.SetHandIKForWeapon(rightHandIKTarget, leftHandIKTarget, isTwoHanding);
        }

        public void EraseHandIKForWeapon()
        {
            _characterAnimatorManager.EraseHandIKForWeapon();
        }
        #endregion

    //Methods for CharacterWeaponSlotManager
        #region Methods for CharacterWeaponSlotManager

        public WeaponItem GetUnarmedWeapon()
        {
            return _characterWeaponSlotManager.unarmedWeapon;
        }

        /// <summary>
        /// Method to invoke CharacterWeaponSlotManager.LoadWeaponOnSlot(..)
        /// </summary>
        /// <param name="weaponItem"></param>
        /// <param name="isLeft"></param>
        public virtual void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            _characterWeaponSlotManager.LoadWeaponOnSlot(weaponItem, isLeft);
        }

        /// <summary>
        /// Method to invoke CharacterWeaponSlotManager.LoadBothWeaponsOnSlots()
        /// </summary>
        public virtual void LoadBothWeaponsOnSlots()
        {
            _characterWeaponSlotManager.LoadBothWeaponsOnSlots();
        }

        public virtual void CloseDamageCollider()
        {
            _characterWeaponSlotManager.CloseDamageCollider();
        }

        public virtual void OpenDamageCollider()
        {
            _characterWeaponSlotManager.OpenDamageCollider();
        }

        /// <summary>
        /// Getter method to retrieve _characterWeaponSlotManager.leftHandSlot
        /// </summary>
        /// <returns>The leftHandSlot instance of type WeaponHolderSlot</returns>
        public virtual WeaponHolderSlot GetLeftHandSlot()
        {
            return _characterWeaponSlotManager.leftHandSlot;
        }

        /// <summary>
        /// Getter method to retrieve _characterWeaponSlotManager.rightHandSlot
        /// </summary>
        /// <returns>The rightHandSlot instance of type WeaponHolderSlot</returns>
        public virtual WeaponHolderSlot GetRightHandSlot()
        {
            return _characterWeaponSlotManager.rightHandSlot;
        }

        /// <summary>
        /// Method to invoke _characterWeaponSlotManager.LoadTwoHandIKTargets(.)
        /// </summary>
        /// <param name="isTwoHanding"></param>
        public virtual void LoadTwoHandIKTargets(bool isTwoHanding)
        {
            _characterWeaponSlotManager.LoadTwoHandIKTargets(isTwoHanding);
        }

        public virtual DamageCollider GetRightHandDamageCollider()
        {
            return _characterWeaponSlotManager.rightHandDamageCollider;
        }

        public virtual DamageCollider GetLeftHandDamageCollider()
        {
            return _characterWeaponSlotManager.leftHandDamageCollider;
        }
        #endregion

    //Methods for CharacterSoundFXManager
        #region Methods for CharacterSoundFXManager
        public virtual void PlayRandomWeaponWhoosh()
        {
            _characterSoundFXManager.PlayRandomWeaponWhoosh();
        }

        public virtual void PlayRandomDamageSoundFX()
        {
            _characterSoundFXManager.PlayRandomDamageSoundFX();
        }
        #endregion
    }
}

