using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Charge Attack Action")]

    public class ChargeAttackAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            if (characterManager.GetCurrentStamina() <= 0)
                return;
            characterManager.EraseHandIKForWeapon();
            characterManager.PlayWeaponFX(characterManager.isUsingLeftHand);

            if (characterManager.CanDoCombo())
            {
                HandleChargeAttackCombo(characterManager);
            }
            else
            {
                if (characterManager.isInteracting)
                    return;
                if (characterManager.canDoCombo)
                    return;
                HandleChargeAttack(characterManager);
            }

        }

        /// <summary>
        /// Function to handle a charge attack
        /// </summary>
        /// <param name="characterManager">Instance of playerManager performing this attack</param>
        /*
         * Implementation Logic:
         * If the player is two handing the weapon, perfrom the two handed variant of the charge attack
         * If the player is not two handing the weapon, check if they are using their left or right hand to perform the attack
         * The logic is the same except that the animation has to be mirrored if the player is using their left hand  
         */
        private void HandleChargeAttack(CharacterManager characterManager)
        {
            string attackAction;
            characterManager.SetCurrentAttackType(AttackType.heavyAttack01);
            if (characterManager.IsTwoHanding())
            {
                attackAction = characterManager.GetTwoHandedChargeAttack01();
                characterManager.PlayTargetAnimation(attackAction, true);
                characterManager.SetLastAttack(attackAction);
            }
            else
            {
                attackAction = characterManager.GetOneHandedChargeAttack01();
                if (characterManager.IsUsingLeftHand())
                {
                    characterManager.PlayTargetAnimation(attackAction, true, false, true);
                }
                else if (characterManager.IsUsingRightHand())
                {
                    characterManager.PlayTargetAnimation(attackAction, true);
                }
                characterManager.SetLastAttack(attackAction);
            }
        }

        /// <summary>
        /// Function to handle Charge Attack Combo
        /// </summary>
        /// <param name="characterManager">Instance of playerManager performing this combo</param>
        /* Implementation Logic:
         * If the player can do a combo, reset the combo flag on the animator
         * If the player is two handing a weapon
         *     If the last attack is the first charge attack variant, combo with the second charge attack variant
         *     otherwise, perform the first charge attack variant
         * If the player is not two handing a weapon, check if they are using their left hand or right hand to perfrom the attack
         * The logic is the same, just that if the player is using the left hand, the animation must be mirrored.
         */
        private void HandleChargeAttackCombo(CharacterManager characterManager)
        {
            if (characterManager.CanDoCombo())
            {
                characterManager.SetCurrentAttackType(AttackType.heavyAttack02);
                characterManager.SetAnimatorBool("canDoCombo", false);

                string attackAction;
                string lastAttack = characterManager.GetLastAttack();

                if (characterManager.IsTwoHanding())
                {
                    if (lastAttack == characterManager.GetTwoHandedChargeAttack01())
                    {
                        attackAction = characterManager.GetTwoHandedChargeAttack02();
                        characterManager.PlayTargetAnimation(attackAction, true);
                        characterManager.SetLastAttack(attackAction);
                    }
                    else
                    {
                        attackAction = characterManager.GetTwoHandedChargeAttack01();
                        characterManager.PlayTargetAnimation(attackAction, true);
                        characterManager.SetLastAttack(attackAction);
                    }
                }
                else
                {
                    if (characterManager.IsUsingLeftHand())
                    {
                        if (lastAttack == characterManager.GetOneHandedChargeAttack01())
                        {
                            attackAction = characterManager.GetOneHandedChargeAttack02();
                            characterManager.PlayTargetAnimation(attackAction, true, false, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                        else
                        {
                            attackAction = characterManager.GetOneHandedChargeAttack01();
                            characterManager.PlayTargetAnimation(attackAction, true, false, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                    }
                    else if (characterManager.IsUsingRightHand())
                    {
                        if (lastAttack == characterManager.GetOneHandedChargeAttack01())
                        {
                            attackAction = characterManager.GetOneHandedChargeAttack02();
                            characterManager.PlayTargetAnimation(attackAction, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                        else
                        {
                            attackAction = characterManager.GetOneHandedChargeAttack01();
                            characterManager.PlayTargetAnimation(attackAction, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                    }
                }
            }
        }
    }
}

