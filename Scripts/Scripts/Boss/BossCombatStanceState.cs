using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class BossCombatStanceState : CombatStanceState
    {
        [Header("Second Phase Attacks")]
        public bool hasPhaseShifted;
        public ItemBasedAttackAction[] secondPhaseAttacks;
        //public PursueTargetState pursueTargetState;
        protected override void GetNewAttack(EnemyManager enemyManager)
        {
            if(hasPhaseShifted)
            {
                Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
                //float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

                int maxScore = 0;
                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    ItemBasedAttackAction enemyAttackAction = secondPhaseAttacks[i];

                    if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                        && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                        {
                            maxScore += enemyAttackAction.attackScore;
                        }
                    }
                }

                int randomValue = Random.Range(0, maxScore);
                int temporaryScore = 0;

                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    ItemBasedAttackAction enemyAttackAction = secondPhaseAttacks[i];

                    if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                        && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                        {
                            if (attackState.currentAttack != null)
                                return;
                            temporaryScore += enemyAttackAction.attackScore;

                            if (temporaryScore > randomValue)
                            {
                                attackState.currentAttack = enemyAttackAction;
                            }
                        }
                    }
                }
            }
            else
            {
                base.GetNewAttack(enemyManager);
            }
        }
    }
}

