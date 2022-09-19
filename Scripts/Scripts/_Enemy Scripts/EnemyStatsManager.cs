using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        public EnemyManager enemy;
        public UIEnemyHealthBar enemyHealthBar;

        protected override void Awake()
        {
            base.Awake();
            enemy = GetComponent<EnemyManager>();
            enemyHealthBar = GetComponentInChildren<UIEnemyHealthBar>();

            maxHealth = SetMaxStatFromStatLevel(maxHealth, healthLevel);
            currentHealth = maxHealth;
        }
        void Start()
        {
            if(!enemy.isBoss)
            {
                enemyHealthBar.SetMaxHealth(maxHealth);
            }
        }
        protected override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else if(poiseResetTimer <= 0 && !enemy.isInteracting)
            {
                totalPoiseDefense = armorPoiseBonus;
            }
        }

        public override void TakePoisonDamage(int damage)
        {
            if (enemy.isDead)
                return;
            base.TakePoisonDamage(damage);
            if (!enemy.isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (enemy.isBoss && enemy.enemyBossManager != null)
            {
                enemy.enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                enemy.isDead = true;
                enemy.enemyAnimatorManager.PlayTargetAnimation("Dead_01", true);
            }
        }
        public override void TakeDamageNoAnimation(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage) //There's a bug where if the boss dies from a critical attack it only plays Death_01 and not the crit death
        {
            base.TakeDamageNoAnimation(damageDealer, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);
            if (!enemy.isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (enemy.isBoss && enemy.enemyBossManager != null)
            {
                enemy.enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }
            if(enemy.isDead && enemy.isBoss)
            {
                HandleDeathAnimation();
            }
        }

        public override void TakeDamage(CharacterManager damageDealer, int physicalDamage, int magicDamage, int fireDamage, int lightningDamage, int umbraDamage, int holyDamage, string damageAnimation)
        {
            if (enemy.isDead)
                return;
            base.TakeDamage(damageDealer, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage, damageAnimation);
            if(!enemy.isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if(enemy.isBoss && enemy.enemyBossManager != null)
            {
                enemy.enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }
            enemy.enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
            enemy.enemyWeaponSlotManager.CloseDamageCollider();

            if (enemy.isDead)
            {
                HandleDeathAnimation();
            }
        }

        public void BreakGuard()
        {
            enemy.enemyAnimatorManager.PlayTargetAnimation("Break Guard", true);
        }

        private void HandleDeathAnimation()
        {
            enemy.enemyAnimatorManager.PlayTargetAnimation("Dead_01", true);
        }
    }
}

