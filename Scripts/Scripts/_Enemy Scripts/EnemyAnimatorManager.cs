using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class EnemyAnimatorManager : CharacterAnimatorManager
    {
        EnemyManager enemyManager;

        protected override void Awake()
        {
            base.Awake();
            enemyManager = GetComponent<EnemyManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = enemyManager.GetAnimator().deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidBody.velocity = velocity;

            if(enemyManager.isRotatingWithRootMotion)
            {
                enemyManager.transform.rotation *= enemyManager.GetAnimator().deltaRotation;
            }
        }

        public void AwardSoulsOnDeath()
        {
            PlayerStatsManager playerStats = FindObjectOfType<PlayerStatsManager>();
            SoulCount soulCount = FindObjectOfType<SoulCount>();
            if (playerStats != null)
            {
                playerStats.AddSouls(enemyManager.GetSoulsAwardedOnDeath());
                if (soulCount != null)
                {
                    soulCount.SetSoulCountText(playerStats.currentSoulCount);
                }
            }
        }

        public void InstantiateBossParticleFX()
        {
            BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();

            GameObject phaseFX = Instantiate(enemyManager.enemyBossManager.particleFX, bossFXTransform.transform);
        }

        public void PlayWeaponTrailFX()
        {
            enemyManager.enemyEffectsManager.PlayWeaponFX(false);
        }
    }
}

