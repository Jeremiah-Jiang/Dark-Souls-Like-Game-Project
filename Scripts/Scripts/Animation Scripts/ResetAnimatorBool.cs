using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ResetAnimatorBool : StateMachineBehaviour
    {
        public string isInvulnerableBool = "isInvulnerable";
        public bool isInvulnerableStatus = false;

        public string isInteractingBool = "isInteracting";
        public bool isInteractingStatus = false;

        public string canRotateBool = "canRotate";
        public bool canRotateStatus = true;

        public string isFiringSpellBool = "isFiringSpell";
        public bool isFiringSpellStatus = false;

        public string isRotatingWithRootMotionBool = "isRotatingWithRootMotion";
        public bool isRotatingWithRootMotionStatus = false;

        public string isMirroredBool = "isMirrored";
        public bool isMirroredStatus = false;

        public string isPerformingFullyChargedAttack = "isPerformingFullyChargedAttack";
        public bool isPerformingFullyChargedAttackStatus = false;

        public string isUsingConsumable = "isUsingConsumable";
        public bool isUsingConsumableStatus = false;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterManager characterManager = animator.GetComponent<CharacterManager>();
            if(characterManager != null)
            {
                characterManager.isUsingLeftHand = false;
                characterManager.isUsingRightHand = false;
                characterManager.ResetChargeAttackModifier();
                characterManager.CloseDamageCollider();
            }
            animator.SetBool(isInteractingBool, isInteractingStatus);
            animator.SetBool(canRotateBool, canRotateStatus);
            animator.SetBool(isRotatingWithRootMotionBool, isRotatingWithRootMotionStatus);
            animator.SetBool(isInvulnerableBool, isInvulnerableStatus);
            animator.SetBool(isFiringSpellBool, isFiringSpellStatus);
            animator.SetBool(isMirroredBool, isMirroredStatus);
            animator.SetBool(isPerformingFullyChargedAttack, isPerformingFullyChargedAttackStatus);
            animator.SetBool(isUsingConsumable, isUsingConsumableStatus);
            //this line of code is here as the enemy's animator is replaced with the sword's controller
            //There isn't a "isPerformingAction" bool in Humanoid Animator
            //and ResetAnimatorBoolAI won't ever be called since we are entering Humanoid's Empty state
            //animator.SetBool("isPerformingAction", false); 
        }
    }
}
