using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class CharacterCombatManager : MonoBehaviour
    {
        CharacterManager characterManager;
        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header("Attack Animations")]
        public string OH_Light_Attack_01 = "OH_Light_Attack_01";
        public string OH_Light_Attack_02 = "OH_Light_Attack_02";
        public string OH_Heavy_Attack_01 = "OH_Heavy_Attack_01";
        public string OH_Heavy_Attack_02 = "OH_Heavy_Attack_02";
        public string OH_Running_Attack_01 = "OH_Running_Attack_01";
        public string OH_Jumping_Attack_01 = "OH_Jumping_Attack_01";
        public string OH_Charge_Attack_01 = "OH_Charging_Attack_Charge_01";
        public string OH_Charge_Attack_02 = "OH_Charging_Attack_Charge_02";

        public string TH_Light_Attack_01 = "TH_Light_Attack_01";
        public string TH_Light_Attack_02 = "TH_Light_Attack_02";
        public string TH_Heavy_Attack_01 = "TH_Heavy_Attack_01";
        public string TH_Heavy_Attack_02 = "TH_Heavy_Attack_02";
        public string TH_Running_Attack_01 = "TH_Running_Attack_01";
        public string TH_Jumping_Attack_01 = "TH_Jumping_Attack_01";
        public string TH_Charge_Attack_01 = "TH_Charging_Attack_Charge_01";
        public string TH_Charge_Attack_02 = "TH_Charging_Attack_Charge_02";

        public string weaponArt = "Weapon Art";

        public string lastAttack;

        protected LayerMask backstabLayer = 1 << 13;
        protected LayerMask riposteLayer = 1 << 14;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        public virtual void SetBlockingAbsorptionFromBlockingWeapon()
        {
            WeaponItem blockingWeapon = characterManager.GetCurrentWeapon();
            if(blockingWeapon != null)
            {
                characterManager.SetGuardedPhysicalDamageNegation(blockingWeapon.GetGuardedPhysicalDamageNegation());
                characterManager.SetGuardedMagicDamageNegation(blockingWeapon.GetGuardedMagicDamageNegation());
                characterManager.SetGuardedFireDamageNegation(blockingWeapon.GetGuardedFireDamageNegation());
                characterManager.SetGuardedLightningDamageNegation(blockingWeapon.GetGuardedLightningDamageNegation());
                characterManager.SetGuardedUmbraDamageNegation(blockingWeapon.GetGuardedUmbraDamageNegation());
                characterManager.SetGuardedMagicDamageNegation(blockingWeapon.GetGuardedHolyDamageNegation());
                characterManager.SetStability(blockingWeapon.stability);
            }
        }

        public virtual void ResetBlockingAbsorptionFromBlockingWeapon()
        {
            characterManager.SetGuardedPhysicalDamageNegation(0);
            characterManager.SetGuardedFireDamageNegation(0);
            characterManager.SetGuardedLightningDamageNegation(0);
            characterManager.SetGuardedUmbraDamageNegation(0);
            characterManager.SetGuardedMagicDamageNegation(0);
            characterManager.SetStability(0);
        }

        public virtual void DrainStaminaBasedOnAttack()
        {
            if (characterManager is EnemyManager)
                return;
        }

        /// <summary>
        /// Calculates stamina cost of blocking and plays Animations accordingly
        /// </summary>
        /// <param name="attackingWeapon"></param>
        /// <param name="physicalDamage"></param>
        /// <param name="fireDamage"></param>
        public virtual void AttemptBlock(DamageCollider attackingWeapon, float physicalDamage, float fireDamage, float lightningDamage, float umbraDamage, float magicDamage)
        {
            float totalDamage = physicalDamage + fireDamage + lightningDamage + umbraDamage + magicDamage;
            //float staminaDamageAbsorption = (totalDamage * attackingWeapon.guardBreakModifier) * (characterManager.GetBlockingStabilityRating() / 100);
            float staminaDamage = totalDamage * (1 - (characterManager.GetStability() / 100));
            characterManager.DeductStamina(staminaDamage);
            if(characterManager.GetCurrentStamina() <= 0)
            {
                characterManager.isBlocking = false;
                if(characterManager.IsTwoHanding())
                {
                    characterManager.PlayTargetAnimation("TH_Break_Guard", true);
                }
                else
                {
                    characterManager.PlayTargetAnimation("OH_Break_Guard", true);
                }
            }
            else
            {
                if(characterManager.isTwoHanding)
                {
                    characterManager.PlayTargetAnimation("TH_Block_Guard", true);
                }
                else
                {
                    characterManager.PlayTargetAnimation("OH_Block_Guard", true);
                }
            }
        }

        public virtual void AttemptBlock(float physicalDamage, float magicDamage, float fireDamage, float lightningDamage, float umbraDamage, float holyDamage)
        {
            float totalDamage = physicalDamage + magicDamage + fireDamage + lightningDamage + umbraDamage + holyDamage;
            //float staminaDamageAbsorption = (totalDamage * attackingWeapon.guardBreakModifier) * (characterManager.GetBlockingStabilityRating() / 100);
            float staminaDamage = totalDamage * (1 - (characterManager.GetStability() / 100));
            Debug.Log("Stamina Damage = " + staminaDamage);
            characterManager.DeductStamina(staminaDamage);
            if (characterManager.GetCurrentStamina() <= 0)
            {
                characterManager.isBlocking = false;
                if(characterManager.IsTwoHanding())
                {
                    characterManager.PlayTargetAnimation("TH_Break_Guard", true);
                }
                else
                {
                    characterManager.PlayTargetAnimation("OH_Break_Guard", true);
                }
            }
            else
            {
                if (characterManager.isTwoHanding)
                {

                    characterManager.PlayTargetAnimation("TH_Block_Guard", true);
                }
                else
                {
                    characterManager.PlayTargetAnimation("OH_Block_Guard", true);
                }
            }
        }

    }

}
