using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Fire Arrow Action")]
    public class FireArrowAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            PlayerManager playerManager = characterManager as PlayerManager;
            CameraHandler cameraHandler = null;
            if(playerManager != null)
            {
                cameraHandler = playerManager.cameraHandler;
            }

            //Create the live arrow instantiation location
            ArrowInstantiationLocation arrowInstantationLocation;
            arrowInstantationLocation = characterManager.GetRightHandSlot().GetComponentInChildren<ArrowInstantiationLocation>();

            //Animate the bow firing the arrow
            Animator bowAnimator = characterManager.GetRightHandSlot().GetComponentInChildren<Animator>();
            bowAnimator.SetBool("isDrawn", false);
            bowAnimator.Play("Bow_ONLY_Fire_01");
            Destroy(characterManager.GetCurrentRangeFX()); //Destory the loaded arrow model in players Hand

            //Reset player's holding arrow flag
            characterManager.PlayTargetAnimation("Bow_TH_Fire_01", true);
            characterManager.SetAnimatorBool("isHoldingArrow", false);

            //Create and fire the live arrow
            GameObject liveArrow = Instantiate(characterManager.GetCurrentAmmoLiveItemModel(), arrowInstantationLocation.transform.position, cameraHandler.cameraPivotTransform.rotation);
            Rigidbody rigidbody = liveArrow.GetComponentInChildren<Rigidbody>();
            ProjectileDamageCollider arrowDamageCollider = liveArrow.GetComponentInChildren<ProjectileDamageCollider>();
            RangedAmmoItem currentAmmo = characterManager.GetCurrentAmmo();

            if (characterManager.isAiming)
            {
                Ray ray = cameraHandler.cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hitPoint;
                if (Physics.Raycast(ray, out hitPoint, 100.0f))
                {
                    liveArrow.transform.LookAt(hitPoint.point);
                }
                else
                {
                    liveArrow.transform.rotation = Quaternion.Euler(cameraHandler.cameraTransform.localEulerAngles.x, characterManager.lockOnTransform.eulerAngles.y, 0);
                }
            }
            else
            {
                if (cameraHandler.currentLockOnTarget != null)
                {
                    Quaternion arrowRotation = Quaternion.LookRotation(cameraHandler.currentLockOnTarget.lockOnTransform.position - liveArrow.transform.position);
                    liveArrow.transform.rotation = arrowRotation;
                }
                else
                {
                    liveArrow.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, characterManager.lockOnTransform.eulerAngles.y, 0);
                }
            }


            rigidbody.AddForce(liveArrow.transform.forward * currentAmmo.forwardVelocity);
            rigidbody.AddForce(liveArrow.transform.up * currentAmmo.upwardVelocity);
            rigidbody.useGravity = currentAmmo.useGravity;
            rigidbody.mass = currentAmmo.ammoMass;
            liveArrow.transform.parent = null;

            arrowDamageCollider.characterManager = characterManager;
            arrowDamageCollider.teamID = characterManager.GetTeamID();
            arrowDamageCollider.ammoItem = currentAmmo;
            arrowDamageCollider.physicalDamage = currentAmmo.physicalDamage;
            arrowDamageCollider.fireDamage = currentAmmo.fireDamage;
        }
    }
}

