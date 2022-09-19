using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JJ
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        PlayerManager playerManager;

        protected override void Awake()
        {
            base.Awake();
            playerManager = GetComponent<PlayerManager>();
        }

        public void AttemptBackstabOrRiposte()
        {
            if (playerManager.GetCurrentStamina() <= 0)
                return;
            RaycastHit hit;
            bool canBackStab = Physics.Raycast(playerManager.GetCriticalAttackRayCastStartPoint().position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backstabLayer);
            if (canBackStab)
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = playerManager.GetRightHandDamageCollider();
  
                if(enemyCharacterManager != null)
                {
                    playerManager.transform.position = enemyCharacterManager.backstabCollider.criticalDamagerStandPosition.position;
                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int damageMultiplier = playerManager.GetRightWeapon().criticalDamageMultiplier;
                    enemyCharacterManager.SetPendingPhysicalCriticalDamage(damageMultiplier * rightWeapon.physicalDamage);
                    enemyCharacterManager.SetPendingMagicCriticalDamage(damageMultiplier * rightWeapon.magicDamage);
                    enemyCharacterManager.SetPendingFireCriticalDamage(damageMultiplier * rightWeapon.fireDamage);
                    enemyCharacterManager.SetPendingLightningCriticalDamage(damageMultiplier * rightWeapon.lightningDamage);
                    enemyCharacterManager.SetPendingUmbraCriticalDamage(damageMultiplier * rightWeapon.umbraDamage);
                    enemyCharacterManager.SetPendingHolyCriticalDamage(damageMultiplier * rightWeapon.holyDamage);
                    playerManager.PlayTargetAnimation("Backstab", true);
                    enemyCharacterManager.GetComponent<EnemyAnimatorManager>().PlayTargetAnimation("Backstabbed", true);
                }
            }
            else if(Physics.Raycast(playerManager.GetCriticalAttackRayCastStartPoint().position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = playerManager.GetRightHandDamageCollider();
                if(enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
                {
                    playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int damageMultiplier = playerManager.GetRightWeapon().criticalDamageMultiplier;
                    enemyCharacterManager.SetPendingPhysicalCriticalDamage(damageMultiplier * rightWeapon.physicalDamage);
                    enemyCharacterManager.SetPendingMagicCriticalDamage(damageMultiplier * rightWeapon.magicDamage);
                    enemyCharacterManager.SetPendingFireCriticalDamage(damageMultiplier * rightWeapon.fireDamage);
                    enemyCharacterManager.SetPendingLightningCriticalDamage(damageMultiplier * rightWeapon.lightningDamage);
                    enemyCharacterManager.SetPendingUmbraCriticalDamage(damageMultiplier * rightWeapon.umbraDamage);
                    enemyCharacterManager.SetPendingHolyCriticalDamage(damageMultiplier * rightWeapon.holyDamage);
                    enemyCharacterManager.canBeRiposted = false; //To reset the bool 
                    playerManager.PlayTargetAnimation("Riposte", true);
                    enemyCharacterManager.GetComponent<EnemyAnimatorManager>().PlayTargetAnimation("Riposted", true);

                }
            }
        }

        /// <summary>
        /// This function is meant to be an animation event
        /// </summary>
        private void SuccessfullyCastSpell()
        {
            playerManager.GetCurrentSpell().SuccessfullyCastedSpell(playerManager, playerManager.isUsingLeftHand);
            playerManager.SetAnimatorBool("isFiringSpell", true);
        }

        public override void DrainStaminaBasedOnAttack()
        {
            WeaponItem currentWeaponUsed = playerManager.GetCurrentWeapon();
            if (currentWeaponUsed != null)
            {
                if(currentAttackType == AttackType.lightAttack01 || currentAttackType == AttackType.lightAttack02)
                {
                    playerManager.DeductStamina(currentWeaponUsed.baseStaminaCost * currentWeaponUsed.lightAttackStaminaModifier);
                }
                else if(currentAttackType == AttackType.heavyAttack01 || currentAttackType == AttackType.heavyAttack02)
                {
                    playerManager.DeductStamina(currentWeaponUsed.baseStaminaCost * currentWeaponUsed.heavyAttackStaminaModifier);
                }
            }
        }

        public override void AttemptBlock(DamageCollider attackingWeapon, float physicalDamage, float fireDamage, float lightningDamage, float umbraDamage, float magicDamage)
        {
            base.AttemptBlock(attackingWeapon, physicalDamage, fireDamage, lightningDamage, umbraDamage, magicDamage);
            playerManager.uiManager.SetCurrentStaminaOnSlider(Mathf.RoundToInt(playerManager.GetCurrentStamina()));
        }


    }
}

