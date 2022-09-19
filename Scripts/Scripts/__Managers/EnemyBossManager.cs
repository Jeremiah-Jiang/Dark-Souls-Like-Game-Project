using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class EnemyBossManager : MonoBehaviour
    {
        public string bossName;
        EnemyManager enemy;
        UIBossHealthBar bossHealthBar;
        BossCombatStanceState bossCombatStanceState;

        [Header("Second Phase FX")]
        public GameObject particleFX;
        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            enemy = GetComponent<EnemyManager>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemy.enemyStatsManager.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth, int maxHealth)
        {
            bossHealthBar.SetBossCurrentHealth(currentHealth);
            if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted)
            {
                bossCombatStanceState.hasPhaseShifted = true;
                InitiateSecondPhase();
            }
            if(currentHealth <= 0)
            {
                bossHealthBar.SetUIHealthBarToInactive();
            }
        }

        public void InitiateSecondPhase()
        {
            enemy.SetAnimatorBool("isInvulnerable", true);
            enemy.SetAnimatorBool("isPhaseShifting", true);
            enemy.enemyAnimatorManager.PlayTargetAnimation("Phase Shift", true);
            enemy.enemyWeaponSlotManager.rightHandDamageCollider.fireDamage = 10;
            bossCombatStanceState.hasPhaseShifted = true;
        }

        public void InitiateDeath()
        {
            bossHealthBar.SetUIHealthBarToInactive();
        }
    }
}

