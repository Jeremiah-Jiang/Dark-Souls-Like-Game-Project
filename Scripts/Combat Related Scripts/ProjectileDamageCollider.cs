using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ProjectileDamageCollider : DamageCollider
    {
        public RangedAmmoItem ammoItem;
        protected bool hasAlreadyPenetratedASurface;
        protected GameObject penetratedProjectile;
        protected override void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Character")
            {
                shieldHasBeenHit = false;
                hasBeenParried = false;
                CharacterManager targetCharacterManager = collision.GetComponent<CharacterManager>();
                if (targetCharacterManager != null)
                {
                    if (targetCharacterManager.GetTeamID() == teamID)
                        return;
                    CheckForParry(targetCharacterManager);
                    CheckForBlock(targetCharacterManager);
                }
                //if(targetEffectsManager.instantiatedFXModel != null)
                //{
                //    targetEffectsManager.DestroyConsumableAndReloadRightHand();
                //}
                if (targetCharacterManager != null)
                {
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position); ;
                    float damageDirection = (Vector3.SignedAngle(characterManager.transform.forward, targetCharacterManager.transform.forward, Vector3.up));
                    if (targetCharacterManager.GetTeamID() == teamID)
                        return;
                    if (hasBeenParried)
                        return;
                    if (shieldHasBeenHit)
                    {
                        targetCharacterManager.PlayBlockSparksFX(contactPoint);
                        return;
                    }
                    targetCharacterManager.ResetPoiseTimer();
                    float targetTotalPoise = targetCharacterManager.GetTotalPoise();
                    targetTotalPoise -= poiseBreak;
                    targetCharacterManager.SetTotalPoise(targetTotalPoise);
                    //Spawn blood splatter

                    ChooseDamageDirection(damageDirection);
                    targetCharacterManager.PlayBloodSplatterFX(contactPoint);
                    if (targetCharacterManager.GetTotalPoise() > poiseBreak)
                    {
                        targetCharacterManager.TakeDamageNoAnimation(characterManager, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);
                    }
                    else
                    {
                        if (targetCharacterManager is EnemyManager)
                        {
                            EnemyManager enemyManager = (EnemyManager)targetCharacterManager;
                            if (enemyManager.isBoss)
                            {
                                targetCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Parried", true);
                                targetCharacterManager.ResetTotalPoise();
                                return;
                            }
                        }
                        targetCharacterManager.TakeDamage(characterManager, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage, currentDamageAnimation);
                    }
                }
            }

            if (collision.tag == "Illusionary Wall")
            {
                IllusionaryWall illusionaryWall = collision.GetComponent<IllusionaryWall>();
                illusionaryWall.wallHasBeenHit = true;
            }

            if (!hasAlreadyPenetratedASurface && penetratedProjectile == null)
            {
                hasAlreadyPenetratedASurface = true;

                Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                GameObject penetratedArrow = Instantiate(ammoItem.contactModel, contactPoint, Quaternion.Euler(0, 0, 0));
                penetratedProjectile = penetratedArrow;
                penetratedArrow.transform.parent = collision.transform;
                penetratedArrow.transform.rotation = Quaternion.LookRotation(gameObject.transform.forward);
            }

            Destroy(transform.root.gameObject);
        }
    }
}

