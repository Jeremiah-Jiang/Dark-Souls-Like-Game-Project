using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Heavy Attack Action")]
    public class HeavyAttackAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            if (characterManager.GetCurrentStamina() <= 0)
                return;
            characterManager.EraseHandIKForWeapon();
            characterManager.PlayWeaponFX(characterManager.isUsingLeftHand);

            if (characterManager.isSprinting)
            {
                HandleJumpingAttack(characterManager);
                characterManager.SetCurrentAttackType(AttackType.heavyAttack01);
                return;
            }
            if (characterManager.CanDoCombo())
            {
                HandleHeavyAttackCombo(characterManager);
                characterManager.SetCurrentAttackType(AttackType.heavyAttack02);
            }
            else
            {
                if (characterManager.IsInteracting())
                    return;
                if (characterManager.CanDoCombo())
                    return;
                HandleHeavyAttack(characterManager);
                characterManager.SetCurrentAttackType(AttackType.heavyAttack01);
            }
        }

        /// <summary>
        /// Function to handle a heavy attack
        /// </summary>
        /// <param name="characterManager">Instance of playerManager performing this attack</param>
        /*
         * Implementation Logic:
         * If the player is two handing the weapon, perfrom the two handed variant of the heavy attack
         * If the player is not two handing the weapon, check if they are using their left or right hand to perform the attack
         * The logic is the same except that the animation has to be mirrored if the player is using their left hand  
         */
        private void HandleHeavyAttack(CharacterManager characterManager)
        {
            string attackAction;

            if (characterManager.IsTwoHanding())
            {
                attackAction = characterManager.GetTwoHandedHeavyAttack01();
                characterManager.PlayTargetAnimation(attackAction, true);
                characterManager.SetLastAttack(attackAction);
            }
            else
            {
                attackAction = characterManager.GetOneHandedHeavyAttack01();
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
        /// Functino to handle jumping attack
        /// </summary>
        /// <param name="characterManager"></param>
        /* Implementation Logic:
         * Same as HandleHeavyAttack(), differing only in the name of the attackActions
         */
        private void HandleJumpingAttack(CharacterManager characterManager)
        {
            string attackAction;

            if (characterManager.IsTwoHanding())
            {
                attackAction = characterManager.GetTwoHandedJumpingAttack01();
                characterManager.PlayTargetAnimation(attackAction, true);
                characterManager.SetLastAttack(attackAction);
            }
            else
            {
                attackAction = characterManager.GetOneHandedJumpingAttack01();
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
        /// Function to handle Heavy Attack Combo
        /// </summary>
        /// <param name="characterManager">Instance of playerManager performing this combo</param>
        /* Implementation Logic:
         * If the player can do a combo, reset the combo flag on the animator
         * If the player is two handing a weapon
         *     If the last attack is the first heavy attack variant, combo with the second heavy attack variant
         *     otherwise, perform the first heavy attack variant
         * If the player is not two handing a weapon, check if they are using their left hand or right hand to perfrom the attack
         * The logic is the same, just that if the player is using the left hand, the animation must be mirrored.
         */
        private void HandleHeavyAttackCombo(CharacterManager characterManager)
        {
            if (characterManager.CanDoCombo())
            {
                characterManager.SetAnimatorBool("canDoCombo", false);

                string attackAction;
                string lastAttack = characterManager.GetLastAttack();

                if (characterManager.IsTwoHanding())
                {
                    if (lastAttack == characterManager.GetTwoHandedHeavyAttack01())
                    {
                        attackAction = characterManager.GetTwoHandedHeavyAttack02();
                        characterManager.PlayTargetAnimation(attackAction, true);
                        characterManager.SetLastAttack(attackAction);
                    }
                    else
                    {
                        attackAction = characterManager.GetTwoHandedHeavyAttack01();
                        characterManager.PlayTargetAnimation(attackAction, true);
                        characterManager.SetLastAttack(attackAction);
                    }
                }
                else
                {
                    if (characterManager.IsUsingLeftHand())
                    {
                        if (lastAttack == characterManager.GetOneHandedHeavyAttack01())
                        {
                            attackAction = characterManager.GetOneHandedHeavyAttack02();
                            characterManager.PlayTargetAnimation(attackAction, true, false, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                        else
                        {
                            attackAction = characterManager.GetOneHandedHeavyAttack01();
                            characterManager.PlayTargetAnimation(attackAction, true, false, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                    }
                    else if (characterManager.IsUsingRightHand())
                    {
                        if (lastAttack == characterManager.GetOneHandedHeavyAttack01())
                        {
                            attackAction = characterManager.GetOneHandedHeavyAttack02();
                            characterManager.PlayTargetAnimation(attackAction, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                        else
                        {
                            attackAction = characterManager.GetOneHandedHeavyAttack01();
                            characterManager.PlayTargetAnimation(attackAction, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                    }
                }
            }
        }
    }
}

