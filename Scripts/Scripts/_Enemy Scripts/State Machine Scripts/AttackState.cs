using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class AttackState : State
    {
        public RotateTowardsTargetState rotateTowardsTargetState;
        public CombatStanceState combatStanceState;
        public PursueTargetState pursueTargetState;
        public ItemBasedAttackAction currentAttack;

        bool willDoComboOnNextAttack = false;
        public bool hasPerformedAttack = false;
        public override State Tick(EnemyManager enemy)
        {
            //float distanceFromTarget = Vector3.Distance(enemy.currentTarget.transform.position, enemy.transform.position);
            RotateTowardsTargetsWhileAttacking(enemy);
            if(enemy.distanceFromTarget > enemy.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            if(willDoComboOnNextAttack && enemy.canDoCombo)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.navmeshAgent.transform.position.y, enemy.transform.position.z);
                AttackTargetWithCombo(enemy);
            }

            if(!hasPerformedAttack)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.navmeshAgent.transform.position.y, enemy.transform.position.z);
                AttackTarget(enemy);
                RollForComboChance(enemy);
            }

            if(willDoComboOnNextAttack && hasPerformedAttack)
            {
                return this; //Go back up and perform the combo
            }

            return rotateTowardsTargetState;
        }

        private void AttackTarget(EnemyManager enemy)
        {
            if(currentAttack != null)
            {
                enemy.isUsingRightHand = currentAttack.isRightHandedAction;
                enemy.isUsingLeftHand = !currentAttack.isRightHandedAction;
                enemy.enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                enemy.enemyAnimatorManager.PlayWeaponTrailFX();
                enemy.currentRecoveryTime = currentAttack.recoveryTime;
                hasPerformedAttack = true;
            }
        }

        private void AttackTargetWithCombo(EnemyManager enemy)
        {
            enemy.isUsingRightHand = currentAttack.isRightHandedAction;
            enemy.isUsingLeftHand = !currentAttack.isRightHandedAction;
            willDoComboOnNextAttack = false;
            enemy.enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemy.enemyAnimatorManager.PlayWeaponTrailFX();
            enemy.currentRecoveryTime = currentAttack.recoveryTime;
            currentAttack = null;
        }
        private void RotateTowardsTargetsWhileAttacking(EnemyManager enemyManager)
        {
            //If attacking enemy, rotate manually
            if (enemyManager.canRotate && enemyManager.isInteracting)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
            //rotate using navmesh agent (pathfinding)

        }

        private void RollForComboChance(EnemyManager enemyManager)
        {
            if(currentAttack != null)
            {
                float comboChance = Random.Range(0, 100);
                if (enemyManager.allowAIToPerformCombos && comboChance <= enemyManager.comboLikelihood)
                {
                    if (currentAttack.comboAction != null)
                    {
                        willDoComboOnNextAttack = true;
                        currentAttack = currentAttack.comboAction;
                    }
                    else
                    {
                        willDoComboOnNextAttack = false;
                        currentAttack = null;
                    }
                }
            }
        }
    }
}

