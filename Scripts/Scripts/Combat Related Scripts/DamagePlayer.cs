using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class DamagePlayer : MonoBehaviour
    {
        public int physicalDamage = 2;
        public int magicDamage = 2;
        public int fireDamage = 2;
        public int lightningDamage = 2;
        public int umbraDamage = 2;
        public int holyDamage = 2;
        private void OnTriggerEnter(Collider other)
        {

            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                if (CheckForBlock(playerManager))
                {
                    Vector3 contactPoint = other.gameObject.GetComponentInChildren<BoxCollider>().ClosestPointOnBounds(transform.position);
                    playerManager.GetPlayerEffectsManager().PlayBlockSparksFX(contactPoint);
                    return;
                }
                else
                {
                    Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    playerManager.GetPlayerEffectsManager().PlayBloodSplatterFX(contactPoint);
                    playerManager.TakeDamage(playerManager, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage, "Damage_01");
                }
            }
        }

        protected virtual bool CheckForBlock(CharacterManager target)
        {
            Vector3 directionFromCharacterToTarget = (this.transform.position - target.transform.position);
            float dotValueFromCharacterToTarget = Vector3.Dot(directionFromCharacterToTarget, target.transform.forward);
            if (target.IsBlocking() && dotValueFromCharacterToTarget > 0.3f)
            {
                //shieldHasBeenHit = true;
                int physicalDamageAfterBlock = Mathf.RoundToInt(physicalDamage - (physicalDamage * target.GetGuardedPhysicalDamageNegation()) / 100);
                int magicDamageAfterBlock = Mathf.RoundToInt(magicDamage - (magicDamage * target.GetGuardedMagicDamageNegation()) / 100);
                int fireDamageAfterBlock = Mathf.RoundToInt(fireDamage - (fireDamage * target.GetGuardedFireDamageNegation()) / 100);
                int lightningDamageAfterBlock = Mathf.RoundToInt(lightningDamage - (lightningDamage * target.GetGuardedLightningDamageNegation()) / 100);
                int umbraDamageAfterBlock = Mathf.RoundToInt(umbraDamage - (umbraDamage * target.GetGuardedUmbraDamageNegation()) / 100);
                int holyDamageAfterBlock = Mathf.RoundToInt(holyDamage - (holyDamage * target.GetGuardedHolyDamageNegation()) / 100);
                target.AttemptBlock(physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);
                target.TakeDamageAfterBlocking(target, physicalDamageAfterBlock, magicDamageAfterBlock, fireDamageAfterBlock, lightningDamageAfterBlock, umbraDamageAfterBlock, holyDamageAfterBlock);
                return true;
            }
            return false;
        }

    }
}

