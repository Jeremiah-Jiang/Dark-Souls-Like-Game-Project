using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class CombatStanceStateHumanoid : State
    {
        public AttackState attackState;
        //public EnemyAttackAction[] enemyAttacks;
        public ItemBasedAttackAction[] enemyAttacks;
        public PursueTargetState pursueTargetState;

        protected bool randomDestinationSet = false;
        protected float verticalMovementValue = 0;
        protected float horizontalMovementValue = 0;

        [Header("State Flags")]
        bool willPerformBlock = false;
        bool willPerformDodge = false;
        bool willPerformParry = false;

        public override State Tick(EnemyManager enemyManager)
        {
            switch(enemyManager.combatStyle)
            {
                case AICombatStyle.swordAndShield:
                    ProcessSwordAndShieldCombatStyle(enemyManager);
                    break;
                case AICombatStyle.archer:
                    ProcessArcherCombatStyle(enemyManager);
                    break;
            }
            return this;
            /*
            if(enemyManager.combatStyle == AICombatStyle.swordAndShield)
            {
                ProcessSwordAndShieldCombatStyle(enemyManager);
            }
            else if(enemyManager.combatStyle == AICombatStyle.archer)
            {
                ProcessArcherCombatStyle(enemyManager);
            }
            return this;
            */
        }

        private State ProcessSwordAndShieldCombatStyle(EnemyManager enemyManager)
        {
            ResetStateFlags();
            enemyManager.SetAnimatorFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemyManager.SetAnimatorFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);


            if (!enemyManager.isGrounded || enemyManager.isInteracting)
            {
                enemyManager.SetAnimatorFloat("Vertical", 0);
                enemyManager.SetAnimatorFloat("Horizontal", 0);
                return this;
            }

            //if (enemy.distanceFromTarget > enemy.maximumAggroRadius)
            //{
            //    return pursueTargetState;
            //}

            if (enemyManager.distanceFromTarget > enemyManager.cautionRadius)
            {
                return pursueTargetState;
            }


            if (!randomDestinationSet)
            {
                randomDestinationSet = true;
                DecideCirclingAction(enemyManager.enemyAnimatorManager);
            }

            if(enemyManager.canAIPerformBlock() == true)
            {
                willPerformBlock = RollForActionChance(enemyManager.blockLikelihood);
            }

            if (enemyManager.canAIPerformDodge() == true)
            {
                willPerformDodge = RollForActionChance(enemyManager.dodgeLikelihood);
            }

            if (enemyManager.canAIPerformParry() == true)
            {
                willPerformParry = RollForActionChance(enemyManager.parryLikelihood);
            }

            if (willPerformBlock)
            {
                //Block using off hand
            }

            if(willPerformDodge)
            {
                //perform dodge
            }

            if(willPerformParry)
            {
                //perform parry
            }

            //potentially circle player or walk around them
            HandleRotateTowardsTarget(enemyManager);
            /*
            if (enemy.currentRecoveryTime <= 0 && attackState.currentAttack != null)
            {
                randomDestinationSet = false;
                return attackState;
            }
            */
            if (enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack != null && enemyManager.distanceFromTarget <= enemyManager.maximumAggroRadius)
            {
                randomDestinationSet = false;
                return attackState;
            }
            else
            {
                GetNewAttack(enemyManager);
                return this;
            }

        }

        private State ProcessArcherCombatStyle(EnemyManager enemyManager)
        {
            return this;
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //If attacking enemy, rotate manually
            if (enemyManager.isPerformingAction)
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
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

                enemyManager.navmeshAgent.enabled = true;
                enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidBody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
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

            if (horizontalMovementValue <= 2 && horizontalMovementValue > 0)
            {
                Debug.Log("Circling left");
                horizontalMovementValue = 0.5f;
            }
            else if (horizontalMovementValue >= -1 && horizontalMovementValue <= 0)
            {
                Debug.Log("Circling Right");
                horizontalMovementValue = -0.5f;
            }
        }

        private  bool RollForActionChance(int actionLikelihood)
        {
            int actionChance = Random.Range(0, 100);
            if(actionChance <= actionLikelihood)
            {
                return true;
            }
            return false;
        }

        private void ResetStateFlags()
        {
            willPerformBlock = false;
            willPerformDodge = false;
            willPerformParry = false;
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

