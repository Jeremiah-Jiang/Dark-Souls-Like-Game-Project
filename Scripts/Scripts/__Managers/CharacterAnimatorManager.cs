using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace JJ
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        protected CharacterManager character;

        protected RigBuilder rigBuilder;
        public TwoBoneIKConstraint leftHandConstraint;
        public TwoBoneIKConstraint rightHandConstraint;
        bool handIKWeightReset = false;

        protected virtual void Awake()
        {
            rigBuilder = GetComponent<RigBuilder>();
            character = GetComponent<CharacterManager>();

        }
        public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false, bool mirrorAnim = false)
        {
            character.GetAnimator().applyRootMotion = isInteracting;
            character.SetAnimatorBool("canRotate", canRotate);
            character.SetAnimatorBool("isInteracting", isInteracting);
            character.SetAnimatorBool("isMirrored", mirrorAnim);
            character.GetAnimator().CrossFade(targetAnim, 0.2f);
        }

        public void PlayTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
        {
            character.GetAnimator().applyRootMotion = isInteracting;
            character.SetAnimatorBool("isRotatingWithRootMotion", true);
            character.SetAnimatorBool("isInteracting", isInteracting);
            character.GetAnimator().CrossFade(targetAnim, 0.2f);
        }

        public virtual void TakeCriticalDamageAnimationEvent() //This is an animation event
        {
            character.TakeDamageNoAnimation(null, character.GetPendingPhysicalCriticalDamage(), 
                character.GetPendingMagicCriticalDamage(), 
                character.GetPendingFireCriticalDamage(), 
                character.GetPendingLightningCriticalDamage(), 
                character.GetPendingUmbraCriticalDamage(), 
                character.GetPendingHolyCriticalDamage());
            character.ResetPendingCriticalDamages();
        }

        public virtual void CanRotate()
        {
            character.SetAnimatorBool("canRotate", true);
        }

        public virtual void StopRotation()
        {
            character.SetAnimatorBool("canRotate", false);
        }

        public virtual void EnableCombo()
        {
            character.SetAnimatorBool("canDoCombo", true);
        }

        public virtual void DisableCombo()
        {
            character.SetAnimatorBool("canDoCombo", false);
        }

        public virtual void EnableIsInvulnerable()
        {
            character.SetAnimatorBool("isInvulnerable", true);
        }

        public virtual void DisableIsInvulnerable()
        {
            character.SetAnimatorBool("isInvulnerable", false);
        }

        public virtual void EnableIsParrying()
        {
            character.isParrying = true;
        }

        public virtual void DisableIsParrying()
        {
            character.isParrying = false;
        }

        public virtual void EnableCanBeRiposted()
        {
            character.canBeRiposted = true;
        }

        public virtual void DisableCanBeRiposted()
        {
            character.canBeRiposted = false;
        }

        public virtual void SetHandIKForWeapon(RightHandIKTarget rightHandIKTarget, LeftHandIKTarget leftHandIKTarget, bool isTwoHanding)
        {
            if(isTwoHanding)
            {
                if (rightHandIKTarget != null)
                {
                    rightHandConstraint.data.target = rightHandIKTarget.transform;
                    rightHandConstraint.data.targetPositionWeight = 1;
                    rightHandConstraint.data.targetRotationWeight = 1;
                }
                if(leftHandIKTarget != null)
                {
                    leftHandConstraint.data.target = leftHandIKTarget.transform;
                    leftHandConstraint.data.targetPositionWeight = 1;
                    leftHandConstraint.data.targetRotationWeight = 1;
                }
            }
            else
            {
                rightHandConstraint.data.target = null;
                leftHandConstraint.data.target = null;
            }
            rigBuilder.Build();
            //Check if two handing weapon
            //if we are, apply hand IK 
            //Assign hand IK to a targets
            //if not, disable hand IK
        }

        public virtual void CheckHandIKWeight(RightHandIKTarget rightHandIKTarget, LeftHandIKTarget leftHandIKTarget, bool isTwoHanding)
        {
            if (character.isInteracting)
                return;
            if(handIKWeightReset)
            {
                handIKWeightReset = false;
                if (rightHandConstraint.data.target != null)
                {
                    rightHandConstraint.data.target = rightHandIKTarget.transform;
                    rightHandConstraint.data.targetPositionWeight = 1;
                    rightHandConstraint.data.targetRotationWeight = 1;
                }

                if (leftHandConstraint.data.target != null)
                {
                    leftHandConstraint.data.target = leftHandIKTarget.transform;
                    leftHandConstraint.data.targetPositionWeight = 1;
                    leftHandConstraint.data.targetRotationWeight = 1;
                }
            }
        }

        public virtual void EraseHandIKForWeapon()
        {
            handIKWeightReset = true;
            if(rightHandConstraint.data.target != null)
            {
                rightHandConstraint.data.targetPositionWeight = 0;
                rightHandConstraint.data.targetRotationWeight = 0;
            }

            if(leftHandConstraint.data.target != null)
            {
                leftHandConstraint.data.targetPositionWeight = 0;
                leftHandConstraint.data.targetRotationWeight = 0;
            }
        }
    }
}


