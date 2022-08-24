using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Light Attack Action")]
    public class LightAttackAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            if (characterManager.GetCurrentStamina() <= 0)
                return;
            characterManager.EraseHandIKForWeapon();
            characterManager.PlayWeaponFX(characterManager.isUsingLeftHand);

            if (characterManager.isSprinting)
            {
                HandleRunningAttack(characterManager);
                characterManager.SetCurrentAttackType(AttackType.lightAttack01);
                return;
            }
            if (characterManager.CanDoCombo())
            {
                HandleLightAttackCombo(characterManager);
                characterManager.SetCurrentAttackType(AttackType.lightAttack02);

            }
            else
            {
                if (characterManager.IsInteracting())
                    return;
                if (characterManager.CanDoCombo())
                    return;
                HandleLightAttack(characterManager);
                characterManager.SetCurrentAttackType(AttackType.lightAttack01);

            }

        }

        /// <summary>
        /// Function to handle a light attack
        /// </summary>
        /// <param name="characterManager">Instance of characterManager performing this attack</param>
        /*
         * Implementation Logic:
         * If the player is two handing the weapon, perfrom the two handed variant of the light attack
         * If the player is not two handing the weapon, check if they are using their left or right hand to perform the attack
         * The logic is the same except that the animation has to be mirrored if the player is using their left hand  
         */
        private void HandleLightAttack(CharacterManager characterManager)
        {
            string attackAction;

            if (characterManager.isTwoHanding)
            {
                attackAction = characterManager.GetTwoHandedLightAttack01();
                characterManager.PlayTargetAnimation(attackAction, true);
                characterManager.SetLastAttack(attackAction);
            }
            else
            {
                attackAction = characterManager.GetOneHandedLightAttack01();
                if (characterManager.isUsingLeftHand)
                {
                    characterManager.PlayTargetAnimation(attackAction, true, false, true);
                }
                else if (characterManager.isUsingRightHand)
                {
                    characterManager.PlayTargetAnimation(attackAction, true);
                }
                characterManager.SetLastAttack(attackAction);
            }
        }

        /// <summary>
        /// Functino to handle running attack
        /// </summary>
        /// <param name="characterManager"></param>
        /* Implementation Logic:
         * Same as HandleLightAttack(), differing only in the name of the attackActions
         */
        private void HandleRunningAttack(CharacterManager characterManager)
        {
            string attackAction;

            if(characterManager.IsTwoHanding())
            {
                attackAction = characterManager.GetTwoHandedRunningAttack01();
                characterManager.PlayTargetAnimation(attackAction, true);
                characterManager.SetLastAttack(attackAction);
            }
            else
            {
                attackAction = characterManager.GetOneHandedRunningAttack01();
                if(characterManager.IsUsingLeftHand())
                {
                    characterManager.PlayTargetAnimation(attackAction, true, false, true);
                }
                else if(characterManager.IsUsingRightHand())
                {
                    characterManager.PlayTargetAnimation(attackAction, true);
                }
                characterManager.SetLastAttack(attackAction);
            }
        }

        /// <summary>
        /// Function to handle Light Attack Combo
        /// </summary>
        /// <param name="characterManager">Instance of playerManager performing this combo</param>
        /* Implementation Logic:
         * If the player can do a combo, reset the combo flag on the animator
         * If the player is two handing a weapon
         *     If the last attack is the first light attack variant, combo with the second light attack variant
         *     otherwise, perform the first light attack variant
         * If the player is not two handing a weapon, check if they are using their left hand or right hand to perfrom the attack
         * The logic is the same, just that if the player is using the left hand, the animation must be mirrored.
         */
        private void HandleLightAttackCombo(CharacterManager characterManager)
        {
            if (characterManager.CanDoCombo())
            {
                characterManager.SetAnimatorBool("canDoCombo", false);

                string attackAction;
                string lastAttack = characterManager.GetLastAttack();

                if(characterManager.IsTwoHanding())
                {
                    if(lastAttack == characterManager.GetTwoHandedLightAttack01())
                    {
                        attackAction = characterManager.GetTwoHandedLightAttack02();
                        characterManager.PlayTargetAnimation(attackAction, true);
                        characterManager.SetLastAttack(attackAction);
                    }
                    else
                    {
                        attackAction = characterManager.GetTwoHandedLightAttack01();
                        characterManager.PlayTargetAnimation(attackAction, true);
                        characterManager.SetLastAttack(attackAction);
                    }
                }
                else
                {
                    if(characterManager.IsUsingLeftHand())
                    {
                        if(lastAttack == characterManager.GetOneHandedLightAttack01())
                        {
                            attackAction = characterManager.GetOneHandedLightAttack02();
                            characterManager.PlayTargetAnimation(attackAction, true, false, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                        else
                        {
                            attackAction = characterManager.GetOneHandedLightAttack01();
                            characterManager.PlayTargetAnimation(attackAction, true, false, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                    }
                    else if (characterManager.IsUsingRightHand())
                    {
                        if(lastAttack == characterManager.GetOneHandedLightAttack01())
                        {
                            attackAction = characterManager.GetOneHandedLightAttack02();
                            characterManager.PlayTargetAnimation(attackAction, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                        else
                        {
                            attackAction = characterManager.GetOneHandedLightAttack01();
                            characterManager.PlayTargetAnimation(attackAction, true);
                            characterManager.SetLastAttack(attackAction);
                        }
                    }
                }
            }
        }
    }
}

