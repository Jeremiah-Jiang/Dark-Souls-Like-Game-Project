using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {

        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Animator Override")]
        public AnimatorOverrideController weaponController;
        //public string offHandIdleAnimation = "Left_Arm_Idle_01";

        [Header("Weapon Type")]
        public WeaponType weaponType;

        [Header("Weapon Effect")]
        public WeaponEffect weaponEffect;

        [Header("Attack Power")]
        [SerializeField] protected int physicalDamage;
        [SerializeField] protected int magicDamage;
        [SerializeField] protected int fireDamage;
        [SerializeField] protected int lightningDamage;
        [SerializeField] protected int umbraDamage;
        [SerializeField] protected int holyDamage;
        [SerializeField] protected int[] allDamageValues;

        [Header("Guarded Damage Negation")]
        [SerializeField] protected float physicalGuardedDamageNegation;
        [SerializeField] protected float magicGuardedDamageNegation;
        [SerializeField] protected float fireGuardedDamageNegation;
        [SerializeField] protected float lightningGuardedDamageNegation;
        [SerializeField] protected float umbraGuardedDamageNegation;
        [SerializeField] protected float holyGuardedDamageNegation;
        [SerializeField] protected float[] allGuardedDamageNegations;

        [Header("Attribute Scaling")]
        [SerializeField] protected int strengthScaling = 0;
        [SerializeField] protected int intelligenceScaling = 0;
        [SerializeField] protected int arcaneScaling = 0;
        [SerializeField] protected int dexterityScaling = 0;
        [SerializeField] protected int faithScaling = 0;

        [Header("Attribute Requirement")]
        [SerializeField] protected int strengthLevelRequirement = 0;
        [SerializeField] protected int intelligenceLevelRequirement = 0;
        [SerializeField] protected int arcaneLevelRequirement = 0;
        [SerializeField] protected int dexterityLevelRequirement = 0;
        [SerializeField] protected int faithLevelRequirement = 0;

        [Header("Passive Effects")]
        [SerializeField] protected string passiveEffect01 = "-";
        [SerializeField] protected string passiveEffect02 = "-";
        [SerializeField] protected string passiveEffect03 = "-";

        [Header("Damage Modifiers")]
        public float lightAttack01DamageModifier = 1;
        public float lightAttack02DamageModifier = 1.2f;
        public float heavyAttack01DamageModifier = 2;
        public float heavyAttack02DamageModifier = 2.4f;
        public int criticalDamageMultiplier = 4;
        public float guardBreakModifier = 1;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseBonus;

        [Header("Stability")] //1 point of stability reduces stamina drain while blocking by 1 %
        public int stability = 67;

        [Header("Stamina Costs")]
        public int baseStaminaCost = 10;
        public float lightAttackStaminaModifier = 1.0f;
        public float heavyAttackStaminaModifier = 1.5f;

        [Header("Item Actions")]
        public ItemAction oh_Tap_RB_Action;
        public ItemAction oh_Tap_RT_Action;
        public ItemAction oh_Tap_LB_Action;
        public ItemAction oh_Tap_LT_Action;
        public ItemAction oh_Hold_RB_Action;
        public ItemAction oh_Hold_RT_Action;
        public ItemAction oh_Hold_LB_Action;
        public ItemAction oh_Hold_LT_Action;

        [Header("Two Handed Item Actions")]
        public ItemAction th_Tap_RB_Action;
        public ItemAction th_Tap_RT_Action;
        public ItemAction th_Tap_LB_Action;
        public ItemAction th_Tap_LT_Action;
        public ItemAction th_Hold_RB_Action;
        public ItemAction th_Hold_RT_Action;
        public ItemAction th_Hold_LB_Action;
        public ItemAction th_Hold_LT_Action;

        [Header("Sound FX")]
        public AudioClip[] weaponWhooshes;

        #region Getter Methods for Weapon Attack Power
        /// <summary>
        /// Method to return the Weapon Item's damage values.<br />
        /// Index 0: Physical Damage.<br />
        /// Index 1: Magic Damage.<br />
        /// Index 2: Fire Damage.<br />
        /// Index 3: Lightning Damage.<br />
        /// Index 4: Umbra Damage.<br />
        /// Index 5: Holy Damage.<br />
        /// </summary>
        /// <returns>A List of all the damage values</returns>
        public int[] GetAllDamageValues()
        {
            if(allDamageValues == null || allDamageValues.Length == 0)
            {
                allDamageValues = new int[6];
                allDamageValues[(int)DamageType.Physical] = GetPhysicalDamage();
                allDamageValues[(int)DamageType.Magic] = GetMagicDamage();
                allDamageValues[(int)DamageType.Fire] = GetFireDamage();
                allDamageValues[(int)DamageType.Lightning] = GetLightningDamage();
                allDamageValues[(int)DamageType.Umbra] = GetUmbraDamage();
                allDamageValues[(int)DamageType.Holy] = GetHolyDamage();
            }
            return allDamageValues;
        }
        public int GetPhysicalDamage()
        {
            return physicalDamage;
        }

        public int GetMagicDamage()
        {
            return magicDamage;
        }

        public int GetFireDamage()
        {
            return fireDamage;
        }

        public int GetLightningDamage()
        {
            return lightningDamage;
        }

        public int GetUmbraDamage()
        {
            return umbraDamage;
        }

        public int GetHolyDamage()
        {
            return holyDamage;
        }

        public int GetWeaponCriticalDamageMultiplier()
        {
            return criticalDamageMultiplier;
        }
        #endregion

        #region Getter methods for Weapon Guarded Damage Negation
        /// <summary>
        /// Method to return the Weapon Item's Guarded Damage Negation values.<br />
        /// Index 0: Physical Guarded Damage Negationbr />
        /// Index 1: Magic Guarded Damage Negation.<br />
        /// Index 2: Fire Guarded Damage Negation.<br />
        /// Index 3: Lightning Guarded Damage Negation.<br />
        /// Index 4: Umbra Guarded Damage Negation.<br />
        /// Index 5: Holy Guarded Damage Negation.<br />
        /// </summary>
        /// <returns>A List of all the damage values</returns>
        public float[] GetAllGuardedDamageNegationValues()
        {
            if (allGuardedDamageNegations == null || allGuardedDamageNegations.Length == 0)
            {
                allGuardedDamageNegations = new float[6];
                allGuardedDamageNegations[(int)DamageType.Physical] = GetGuardedPhysicalDamageNegation();
                allGuardedDamageNegations[(int)DamageType.Magic] = GetGuardedMagicDamageNegation();
                allGuardedDamageNegations[(int)DamageType.Fire] = GetGuardedFireDamageNegation();
                allGuardedDamageNegations[(int)DamageType.Lightning] = GetGuardedLightningDamageNegation();
                allGuardedDamageNegations[(int)DamageType.Umbra] = GetGuardedUmbraDamageNegation();
                allGuardedDamageNegations[(int)DamageType.Holy] = GetGuardedHolyDamageNegation();
            }
            return allGuardedDamageNegations;
        }

        public float GetGuardedPhysicalDamageNegation()
        {
            return physicalGuardedDamageNegation;
        }

        public float GetGuardedMagicDamageNegation()
        {
            return magicGuardedDamageNegation;
        }

        public float GetGuardedFireDamageNegation()
        {
            return fireGuardedDamageNegation;
        }

        public float GetGuardedLightningDamageNegation()
        {
            return lightningGuardedDamageNegation;
        }

        public float GetGuardedUmbraDamageNegation()
        {
            return umbraGuardedDamageNegation;
        }

        public float GetGuardedHolyDamageNegation()
        {
            return holyGuardedDamageNegation;
        }

        public int GetWeaponStability()
        {
            return stability;
        }
        #endregion

        #region Getter Methods for Weapon Attribute Scaling
        public int GetStrengthScaling()
        {
            return strengthScaling;
        }

        public int GetIntelligenceScaling()
        {
            return intelligenceScaling;
        }

        public int GetArcaneScaling()
        {
            return arcaneScaling;
        }

        public int GetDexterityScaling()
        {
            return dexterityScaling;
        }

        public int GetFaithScaling()
        {
            return faithScaling;
        }
        #endregion

        #region Getter Methods for Weapon Attribute Requirements
        public int GetStrengthLevelRequirement()
        {
            return strengthLevelRequirement;
        }

        public int GetIntelligenceLevelRequirement()
        {
            return intelligenceLevelRequirement;
        }

        public int GetArcaneLevelRequirement()
        {
            return arcaneLevelRequirement;
        }

        public int GetDexterityLevelRequirement()
        {
            return dexterityLevelRequirement;
        }

        public int GetFaithLevelRequirment()
        {
            return faithLevelRequirement;
        }
        #endregion

        #region Getter Methods for Weapon Passive Effects
        public string GetPassiveEffect01()
        {
            return passiveEffect01;
        }

        public string GetPassiveEffect02()
        {
            return passiveEffect02;
        }

        public string GetPassiveEffect03()
        {
            return passiveEffect03;
        }
        #endregion
    }
}

