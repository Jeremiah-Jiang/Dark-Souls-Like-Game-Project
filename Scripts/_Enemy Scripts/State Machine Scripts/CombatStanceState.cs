using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public ItemBasedAttackAction[] enemyAttacks;
        public PursueTargetState pursueTargetState;

        protected bool randomDestinationSet = false;
        protected float verticalMovementValue = 0;
        protected float horizontalMovementValue = 0;
        public override State Tick(EnemyManager enemy)
        {
            enemy.SetAnimatorFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemy.SetAnimatorFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
            attackState.hasPerformedAttack = false;

            if (enemy.isInteracting)
            {
                enemy.SetAnimatorFloat("Vertical", 0);
                enemy.SetAnimatorFloat("Horizontal", 0);
                return this;
            }

            //if (enemy.distanceFromTarget > enemy.maximumAggroRadius)
            //{
            //    return pursueTargetState;
            //}

            if(enemy.distanceFromTarget > enemy.cautionRadius)
            {
                return pursueTargetState;
            }


            if(!randomDestinationSet)
            {
                randomDestinationSet = true;
                DecideCirclingAction(enemy.enemyAnimatorManager);
            }


            //potentially circle player or walk around them
            HandleRotateTowardsTarget(enemy);
            /*
            if (enemy.currentRecoveryTime <= 0 && attackState.currentAttack != null)
            {
                randomDestinationSet = false;
                return attackState;
            }
            */
            if (enemy.currentRecoveryTime <= 0 && attackState.currentAttack != null && enemy.distanceFromTarget <= enemy.maximumAggroRadius)
            {
                randomDestinationSet = false;
                return attackState;
            }
            else
            {
                GetNewAttack(enemy);
                return this;
            }
        }
        private void HandleRotateTowardsTarget(EnemyManager enemy)
        {
            //If attacking enemy, rotate manually
            if (enemy.isPerformingAction)
            {
                Vector3 direction = enemy.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemy.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemy.rotationSpeed / Time.deltaTime);
            }
            //rotate using navmesh agent (pathfinding)
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemy.navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemy.enemyRigidBody.velocity;

                enemy.navmeshAgent.enabled = true;
                enemy.navmeshAgent.SetDestination(enemy.currentTarget.transform.position);
                enemy.enemyRigidBody.velocity = targetVelocity;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, enemy.navmeshAgent.transform.rotation, enemy.rotationSpeed / Time.deltaTime);
            }
        }

        private void DecideCirclingAction(EnemyAnimatorManager enemyAnimatorManager)
        {
            WalkAroundTarget(enemyAnimatorManager);
        }

        private void WalkAroundTarget(EnemyAnimatorManager enemyAnimatorManager)
        {
            verticalMovementValue = 0.5f;
            /*
            verticalMovementValue = Random.Range(-1, 1);
            if (verticalMovementValue <= 1 && verticalMovementValue >= 0)
            {
                verticalMovementValue = 0.5f;
            }
            else if (verticalMovementValue >= -1 && verticalMovementValue < 0)
            {
                verticalMovementValue = -0.5f;
            }
            */
            horizontalMovementValue = Random.Range(-1, 3);

            if(horizontalMovementValue <= 2 && horizontalMovementValue > 0)
            {
                Debug.Log("Circling left");
                horizontalMovementValue = 0.5f;
            }
            else if(horizontalMovementValue >= -1 && horizontalMovementValue <= 0)
            {
                Debug.Log("Circling Right");
                horizontalMovementValue = -0.5f;
            }
        }

        protected virtual void GetNewAttack(EnemyManager enemy)
        {

            int maxScore = 0;
            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                ItemBasedAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemy.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && enemy.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (enemy.viewableAngle <= enemyAttackAction.maximumAttackAngle && enemy.viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                ItemBasedAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemy.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && enemy.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (enemy.viewableAngle <= enemyAttackAction.maximumAttackAngle && enemy.viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (attackState.currentAttack != null)
                            return;
                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            attackState.currentAttack = enemyAttackAction;
                            enemy.maximumAggroRadius = enemyAttackAction.maximumDistanceNeededToAttack;
                        }
                    }
                }
            }
        }

    }

}
